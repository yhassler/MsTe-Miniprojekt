using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoReservation.UI.ViewModels
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string name = null)
        {
            if (Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(name);
            return true;
        }

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}