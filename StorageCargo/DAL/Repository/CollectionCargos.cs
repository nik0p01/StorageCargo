using StorageCargo.DAL.Entities;
using System;
using System.Collections.Generic;

namespace StorageCargo.DAL.Repository
{
    /// <summary>
    /// Класс представляющий таблицу Грузов (коллекция объектов Cargo)
    /// </summary>
    class CollectionCargos : ICollectionEntities
    {
        public CollectionCargos()
        {
            Entities = new List<IEntity>();
        }

        public List<IEntity> Entities { get; private set; }
        /// <summary>
        /// Метод добавления строки в таблицу (добавления элемента Cargo)
        /// </summary>
        /// <param name="elements">Массив строк представляющий элементы строки таблицы Груз</param>
        public void AddEntity(string[] elements)
        {
            var name = elements[0];
            var arrival = DateTime.Parse(elements[1]);
            DateTime leavingParse;
            DateTime? leaving;
            if (DateTime.TryParse(elements[2], out leavingParse))
            {
                leaving = leavingParse;
            }
            else
            {
                leaving = null;
            }

            Entities.Add(new Cargo() { name = name, arrival = arrival, leaving = leaving });
        }
    }
}
