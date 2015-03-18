var application = (function (app) {
    app.list = {
        make: function (repo, label) {

            label.githubRepositoryId = label.githubRepositoryId || repo.githubId;
            label.color = ko.observable(label.color || Math.floor(Math.random() * 16777215).toString(16));
            label.name = ko.observable(label.name);
            label.isIssueList = ko.observable(label.isIssueList || false);

            label.color.isDark = ko.computed(function () {
                var c = label.color(),
                    r = parseInt(c.substr(0, 2), 16),
                    g = parseInt(c.substr(2, 2), 16),
                    b = parseInt(c.substr(4, 2), 16);
                var yiq = ((r * 299) + (g * 587) + (b * 114)) / 1000;
                return !(yiq >= 128);
            });

            label.issues = ko.computed(function () {
                return ko.utils.arrayFilter(repo.issues(), function (i) {
                    return ko.utils.arrayFirst(i.labels(), function (l) {
                        return l.toLowerCase() === label.name().toLowerCase();
                    })
                });
            });

            //Settings stuff
            label.isBacklog = ko.observable(false);

            return label;
        }
    }
    return app;
})(application || {});