using AutoReservation.UI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.UI.ViewModels
{
    public class UpdateConcurrencyWindowViewModel : BindableBase
    {

        public RelayCommand Cancel { get; set; }

        public RelayCommand Reload { get; set; }

        public RelayCommand Overwrite { get; set; }

    }
}
