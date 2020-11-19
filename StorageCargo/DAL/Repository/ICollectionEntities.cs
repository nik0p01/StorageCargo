using StorageCargo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageCargo.DAL.Repository
{

    interface ICollectionEntities
    {
        List<IEntity> Entities { get;  }

        void AddEntity(string[] elements);
    }
}
