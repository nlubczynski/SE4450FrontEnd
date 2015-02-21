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
            this.id          = options.id
            this.urlGetAll   = "/api/sensorReadings/?action=GetOne&timestamp=0&id=" + options.id
            this.urlGetAfter = function (timestamp) {
                return "/api/sensorReadings/?action=GetAfter&id=" + this.id + "&timestamp=" + timestamp;
            }
        },

        model: window.appFsMvc.SensorReadingMin
    });
});