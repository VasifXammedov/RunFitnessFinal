using Microsoft.AspNetCore.Mvc;
using RunFitness.DAL;
using RunFitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunFitness.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private readonly AppDbContext _db;
        public FooterViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Bio model = _db.Bios.Where(b => b.IsDeleted == false).FirstOrDefault();
            return View(await Task.FromResult(model));
        }
    }
}
