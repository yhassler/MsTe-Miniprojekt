using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoReservation.UI.Views.Pages
{
    /// <summary>
    /// Interaction logic for KundeListePage.xaml
    /// </summary>
    public partial class KundeListePage : Page
    {
        CollectionViewSource ReservationListe;

        public KundeListePage()
        {
            ReservationListe = (CollectionViewSource)(this.Resources[ReservationListe]);
            DataContext = ReservationListe;
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {

        }
    }
}
