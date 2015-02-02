var application = (function (app) {
    app.repository = {
        make: function (repo) {
            repo.issues = ko.observableArray(repo.issues);
            repo.labels = ko.observableArray(repo.labels);

            for (var i = 0; i < repo.issues().length; i++) {
                repo.issues()[i].labels = ko.observableArray(repo.issues()[i].labels); //TODO: move to own factory
            }
            for (var i = 0; i < repo.labels().length; i++) {
                app.list.make(repo, repo.labels()[i]);
            }

            repo.labels.labels = ko.computed(function () {
                return ko.utils.arrayFilter(repo.labels(), function (l) {
                    return !l.isIssueList();
                });
            });
            repo.labels.lists = ko.computed(function () {
                return ko.utils.arrayFilter(repo.labels(), function (l) {
                    return l.isIssueList();
                });
            });

            repo.milestones = ko.observableArray(repo.milestones);

            return repo;
        },
        addLabel: function (repo, addAsList, name) {
            if (!name)
                return;
            if (ko.utils.arrayFirst(repo.labels(), function (l) { return l.name().toLowerCase() === name.toLowerCase() }))
                return;

            repo.labels.push(app.list.make(repo, {
                isIssueList: addAsList,
                name: name
            }));
        }
    };
    return app;
})(application || {});