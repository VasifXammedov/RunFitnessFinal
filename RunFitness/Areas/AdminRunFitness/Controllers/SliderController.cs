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
    public class SliderController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {

            return View(_db.Sliders.Where(t => t.IsDeleted == false).ToList());
        }

        #region Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider Slider)
        {

            bool isExist = _db.Sliders.Where(c => c.IsDeleted == false)
                   .Any(c => c.Title.ToLower() == Slider.Title.ToLower());
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu artiq movcuddur");
                return View();
            }
            if (Slider.Photo == null)
            {
                ModelState.AddModelError("", "Ayeee wekili elave ele!!!");
                return View();
            }
            if (!Slider.Photo.IsImage())
            {
                ModelState.AddModelError("", "Bunu yaratmaq ucun wekil tipi yarat!!!");
                return View();
            }
            if (!Slider.Photo.MaxSize(200))
            {
                ModelState.AddModelError("", "Wekilin olcusu 200kb-dan az olmalidi!!!");
                return View();
            }

            string folder = Path.Combine("img", "Slider");
            string fileName = await Slider.Photo.SaveImgAsync(_env.WebRootPath, folder);
            Slider.Image = fileName;
            Slider.IsDeleted = false;
            await _db.Sliders.AddAsync(Slider);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        #endregion

        #region Deleted

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Slider Slider = _db.Sliders
                .FirstOrDefault(c => c.IsDeleted == false && c.Id == id);
            if (Slider == null) return NotFound();
            return View(Slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            Slider Slider = _db.Sliders
                .FirstOrDefault(c => c.IsDeleted == false && c.Id == id);
            if (Slider == null) return NotFound();
            Slider.IsDeleted = true;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Update

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Slider Slider = _db.Sliders
               .FirstOrDefault(c => c.IsDeleted == false && c.Id == id);
            if (Slider == null) return NotFound();
            return View(Slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Slider Slider)
        {
            Slider viewSlider = _db.Sliders.FirstOrDefault(c => c.Id == id && c.IsDeleted == false);

            if (Slider.Photo != null)
            {

                if (!Slider.Photo.IsImage())
                {
                    ModelState.AddModelError("", "Bunu yaratmaq ucun wekil tipi yarat!!!");
                    return View(viewSlider);
                }
                if (!Slider.Photo.MaxSize(200))
                {
                    ModelState.AddModelError("", "Wekilin olcusu 200kb-dan az olmalidi!!!");
                    return View(viewSlider);
                }

                string folder = Path.Combine("img", "Slider");
                string fileName = await Slider.Photo.SaveImgAsync(_env.WebRootPath, folder);
                Slider.Image = fileName;
            }

            Slider.IsDeleted = false;
            viewSlider.Title = Slider.Title;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        #endregion

        #region Detail


        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            Slider slider = _db.Sliders
                .FirstOrDefault(c => c.IsDeleted == false && c.Id == id);
            if (slider == null) return NotFound();
            return View(slider);

        }

        #endregion
    }
}
