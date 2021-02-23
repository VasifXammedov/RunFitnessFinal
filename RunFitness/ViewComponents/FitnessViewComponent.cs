using Microsoft.AspNetCore.Mvc;
using RunFitness.DAL;
using RunFitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunFitness.ViewComponents
{
    public class FitnessViewComponent:ViewComponent
    {
        private readonly AppDbContext _db;
        public FitnessViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Fitness fitness = _db.Fitnesses.Where(f=>f.IsDeleted==false).FirstOrDefault();
            return View(await Task.FromResult(fitness));
        }
    }
}
