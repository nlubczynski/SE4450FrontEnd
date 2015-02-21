$(function () {
    appFsMvc.SensorReading = Backbone.Model.extend();

    appFsMvc.SensorReadingsCollection = Backbone.Collection.extend({
        model: window.appFsMvc.SensorReading,
        url: "/api/sensorReadings/"
    });
});

$(function () {
    appFsMvc.SensorReadingMin = Backbone.Model.extend();

    appFsMvc.SensorReadingsMinCollection = Backbone.Collection.extend({

        initialize: function (models, options) {
            this.url = "/api/sensorReadings/" + options.id
        },

        model: window.appFsMvc.SensorReadingMin
    });
});