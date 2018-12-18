using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.UI.Commands;

namespace AutoReservation.UI.ViewModels
{
    public class KundeListeViewModel : BindableBase
    {
        public RelayCommand AddCommand { get; set; }

        public RelayCommand EditCommand { get; set; }

        public RelayCommand DeleteCommand { get; set; }

        private ObservableCollection<KundeDto> _kunden;
        public ObservableCollection<KundeDto> Kunden
        {
            get { return _kunden; }
            set { SetProperty(ref _kunden, value); }
        }

        private KundeDto _selectedKunde;
        public KundeDto SelectedKunde
        {
            get { return _selectedKunde; }
            set { SetProperty(ref _selectedKunde, value); }
        }

        public Action OnAdd { get; set; }

        public Action<KundeDto> OnEdit { get; set; }

        public Action<KundeDto> OnDelete { get; set; }

        public KundeListeViewModel(IList<KundeDto> kundeDtoList)
        {
            Kunden = new ObservableCollection<KundeDto>(kundeDtoList);

            AddCommand = new RelayCommand(() => OnAdd?.Invoke());
            EditCommand = new RelayCommand(() => OnEdit?.Invoke(_selectedKunde));
            DeleteCommand = new RelayCommand(() => OnDelete?.Invoke(_selectedKunde));
        }
    }
}