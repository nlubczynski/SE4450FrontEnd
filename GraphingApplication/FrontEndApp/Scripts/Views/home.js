$(function () {
    /**
     * Load new data depending on the selected min and max
     */
    var mySqlLoading = false;
    var lambdaLoading = false;
    function afterSetExtremes(e) {

        var MySQL = $('#MySQL').highcharts();
        var Lambda = $('#Lambda').highcharts();

        MySQL.showLoading('Loading data from server...');
        Lambda.showLoading('Loading data from server...');

        if (mySqlLoading != true) {
            mySqlLoading = true;
            $.getJSON('http://www.highcharts.com/samples/data/from-sql.php?start=' + Math.round(e.min) +
                    '&end=' + Math.round(e.max) + '&callback=?', function (data) {

                        MySQL.series[0].setData(data);
                        MySQL.hideLoading();
                        mySqlLoading = false;
                    });
        };

        if (lambdaLoading != true) {
            lambdaLoading = true;
            $.getJSON('http://www.highcharts.com/samples/data/from-sql.php?start=' + Math.round(e.min) +
                    '&end=' + Math.round(e.max) + '&callback=?', function (data) {

                        Lambda.series[0].setData(data);
                        Lambda.hideLoading();
                        lambdaLoading = false;
                    });
        }
    }

    // MySql
    $.getJSON('api/sensor/', function (sensorData) {

        // Add a null value for the end date
        //data = [].concat(data, [[Date.UTC(2011, 9, 14, 19, 59), null, null, null, null]]);

        sensorData = $.parseJSON(sensorData);
        var dataSeries = [];

        $.ajaxSetup({
            async: false
        });

        // load the data
        for (var i = 0; i < sensorData.length; ++i) {
            $.getJSON("api/SensorData/get/" + sensorData[i].ID,
                (function (index) {
                    return function (data) {
                        dataSeries[index] = $.parseJSON(data);
                    };
                }(i))
            );
        }

        $.ajaxSetup({
            async: true
        });

        // create the chart
        $('#MySQL').highcharts('StockChart', {
            chart: {
                type: 'candlestick',
                zoomType: 'x'
            },

            navigator: {
                adaptToUpdatedData: false,
                series: {
                    data: dataSeries
                }
            },

            scrollbar: {
                liveRedraw: false
            },

            title: {
                text: 'MySQL Database'
            },

            rangeSelector: {
                buttons: [{
                    type: 'hour',
                    count: 1,
                    text: '1h'
                }, {
                    type: 'day',
                    count: 1,
                    text: '1d'
                }, {
                    type: 'month',
                    count: 1,
                    text: '1m'
                }, {
                    type: 'year',
                    count: 1,
                    text: '1y'
                }, {
                    type: 'all',
                    text: 'All'
                }],
                inputEnabled: false, // it supports only days
                selected: 4 // all
            },

            xAxis: {
                /*events: {
                    afterSetExtremes: afterSetExtremes
                },*/
                minRange: 3600 * 1000 // one hour
            },

            series: [{
                data: dataSeries,
                dataGrouping: {
                    enabled: false
                }
            }]
        });
    });

    // Lambda
    $.getJSON('http://www.highcharts.com/samples/data/from-sql.php?callback=?', function (data) {

        // Add a null value for the end date
        data = [].concat(data, [[new Date(), null, null, null, null]]);

        // create the chart
        $('#Lambda').highcharts('StockChart', {
            chart: {
                type: 'candlestick',
                zoomType: 'x'
            },

            navigator: {
                adaptToUpdatedData: false,
                series: {
                    data: data
                }
            },

            scrollbar: {
                liveRedraw: false
            },

            title: {
                text: 'Lambda Architecture'
            },

            rangeSelector: {
                enabled: false
            },

            xAxis: {
                events: {
                    afterSetExtremes: afterSetExtremes
                },
                minRange: 3600 * 1000 // one hour
            },

            yAxis: {
                floor: 0
            },

            series: [{
                data: data,
                dataGrouping: {
                    enabled: false
                }
            }]
        });
    });
});