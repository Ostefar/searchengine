// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

import ko from 'https://ajax.aspnetcdn.com/ajax/knockout/knockout-3.1.0.js';

let ViewModel = function () {
    let me = this;

    me.searchTerms = ko.observable();
    me.hits = ko.observable();
    me.results = ko.observableArray();
    me.timeUsed = ko.observable();

    me.search = function () {
        $ajax({
            url: "http://localhost:9000/Search?terms=" + me.searchTerms() + "&numberOfResults=10",
            success: function (data) {
                me.hits(data.documents.length);
                me.timeUsed(data.elapsedMilliseconds);
                me.results.removeAll();
                data.documents.forEach(function (hit) {
                    me.results.push(hit)
                });
            }

        });
    }
};
ko.applyBindings(new ViewModel());