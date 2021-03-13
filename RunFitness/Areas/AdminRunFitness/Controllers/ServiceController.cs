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
    public class ServiceController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public ServiceController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Services.Where(t => t.IsDeleted == false).ToList());
        }

        #region Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service Service)
        {

            bool isExist = _db.Services.Where(c => c.IsDeleted == false)
                   .Any(c => c.Title.ToLower() == Service.Title.ToLower());
            if (isExist)
            {
                ModelState.AddModelError("Title", "Bu artiq movcuddur");
                return View();
            }
            if (Service.Photo == null)
            {
                ModelState.AddModelError("", "Ayeee wekili elave ele!!!");
                return View();
            }
            if (!Service.Photo.IsImage())
            {
                ModelState.AddModelError("", "Bunu yaratmaq ucun wekil tipi yarat!!!");
                return View();
            }
            if (!Service.Photo.MaxSize(200))
            {
                ModelState.AddModelError("", "Wekilin olcusu 200kb-dan az olmalidi!!!");
                return View();
            }

            string folder = Path.Combine("img", "services");
            string fileName = await Service.Photo.SaveImgAsync(_env.WebRootPath, folder);
            Service.Image = fileName;
            Service.IsDeleted = false;
            await _db.Services.AddAsync(Service);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        #endregion

        #region Deleted

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Service Service = _db.Services
                .FirstOrDefault(c => c.IsDeleted == false && c.Id == id);
            if (Service == null) return NotFound();
            return View(Service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            Service Service = _db.Services
                .FirstOrDefault(c => c.IsDeleted == false && c.Id == id);
            if (Service == null) return NotFound();
            Service.IsDeleted = true;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Update

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Service Service = _db.Services
                .Include(c => c.ServiceDetail).FirstOrDefault(c => c.IsDeleted == false && c.Id == id);
            if (Service == null) return NotFound();
            return View(Service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Service Service)
        {
            Service viewService = _db.Services.Include(c => c.ServiceDetail).FirstOrDefault(c => c.Id == id && c.IsDeleted == false);

            if (Service.Photo != null)
            {

                if (!Service.Photo.IsImage())
                {
                    ModelState.AddModelError("", "Bunu yaratmaq ucun wekil tipi yarat!!!");
                    return View(viewService);
                }
                if (!Service.Photo.MaxSize(200))
                {
                    ModelState.AddModelError("", "Wekilin olcusu 200kb-dan az olmalidi!!!");
                    return View(viewService);
                }

                string folder = Path.Combine("img", "services");
                string fileName = await Service.Photo.SaveImgAsync(_env.WebRootPath, folder);
                Service.Image = fileName;
            }

            Service.IsDeleted = false;
            viewService.Title = Service.Title;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        #endregion

        #region Detail


        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            Service service = _db.Services.Include(c => c.ServiceDetail)
                .FirstOrDefault(c => c.IsDeleted == false && c.Id == id);
            if (service == null) return NotFound();
            return View(service);

        }

        #endregion




    }
}
