using System.Linq;
using System.Collections.Generic;

namespace VirtualFitnessTrainer.MVC.Controllers
{
    public class EntityDataManager : IDataManager
    {
        #region Methods
        /// <summary>
        /// Выгружает данные из EntityFramework.
        /// </summary>
        /// <typeparam name="T">Тип элементов.</typeparam>
        /// <returns></returns>
        public IEnumerable<T> Load<T>() where T : class
         {
            using (VFTDbContext vftDbContext = new VFTDbContext())
            {
                IEnumerable<T> items = vftDbContext.Set<T>().Where(t => true);

                return items;
            }
        }
        /// <summary>
        /// Сохраняет данные в EntityFramework.
        /// </summary>
        /// <typeparam name="T">Тип элементов.</typeparam>
        /// <param name="items">Элементы.</param>
        public void Save<T>(IEnumerable<T> items) where T : class
        {
            using (VFTDbContext vftDbContext = new VFTDbContext())
            {
                vftDbContext.Set<T>().AddRange(items);
                vftDbContext.SaveChanges();
            }
        }
        #endregion
    }
}