using System.ComponentModel.DataAnnotations;
using System.Web;

namespace PivotalImporter2.Web.Controllers.Models
{
	public class UploadFileModel
	{
		[Required]
		public int ProjectId { get; set; }

		// TODO! Add file type check to validation..?
		[Required]
		public HttpPostedFileBase File { get; set; }

		public UploadFileModel()
		{
		}

		public UploadFileModel(int projectId, HttpPostedFileBase file)
		{
			ProjectId = projectId;
			File = file;
		}
	}
}