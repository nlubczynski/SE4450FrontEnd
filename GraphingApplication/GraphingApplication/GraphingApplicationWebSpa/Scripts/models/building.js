$(function () {
    appFsMvc.Building = Backbone.Model.extend();

    appFsMvc.BuildingCollection = Backbone.Collection.extend({
        model: window.appFsMvc.Building,
        url: "/api/buildings/"
    });
});