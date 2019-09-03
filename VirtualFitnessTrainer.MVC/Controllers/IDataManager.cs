using System.Collections.Generic;

namespace VirtualFitnessTrainer.MVC.Controllers
{
    public interface IDataManager
    {
        List<T> Load<T>() where T : class;
        void Save<T>(List<T> items) where T : class;
    }
}