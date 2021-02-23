using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunFitness.DAL;
using RunFitness.Models;
using RunFitness.ViewModels;

namespace RunFitness.Controllers
{
    public class TrainerController : Controller
    {
        private readonly AppDbContext _db;
        public TrainerController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            ViewData["GetTrainers"] = searchString;
            var trainerQuery = from x in _db.Trainers select x;
            if (!String.IsNullOrEmpty(searchString))
            {
                trainerQuery = trainerQuery.Where(x => x.Name.Contains(searchString) && x.IsDeleted == false);
                return View(await trainerQuery.AsNoTracking().ToListAsync());
            }
            else
            {
                ViewBag.PageCount = Decimal.Ceiling((decimal)_db.Trainers.Where(b => b.IsDeleted == false).Count() / 4);
                ViewBag.page = page;
                if (page == null)
                {
                    List<Trainer> trainer = _db.Trainers.Where(b => b.IsDeleted == false).Take(4).ToList();
                    return View(trainer);
                }
                List<Trainer> trainer1 = _db.Trainers.Where(b => b.IsDeleted == false).Skip((int)(page - 1) * 4).Take(4).ToList();
                return View(trainer1);
            }
           
        }
        public IActionResult Detail(int? id)
        {

            Trainer trainer = _db.Trainers.Where(t => t.IsDeleted == false).Include(td => td.TrainerDetail).FirstOrDefault(t=>t.Id==id);
             
         return View(trainer);

        }       
    }
}
