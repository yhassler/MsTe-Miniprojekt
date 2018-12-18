using System.Windows.Controls;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.Service.Wcf;
using AutoReservation.UI.ViewModels;

namespace AutoReservation.UI.Views.Pages
{
    /// <summary>
    /// Interaction logic for ReservationListePage.xaml
    /// </summary>
    public partial class ReservationListePage : Page
    {
        private IAutoReservationService Service;

        private Frame MainFrame;

        public ReservationListePage(Frame mainFrame)
        {
            InitializeComponent();
            LoadList();
            MainFrame = mainFrame;
        }

        private void LoadList()
        {
            Service = new AutoReservationService();
            var viewModel = new ReservationListeViewModel(Service.GetReservationen());
            viewModel.OnAdd += Add;
            viewModel.OnEdit += Edit;
            viewModel.OnDelete += Delete;

            DataContext = viewModel;
        }

        private void Add()
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

        private void Edit(ReservationDto reservation)
        {
            var viewModel = new ReservationEditierenViewModel(reservation);
            viewModel.OnSave += r =>
            {
                Service.UpdateReservation(r);
                MainFrame.Navigate(new ReservationListePage(MainFrame));
            };

            var editPage = new ReservationEditierenPage();
            editPage.DataContext = viewModel;

            MainFrame.Navigate(editPage);
        }

        private void Delete(ReservationDto reservation)
        {
            Service.DeleteReservation(reservation);
            LoadList();
        }
    }
}
