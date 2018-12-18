using System.Windows.Controls;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.Service.Wcf;
using AutoReservation.UI.ViewModels;

namespace AutoReservation.UI.Views.Pages
{
    /// <summary>
    /// Interaction logic for KundeListePage.xaml
    /// </summary>
    public partial class KundeListePage : Page
    {
        private IAutoReservationService Service;

        private Frame MainFrame;

        public KundeListePage(Frame mainFrame)
        {
            InitializeComponent();
            LoadList();
            MainFrame = mainFrame;
        }

        private void LoadList()
        {
            Service = new AutoReservationService();
            var viewModel = new KundeListeViewModel(Service.GetKunden());
            viewModel.OnAdd += Add;
            viewModel.OnEdit += Edit;
            viewModel.OnDelete += Delete;

            DataContext = viewModel;
        }

        private void Add()
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

        private void Edit(KundeDto kunde)
        {
            var viewModel = new KundeEditierenViewModel(kunde);
            viewModel.OnSave += k =>
            {
                Service.UpdateKunde(k);
                MainFrame.Navigate(new KundeListePage(MainFrame));
            };

            var editPage = new KundeEditierenPage();
            editPage.DataContext = viewModel;

            MainFrame.Navigate(editPage);
        }

        private void Delete(KundeDto kunde)
        {
            Service.DeleteKunde(kunde);
            LoadList();
        }
    }
}
