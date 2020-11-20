using StorageCargo.DAL.Entities;
using System.Collections.Generic;

namespace StorageCargo.DAL.Repository
{
    public interface IRepository
    {
        ICollection<Cargo> GetCargos();
        ICollection<Rate> GetRates();
    }
}
