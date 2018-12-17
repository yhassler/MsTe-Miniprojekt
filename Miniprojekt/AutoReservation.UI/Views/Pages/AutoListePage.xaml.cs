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

namespace AutoReservation.UI
{
    /// <summary>
    /// Interaction logic for AutoListePage.xaml
    /// </summary>
    public partial class AutoListePage : Page
    {
        CollectionViewSource AutoListe;

        public AutoListePage()
        {
            AutoListe = (CollectionViewSource)(this.Resources[AutoListe]);
            DataContext = AutoListe;
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
