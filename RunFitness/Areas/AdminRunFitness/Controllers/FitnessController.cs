using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunFitness.DAL;
using RunFitness.Extensions;
using RunFitness.Models;

namespace RunFitness.Areas.AdminRunFitness.Controllers
{
    [Area("AdminRunFitness")]
    [Authorize(Roles = "Admin")]
    public class FitnessController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public FitnessController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Fitnesses.Where(t => t.IsDeleted == false).ToList());
        }

        #region Update

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Fitness fitness = _db.Fitnesses
                .Include(c => c.FitnessDetail).FirstOrDefault(c => c.IsDeleted == false && c.Id == id);
            if (fitness == null) return NotFound();
            return View(fitness);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Fitness fitness)
        {
            Fitness viewFitness = _db.Fitnesses.Include(c => c.FitnessDetail).FirstOrDefault(c => c.Id == id && c.IsDeleted == false);

            if (fitness.Photo != null)
            {

                if (!fitness.Photo.IsImage())
                {
                    ModelState.AddModelError("", "Bunu yaratmaq ucun wekil tipi yarat!!!");
                    return View(viewFitness);
                }
                if (!fitness.Photo.MaxSize(200))
                {
                    ModelState.AddModelError("", "Wekilin olcusu 200kb-dan az olmalidi!!!");
                    return View(viewFitness);
                }

                string folder = Path.Combine("img", "fitness");
                string fileName = await fitness.Photo.SaveImgAsync(_env.WebRootPath, folder);
                fitness.Image = fileName;
            }

            fitness.IsDeleted = false;
            viewFitness.Title = fitness.Title;
            viewFitness.Description = fitness.Description;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        #endregion

        #region Detail


        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            Fitness fitness = _db.Fitnesses.Include(c => c.FitnessDetail)
                .FirstOrDefault(c => c.IsDeleted == false && c.Id == id);
            if (fitness == null) return NotFound();
            return View(fitness);

        }

        #endregion
    }
}
