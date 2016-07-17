/// <reference path="../jquery-2.2.3.intellisense.js" />
/// <reference path="../jquery-2.2.3.js" />
/// <reference path="../jquery.signalR-2.2.0.js" />
/// <reference path="../knockout-3.4.0.js" />

$(function () {
    app.initialize();
        
    // Activate Knockout
    ko.validation.init({ grouping: { observable: false } });
    ko.applyBindings(app);

    // Esta la tenía en una función document ready específica para lo de SingalR pero mejor la traje a esta función ya existente para mejor readability
    var connection_local = $.hubConnection();
    var hub_local = connection_local.createHubProxy('hitCounter');

    var connection_remote = $.hubConnection("https://localhost:8082");
    var hub_remote = connection_remote.createHubProxy('hitCounter');

    self.RegisterCallbacks(hub_local, connection_local, '#progress_local', '#state_local', '#timer_local');
    self.RegisterCallbacks(hub_remote, connection_remote, '#progress_remote', '#state_remote', '#timer_remote');
    // PATO
});

function RegisterCallbacks(hub, connection, progress_label, state_label, timer_label) {
    try {

        hub.on("onRecordHit", function (hitCount) {
            $(progress_label).text(hitCount);
        });

        // Nos está llegando una lista
        hub.on("onTimer", function (SerializedBag) {
            var Bag = JSON.parse(SerializedBag);
            var Time = Bag[0].texto;
            $(timer_label).html(Time);
        });
        
        connection.start().done(function () {
            hub.invoke("recordHit");
        });

        // Monitoreo y reconexión
        connection.logging = true;

        // Reconexión!!! (hay duplicidad, el stateChanged puede quitarse y meterse acá
        connection.stateChanged(function (change) {
            if (change.newState === $.signalR.connectionState.connected) {
                $(state_label).html("Connected");
                $(state_label).css('background-color', 'green');
            } else if (change.newState === $.signalR.connectionState.reconnecting) {
                $(state_label).html("Reconnecting");
                $(state_label).css('background-color', 'yellow');
            } else if (change.newState === $.signalR.connectionState.disconnected) {
                $(state_label).html("Disconnected");
                $(state_label).css('background-color', 'red');
            }
        });

        // Parecería que esta es perpetua, pero solo cae una vez
        connection.disconnected(function () {
            setTimeout(function () {
                connection.start().done(function () {
                    // self.RegisterCallbacks_Local(hub_local, connection_local);
                    connection.start().done(function () {
                        hub.invoke("recordHit");
                    });
                });
            }, 1618);
        });

    } catch (e) {
        $(state_label).html(e.message);
    }

}

$(window).unload(function () {
    return "Yikes!";
});


