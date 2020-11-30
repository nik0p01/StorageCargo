using StorageCargo.DAL.Entities;
using System.Collections.Generic;

namespace StorageCargo.DAL.Repository
{
    /// <summary>
    /// Интерфейс таблицы 
    /// </summary>
    interface ICollectionEntities
    {
        List<IEntity> Entities { get; }
        /// <summary>
        /// Метод добавления строки
        /// </summary>
        /// <param name="elements">Массив ячеек</param>
        void AddEntity(string[] elements);
    }
}
