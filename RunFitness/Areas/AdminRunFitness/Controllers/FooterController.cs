using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RunFitness.DAL;
using RunFitness.Extensions;
using RunFitness.Models;

namespace RunFitness.Areas.AdminRunFitness.Controllers
{
    [Area("AdminRunFitness")]
    public class FooterController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public FooterController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Bios.Where(t => t.IsDeleted == false).ToList());
        }

        #region Update

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Bio Bio = _db.Bios
                .FirstOrDefault(c => c.IsDeleted == false && c.Id == id);
            if (Bio == null) return NotFound();
            return View(Bio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Bio Bio)
        {
            Bio viewBio = _db.Bios.FirstOrDefault(c => c.Id == id && c.IsDeleted == false);

            if (Bio.Photo != null)
            {

                if (!Bio.Photo.IsImage())
                {
                    ModelState.AddModelError("", "Bunu yaratmaq ucun wekil tipi yarat!!!");
                    return View(viewBio);
                }
                if (!Bio.Photo.MaxSize(200))
                {
                    ModelState.AddModelError("", "Wekilin olcusu 200kb-dan az olmalidi!!!");
                    return View(viewBio);
                }

                string folder = Path.Combine("img");
                string fileName = await Bio.Photo.SaveImgAsync(_env.WebRootPath, folder);
                Bio.LogoImage = fileName;
            }

            Bio.IsDeleted = false;
            viewBio.Title = Bio.Title;
            viewBio.Description = Bio.Description;
            viewBio.Address = Bio.Address;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        #endregion

        #region Detail


        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            Bio Bio = _db.Bios
                .FirstOrDefault(c => c.IsDeleted == false && c.Id == id);
            if (Bio == null) return NotFound();
            return View(Bio);

        }

        #endregion

    }
}
