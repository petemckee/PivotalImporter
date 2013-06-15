using System.Collections.Generic;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalImporter2.Domain.Services
{
	public class PivotalExcelImportInfo
	{
		public IEnumerable<PivotalStory> Stories;
		public string GenericLabels;

		public PivotalExcelImportInfo(IEnumerable<PivotalStory> stories, string genericLabels)
		{
			Stories = stories;
			GenericLabels = genericLabels;
		}
	}
}