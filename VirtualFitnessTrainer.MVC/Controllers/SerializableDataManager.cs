using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace VirtualFitnessTrainer.MVC.Controllers
{
    /// <summary>
    /// Serializable менеджер данных.
    /// </summary>
    public class SerializableDataManager : IDataManager
    {
        #region Methods
        /// <summary>
        /// Выгружает (десериализует) данные.
        /// </summary>
        /// <typeparam name="T">Тип элментов.</typeparam>
        /// <returns></returns>
        public List<T> Load<T>() where T : class
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            string filePath = typeof(T).Name + ".dat";

            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                if (fileStream.Length < 1)
                {
                    return new List<T>();
                }

                List<T> items = binaryFormatter.Deserialize(fileStream) as List<T>;

                return items;
            }
        }
        /// <summary>
        /// Сохраняет (сериализует) данные.
        /// </summary>
        /// <typeparam name="T">Тип элементов.</typeparam>
        /// <param name="items">Элементы.</param>
        public void Save<T>(List<T> items) where T : class
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            string filePath = typeof(T).Name + ".dat";

            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fileStream, items);
            }
        }
        #endregion
    }
}