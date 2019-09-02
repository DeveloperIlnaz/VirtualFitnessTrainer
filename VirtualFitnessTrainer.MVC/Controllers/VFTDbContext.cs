using System.Data.Entity;

namespace VirtualFitnessTrainer.MVC.Controllers
{
    public class VFTDbContext : DbContext
    {
        public VFTDbContext() : base("VFTDbConnection")
        {

        }
    }
}