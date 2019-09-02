using System;
using System.Collections.Generic;

namespace VirtualFitnessTrainer.MVC.Controllers
{
    public class BaseController
    {
        protected IDataManager dataManager = new SerializableDataManager();

        protected void Save<T>(IEnumerable<T> items) where T : class
        {
            dataManager.Save<T>(items);
        }

        protected IEnumerable<T> Load<T>() where T : class
        {
            return dataManager.Load<T>();
        }
    }
}