using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Lynx.ViewModels.Panels;

namespace Lynx.Views.Panels
{
    public partial class PanelCharabase : UserControl
    {
        public PanelCharabaseViewModel ViewModel => DataContext as PanelCharabaseViewModel;

        public PanelCharabase()
        {
            InitializeComponent();
        }

        private void CharabaseListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Handle double-click if needed
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == "Search...")
            {
                SearchTextBox.Text = "";
            }
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                SearchTextBox.Text = "Search...";
            }
        }
    }
}