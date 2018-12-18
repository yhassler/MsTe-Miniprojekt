using System.Windows;
using AutoReservation.UI.Views.Pages;

namespace AutoReservation.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NaviAutoListe(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AutoListePage());
        }

        private void NaviAutoAdd(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AutoEditierenPage());
        }

        private void NaviKundeListe(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new KundeListePage());
        }

        private void NaviKundeAdd(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new KundeEditierenPage());
        }

        private void NaviReservationListe(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ReservationListePage());
        }

        private void NaviReservationAdd(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ReservationEditierenPage());
        }
    }
}
