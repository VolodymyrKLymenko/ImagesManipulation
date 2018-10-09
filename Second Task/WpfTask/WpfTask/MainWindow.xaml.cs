using LiveCharts;
using LiveCharts.Wpf;
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
using WpfTask.ViewModel;

namespace WpfTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string imgPath = @"D:\Education\University\4_semester\Images Manipulation\Second Task\Data\";
        public SeriesCollection testSeriesCollection;
        public string text = "fsafasfaf";

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ViewModel.ViewModel();
        }
    }
}
