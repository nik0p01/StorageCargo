using StorageCargo.BL.BLEntities;
using StorageCargo.DAL.Entities;
using StorageCargo.DAL.Repository;
using System;
using System.Collections.Generic;

namespace StorageCargo.BL
{
    public class CalculationProcessor// : ICalculationProcessor
    {
        private readonly IRepository _repository;
        private readonly DateTime _dateBeginPeriod;
        private readonly DateTime _dateEndPeriod;



        public CalculationProcessor(IRepository repository, DateTime dateStart, DateTime dateEnd)
        {
            _repository = repository;
            _dateBeginPeriod = dateStart;
            _dateEndPeriod = dateEnd;
        }

        public void ProgressiveRate(ICollection<ProgressiveRate> progressiveRates)
        {
            if (_dateBeginPeriod > _dateEndPeriod)
            {
                return;
            }
            var cargos = _repository.GetCargos();
            var rates = _repository.GetRates();
            List<Rate> ratesSort = new List<Rate>(rates);
            ratesSort.Sort((x, y) => x.number.CompareTo(y.number));
            foreach (var cargo in cargos)
            {
                DateTime leaving = IfNullTakeCurrentDate(cargo.leaving);
                DateTime arrival = RoundToDays(cargo.arrival);
                if (NotInRange(arrival, leaving))
                {
                    continue;
                }
                DateTime beginCalc = arrival;// начало расчета без учета периода расчета
                DateTime endCalc;// конец расчета без учета периода расчета
                foreach (var rate in ratesSort)
                {
                    int endPeriod = GetEndPeriodIfNull(arrival, leaving, rate.endPeriod);
                    //Находим дату окончания расчета сдвигая начало расчета на число дней в расчете уменьшенное на 1
                    if (rate.beginingPeriod == 0)
                    {
                        endCalc = beginCalc.AddDays(endPeriod - 1);
                    }
                    else
                    {
                        //разница между Окончание периода и Начало периода и будет число дней в расчете уменьшенное на 1
                        endCalc = beginCalc.AddDays(endPeriod - rate.beginingPeriod);
                    }
                    if (CalculationCrossesPeriod(beginCalc, endCalc)) // расчет попадает в заданныйпериод
                    {
                        DateTime beginCalcPeriod = beginCalc;// начало расчета c учета периода расчета
                        DateTime endCalcPeriod = endCalc;// конец расчета c учета периода расчета
                        if (beginCalc < _dateBeginPeriod)
                        {
                            beginCalcPeriod = _dateBeginPeriod;
                        }
                        if (endCalc > _dateEndPeriod)
                        {
                            endCalcPeriod = _dateEndPeriod;
                        }
                        int storageNumberDays = (int)(endCalcPeriod - beginCalcPeriod).TotalDays + 1;
                        progressiveRates.Add(new ProgressiveRate()
                        {
                            BeginCalculation = beginCalc,
                            EndCalculation = endCalcPeriod,
                            Cargo = cargo.name,
                            DateArrival = cargo.arrival,
                            DateLeaving = cargo.leaving,
                            RateCount = rate.rate,
                            Note = $"Период №{rate.number}",
                            StorageNumberDays = storageNumberDays
                        });
                    }
                    beginCalc = endCalc.AddDays(1);
                    if (beginCalc > _dateEndPeriod)
                    {
                        break;
                    }
                }
            }
        }

        private bool CalculationCrossesPeriod(DateTime beginCalc, DateTime endCalc)
        {
            return !(beginCalc > _dateEndPeriod || endCalc < _dateBeginPeriod);
        }

        private bool NotInRange(DateTime arrival, DateTime leaving)
        {
            return arrival > _dateEndPeriod || leaving < _dateBeginPeriod;
        }

        private static int GetEndPeriodIfNull(DateTime arrival, DateTime leaving, int? rateEndPeriod)
        {
            return (rateEndPeriod is null) ? (leaving - arrival).Days : rateEndPeriod.Value;//в качестве окончания периода возьмем количество дней, которое груз находился на складе
        }

        private static DateTime RoundToDays(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }

        private static DateTime IfNullTakeCurrentDate(DateTime? cargoLeaving)
        {
            DateTime leaving;
            if (cargoLeaving == null)
            {
                leaving = DateTime.Now;
            }
            else
            {
                leaving = cargoLeaving.Value;
            }

            return leaving;
        }
    }
}
