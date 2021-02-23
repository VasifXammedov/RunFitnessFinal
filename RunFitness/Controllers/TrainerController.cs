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
        public IActionResult Index()
        {
            List<Trainer> trainers = _db.Trainers.Where(t => t.IsDeleted == false).ToList();
            return View(trainers);
        }
        public IActionResult Detail(int? id)
        {

            Trainer trainer = _db.Trainers.Where(t => t.IsDeleted == false).Include(td => td.TrainerDetail).FirstOrDefault(t=>t.Id==id);
             
         return View(trainer);

        }       
    }
}
