using StorageCargo.BL.BLEntities;
using StorageCargo.DAL.Entities;
using StorageCargo.DAL.Repository;
using System;
using System.Collections.Generic;

namespace StorageCargo.BL
{
    public class CalculationProcessor
    {
        private readonly IRepository _repository;
        private readonly DateTime _dateBeginRange;
        private readonly DateTime _dateEndRange;
        private ICollection<ProgressiveRate> _progressiveRates;

        public CalculationProcessor(IRepository repository, ICollection<ProgressiveRate> progressiveRates, DateTime dateStart, DateTime dateEnd)
        {
            _repository = repository;
            _dateBeginRange = dateStart;
            _dateEndRange = dateEnd;
            _progressiveRates = progressiveRates;
        }


        public void CalculateProgressiveRate()
        {
            if (_dateBeginRange > _dateEndRange)
            {
                return;
            }
            var cargos = _repository.GetCargos();
            var rates = _repository.GetRates();
            List<Rate> ratesSort = new List<Rate>(rates);
            ratesSort.Sort((x, y) => x.number.CompareTo(y.number));
            foreach (var cargo in cargos)
            {
                DateTime arrivalCargo = RoundToDays(cargo.arrival);
                DateTime leavingCargo = IfNullTakeCurrentDate(cargo.leaving);
                if (NotInPeriod(arrivalCargo, leavingCargo))
                {
                    continue;
                }
                DateTime beginCalc = arrivalCargo;// начало расчета без учета диапазона расчета
                DateTime endCalc;// конец расчета без учета диапазона расчета
                foreach (var rate in ratesSort)
                {
                    int endPeriod = GetEndPeriodIfNull(arrivalCargo, leavingCargo, rate.endPeriod);//
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
                    if (endCalc > leavingCargo)
                    {
                        endCalc = leavingCargo;
                    }

                    if (CalculationCrossesRange(beginCalc, endCalc)) // расчет попадает в заданный диапазон
                    {
                        DateTime beginCalcPeriod = beginCalc;// начало расчета c учета диапазона расчета
                        DateTime endCalcPeriod = endCalc;// конец расчета c учета диапазона расчета
                        if (beginCalc < _dateBeginRange)
                        {
                            beginCalcPeriod = _dateBeginRange;
                        }
                        if (endCalc > _dateEndRange)
                        {
                            endCalcPeriod = _dateEndRange;
                        }
                        int storageNumberDays = (int)(endCalcPeriod - beginCalcPeriod).TotalDays + 1;
                        _progressiveRates.Add(new ProgressiveRate()
                        {
                            BeginCalculation = beginCalcPeriod,
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
                    if (beginCalc > _dateEndRange)
                    {
                        break;
                    }
                    if (beginCalc > leavingCargo)
                    {
                        break;
                    }
                }
            }
        }

        private bool CalculationCrossesRange(DateTime beginCalc, DateTime endCalc)
        {
            return !(beginCalc > _dateEndRange || endCalc < _dateBeginRange);
        }

        private bool NotInPeriod(DateTime arrival, DateTime leaving)
        {
            return arrival > _dateEndRange || leaving < _dateBeginRange;
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
