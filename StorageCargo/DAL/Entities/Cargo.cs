using System;

namespace StorageCargo.DAL.Entities
{
    /// <summary>
    /// Класс представляющий  строку из таблицы Груз
    /// </summary>
    public class Cargo : IEntity
    {
        public string name;
        public DateTime arrival;
        public DateTime? leaving;
    }
}
