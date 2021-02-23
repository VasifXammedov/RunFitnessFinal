using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RunFitness.DAL;
using RunFitness.ViewModels;

namespace RunFitness.Controllers
{
    public class ServiceController : Controller
    {
        private readonly AppDbContext _db;
        public ServiceController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            ServiceVM serviceVM = new ServiceVM
            {
                Services = _db.Services.Where(s => s.IsDeleted == false).ToList(),
                ServiceDetails = _db.ServiceDetails.Where(sd => sd.IsDeleted == false).ToList(),
                Populars = _db.Populars.Where(p => p.IsDeleted == false).ToList(),
                PopularDetails = _db.PopularDetails.Where(pd => pd.IsDeleted == false).ToList()
            };
            return View(serviceVM);
        }
    }
}
