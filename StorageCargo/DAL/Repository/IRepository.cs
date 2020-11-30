using StorageCargo.DAL.Entities;
using System.Collections.Generic;

namespace StorageCargo.DAL.Repository
{
    /// <summary>
/// Интерфейс получения данных 
/// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Получить таблицу Груз
        /// </summary>
        /// <returns></returns>
        ICollection<Cargo> GetCargos();
        /// <summary>
        /// Получить таблицу Тариф
        /// </summary>
        /// <returns></returns>
        ICollection<Rate> GetRates();
    }
}
