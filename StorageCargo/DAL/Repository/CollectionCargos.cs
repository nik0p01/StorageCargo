using StorageCargo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageCargo.DAL.Repository
{
    class CollectionCargos:ICollectionEntities
    {
        public CollectionCargos()
        {
            Entities = new List<IEntity>();
        }

        public List<IEntity> Entities { get; private set; }

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
