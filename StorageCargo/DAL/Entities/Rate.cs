namespace StorageCargo.DAL.Entities
{
    /// <summary>
    /// Класс представляющий  строку из таблицы Тарифы
    /// </summary>
    public class Rate : IEntity
    {
        public int number;
        public int beginingPeriod;
        public int? endPeriod;
        public int rate;
    }
}
