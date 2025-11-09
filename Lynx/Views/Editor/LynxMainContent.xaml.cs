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
using System.Windows.Shapes;
using ImaginationGUI.Models;
using ImaginationGUI.ViewModels;
using ImaginationGUI.Views;
using Lynx.Models.InazumaEleven.Games;
using Lynx.ViewModels.Editor;

namespace Lynx.Views.Editor
{
    /// <summary>
    /// Logique d'interaction pour LynxMainContent.xaml
    /// </summary>
    public partial class LynxMainContent : UserControl
    {
        public LynxViewModel ViewModel => DataContext as LynxViewModel;

        public LynxMainContent()
        {
            InitializeComponent();
        }

        private void EditorOptionsTreeView_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            if (DataContext is LynxViewModel viewModel && e.NewValue is EditorOptionItem selectedOption)
            {
                viewModel.SelectedOption = selectedOption;
            }
        }
    }
}
