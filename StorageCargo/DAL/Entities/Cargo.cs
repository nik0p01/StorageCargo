using System;

namespace StorageCargo.DAL.Entities
{
    public class Cargo : IEntity
    {
        public string name;
        public DateTime arrival;
        public DateTime? leaving;
    }
}
