var application = (function (app) {
    app.board = {
        drag: {
            over: function (repo,event) {
                return 0 > event.dataTransfer.types.indexOf('agile-kitten-label'); //Stop from propagating if it's a type that we can handle
            },
            drop: function (repo, event) {
                //TODO: pretty up this shit
                ko.utils.arrayFirst(repo.labels(), function (l) {
                    return l.name() === event.dataTransfer.getData('agile-kitten-label');
                }).isIssueList(true);
            }
        }
    };
    return app;
})(application || {});