using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace myoung_portfolio2222.Models
{
	public class PersonalEmail
	{
		public async Task SendAsync(MailMessage message)
		{
			var GmailUsername = WebConfigurationManager.AppSettings["username"];
			var GmailPassword = WebConfigurationManager.AppSettings["password"];
			var host = WebConfigurationManager.AppSettings["host"];
			int port = Convert.ToInt32(WebConfigurationManager.AppSettings["port"]);

			using (var smtp = new SmtpClient() // turns it into smtp format and sends it
			{
				Host = host,
				Port = port,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(GmailUsername, GmailPassword)
			})
			{
				try // try catch prevents any exception screens (red or yellow badrequest type pages)
				{
					await smtp.SendMailAsync(message);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					await Task.FromResult(0);
				}
			};
		}
	}
}
