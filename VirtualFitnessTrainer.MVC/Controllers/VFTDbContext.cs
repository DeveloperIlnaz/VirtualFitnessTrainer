using System.Data.Entity;
using VirtualFitnessTrainer.MVC.Models;

namespace VirtualFitnessTrainer.MVC.Controllers
{
    public class VFTDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Exercise> Exercises { get; set; }

        public VFTDbContext() : base("DBConnection")
        {

        }
    }
}