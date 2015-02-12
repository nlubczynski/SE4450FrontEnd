$(function () {
    appFsMvc.SensorsDetailsView = Backbone.View.extend({

        events: {
            "click #createBuilding": "gotoCreateView"
        },

        render: function () {
            appFsMvc.utility.renderTemplate("sensorDetail.htm", $(this.el), { data: this.model.toJSON() });
        },

        gotoCreateView: function () {
            appFsMvc.App.navigate("create", { trigger: true });
        }

    });
});