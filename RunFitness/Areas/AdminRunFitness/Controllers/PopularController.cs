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
    public class PopularController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public PopularController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Populars.Where(t => t.IsDeleted == false).ToList());
        }

        #region Update

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Popular Popular = _db.Populars
                .Include(c => c.PopularDetail).FirstOrDefault(c => c.IsDeleted == false && c.Id == id);
            if (Popular == null) return NotFound();
            return View(Popular);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Popular Popular)
        {
            Popular viewPopular = _db.Populars.Include(c => c.PopularDetail).FirstOrDefault(c => c.Id == id && c.IsDeleted == false);

            if (Popular.Photo != null)
            {

                if (!Popular.Photo.IsImage())
                {
                    ModelState.AddModelError("", "Bunu yaratmaq ucun wekil tipi yarat!!!");
                    return View(viewPopular);
                }
                if (!Popular.Photo.MaxSize(200))
                {
                    ModelState.AddModelError("", "Wekilin olcusu 200kb-dan az olmalidi!!!");
                    return View(viewPopular);
                }

                string folder = Path.Combine("img", "popular");
                string fileName = await Popular.Photo.SaveImgAsync(_env.WebRootPath, folder);
                Popular.Image = fileName;
            }

            Popular.IsDeleted = false;
            viewPopular.Image = Popular.Image;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        #endregion

        #region Detail


        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            Popular Popular = _db.Populars.Include(c => c.PopularDetail)
                .FirstOrDefault(c => c.IsDeleted == false && c.Id == id);
            if (Popular == null) return NotFound();
            return View(Popular);

        }

        #endregion
    }
}
