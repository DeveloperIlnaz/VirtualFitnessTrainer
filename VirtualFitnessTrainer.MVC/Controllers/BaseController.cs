using System.Collections.Generic;

namespace VirtualFitnessTrainer.MVC.Controllers
{
    public class BaseController
    {
        #region Properties
        protected IDataManager dataManager = new SerializableDataManager();
        #endregion

        #region Methods
        protected void Save<T>(List<T> items) where T : class
        {
            dataManager.Save<T>(items);
        }
        protected List<T> Load<T>() where T : class
        {
            return dataManager.Load<T>();
        }
        #endregion
    }
}