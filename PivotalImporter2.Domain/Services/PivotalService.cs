using System;
using System.Collections.Generic;
using System.Linq;
using PivotalImporter2.Domain.Cacheing;
using PivotalImporter2.Domain.Security;
using PivotalImporter2.Domain.Services;
using PivotalTrackerAPI.Domain.Enumerations;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalImporter2.Domain.PivotalTracker
{
	public class PivotalService : IPivotalService
	{
		private readonly IMembershipService membershipService;
		private readonly ISessionService sessionService;
		private readonly ICacheProvider cacheProvider;
		private readonly IFormsAuthenticationService formsAuthenticationService;
		private readonly PivotalUser user;
		private IEnumerable<PivotalProject> projects;

		private readonly string cacheNameProjects = "pi_p";
		private readonly string cacheNameProjectUsers = "pi_m";
		private readonly string cacheNameProjectStories = "pi_s";

		public PivotalService(IMembershipService membershipService, ISessionService sessionService, ICacheProvider cacheProvider, IFormsAuthenticationService formsAuthenticationService)
		{
			this.membershipService = membershipService;
			this.sessionService = sessionService;
			this.cacheProvider = cacheProvider;
			this.formsAuthenticationService = formsAuthenticationService;

			var apiToken = sessionService.ApiToken();
			if (String.IsNullOrEmpty(apiToken))
				formsAuthenticationService.SignOut();

			this.user = new PivotalUser(apiToken);
			
			//this.projects = user.FetchProjects();
			//this.projects = Projects();
		}

		public IEnumerable<PivotalProject> Projects()
		{
			return this.Projects(false);
		}
		public IEnumerable<PivotalProject> Projects(bool refreshCache)
		{

			if (cacheProvider.Get(cacheNameProjects) == null || refreshCache)
			{
				this.projects = this.user.FetchProjects();
				// set cache
				cacheProvider.Set(cacheNameProjects, projects, cacheProvider.CacheTime());
				return this.projects;
			}
			else
			{
				if (this.projects != null)
					return this.projects;

				this.projects = cacheProvider.Get(cacheNameProjects) as List<PivotalProject>;
			}

			return this.projects;
		}

		public IEnumerable<PivotalMembership> ProjectUsers(int projectId)
		{
			return this.ProjectUsers(projectId, false);
		}
		public IEnumerable<PivotalMembership> ProjectUsers(int projectId, bool refreshCache)
		{
			var cacheNameCurrentProject = String.Concat(cacheNameProjectUsers, "_", projectId);
			//var userName = sessionService.UserName();
			IList<PivotalMembership> memberships = new List<PivotalMembership>();

			if (cacheProvider.Get(cacheNameCurrentProject) != null && !refreshCache)
			{
				memberships = (cacheProvider.Get(cacheNameCurrentProject) as List<PivotalMembership>);
			}
			else
			{
				var project = this.projects.Where(x => x.Id == projectId).First();
				memberships = project.FetchMembers(user);
				cacheProvider.Set(cacheNameCurrentProject, memberships, cacheProvider.CacheTime());
			}

			// TODO - option to order or not / extension?
			return memberships.OrderBy(x => x.Person.Name);
		}



		public IEnumerable<PivotalStory> ProjectStories(int projectId)
		{
			return this.ProjectStories(projectId, false);
		}
		public IEnumerable<PivotalStory> ProjectStories(int projectId, bool refreshCache)
		{
			var cacheName = String.Concat(cacheNameProjectStories, "_", projectId);
			IEnumerable<PivotalStory> stories;

			if (cacheProvider.Get(cacheName) == null || refreshCache)
			{
				var project = this.projects.Where(p => p.Id == projectId).First();
				if (project == null)
					throw new Exception("Project doesnt exist: " + projectId);
				
				stories = project.FetchStories(this.user);

				// set cache
				cacheProvider.Set(cacheName, stories, cacheProvider.CacheTime());
			}
			else
			{
				stories = cacheProvider.Get(cacheName) as List<PivotalStory>;
			}

			return stories;
		}

		public IEnumerable<string> StoryTypes()
		{
			IList<string> types = new List<string>();
			foreach (var type in Enum.GetValues(typeof(PivotalStoryType)))
			{
				types.Add(type.ToString());
			}
			return types;
		}


		/// <summary>
		///  Not yet used
		/// </summary>
		/// <param name="stories"></param>
		/// <param name="projectId"></param>
		/// <returns></returns>
		public IEnumerable<PivotalStory> SaveStories(List<PivotalStory> stories, int? projectId)
		{
			var proj = this.Projects().Where(x => x.Id == projectId).FirstOrDefault();
			if (proj != null)
			{
				foreach (var story in stories)
				{
					var st = new PivotalStory(PivotalStoryType.feature, story.Name, story.Description);
					st.Estimate = story.Estimate;
					st.Owner = story.Owner;
					st.Requestor = story.Requestor;
					st.Labels = story.Labels;
					
					proj.AddStory(this.membershipService.CurrentUser(), st, false);
				}
			}
			// TODO! Return a helpful object
			return Enumerable.Empty<PivotalStory>();
		}
	}
}