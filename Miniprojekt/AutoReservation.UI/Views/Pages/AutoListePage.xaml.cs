using System.Windows.Controls;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.Service.Wcf;
using AutoReservation.UI.ViewModels;

namespace AutoReservation.UI.Views.Pages
{
    /// <summary>
    /// Interaction logic for AutoListePage.xaml
    /// </summary>
    public partial class AutoListePage : Page
    {
        private IAutoReservationService Service;

        private Frame MainFrame;

        public AutoListePage(Frame mainFrame)
        {
            InitializeComponent();
            LoadList();
            MainFrame = mainFrame;
        }

        private void LoadList()
        {
            Service = new AutoReservationService();
            var viewModel = new AutoListeViewModel(Service.GetAutos());
            viewModel.OnAdd += Add;
            viewModel.OnEdit += Edit;
            viewModel.OnDelete += Delete;

            DataContext = viewModel;
        }

        private void Add()
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

        private void Edit(AutoDto auto)
        {
            var viewModel = new AutoEditierenViewModel(auto);
            viewModel.OnSave += a =>
            {
                Service.UpdateAuto(a);
                MainFrame.Navigate(new AutoListePage(MainFrame));
            };

            var editPage = new AutoEditierenPage();
            editPage.DataContext = viewModel;

            MainFrame.Navigate(editPage);
        }

        private void Delete(AutoDto auto)
        {
            Service.DeleteAuto(auto);
            LoadList();
        }
    }
}
