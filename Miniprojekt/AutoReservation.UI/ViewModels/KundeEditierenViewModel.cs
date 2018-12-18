using System;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.UI.Commands;

namespace AutoReservation.UI.ViewModels
{
    public class KundeEditierenViewModel : BindableBase
    {
        public RelayCommand SaveCommand { get; set; }

        private KundeDto KundeDto { get; set; }

        private string _vorname;
        public string Vorname
        {
            get { return _vorname; }
            set { SetProperty(ref _vorname, value); }
        }

        private string _nachname;
        public string Nachname
        {
            get { return _nachname; }
            set { SetProperty(ref _nachname, value); }
        }

        private DateTime _geburtsdatum;
        public DateTime Geburtsdatum
        {
            get { return _geburtsdatum; }
            set { SetProperty(ref _geburtsdatum, value); }
        }

        public Action<KundeDto> OnSave { get; set; }

        public KundeEditierenViewModel(KundeDto kundeDto)
        {
            Load(kundeDto);

            SaveCommand = new RelayCommand(Save);
        }

        public void Load(KundeDto kundeDto)
        {
            KundeDto = kundeDto;
            Vorname = kundeDto.Vorname;
            Nachname = kundeDto.Nachname;
            Geburtsdatum = kundeDto.Geburtsdatum;
        }

        public void Save()
        {
            KundeDto.Vorname = Vorname;
            KundeDto.Nachname = Nachname;
            KundeDto.Geburtsdatum = Geburtsdatum;

            // TODO Validate and store in the database
            OnSave?.Invoke(KundeDto);
        }
    }
}