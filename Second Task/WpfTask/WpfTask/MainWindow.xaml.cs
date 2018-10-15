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
            DataContext = new ViewModel.ViewModel();

            InitializeComponent();
        }

        private void ChooseBlueChanel(object sender, RoutedEventArgs e)
        {
            ((ViewModel.ViewModel)DataContext).SetChannel(0);
        }

        private void ChooseGreenChanel(object sender, RoutedEventArgs e)
        {
            ((ViewModel.ViewModel)DataContext).SetChannel(1);
        }

        private void ChooseRedChanel(object sender, RoutedEventArgs e)
        {
            ((ViewModel.ViewModel)DataContext).SetChannel(2);
        }

        private void ChooseAllChanel(object sender, RoutedEventArgs e)
        {
            ((ViewModel.ViewModel)DataContext).SetChannel(4);
        }

        private void SobelVerticalMask(object sender, RoutedEventArgs e)
        {
            ((ViewModel.ViewModel)DataContext).ApplyMask(MaskApplier.sobelMaskVertical);
        }
        private void SobelHorizontalMask(object sender, RoutedEventArgs e)
        {
            ((ViewModel.ViewModel)DataContext).ApplyMask(MaskApplier.sobelMaskHorizontal);
        }

        private void PrivetVerticalMask(object sender, RoutedEventArgs e)
        {
            ((ViewModel.ViewModel)DataContext).ApplyMask(MaskApplier.privetMaskVertical);
        }
        private void PrivetHorizontalMask(object sender, RoutedEventArgs e)
        {
            ((ViewModel.ViewModel)DataContext).ApplyMask(MaskApplier.privetMaskHorizontal);
        }

        private void RobertsVerticalMask(object sender, RoutedEventArgs e)
        {
            ((ViewModel.ViewModel)DataContext).ApplyMask(MaskApplier.robertsMaskVertical);
        }
        private void RobertsHorizontalMask(object sender, RoutedEventArgs e)
        {
            ((ViewModel.ViewModel)DataContext).ApplyMask(MaskApplier.robertsMaskHorizontal);
        }

        private void BlurringMask(object sender, RoutedEventArgs e)
        {
            ((ViewModel.ViewModel)DataContext).ApplyMask(MaskApplier.forTest);
        }
    }
}
