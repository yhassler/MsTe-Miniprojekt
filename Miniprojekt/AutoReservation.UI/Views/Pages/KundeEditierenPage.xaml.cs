using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutoReservation.UI
{
    /// <summary>
    /// Interaction logic for KundeEditierenPage.xaml
    /// </summary>
    public partial class KundeEditierenPage : Page
    {
        public KundeEditierenPage()
        {
            InitializeComponent();
        }

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }
    }
}
