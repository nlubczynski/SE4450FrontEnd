$(function () {
    var AppRouter = Backbone.Router.extend({

        routes: {
            "": "list",
            "create": "create"
        },

        initialize: function () {
            this.contactDetailsView = new appFsMvc.ContactDetailsView( { el: $("#content"), model: window.appFsMvc.contacts } );
            this.createContactView = new appFsMvc.ContactCreateView({ el: $("#content"), model: window.appFsMvc.contacts });
            this.buildingDetailsView = new appFsMvc.BuildingDetailsView({ el: $("#content"), model: window.appFsMvc.buildings });
        },

        list: function () {
            //this.contactDetailsView.render();
            this.buildingDetailsView.render();
        },

        create: function () {
            this.createContactView.render();
        }
    });

    appFsMvc.contacts = new appFsMvc.ContactCollection();
    $.getJSON( appFsMvc.contacts.url, function ( data ) {
        appFsMvc.contacts.reset( data );
        appFsMvc.App = new AppRouter();
        Backbone.history.start();
    });

    appFsMvc.buildings = new appFsMvc.BuildingCollection();
    $.getJSON(appFsMvc.buildings.url, function (data) {
        appFsMvc.buildings.reset(data);
    });
});
