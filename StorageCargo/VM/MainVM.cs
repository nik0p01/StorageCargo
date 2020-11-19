using Microsoft.Win32;
using StorageCargo.BL;
using StorageCargo.BL.BLEntities;
using StorageCargo.DAL.Repository;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace StorageCargo.VM
{
    public class MainVM : INotifyPropertyChanged
    {


        public MainVM()
        {
            _calculateCommand = new DelegateCommand(ExecuteCalculate);
            _takeFileCommand = new DelegateCommand(ExecuteTakeFile);
            ProgressiveRates = new ObservableCollection<ProgressiveRate>();
        }

        //TODO: Delete it
        private string _excelFilePatch = @"c:\Temp\Test.xlsx";
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
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        DelegateCommand _calculateCommand;
        public DelegateCommand CalculateCommand
        {
            get
            {
                return _calculateCommand;
            }
        }

        DelegateCommand _takeFileCommand;
        public DelegateCommand TakeFileCommand
        {
            get
            {
                return _takeFileCommand;
            }
        }


        private void ExecuteCalculate(object obj)
        {
            CalculationProcessor calculationProcessor = new CalculationProcessor(new RepositoryExcel(_excelFilePatch), _dateStart, _dateEnd);
            calculationProcessor.ProgressiveRate(ProgressiveRates);
        }


        private DateTime _dateStart = DateTime.Now;
        public DateTime DateStart
        {
            get
            {
                return _dateStart;
            }
            set
            {
                _dateStart = value;
                OnPropertyChanged(nameof(DateStart));
            }
        }

        private DateTime _dateEnd = DateTime.Now;
        public DateTime DateEnd
        {
            get
            {
                return _dateEnd;
            }
            set
            {
                _dateEnd = value;
                OnPropertyChanged(nameof(DateEnd));
            }

        }
    }
}
