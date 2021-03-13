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
    public class SuccessController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public SuccessController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Successes.Where(t => t.IsDeleted == false).ToList());
        }

        #region Update

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Success success = _db.Successes
                .Include(c => c.SuccessDetail).FirstOrDefault(c => c.IsDeleted == false && c.Id == id);
            if (success == null) return NotFound();
            return View(success);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Success Success)
        {
            Success viewSuccess = _db.Successes.Include(c => c.SuccessDetail).FirstOrDefault(c => c.Id == id && c.IsDeleted == false);

            if (Success.Photo != null)
            {

                if (!Success.Photo.IsImage())
                {
                    ModelState.AddModelError("", "Bunu yaratmaq ucun wekil tipi yarat!!!");
                    return View(viewSuccess);
                }
                if (!Success.Photo.MaxSize(200))
                {
                    ModelState.AddModelError("", "Wekilin olcusu 200kb-dan az olmalidi!!!");
                    return View(viewSuccess);
                }

                string folder = Path.Combine("img", "stories");
                string fileName = await Success.Photo.SaveImgAsync(_env.WebRootPath, folder);
                Success.Image = fileName;
            }

            Success.IsDeleted = false;
            viewSuccess.Title = Success.Title;
            viewSuccess.Description = Success.Description;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        #endregion

        #region Detail


        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            Success success = _db.Successes.Include(c => c.SuccessDetail)
                .FirstOrDefault(c => c.IsDeleted == false && c.Id == id);
            if (success == null) return NotFound();
            return View(success);

        }

        #endregion
    }
}
