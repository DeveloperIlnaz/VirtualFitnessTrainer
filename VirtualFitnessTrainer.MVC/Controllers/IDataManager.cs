using System.Collections.Generic;

namespace VirtualFitnessTrainer.MVC.Controllers
{
    public interface IDataManager
    {
        IEnumerable<T> Load<T>() where T : class;
        void Save<T>(IEnumerable<T> items) where T : class;
    }
}