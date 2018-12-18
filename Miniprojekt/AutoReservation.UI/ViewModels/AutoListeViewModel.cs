using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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