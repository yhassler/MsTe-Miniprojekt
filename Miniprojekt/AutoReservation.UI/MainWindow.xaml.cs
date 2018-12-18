using System.Windows;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.Service.Wcf;
using AutoReservation.UI.ViewModels;
using AutoReservation.UI.Views.Pages;

namespace AutoReservation.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IAutoReservationService Service;

        public MainWindow()
        {
            InitializeComponent();
            Service = new AutoReservationService();
        }

        private void NaviAutoListe(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AutoListePage(MainFrame));
        }

        private void NaviAutoAdd(object sender, RoutedEventArgs e)
        {
            var viewModel = new AutoEditierenViewModel(new AutoDto());
            viewModel.OnSave += a =>
            {
                Service.InsertAuto(a);
                MainFrame.Navigate(new AutoListePage(MainFrame));
            };

            var editPage = new AutoEditierenPage();
            editPage.DataContext = viewModel;

            MainFrame.Navigate(editPage);
        }

        private void NaviKundeListe(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new KundeListePage(MainFrame));
        }

        private void NaviKundeAdd(object sender, RoutedEventArgs e)
        {
            var viewModel = new KundeEditierenViewModel(new KundeDto());
            viewModel.OnSave += k =>
            {
                Service.InsertKunde(k);
                MainFrame.Navigate(new KundeListePage(MainFrame));
            };

            var editPage = new KundeEditierenPage();
            editPage.DataContext = viewModel;

            MainFrame.Navigate(editPage);
        }

        private void NaviReservationListe(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ReservationListePage(MainFrame));
        }

        private void NaviReservationAdd(object sender, RoutedEventArgs e)
        {
            var viewModel = new ReservationEditierenViewModel(new ReservationDto());
            viewModel.OnSave += r =>
            {
                Service.InsertReservation(r);
                MainFrame.Navigate(new ReservationListePage(MainFrame));
            };

            var editPage = new ReservationEditierenPage();
            editPage.DataContext = viewModel;

            MainFrame.Navigate(editPage);
        }
    }
}
