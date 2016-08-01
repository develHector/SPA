function AppDataModel() {

    // El uso de esto del "self" es para que cuando defines una función de regreso, dentro de ella se sepa quien era el llamador
    var self = this;

    // Routes
    self.userInfoUrl = "/api/Me";
    self.siteUrl = "/";

    // Route operations

    // Other private operations

    // Operations

    // Data
    self.returnUrl = self.siteUrl;

    // Data access operations
    self.setAccessToken = function (accessToken) {
        sessionStorage.setItem("accessToken", accessToken);
    };

    self.getAccessToken = function () {
        return sessionStorage.getItem("accessToken");
    };

    // Del MVA de knockout
    self.firstName = ko.observable("Hector");
    self.lastName = ko.observable("Casavantes");
    self.lastUpdate = ko.observable( Date.now );

    // En JS ojo con no reinicializar las variables, es cláramente distinto uno del otro:
    // self.lastName = "Casavantes" ;
    // self.lastName("Casavantes");
    self.fullName = ko.pureComputed(function () {
        // Ojo pues self.firstName + ' ' + self.lastName sin los parent-parent regresa los objetos
        return self.firstName() + ' ' + self.lastName();
    });

}
