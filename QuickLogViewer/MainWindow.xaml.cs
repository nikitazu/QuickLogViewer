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

namespace QuickLogViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Components.AppComponent AppComponent { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            AppComponent = new Components.AppComponent();
            DataContext = new ViewModels.AppViewModel
            {
                LogDays = AppComponent.LogComponent.GetLogs()
            };
        }
    }
}
