using StorageCargo.DAL.Entities;
using System.Collections.Generic;

namespace StorageCargo.DAL.Repository
{

    /// <summary>
    /// Класс представляющий таблицу Тариф (коллекция объектов Rate)
    /// </summary>
    class CollectionRates : ICollectionEntities
    {
        public CollectionRates()
        {
            Entities = new List<IEntity>();
        }

        public List<IEntity> Entities { get; private set; }
        /// <summary>
        /// Метод добавления строки в таблицу (добавления элемента Rate)
        /// </summary>
        /// <param name="elements">Массив строк представляющий элементы строки таблицы Тариф</param>
        public void AddEntity(string[] elements)
        {
            var number = int.Parse(elements[0]);
            var beginOfPeriod = int.Parse(elements[1]);
            var endOfPeriodText = elements[2];
            var rate = int.Parse(elements[3]);
            int endOfPeriodParse;
            int? endOfPeriod;
            if (int.TryParse(endOfPeriodText, out endOfPeriodParse))
            {
                endOfPeriod = endOfPeriodParse;
            }
            else
            {
                endOfPeriod = null;
            }
            Entities.Add(new Rate()
            {
                number = number,
                beginingPeriod = beginOfPeriod,
                endPeriod = endOfPeriod,
                rate = rate
            });
        }
    }
}
