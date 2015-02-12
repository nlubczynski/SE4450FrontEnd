$(function () {
    appFsMvc.SensorsDetailsView = Backbone.View.extend({

        render: function () {
            appFsMvc.utility.renderTemplate("sensorDetail.htm",
                $(this.el),
                { data: this.model.toJSON() }
            );
        },

    });
});