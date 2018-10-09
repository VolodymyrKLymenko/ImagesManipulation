using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Win32;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using LiveCharts;
using System.Drawing;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using WpfTask.CommonExtensions;

namespace WpfTask.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        private SeriesCollection testSeriesCollection;
        public SeriesCollection TestSeriesCollection
        {
            get { return testSeriesCollection; }
            set
            {
                testSeriesCollection = value;
                OnPropertyChanged("TestSeriesCollection");
            }
        }

        private SeriesCollection workCollection;
        public SeriesCollection WorkCollection
        {
            get { return workCollection; }
            set
            {
                workCollection = value;
                OnPropertyChanged("WorkCollection");
            }
        }

        private Bitmap bitmap;
        public Bitmap BitmapImg
        {
            get { return bitmap; }
            set
            {
                bitmap = value;
                OnPropertyChanged("BitmapImg");

            }
        }

        private Bitmap ekvalizedBitmap;

        private RelayCommand openCmd;
        public RelayCommand OpenCmd
        {
            get
            {
                return openCmd ??
                    (openCmd = new RelayCommand((obj) =>
                    {
                        OpenFileDialog op = new OpenFileDialog();
                        op.Title = "Select a picture";
                        op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                          "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                          "Portable Network Graphic (*.png)|*.png";
                        if (op.ShowDialog() == true)
                        {
                            BitmapImg = new Bitmap(op.FileName);

                            var reHisto = HistogramCalc.GetHistogram(BitmapImg, HistogramCalc.ColorChannel.Red);
                            var values = new ChartValues<ObservableValue>();

                            foreach (var item in reHisto)
                            {
                                values.Add(new ObservableValue(item));
                            }

                            TestSeriesCollection = new SeriesCollection
                            {
                                new ColumnSeries
                                {
                                    Values = values
                                }
                            };
                        }
                    },
                        (obj) =>
                        {
                            return true;
                        })
                    );
            }
        }

        private RelayCommand svaeCmd;
        public RelayCommand SaveCmd
        {
            get
            {
                return svaeCmd ??
                    (svaeCmd = new RelayCommand((obj) =>
                    {
                        SaveFileDialog fileDialog = new SaveFileDialog();

                        if (fileDialog.ShowDialog() == true)
                        {
                            ImageExtension.SaveBitmap(ekvalizedBitmap, fileDialog.FileName);
                        }
                    },
                        (obj) =>
                        {
                            return ekvalizedBitmap != null;
                        })
                    );
            }
        }

        private RelayCommand ekvalizeCmd;
        public RelayCommand EkvalizeCmd
        {
            get
            {
                return ekvalizeCmd ??
                    (ekvalizeCmd = new RelayCommand((obj) =>
                    {
                        ekvalizedBitmap = EcvalizeCalc.equalizing(BitmapImg);
                        var reHisto = HistogramCalc.GetHistogram(ekvalizedBitmap, HistogramCalc.ColorChannel.Red);

                        var values = new ChartValues<ObservableValue>();

                        foreach (var item in reHisto)
                        {
                            values.Add(new ObservableValue(item));
                        }

                        WorkCollection = new SeriesCollection
                            {
                                new ColumnSeries
                                {
                                    Values = values
                                }
                            };
                    },
                    (obj) =>
                    {
                        return bitmap != null;
                    })
                );
            }
        }

        public ViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
