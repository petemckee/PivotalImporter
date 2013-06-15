using System;
using System.Globalization;
using System.Threading;
using PivotalTrackerAPI.Domain.Enumerations;

namespace PivotalImporter2.Domain.Extensions
{
	public static class Stringextensions
	{
		public static Guid ToGuid(this string str)
		{
			return Guid.Parse(str);
		}

		public static string ToTitleCase(this PivotalStoryType type)
		{
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			return textInfo.ToTitleCase(type.ToString());
		}
	}
}
