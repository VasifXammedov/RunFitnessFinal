using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RunFitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunFitness.DAL
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Fitness> Fitnesses { get; set; }
        public DbSet<FitnessDetail> FitnessDetails { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<TrainerDetail> TrainerDetails { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<TrainerWeek> TrainerWeeks { get; set; }
        public DbSet<Week> Weeks { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceDetail> ServiceDetails { get; set; }
        public DbSet<PopularDetail> PopularDetails { get; set; }
        public DbSet<Popular> Populars { get; set; }
        public DbSet<Success> Successes { get; set; }
        public DbSet<SuccessDetail> SuccessDetails { get; set; }
        public DbSet<Bio> Bios { get; set; }
        public DbSet<FooterBottom> FooterBottoms { get; set; }
        public DbSet<Contact> Contacts { get; set; }

    }
}
