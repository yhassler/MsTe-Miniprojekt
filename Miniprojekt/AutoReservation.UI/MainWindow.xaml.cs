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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = new MainWindow();
            DataContext = vm;

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
            MainFrame.Navigate(new Kunde);
        }

        private void NaviKundeAdd(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new KundeEditierenPage());
        }

        private void NaviReservationListe(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ReseravationListePage());
        }

        private void NaviReservationAdd(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(ReservationEditierenPage);
        }
    }
