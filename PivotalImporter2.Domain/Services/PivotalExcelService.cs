using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Excel;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalImporter2.Domain.Services
{
	public interface IPivotalExcelService
	{
		IEnumerable<PivotalStory> Stories(string filePath);
		PivotalExcelImportInfo ImportInfo(string combine);
	}

	public class PivotalExcelService : IPivotalExcelService
	{
		// TODO Make configurable
		private string headerRowText = "EPIC";
		private string totalRowText = "TOTAL";
		private string genericLabelText = "GENERIC LABELS";

		public IEnumerable<PivotalStory> Stories(string filePath)
		{
			FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
			IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
			DataSet ds = excelReader.AsDataSet();
			var dt = ds.Tables[0];
			excelReader.Close();

			var headerRow = 0;
			var totalRow = 0;

			for (var r = 0; r < dt.Rows.Count; r++)
			{
				var txt = dt.Rows[r][0];
				if (txt != null && txt.ToString().ToUpperInvariant() == this.headerRowText)
				{
					headerRow = r;
					continue;
				}
				if (txt != null && txt.ToString().ToUpperInvariant() == this.totalRowText)
				{
					totalRow = r;
					break;
				}
			}

			//-- import into list
			var stories = new List<PivotalStory>();
			for (int r = (headerRow + 1); r <= (totalRow -1); r++)
			{
				var row = (DataRow) dt.Rows[r];

				// TODO - put in Enum
				var colName = 1;
				var colDesc = 2;
				var colEstimateHours = 3;
				var colEstimatePts = 4;
				var colRemaining = 5;
				var colRequestor = 6;
				var colOwner = 7;
				var colLabels = 8;

				//-- TODO - Check for nothing!
				if (String.IsNullOrEmpty(String.Concat(row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString())))
					continue;

				var story = new PivotalStory();
				story.Name = row[colName].ToString();
				story.Description = row[colDesc].ToString();
				story.Estimate = (row[colEstimatePts] != null && !string.IsNullOrEmpty(row[colEstimatePts].ToString())) ? int.Parse(row[colEstimatePts].ToString()) : 0;
				story.Requestor = row[colRequestor].ToString();
				story.Owner = row[colOwner].ToString();
				var labels = row[colLabels].ToString();
				if (!String.IsNullOrEmpty(labels))
				{
					story.LabelValues = labels.Split(',').Select(s => s.Trim()).ToList();
				}
				
				stories.Add(story);

			}

			return stories;
		}

		public PivotalExcelImportInfo ImportInfo(string filePath)
		{
			string genericLabels = String.Empty;

			FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
			IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
			DataSet ds = excelReader.AsDataSet();
			var dt = ds.Tables[0];
			excelReader.Close();

			var headerRow = 0;
			var totalRow = 0;

			for (var r = 0; r < dt.Rows.Count; r++)
			{
				var txt = dt.Rows[r][0];

				if (String.IsNullOrEmpty(genericLabels) && txt != null && txt.ToString().ToUpperInvariant() == this.genericLabelText)
				{
					genericLabels = (dt.Rows[r][1] != null) ? dt.Rows[r][1].ToString() : String.Empty;
				}

				if (txt != null && txt.ToString().ToUpperInvariant() == this.headerRowText)
				{
					headerRow = r;
					continue;
				}
				if (txt != null && txt.ToString().ToUpperInvariant() == this.totalRowText)
				{
					totalRow = r;
					break;
				}

			}

			//-- import into list
			var stories = new List<PivotalStory>();
			for (int r = (headerRow + 1); r <= (totalRow - 1); r++)
			{
				var row = (DataRow)dt.Rows[r];

				// TODO - put in Enum
				var colName = 1;
				var colDesc = 2;
				var colEstimateHours = 3;
				var colEstimatePts = 4;
				var colRemaining = 5;
				var colRequestor = 6;
				var colOwner = 7;
				var colLabels = 8;

				//-- TODO - Check for nothing!
				if (String.IsNullOrEmpty(String.Concat(row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString())))
					continue;

				var story = new PivotalStory();
				story.Name = row[colName].ToString();
				story.Description = row[colDesc].ToString();
				story.Estimate = (row[colEstimatePts] != null && !string.IsNullOrEmpty(row[colEstimatePts].ToString())) ? int.Parse(row[colEstimatePts].ToString()) : 0;
				story.Requestor = row[colRequestor].ToString();
				story.Owner = row[colOwner].ToString();
				var labels = row[colLabels].ToString();
				if (!String.IsNullOrEmpty(labels))
				{
					story.LabelValues = labels.Split(',').Select(s => s.Trim()).ToList();
				}

				stories.Add(story);

			}

			return new PivotalExcelImportInfo(stories, genericLabels);
		}

	}



	

	
}
