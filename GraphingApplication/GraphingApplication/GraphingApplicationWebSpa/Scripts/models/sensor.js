$(function () {
    appFsMvc.Sensor = Backbone.Model.extend();

    appFsMvc.SensorCollection = Backbone.Collection.extend({
        model: window.appFsMvc.Sensor,
        url: "/api/sensors/"
    });
});