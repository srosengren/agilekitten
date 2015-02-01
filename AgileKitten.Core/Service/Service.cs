using AgileKitten.Core.Model;
using gh = Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgileKitten.Core.Repositories;

namespace AgileKitten.Core.Service
{
    public class Service
    {
        private GHRepository repository;

        public GHRepository Repository
        {
            get { return repository ?? (repository = new GHRepository()); }
        }

        public gh.GitHubClient Client { get; set; }

        public async Task<IEnumerable<RepositoryMeta>> GetRepositories()
        {
            var ghRepos = await Client.Repository.GetAllForCurrent();

            return ghRepos.Select(r => AgileKitten.Core.Model.RepositoryMeta.Make(r));
        }

        public async Task<IEnumerable<Issue>> GetIssuesForRepository(string owner, string name)
        {
            var ghIssues = await Client.Issue.GetForRepository(owner,name,new gh.RepositoryIssueRequest{
                Filter = gh.IssueFilter.All
            });
            return ghIssues.Select(i => Issue.Make(i));
        }

        public async Task<IEnumerable<Milestone>> GetMilestonesForRepository(string owner,string name)
        {
            var milestones = await Client.Issue.Milestone.GetForRepository(owner, name);
            return milestones.Select(m => Milestone.Make(m));
        }

        public async Task<IEnumerable<Label>> GetLabelsForRepository(string owner, string name)
        {
            var labels = await Client.Issue.Labels.GetForRepository(owner, name);
            return labels.Select(l => Label.Make(l));
        }

        public async Task<RepositoryContent> GetRepository(int repoId)
        {
            var board  = await Repository.GetBoard(repoId);
            var labelTask = GetLabelsForRepository(board.OwnerName,board.RepositoryName);
            var milestonesTask = GetMilestonesForRepository(board.OwnerName, board.RepositoryName);
            var issueTask = GetIssuesForRepository(board.OwnerName, board.RepositoryName);

            board.labels = await labelTask;
            board.Milestones = await milestonesTask;
            board.Issues = await issueTask;

            return board;
        }
    }
}
