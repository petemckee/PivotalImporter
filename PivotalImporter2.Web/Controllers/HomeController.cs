﻿using System.Web.Mvc;

namespace PivotalImporter2.Web.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}
	}
}