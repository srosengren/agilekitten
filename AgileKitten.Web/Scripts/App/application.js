var application = (function (app) {

    app.repositoriesVM = (function (reposVM) {
        reposVM.repositories = ko.observableArray();
        reposVM.init = function (repos) {
            reposVM.repositories(repos);
        }

        return reposVM;
    })({});

    app.init = function (serverVM) {
        app.repositoriesVM.init(serverVM.repositories);

        ko.applyBindings(app);
    }

    return app;
})({});