using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public IList<string> SortOptions => new List<string>
        {
            "Vorname",
            "Nachname",
            "Geburtsdatum"
        };

        private string _selectedSortOption;
        public string SelectedSortOption
        {
            get { return _selectedSortOption; }
            set
            {
                switch (value)
                {
                    case "Vorname":
                        Kunden = new ObservableCollection<KundeDto>(Kunden.OrderBy(k => k.Vorname));
                        break;
                    case "Nachname":
                        Kunden = new ObservableCollection<KundeDto>(Kunden.OrderBy(k => k.Nachname));
                        break;
                    case "Geburtsdatum":
                        Kunden = new ObservableCollection<KundeDto>(Kunden.OrderBy(k => k.Geburtsdatum));
                        break;
                }
                SetProperty(ref _selectedSortOption, value);
                OnPropertyChanged(nameof(Kunden));
            }
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