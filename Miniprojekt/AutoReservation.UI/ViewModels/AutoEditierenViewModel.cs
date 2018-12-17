using System;
using System.Collections.Generic;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.UI.Commands;

namespace AutoReservation.UI.ViewModels
{
    public class AutoEditierenViewModel : BindableBase
    {
        public RelayCommand SaveCommand { get; set; }

        private AutoDto AutoDto { get; set; }

        private string _marke;
        public string Marke
        {
            get { return _marke; }
            set { SetProperty(ref _marke, value); }
        }

        private int _tagestarif;
        public int Tagestarif
        {
            get { return _tagestarif; }
            set { SetProperty(ref _tagestarif, value); }
        }

        private int _basistarif;
        public int Basistarif
        {
            get { return _basistarif; }
            set { SetProperty(ref _basistarif, value); }
        }

        private AutoKlasse _autoKlasse;
        public AutoKlasse AutoKlasse
        {
            get { return _autoKlasse; }
            set
            {
                SetProperty(ref _autoKlasse, value);
                OnPropertyChanged(nameof(HasBasistarif));
            }
        }

        public IList<AutoKlasse> AutoKlassen => new List<AutoKlasse>
        {
            AutoKlasse.Standard,
            AutoKlasse.Mittelklasse,
            AutoKlasse.Luxusklasse
        };

        public bool HasBasistarif => AutoKlasse == AutoKlasse.Luxusklasse;

        public Action<AutoDto> OnSave { get; set; }

        public AutoEditierenViewModel(AutoDto autoDto)
        {
            Load(autoDto);

            SaveCommand = new RelayCommand(Save);
        }

        public void Load(AutoDto autoDto)
        {
            AutoDto = autoDto;
            Marke = autoDto.Marke;
            AutoKlasse = autoDto.AutoKlasse;
            Tagestarif = autoDto.Tagestarif;
            Basistarif = autoDto.Basistarif;
        }

        public void Save()
        {
            AutoDto.Marke = Marke;
            AutoDto.AutoKlasse = AutoKlasse;
            AutoDto.Tagestarif = Tagestarif;
            AutoDto.Basistarif = Basistarif;

            // TODO Validate and store in the database
            OnSave?.Invoke(AutoDto);
        }
    }
}