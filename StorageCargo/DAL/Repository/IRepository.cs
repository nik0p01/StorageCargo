using StorageCargo.DAL.Entities;
using System.Collections.Generic;

namespace StorageCargo.DAL.Repository
{
    public interface IRepository
    {
        IEnumerable<Cargo> GetCargos();
        IEnumerable<Rate> GetRates();
    }
}
