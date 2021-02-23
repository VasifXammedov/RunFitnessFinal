using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RunFitness.DAL;
using RunFitness.ViewModels;

namespace RunFitness.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _db;
        public AboutController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            AboutVM aboutVM = new AboutVM
            {
                FitnessDetail = _db.FitnessDetails.Where(fd => fd.IsDeleted == false).FirstOrDefault(),
                About=_db.Abouts.Where(a=>a.IsDeleted==false).FirstOrDefault(),
                Success = _db.Successes.Where(s => s.IsDeleted == false).FirstOrDefault(),
                SuccessDetail = _db.SuccessDetails.Where(sd => sd.IsDeleted == false).FirstOrDefault()
            };
            return View(aboutVM);
        }
    }
}
