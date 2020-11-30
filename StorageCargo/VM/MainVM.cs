using StorageCargo.BL;
using StorageCargo.BL.BLEntities;
using StorageCargo.DAL.Repository;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;

namespace StorageCargo.VM
{
    /// <summary>
    /// Класс Представление - Модель (VM)  из архитектуры MVVM
    /// </summary>
    public class MainVM : INotifyPropertyChanged
    {
        readonly DelegateCommand _calculateCommand;
        readonly DelegateCommand _takeFileCommand;
        private DateTime _dateStart = new DateTime(2017, 10, 1);
        private DateTime _dateEnd = new DateTime(2017, 10, 15);
        private string _excelFilePatch = @"c:\Temp\Test.xlsx";

        public MainVM()
        {
            _calculateCommand = new DelegateCommand(ExecuteCalculate,(b=>_dateStart<=_dateEnd  ));//Отключение кнопки Расчета при некорректных датах
            _takeFileCommand = new DelegateCommand(ExecuteTakeFile);
            ProgressiveRates = new ObservableCollection<ProgressiveRate>();
        }

    
       /// <summary>
       /// Обраблотчик нажатия кнопки выбора файла Excel 
       /// </summary>
       /// <param name="obj"></param>
        private void ExecuteTakeFile(object obj)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Файлы xls|*.xls|Файлы xlsx|*.xlsx";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                _excelFilePatch = fileDialog.FileName;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ProgressiveRate> ProgressiveRates { get; set; }

        /// <summary>
        /// Метод позволяющий представлению отслеживать изменение данных в ПредставлениеМодели
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DelegateCommand CalculateCommand
        {
            get
            {
                return _calculateCommand;
            }
        }

        public DelegateCommand TakeFileCommand
        {
            get
            {
                return _takeFileCommand;
            }
        }

        public DateTime DateStart
        {
            get
            {
                return _dateStart;
            }
            set
            {
                _dateStart = value;
                _calculateCommand.RaiseCanExecuteChanged();
            }
        }

        public DateTime DateEnd
        {
            get
            {
                return _dateEnd;
            }
            set
            {
                _dateEnd = value;
                _calculateCommand.RaiseCanExecuteChanged();

            }

        }

        /// <summary>
        /// Обработчик нажатия кнопки расчета 
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteCalculate(object obj)
        {
            ProgressiveRates.Clear();
            CalculationProcessor calculationProcessor = new CalculationProcessor(new RepositoryExcel(_excelFilePatch), ProgressiveRates, _dateStart, _dateEnd);
            calculationProcessor.CalculateProgressiveRate();
        }
    }
}
