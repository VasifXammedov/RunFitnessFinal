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
    public class TableController : Controller
    {
        private readonly AppDbContext _db;
        public TableController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {


            TableVM table = new TableVM
            {
                Weeks = _db.Weeks.Include(w=>w.TrainerWeeks).ThenInclude(w=>w.Trainer)
                .Include(w=>w.TrainerWeeks).ThenInclude(w=>w.Time).ToList(),
                trainerWeeks = _db.TrainerWeeks.Include(w=>w.Trainer).Include(w=>w.Time).ToList(),
                Times=_db.Times.Include(t=>t.TrainerWeeks).ThenInclude(t=>t.Trainer).ToList(),
                Trainers =_db.Trainers.Include(t=>t.TrainerWeeks).ThenInclude(t=>t.Week).ToList(),
            };

            return View(table);
        }
    }
}
