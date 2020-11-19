using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StorageCargo.BL.BLEntities
{
    public class ProgressiveRate : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private string _cargo;
        private DateTime _dateArrival;
        private DateTime? _dateLeaving;
        private DateTime _beginCalculation;
        private DateTime _endCalculation;
        private int _storageNumber;
        private int _rate;
        private string _note;

        public string Note
        {
            get { return _note; }
            set
            {
                _note = value;
                OnPropertyChanged("Note");
            }
        }

        public int Rate
        {
            get { return _rate; }
            set
            {
                _rate = value;
                OnPropertyChanged("Rate");
            }
        }

        public int StorageNumber
        {
            get { return _storageNumber; }
            set
            {
                _storageNumber = value;
                OnPropertyChanged("StorageNumber");
            }
        }

        public DateTime EndCalculation
        {
            get { return _endCalculation; }
            set
            {
                _endCalculation = value;
                OnPropertyChanged("EndCalculation");
            }
        }

        public DateTime BeginCalculation
        {
            get { return _beginCalculation; }
            set
            {
                _beginCalculation = value;
                OnPropertyChanged("BeginCalculation");
            }
        }

        public string Cargo
        {
            get { return _cargo; }
            set
            {
                _cargo = value;
                OnPropertyChanged("Cargo");
            }
        }


        public DateTime? DateLeaving
        {
            get { return _dateLeaving; }
            set
            {
                _dateLeaving = value;
                OnPropertyChanged("DateLeaving");
            }
        }

        public DateTime DateArrival
        {
            get { return _dateArrival; }
            set
            {
                _dateArrival = value;
                OnPropertyChanged("DateArrival");
            }
        }

    }
}
