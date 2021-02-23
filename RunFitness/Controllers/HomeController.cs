﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
               Sliders=_db.Sliders.Where(s=>s.IsDeleted==false).ToList(), 
               FitnessDetail=_db.FitnessDetails.Where(fd=>fd.IsDeleted==false).FirstOrDefault(),
               Services=_db.Services.Where(s=>s.IsDeleted==false).ToList(),
               ServiceDetails=_db.ServiceDetails.Where(sd=>sd.IsDeleted==false).ToList(),
               Populars=_db.Populars.Where(p=>p.IsDeleted==false).ToList(),
               PopularDetails=_db.PopularDetails.Where(pd=>pd.IsDeleted==false).ToList(),
               Success=_db.Successes.Where(s=>s.IsDeleted==false).FirstOrDefault(),
               SuccessDetail=_db.SuccessDetails.Where(sd=>sd.IsDeleted==false).FirstOrDefault(),
               FooterBottom=_db.FooterBottoms.Where(fb=>fb.IsDeleted==false).FirstOrDefault(),
               Trainers=_db.Trainers.Where(t=>t.IsDeleted==false).ToList()
               
            };
            return View(homeVM);
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