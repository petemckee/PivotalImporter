using System;
using System.Web.Mvc;
using System.Web.Routing;
using PivotalImporter2.Domain.Security;
using PivotalImporter2.Web.Controllers.Models;

namespace PivotalImporter2.Web.Controllers
{
	[HandleError]
	public class AccountController : Controller
	{
		private readonly IFormsAuthenticationService formsService;
		private readonly IMembershipService membershipService;

		public AccountController(IFormsAuthenticationService formsService, IMembershipService membershipService)
		{
			this.formsService = formsService;
			this.membershipService = membershipService;
		}

		protected override void Initialize(RequestContext requestContext)
		{
			/*
			if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
			if (MembershipService == null) { MembershipService = new AccountMembershipService(); }
			*/
			base.Initialize(requestContext);
		}

		// **************************************
		// URL: /Account/LogOn
		// **************************************

		[AllowAnonymous]
		public ActionResult LogOn()
		{
			return View();
		}


		[HttpPost]
		[AllowAnonymous]
		public ActionResult LogOn(LogOnModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				var pivotalUser = membershipService.ValidateUser(model.UserName, model.Password);
				if (pivotalUser != null)
				{
					//formsService.SignIn(model.UserName, model.RememberMe);
					formsService.SignIn(model.UserName, true, pivotalUser.ApiToken);

					if (!String.IsNullOrEmpty(returnUrl))
					{
						return Redirect(returnUrl);
					}
					else
					{
						return RedirectToAction("Index", "Upload");
					}
				}
				else
				{
					ModelState.AddModelError("", "The user name or password provided is incorrect.");
				}
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		// **************************************
		// URL: /Account/LogOff
		// **************************************

		public ActionResult LogOff()
		{
			formsService.SignOut();

			return RedirectToAction("Index", "Home");
		}

		// **************************************
		// URL: /Account/Register
		// **************************************

		public ActionResult Register()
		{
			//ViewData["PasswordLength"] = membershipService.MinPasswordLength;
			return View();
		}

		[HttpPost]
		public ActionResult Register(RegisterModel model)
		{/*
			if (ModelState.IsValid)
			{
				// Attempt to register the user
				MembershipCreateStatus createStatus = membershipService.CreateUser(model.UserName, model.Password, model.Email);

				if (createStatus == MembershipCreateStatus.Success)
				{
					formsService.SignIn(model.UserName, false ); // createPersistentCookie
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
				}
			}
*/
			// If we got this far, something failed, redisplay form
			//ViewData["PasswordLength"] = membershipService.MinPasswordLength;
			return View(model);
		}

		// **************************************
		// URL: /Account/ChangePassword
		// **************************************

		[Authorize]
		public ActionResult ChangePassword()
		{
			//ViewData["PasswordLength"] = membershipService.MinPasswordLength;
			return View();
		}

		[Authorize]
		[HttpPost]
		public ActionResult ChangePassword(ChangePasswordModel model)
		{/*
			if (ModelState.IsValid)
			{
				if (membershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
				{
					return RedirectToAction("ChangePasswordSuccess");
				}
				else
				{
					ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
				}
			}

			// If we got this far, something failed, redisplay form
			ViewData["PasswordLength"] = membershipService.MinPasswordLength;
		  */
			return View(model);
		}

		// **************************************
		// URL: /Account/ChangePasswordSuccess
		// **************************************

		public ActionResult ChangePasswordSuccess()
		{
			return View();
		}

	}
}
