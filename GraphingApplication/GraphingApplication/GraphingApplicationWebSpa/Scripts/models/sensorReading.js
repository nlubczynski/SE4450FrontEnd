$(function () {
    appFsMvc.SensorReading = Backbone.Model.extend();

    appFsMvc.SensorReadingsCollection = Backbone.Collection.extend({
        model: window.appFsMvc.SensorReading,
        url: "/api/sensorReadings/"
    });
});