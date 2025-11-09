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

        private void CharabaseTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is CharabaseTreeNode node && node.Charabase != null)
            {
                ViewModel?.SelectCharabase(node.Charabase);
            }
        }

        private void CharabaseTreeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Handle double-click if needed
        }
    }
}