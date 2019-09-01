using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace VirtualFitnessTrainer.MVC.Controllers
{
    /// <summary>
    /// Сериализуемый контроллер.
    /// </summary>
    public static class SerializableSaver
    {
        #region Methods
        /// <summary>
        /// Выгружает (десериализует) данные.
        /// </summary>
        /// <typeparam name="T">Тип элментов.</typeparam>
        /// <param name="filePath">Путь файла.</param>
        /// <returns>Если true то, возвращает все элменты типа, иначе пустой список.</returns>
        public static List<T> Load<T>(string filePath)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                if (fileStream.Length > 0 && binaryFormatter.Deserialize(fileStream) is List<T> items)
                {
                    return items;
                }
                else
                {
                    return new List<T>();
                }
            }
        }
        /// <summary>
        /// Сохраняет (сериализует) данные.
        /// </summary>
        /// <typeparam name="T">Тип элементов.</typeparam>
        /// <param name="filePath">Путь файла.</param>
        /// <param name="items">Элементы.</param>
        public static void Save<T>(string filePath, List<T> items)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fileStream, items);
            }
        }
        #endregion
    }
}