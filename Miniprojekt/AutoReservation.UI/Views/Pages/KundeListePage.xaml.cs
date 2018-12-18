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
        IAutoReservationService Service;

        public KundeListePage()
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
            viewModel.OnSave += k => Service.InsertKunde(k);

            var editPage = new KundeEditierenPage();
            editPage.DataContext = viewModel;

            // TODO Show
        }

        private void Edit(KundeDto kunde)
        {
            var viewModel = new KundeEditierenViewModel(kunde);
            viewModel.OnSave += k => Service.UpdateKunde(k);

            var editPage = new KundeEditierenPage();
            editPage.DataContext = viewModel;

            // TODO Show
        }

        private void Delete(KundeDto kunde)
        {
            Service.DeleteKunde(kunde);
        }
    }
}
