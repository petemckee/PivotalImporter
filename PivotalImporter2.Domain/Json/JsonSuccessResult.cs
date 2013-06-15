using System.Web.Mvc;

namespace PivotalImporter2.Domain.Json
{
	public sealed class JsonSuccessResult : JsonResult
	{
		public JsonSuccessResult()
		{
			this.ContentType = "application/json";
			this.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
		}
	}
}
