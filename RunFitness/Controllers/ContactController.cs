using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RunFitness.DAL;
using RunFitness.Models;

namespace RunFitness.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _db;
        public ContactController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Indexx(Contact contact)
        {

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "vasifvkh@code.edu.az",
                    Password = "2272422vv"
                }
            };
            MailAddress fromEmail = new MailAddress(contact.Email, contact.Name);
            MailAddress toEmail = new MailAddress("Vasifvkh@code.edu.az", "Vasif");
            MailMessage message = new MailMessage()
            {
                From = fromEmail,
                Subject = contact.Email,
                Body = $"Users Message: {contact.Message}{System.Environment.NewLine}Users PhoneNamber: {contact.PhoneNumber}"
            };

            message.To.Add(toEmail);
            client.Send(message);
            TempData["Success"] = "Emioglu narahat olma messaj getdi!";
            return RedirectToAction("Index", "Contact");
        }
    }
}
