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
using Lynx.ViewModels.StartUp;

namespace Lynx.Views.StartUp
{
    /// <summary>
    /// Logique d'interaction pour LynxStartUp.xaml
    /// </summary>
    public partial class LynxStartUp : UserControl
    {
        public LynxStartUpViewModel ViewModel => DataContext as LynxStartUpViewModel;

        public LynxStartUp()
        {
            InitializeComponent();
        }
    }
}
