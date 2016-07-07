/// <reference path="../jquery-2.2.3.intellisense.js" />
/// <reference path="../jquery-2.2.3.js" />
/// <reference path="../jquery.signalR-2.2.0.js" />
/// <reference path="../knockout-3.4.0.js" />

$(function () {
    app.initialize();
    
    // Comment added on GitHub

    // Activate Knockout
    ko.validation.init({ grouping: { observable: false } });
    ko.applyBindings(app);

    // Esta la tenía en una función document ready específica para lo de SingalR pero mejor la traje a esta función ya existente para mejor readability
    // var connection = $.hubConnection('http://localhost:8080');

    // http://www.asp.net/signalr/overview/guide-to-the-api/hubs-api-guide-javascript-client
    // $.connection.hub is the same object that $.hubConnection() creates

    var connection_local = $.hubConnection();
    var hub_local = connection_local.createHubProxy('hitCounter');

    var connection_remote = $.hubConnection("https://localhost:8082");
    var hub_remote = connection_remote.createHubProxy('hitCounter');

    self.RegisterCallbacks_Local(hub_local, connection_local);
    self.RegisterCallbacks_Remote(hub_remote, connection_remote);

    

});

function RegisterCallbacks_Local(hub_local, connection_local) {
    try {

        hub_local.on("onRecordHit", function (hitCount) {
            $("#progress_local").text(hitCount);
        });

        // Nos está llegando una lista
        hub_local.on("onTimer", function (SerializedBag) {
            var Bag = JSON.parse(SerializedBag);
            var Time = Bag[0].texto;
            $("#timer_local").html(Time);
        });

        connection_local.start().done(function () {
            hub_local.invoke("recordHit");
        });

        // Monitoreo y reconexión
        connection_local.logging = true;

        // Reconexión!!! (hay duplicidad, el stateChanged puede quitarse y meterse acá
        connection_local.stateChanged(function (change) {
            if (change.newState === $.signalR.connectionState.connected) {
                $('#state_local').html("Connected");
                $('#state_local').css('background-color', 'green');
            } else if (change.newState === $.signalR.connectionState.reconnecting) {
                $('#state_local').html("Reconnecting");
                $('#state_local').css('background-color', 'yellow');
            } else if (change.newState === $.signalR.connectionState.disconnected) {
                $('#state_local').html("Disconnected");
                $('#state_local').css('background-color', 'red');
            }
        });

        // Parecería que esta es perpetua, pero solo cae una vez
        connection_local.disconnected(function () {
            setTimeout(function () {
                connection_local.start().done(function () {
                    self.RegisterCallbacks_Local(hub_local, connection_local);
                });
            }, 1000);
        });

    } catch (e) {
        $('#state_local').html(e.message);
    }

}

function RegisterCallbacks_Remote(hub_remote, connection_remote) {

    try {

        hub_remote.on("onRecordHit", function (hitCount) {
            $("#progress_remote").text(hitCount);
        });

        // Nos está llegando una lista
        hub_remote.on("onTimer", function (SerializedBag) {
            // Estoy mandando una lista, entiendo que se serializa en Json
            var Bag = JSON.parse(SerializedBag);
            var Time = Bag[0].texto;
            $("#timer_remote").html(Time);
        });

        connection_remote.start().done(function () {
            hub_remote.invoke("recordHit");
        });

        // Monitoreo y reconexión
        connection_remote.logging = true;

        // Reconexión!!! (hay duplicidad, el stateChanged puede quitarse y meterse acá
        connection_remote.stateChanged(function (change) {
            if (change.newState === $.signalR.connectionState.connected) {
                $('#state_remote').html("Connected");
                $('#state_remote').css('background-color', 'green');
            } else if (change.newState === $.signalR.connectionState.reconnecting) {
                $('#state_remote').html("Reconnecting");
                $('#state_remote').css('background-color', 'yellow');
            } else if (change.newState === $.signalR.connectionState.disconnected) {
                $('#state_remote').html("Disconnected");
                $('#state_remote').css('background-color', 'red');
            }

        });

        // Parecería que esta es perpetua, pero solo cae una vez
        connection_remote.disconnected(function () {
            setTimeout(function () {
                connection_remote.start().done(function () {
                    self.RegisterCallbacks_Remote(hub_remote, connection_remote);
                });
            }, 1000);
        });

    } catch (e) {
        $('#state_remote').html(e.message);
    }


}

$(window).unload(function () {
    return "Yikes!";
});


var Developer = function (first, last) {
    var self = this;

    self.firstName = ko.observable(first);
    self.lastName = ko.observable(last);

    self.fullName = ko.pureComputed(function () {
        return self.firstName() + " " + first.lastName();
    });
};

var Bug = function (description) {
    var self = this;

    self.description

}
