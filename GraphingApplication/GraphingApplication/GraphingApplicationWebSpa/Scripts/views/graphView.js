$(function () {
    appFsMvc.GraphView = Backbone.View.extend({
        // has el: #content from constructor
        // has model: all sensors from constructor

        initialize: function (options) {
            this.data = options.data;
        },

        render: function () {
            this.$el.highcharts('StockChart', {
                chart: {
                    events: {
                        load: function () {

                            // helper vars
                            var currentTime;
                            var chart = this;
                            var seriesSemaphore = [];

                            // update function
                            (function () {

                                // check if locked
                                var locked = false;
                                for (var i = 0; i < seriesSemaphore.length; ++i)
                                    if (seriesSemaphore[i])
                                        locked = true;

                                if (!locked) {
                                    // make sure we have the right sensors
                                    $.getJSON(appFsMvc.sensors.url, function (data) {
                                        appFsMvc.sensors.reset(data);
                                        for (var i = 0; i < appFsMvc.sensors.length; ++i) {
                                            // Lock
                                            seriesSemaphore[i] = true;

                                            // get the sensor var
                                            var sensor = appFsMvc.sensors.models[i];

                                            // get a collection
                                            var sensorReadingCollection = new appFsMvc.SensorReadingsMinCollection([], { id: sensor.id });

                                            // see if this series already exists
                                            if (chart.get(sensor.id) != null) {
                                                var series = chart.get(sensor.id);
                                                $.getJSON(sensorReadingCollection.urlGetAfter(currentTime),
                                                    (function (series, id) {
                                                        return function (data) {
                                                            // add the point(s), and update time
                                                            if (data.length > 0) {
                                                                for (var i = 0; i < data.length; ++i) {
                                                                    series.addPoint(data[i], true, true);
                                                                }

                                                                currentTime = data[data.length - 1][0];
                                                            }

                                                            //unlock
                                                            seriesSemaphore[id - 1] = false;
                                                        };
                                                    }(series, sensor.id))
                                               )
                                            }
                                            else // it doesn't exist, get all the data and add it
                                            {
                                                $.getJSON(sensorReadingCollection.urlGetAll,
                                                    (function (id) {
                                                        return function (data) {
                                                            // update time
                                                            if (data.length > 0) {
                                                                currentTime = data[data.length - 1][0];
                                                            }

                                                            // add the point
                                                            chart.addSeries({
                                                                name: "Sensor " + id,
                                                                id: id,
                                                                data: data
                                                            });

                                                            //unlock
                                                            seriesSemaphore[id - 1] = false;
                                                        };
                                                    }(sensorReadingCollection.id))
                                                );
                                            }
                                        }
                                    });
                                }

                                setTimeout(arguments.callee, 1000); // loop every second
                            })();
                        }
                    }
                },
                title: {
                    text: 'Usage',
                    x: -20 //center
                },
                rangeSelector: {

                    buttons: [{
                        type: 'minute',
                        count: 1,
                        text: '1m'
                    },
                        {
                            type: 'minute',
                            count: 5,
                            text: '5m'
                        },
                        {
                            type: 'day',
                            count: 3,
                            text: '3d'
                        }, {
                            type: 'week',
                            count: 1,
                            text: '1w'
                        }, {
                            type: 'month',
                            count: 1,
                            text: '1m'
                        }, {
                            type: 'month',
                            count: 6,
                            text: '6m'
                        }, {
                            type: 'year',
                            count: 1,
                            text: '1y'
                        }, {
                            type: 'all',
                            text: 'All'
                        }],
                    selected: 0,
                    allButtonsEnabled: true
                }
            });

        },

    });
});