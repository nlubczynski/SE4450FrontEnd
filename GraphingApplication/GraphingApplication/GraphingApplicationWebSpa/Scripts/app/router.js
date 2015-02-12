$(function () {
    var AppRouter = Backbone.Router.extend({

        routes: {
            "": "listBuildings",
            "buildings": "listBuildings",
            "sensors": "listSensors"
        },

        initialize: function () {
            this.buildingDetailsView = new appFsMvc.BuildingDetailsView({ el: $("#content"), model: window.appFsMvc.buildings });
            this.sensorsDetailsView = new appFsMvc.SensorsDetailsView({ el: $("#content"), model: window.appFsMvc.sensors });
        },

        listBuildings: function () {
            this.buildingDetailsView.render();
        },

        listSensors: function () {
            this.sensorsDetailsView.render();
        }
    });

    appFsMvc.buildings = new appFsMvc.BuildingCollection();
    appFsMvc.sensors = new appFsMvc.SensorCollection();

    $.getJSON(appFsMvc.buildings.url, function (data) {
        appFsMvc.buildings.reset(data);
        appFsMvc.App = new AppRouter();
        Backbone.history.start();
    });

    $.getJSON(appFsMvc.sensors.url, function (data) {
        appFsMvc.sensors.reset(data);
    });
});
