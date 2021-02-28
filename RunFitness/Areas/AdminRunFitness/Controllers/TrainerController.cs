using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunFitness.DAL;
using RunFitness.Extensions;
using RunFitness.Models;

namespace RunFitness.Areas.AdminRunFitness.Controllers
{
    [Area("AdminRunFitness")]
    public class TrainerController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public TrainerController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Trainers.Where(t => t.IsDeleted == false).ToList());
        }

        #region Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trainer Trainer)
        {

            bool isExist = _db.Trainers.Where(c => c.IsDeleted == false)
                   .Any(c => c.Name.ToLower() == Trainer.Name.ToLower());
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu Trainer artiq movcuddur");
                return View();
            }
            if (Trainer.Photo == null)
            {
                ModelState.AddModelError("", "Ayeee wekili elave ele!!!");
                return View();
            }
            if (!Trainer.Photo.IsImage())
            {
                ModelState.AddModelError("", "Bunu yaratmaq ucun wekil tipi yarat!!!");
                return View();
            }
            if (!Trainer.Photo.MaxSize(200))
            {
                ModelState.AddModelError("", "Wekilin olcusu 200kb-dan az olmalidi!!!");
                return View();
            }

            string folder = Path.Combine("img", "trainers");
            string fileName = await Trainer.Photo.SaveImgAsync(_env.WebRootPath, folder);
            Trainer.Image = fileName;
            Trainer.IsDeleted = false;
            await _db.Trainers.AddAsync(Trainer);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        #endregion

        #region Deleted

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Trainer trainer = _db.Trainers
                .FirstOrDefault(c => c.IsDeleted == false && c.Id == id);
            if (trainer == null) return NotFound();
            return View(trainer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            Trainer trainer = _db.Trainers
                .FirstOrDefault(c => c.IsDeleted == false && c.Id == id);
            if (trainer == null) return NotFound();
            trainer.IsDeleted = true;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Update

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Trainer Trainer = _db.Trainers
                .Include(c=>c.TrainerDetail).FirstOrDefault(c => c.IsDeleted == false && c.Id == id);
            if (Trainer == null) return NotFound();
            return View(Trainer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Trainer Trainer)
        {
            Trainer viewTrainer = _db.Trainers.Include(c => c.TrainerDetail).FirstOrDefault(c => c.Id == id && c.IsDeleted == false);

            if (Trainer.Photo != null)
            {

                if (!Trainer.Photo.IsImage())
                {
                    ModelState.AddModelError("", "Bunu yaratmaq ucun wekil tipi yarat!!!");
                    return View(viewTrainer);
                }
                if (!Trainer.Photo.MaxSize(200))
                {
                    ModelState.AddModelError("", "Wekilin olcusu 200kb-dan az olmalidi!!!");
                    return View(viewTrainer);
                }

                string folder = Path.Combine( "img", "trainers");
                string fileName = await Trainer.Photo.SaveImgAsync(_env.WebRootPath, folder);
                Trainer.Image = fileName;
            }

            Trainer.IsDeleted = false;
            viewTrainer.Name = Trainer.Name;
            viewTrainer.Profession = Trainer.Profession;
           

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        #endregion

        #region Detail


        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            Trainer trainer = _db.Trainers.Include(c=>c.TrainerDetail)
                .FirstOrDefault(c => c.IsDeleted == false && c.Id == id);
            if (trainer == null) return NotFound();
            return View(trainer);

        }

        #endregion

    }
}
