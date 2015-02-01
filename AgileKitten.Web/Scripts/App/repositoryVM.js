(function (app) {
    app.repository = {
        make: function (repo) {
            repo.issues = ko.observableArray(repo.issues);

            repo.labels = ko.observableArray(repo.labels);
            repo.labels.labels = ko.computed(function () {
                return ko.utils.arrayFilter(repo.labels(), function (l) {
                    return !l.isIssueList;
                });
            });
            repo.labels.lists = ko.computed(function () {
                return ko.utils.arrayFilter(repo.labels(), function (l) {
                    return l.isIssueList;
                });
            });

            repo.milestones = ko.observableArray(repo.milestones);

            return repo;
        },
        addLabel: function (repo, addAsList, name) {
            if (!name)
                return;
            repo.labels.push({
                githubRepositoryId: repo.githubId,
                color: Math.floor(Math.random() * 16777215).toString(16),
                isIssueList: addAsList,
                name: name
            })
        }
    };
})(application || {});