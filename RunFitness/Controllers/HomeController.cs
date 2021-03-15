using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RunFitness.DAL;
using RunFitness.Models;
using RunFitness.ViewModels;

namespace RunFitness.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;

        public HomeController(ILogger<HomeController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Sliders = _db.Sliders.Where(s => s.IsDeleted == false).ToList(),
                FitnessDetail = _db.FitnessDetails.Where(fd => fd.IsDeleted == false).FirstOrDefault(),
                Services = _db.Services.Where(s => s.IsDeleted == false).ToList(),
                ServiceDetails = _db.ServiceDetails.Where(sd => sd.IsDeleted == false).ToList(),
                Populars = _db.Populars.Where(p => p.IsDeleted == false).ToList(),
                PopularDetails = _db.PopularDetails.Where(pd => pd.IsDeleted == false).ToList(),
                Success = _db.Successes.Where(s => s.IsDeleted == false).FirstOrDefault(),
                SuccessDetail = _db.SuccessDetails.Where(sd => sd.IsDeleted == false).FirstOrDefault(),
                FooterBottom = _db.FooterBottoms.Where(fb => fb.IsDeleted == false).FirstOrDefault(),
                Trainers = _db.Trainers.Where(t => t.IsDeleted == false).ToList()
            };
            return View(homeVM);
        }

        // Eger databazada gostermek isteyirikse:.....

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Index(Contact contact)
        //{
        //    contact.IsDeleted = false;
        //    await _db.AddAsync(contact);
        //    await _db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

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
                Subject =contact.Email,
                Body =$"Users Message: {contact.Message}{System.Environment.NewLine}Users PhoneNamber: {contact.PhoneNumber}"
            };

            message.To.Add(toEmail);
            client.Send(message);
            TempData["Success"] = "Emioglu narahat olma messaj getdi!";
            return RedirectToAction("Index","Home");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
