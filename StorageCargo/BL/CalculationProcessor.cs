using StorageCargo.BL.BLEntities;
using StorageCargo.DAL.Repository;
using System;
using System.Collections.Generic;

namespace StorageCargo.BL
{
    public class CalculationProcessor : ICalculationProcessor
    {
        IRepository _repository;
        private DateTime dateStart;
        private DateTime dateEnd;

        public CalculationProcessor(IRepository repository)
        {
            _repository = repository;
        }

        public CalculationProcessor(IRepository repository, DateTime dateStart, DateTime dateEnd) : this(repository)
        {
            this.dateStart = dateStart;
            this.dateEnd = dateEnd;
        }

        public void ProgressiveRate(ICollection<ProgressiveRate> progressiveRates)
        {
            var cargos = _repository.GetCargos();
            var rate = _repository.GetRates();


            foreach (var cargo in cargos)
            {


                progressiveRates.Add(new ProgressiveRate() { Cargo = cargo.name, DateArrival = cargo.arrival, DateLeaving = cargo.leaving });
            }
        }
    }
}
