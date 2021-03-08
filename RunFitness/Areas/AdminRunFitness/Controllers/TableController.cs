using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RunFitness.DAL;
using RunFitness.Extensions;
using RunFitness.Models;
using RunFitness.ViewModels;

namespace RunFitness.Areas.AdminRunFitness.Controllers
{
    [Area("AdminRunFitness")]
    public class TableController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public TableController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            TableVM tableVM = new TableVM
            {
                Weeks = _db.Weeks.Include(w => w.TrainerWeeks).ThenInclude(w => w.Trainer)
               .ToList(),
                Times = _db.Times.Include(w => w.TrainerWeeks).ThenInclude(w => w.Trainer)
               .ToList(),
            };

            return View(tableVM);
        }

       

        public IActionResult Create()
        {
            GetTrainer();
            GetWeek();
            GetTime();
            return View();
        }

        #region Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainerWeek trainerWeek, int? TrainerCtgId, int? TimeCtgId, int? WeekCtgId)
        {
            GetTrainer();
            GetWeek();
            GetTime();

           

            foreach (TrainerWeek tw in await _db.TrainerWeeks.ToListAsync())
            {
                if (WeekCtgId==tw.WeekId&& TimeCtgId==tw.TimeId)
                {
                    return View();
                }
            }
            
            trainerWeek.TrainerId = (int)TrainerCtgId;
            //trainerWeek.TimeId = (int)TimeCtgId;
            trainerWeek.WeekId = (int)WeekCtgId;
            await _db.TrainerWeeks.AddAsync(trainerWeek);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #region Create void

        public void GetTrainer()
        {
            ViewBag.Trainers = _db.Trainers.Where(t => t.IsDeleted == false).ToList();

        }
        public void GetWeek()
        {
            ViewBag.Week = _db.Weeks.ToList();

        }
        public void GetTime()
        {
            ViewBag.Time = _db.Times.ToList();

        }

        #endregion

        #endregion


        #region Deleted

        public async Task<IActionResult> Delete(int? id)
        {  

            ViewData["WeekkId"] = new SelectList(_db.Weeks, "Id", "Name"); 
            ViewData["TrainerId"] = new SelectList(_db.Trainers, "Id", "Name");


            //ViewBag["TimeId"] = _db.TrainerWeeks.Include(t => t.Time); 
            //ViewBag["TrainerId"] = _db.TrainerWeeks.Include(t => t.Trainer );

            if (id == null) return NotFound();
            Time times = _db.Times.Where(t=>t.Id==id).Include(t => t.TrainerWeeks).ThenInclude(t => t.Week)
                .Include(t => t.TrainerWeeks).ThenInclude(t=>t.Trainer).FirstOrDefault();
            if (times == null) return NotFound();
            return View(times);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            Time times = _db.Times.FirstOrDefault(c => c.Id == id);
            if (times == null) return NotFound();
            _db.Times.Remove(times);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Update

        public IActionResult Update()
        {
            GetTrainer();
            GetWeek();
            GetTime();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(TrainerWeek trainerWeek, int? TrainerCtgId, int? TimeCtgId, int? WeekCtgId)
        {
            GetTrainer();
            GetWeek();
            GetTime();



            foreach (TrainerWeek tw in await _db.TrainerWeeks.ToListAsync())
            {
                if (WeekCtgId == tw.WeekId && TimeCtgId == tw.TimeId)
                {
                    return View();
                }
            }

            trainerWeek.TrainerId = (int)TrainerCtgId;
            trainerWeek.WeekId = (int)WeekCtgId;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion


    }
}


