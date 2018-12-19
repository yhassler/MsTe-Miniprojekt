using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.UI.Commands;

namespace AutoReservation.UI.ViewModels
{
    public class AutoListeViewModel : BindableBase
    {
        public RelayCommand AddCommand { get; set; }

        public RelayCommand EditCommand { get; set; }

        public RelayCommand DeleteCommand { get; set; }

        private ObservableCollection<AutoDto> _autos;
        public ObservableCollection<AutoDto> Autos
        {
            get { return _autos; }
            set { SetProperty(ref _autos, value); }
        }

        private AutoDto _selectedAuto;
        public AutoDto SelectedAuto
        {
            get { return _selectedAuto; }
            set { SetProperty(ref _selectedAuto, value); }
        }

        public IList<string> SortOptions => new List<string>
        {
            "Marke",
            "Tagestarif",
            "Autoklasse"
        };

        private string _selectedSortOption;
        public string SelectedSortOption
        {
            get { return _selectedSortOption; }
            set
            {
                switch (value)
                {
                    case "Marke":
                        Autos = new ObservableCollection<AutoDto>(Autos.OrderBy(a => a.Marke));
                        break;
                    case "Tagestarif":
                        Autos = new ObservableCollection<AutoDto>(Autos.OrderBy(a => a.Tagestarif));
                        break;
                    case "Autoklasse":
                        Autos = new ObservableCollection<AutoDto>(Autos.OrderBy(a => a.AutoKlasse));
                        break;
                }
                SetProperty(ref _selectedSortOption, value);
                OnPropertyChanged(nameof(Autos));
            }
        }

        public Action OnAdd { get; set; }

        public Action<AutoDto> OnEdit { get; set; }

        public Action<AutoDto> OnDelete { get; set; }

        public AutoListeViewModel(IList<AutoDto> autoDtoList)
        {
            Autos = new ObservableCollection<AutoDto>(autoDtoList);

            AddCommand = new RelayCommand(() => OnAdd?.Invoke());
            EditCommand = new RelayCommand(() => OnEdit?.Invoke(_selectedAuto));
            DeleteCommand = new RelayCommand(() => OnDelete?.Invoke(_selectedAuto));
        }
    }
}