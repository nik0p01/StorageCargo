using StorageCargo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageCargo.DAL.Repository
{
    class CollectionRates:ICollectionEntities
    {
        public CollectionRates()
        {
            Entities = new List<IEntity>();
        }

        public List<IEntity> Entities { get; private set; }

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
            Entities.Add(new Rate() { number = number, beginingPeriod = beginOfPeriod, endPeriod = endOfPeriod, rate = rate });
        }
    }
}
