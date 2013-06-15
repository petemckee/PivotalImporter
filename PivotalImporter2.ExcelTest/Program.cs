using System;
using PivotalImporter2.Domain.Services;

namespace PivotalImporter2.ExcelTest
{
	class Program
	{
		static void Main(string[] args)
		{
			var importerTest = new ImporterTest();
			importerTest.Import();
		}
	}

	internal class ImporterTest	
	{
		public void Import()
		{
			var filePath = @"C:\Users\pete\Documents\Visual Studio 2012\Projects\PivotalImporter2\PivotalImporter2.ExcelTest\pivotalimporter2_example.xlsx";
			var exService = new PivotalExcelService();
			foreach (var s in exService.Stories(filePath))
			{
				Console.WriteLine(s.Name);
			}
			Console.Read();
		}
	}
}
