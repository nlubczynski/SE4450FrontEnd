$(function () {
    var AppRouter = Backbone.Router.extend({

        routes: {
            "": "listBuildings",
            "buildings": "listBuildings",
            "sensors": "listSensors",
            "graph": "graph"
        },

        initialize: function () {
            this.buildingDetailsView = new appFsMvc.BuildingDetailsView({ el: $("#content"), model: window.appFsMvc.buildings });
            this.sensorsDetailsView = new appFsMvc.SensorsDetailsView({ el: $("#content"), model: window.appFsMvc.sensors });
            this.graphView = new appFsMvc.GraphView({ el: $("#content"), data: window.appFsMvc.sensorReadings });
        },

        listBuildings: function () {
            this.buildingDetailsView.render();
        },

        listSensors: function () {
            this.sensorsDetailsView.render();
        },

        graph: function() {
            this.graphView.render();
        }
    });

    appFsMvc.buildings = new appFsMvc.BuildingCollection();
    appFsMvc.sensors = new appFsMvc.SensorCollection();
    appFsMvc.sensorReadings = new appFsMvc.SensorReadingsCollection();

    $.getJSON(appFsMvc.buildings.url, function (data) {
        appFsMvc.buildings.reset(data);
        appFsMvc.App = new AppRouter();
        Backbone.history.start();
    });

    $.getJSON(appFsMvc.sensors.url, function (data) {
        appFsMvc.sensors.reset(data);
    });

    $.getJSON(appFsMvc.sensorReadings.url, function (data) {
        appFsMvc.sensorReadings.reset(data);
    });

    
    
});
