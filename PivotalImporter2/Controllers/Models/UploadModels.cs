﻿using System.Collections.Generic;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalImporter2.Controllers.Models
{
	public class UploadModel
	{
		public IList<PivotalStory> PivotalStories;
		public IEnumerable<PivotalProject> PivotalProjects;
		public IEnumerable<PivotalMembership> PivotalMemberships;
		public IEnumerable<string> PivotalStoryTypes;
		public int ProjectId;

		public UploadModel()
		{
		}

		public UploadModel(IList<PivotalStory> pivotalStories, IEnumerable<PivotalProject> pivotalProjects, IEnumerable<PivotalMembership> pivotalMemberships, IEnumerable<string> pivotalStoryTypes, int projectId)
		{
			PivotalStories = pivotalStories;
			PivotalProjects = pivotalProjects;
			PivotalMemberships = pivotalMemberships;
			PivotalStoryTypes = pivotalStoryTypes;
			ProjectId = projectId;
		}
	}
}