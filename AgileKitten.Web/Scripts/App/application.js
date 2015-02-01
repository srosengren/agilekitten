var application = (function (app) {

    app.openRepository = ko.observable();

    app.repositoriesVM = (function (reposVM) {
        reposVM.repositories = ko.observableArray();
        reposVM.init = function (repos) {
            reposVM.repositories(repos);
        }
        reposVM.loadRepo = function (repo) {
            sr.ajax({
                url: app.settings.rootUrl + 'api/getrepository?repoid=' + repo.githubId,
                success: function(data)
                {
                    data = JSON.parse(data);
                    app.openRepository(app.repository.make(data));
                }
            });
        };

        return reposVM;
    })({});

    app.init = function (serverVM) {
        app.settings = app.settings || {};
        app.settings.rootUrl = serverVM.rootUrl

        app.repositoriesVM.init(serverVM.repositories);

        ko.applyBindings(app);
    }

    return app;
})(application || {});