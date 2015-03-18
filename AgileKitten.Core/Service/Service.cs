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
        private DBRepository repository;

        public DBRepository dbRepository
        {
            get { return repository ?? (repository = new DBRepository()); }
        }

        public gh.GitHubClient Client { get; set; }

        public async Task<IEnumerable<RepositoryMeta>> GetRepositories()
        {
            var ghRepos = await Client.Repository.GetAllForCurrent();

            return ghRepos.Select(r => AgileKitten.Core.Model.RepositoryMeta.Make(r));
        }

        public async Task<IEnumerable<Issue>> GetIssuesForRepository(string owner, string name)
        {
            var ghIssues = await Client.Issue.GetForRepository(owner, name, new gh.RepositoryIssueRequest
            {
                Filter = gh.IssueFilter.All
            });
            return ghIssues.Select(i => Issue.Make(i));
        }

        public async Task<IEnumerable<Milestone>> GetMilestonesForRepository(string owner, string name)
        {
            var milestones = await Client.Issue.Milestone.GetForRepository(owner, name);
            return milestones.Select(m => Milestone.Make(m));
        }

        public async Task<IEnumerable<Label>> GetLabelsForRepository(string owner, string name)
        {
            var labels = await Client.Issue.Labels.GetForRepository(owner, name);
            return labels.Select(l => Label.Make(l));
        }

        public async Task<RepositoryContent> GetRepository(string owner, string repoName)
        {
            var repoTask = Client.Repository.Get(owner, repoName);
            var labelTask = GetLabelsForRepository(owner, repoName);
            var milestonesTask = GetMilestonesForRepository(owner, repoName);
            var issueTask = GetIssuesForRepository(owner, repoName);

            var repo = await repoTask;
            var retval = new RepositoryContent
            {
                GithubRepositoryId = repo.Id,
                RepositoryName = repo.Name,
                OwnerLogin = repo.Owner.Login,
                Milestones = await milestonesTask,
                Issues = await issueTask,
                Labels = await labelTask
            };

            var dbRepo = await dbRepository.MergeRepository(new Model.DTO.Repository
            {
                OriginId = repo.Id,
                Labels = labelTask.Result.Select(l => new Model.DTO.Label { Name = l.Name }),
                Issues = issueTask.Result.Select(i => new Model.DTO.Issue { Number = i.Number })
            });

            return retval;
        }
    }
}
