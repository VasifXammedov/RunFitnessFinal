using Microsoft.AspNetCore.Mvc;
using RunFitness.DAL;
using RunFitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunFitness.ViewComponents
{
    public class FooterBottomViewComponent:ViewComponent
    {
        private readonly AppDbContext _db;
        public FooterBottomViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            FooterBottom bottom = _db.FooterBottoms.Where(fb => fb.IsDeleted == false).FirstOrDefault();
            return View(await Task.FromResult(bottom));
        }
    }
}
