$(function () {
    appFsMvc.GraphView = Backbone.View.extend({
        // has el: #content from constructor
        // has model: all sensors from constructor

        initialize: function (options) {
            this.data = options.data;
        },

        render: function () {
            var chart = new Highcharts.Chart({
                chart: {
                    renderTo: 'content',
                    defaultSeriesType: 'spline',
                    events: {
                        load: function () {

                            var firstTime = true;
                            var date = new Date();
                            var currentTime = date.getTime();

                            setInterval(function () {
                                $.getJSON(appFsMvc.sensorReadings.url, function (data) {
                                    // get the data
                                    appFsMvc.sensorReadings.reset(data);

                                    for (var i = 0; i < appFsMvc.sensorReadings.length; ++i) {

                                        // get the data point
                                        var sensorReading = appFsMvc.sensorReadings.models[i].attributes;

                                        // add new series if it doesn't exist
                                        if (chart.get(sensorReading.sensorId) === null) {
                                            chart.addSeries({
                                                name: "Sensor " + sensorReading.sensorId,
                                                id: sensorReading.sensorId
                                            });
                                        }

                                        // Convert the time to unix time, for highcharts
                                        // source = "2/12/2015 21:15:24"
                                        var timestamp = new Date(sensorReading.time).getTime();

                                        // add the data point if it's our first time through
                                        // or if the data point is a newer than what we've drawn
                                        if (firstTime || timestamp >= currentTime)
                                            chart.get(sensorReading.sensorId).addPoint(
                                                [timestamp, parseFloat(sensorReading.value)]);
                                    }

                                    chart.redraw();
                                    firstTime = false;
                                    currentTime = date.getTime();
                                });
                            }, 1000); // loop every second
                        }
                    }
                },
                title: {
                    text: 'Usage',
                    x: -20 //center
                },
              /*subtitle: {
                    text: 'Source: WorldClimate.com',
                    x: -20
                },*/
                xAxis: {
                    type: 'datetime',
                    tickPixelInterval: 150,
                    maxZoom: 20 * 1000
                },
                yAxis: {
                    minPadding: 0.2,
                    maxPadding: 0.2,
                    title: {
                        text: 'Value',
                        margin: 80
                    }
                },
              /*tooltip: {
                    valueSuffix: 'C'
                },*/
                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle',
                    borderWidth: 0
                }
            });

        },

    });
});