using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using myoung_portfolio2222.Models;
using myoung_Portfolio2222.Models;
using static myoung_Portfolio2222.EmailService;

namespace myoung_Portfolio2222.Controllers
{
	[RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Contact(EmailModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var body = "<p>Email From: <bold>{0}</bold> ({1})</p><p> Message:</p><p>{2}</p>";
					var from = model.FromName + "<" + model.FromEmail + ">"; //MyPortfolio part is the name that shows up on email preview
																			 //model.Body = "This is a message from your portfolio site. The name and the email of the contacting person is above.";

					var email = new MailMessage(from, ConfigurationManager.AppSettings["emailto"])
					{           //composing body here,
						Subject = "Portfolio Contact",
						Body = string.Format(body, model.FromName, model.FromEmail,
						model.Body),
						IsBodyHtml = true
					};

					var svc = new PersonalEmail(); //instantiation to be able to access SendAsync method(it actually sends the email) and runs the PersonalEmail function in Email Helper
					await svc.SendAsync(email);

					return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					await Task.FromResult(0);
				}
			}
			return View(model); //return to view with empty email model
		}

		public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View("Index");
        }
    }
}