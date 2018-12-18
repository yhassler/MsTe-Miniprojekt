using System;
using System.Collections;
using System.Collections.Generic;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.UI.Commands;

namespace AutoReservation.UI.ViewModels
{
    public class ReservationEditierenViewModel : BindableBase
    {
        public RelayCommand SaveCommand { get; set; }

        private ReservationDto ReservationDto { get; set; }

        private AutoDto _auto;
        public AutoDto Auto
        {
            get { return _auto; }
            set { SetProperty(ref _auto, value); }
        }

        public IList<AutoDto> Autos { get; set; }
        
        private KundeDto _kunde;
        public KundeDto Kunde
        {
            get { return _kunde; }
            set { SetProperty(ref _kunde, value); }
        }

        public IList<KundeDto> Kunden { get; set; }

        private DateTime _von;
        public DateTime Von
        {
            get { return _von; }
            set { SetProperty(ref _von, value); }
        }

        private DateTime _bis;
        public DateTime Bis
        {
            get { return _bis; }
            set { SetProperty(ref _bis, value); }
        }

        public Action<ReservationDto> OnSave { get; set; }

        public ReservationEditierenViewModel(ReservationDto reservationDto, IAutoReservationService service)
        {
            Kunden = service.GetKunden();
            Autos = service.GetAutos();
            OnPropertyChanged(nameof(Autos));
            OnPropertyChanged(nameof(Kunden));

            Load(reservationDto);

            SaveCommand = new RelayCommand(Save);
        }

        public void Load(ReservationDto reservationDto)
        {
            ReservationDto = reservationDto;
            Auto = reservationDto.Auto;
            Kunde = reservationDto.Kunde;
            Von = reservationDto.Von;
            Bis = reservationDto.Bis;
        }

        public void Save()
        {
            ReservationDto.Auto = Auto;
            ReservationDto.Kunde = Kunde;
            ReservationDto.Von = Von;
            ReservationDto.Bis = Bis;

            // TODO Validate and store in the database
            OnSave?.Invoke(ReservationDto);
        }
    }
}