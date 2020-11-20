using StorageCargo.DAL.Entities;
using System.Collections.Generic;

namespace StorageCargo.DAL.Repository
{

    interface ICollectionEntities
    {
        List<IEntity> Entities { get; }

        void AddEntity(string[] elements);
    }
}
