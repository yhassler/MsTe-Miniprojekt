using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.UI.Commands;

namespace AutoReservation.UI.ViewModels
{
    public class ReservationListeViewModel : BindableBase
    {
        public RelayCommand AddCommand { get; set; }

        public RelayCommand EditCommand { get; set; }

        public RelayCommand DeleteCommand { get; set; }

        private ObservableCollection<ReservationDto> _allReservationen;

        private ObservableCollection<ReservationDto> _reservationen;
        public ObservableCollection<ReservationDto> Reservationen
        {
            get { return _reservationen; }
            set { SetProperty(ref _reservationen, value); }
        }

        private ReservationDto _selectedReservation;
        public ReservationDto SelectedReservation
        {
            get { return _selectedReservation; }
            set { SetProperty(ref _selectedReservation, value); }
        }

        private bool _showAll;
        public bool ShowAll
        {
            get { return _showAll; }
            set
            {
                if (value)
                {
                    Reservationen = _allReservationen;
                }
                else
                {
                    Reservationen = new ObservableCollection<ReservationDto>(_allReservationen
                        .Where(r => r.Von < DateTime.Now && r.Bis > DateTime.Now));
                }
                SetProperty(ref _showAll, value);
            }
        }

        public Action OnAdd { get; set; }

        public Action<ReservationDto> OnEdit { get; set; }

        public Action<ReservationDto> OnDelete { get; set; }

        public ReservationListeViewModel(IList<ReservationDto> reservationDtoList)
        {
            Reservationen = new ObservableCollection<ReservationDto>(reservationDtoList);
            _allReservationen = Reservationen;

            AddCommand = new RelayCommand(() => OnAdd?.Invoke());
            EditCommand = new RelayCommand(() => OnEdit?.Invoke(_selectedReservation));
            DeleteCommand = new RelayCommand(() => OnDelete?.Invoke(_selectedReservation));
        }
    }
}