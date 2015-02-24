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
            this.urlGetAll = function (isLambda) {
                return "/api/sensorReadings/?action=GetOne&timestamp=0&id=" + options.id + "&lambda=" + isLambda
            }
            this.urlGetAfter = function (timestamp, isLambda) {
                return "/api/sensorReadings/?action=GetAfter&id=" + this.id + "&timestamp=" + timestamp +
                    "&lambda=" + isLambda;
            }
        },

        model: window.appFsMvc.SensorReadingMin
    });
});