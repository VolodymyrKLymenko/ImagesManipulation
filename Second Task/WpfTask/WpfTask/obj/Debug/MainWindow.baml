   M S B A M L   `   `   `  ����B  >WpfTask, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfTaskWpfTask  5   	   U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5   	   P LLoadingIndicators.WPF, Version=0.0.1.0, Culture=neutral, PublicKeyToken=null\Bclr-namespace:LoadingIndicators.WPF;assembly=LoadingIndicators.WPFLoadingIndicators.WPF 5   	       WpfTask.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfTask     5   	   Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5   	   UloadinBclr-namespace:LoadingIndicators.WPF;assembly=LoadingIndicators.WPF     5   	     � Title$  
MainWindow��5	   	   $��615.26��6   $	��800��6,    � 	Resources 5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $T Npack://application:,,,/LoadingIndicators.WPF;component/Styles/LoadingWave.xaml?�6%   �� 5      $Y Spack://application:,,,/LoadingIndicators.WPF;component/Styles/LoadingThreeDots.xaml?�6%   �� 5      $Y Spack://application:,,,/LoadingIndicators.WPF;component/Styles/LoadingFlipPlane.xaml?�6%   �� 5      $U Opack://application:,,,/LoadingIndicators.WPF;component/Styles/LoadingPulse.xaml?�6%   �� 5      $\ Vpack://application:,,,/LoadingIndicators.WPF;component/Styles/LoadingDoubleBounce.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��0.45* �6   �� 5      $��10 �6   �� 5      $��0.45* �6   .+�5   
   o� -   5         o�Name$
 menu��5      $��5,0,0,0q�6   
�����w�.@�5      m� $��	File menu��6   
�������$��20(�6=   $��0,0,7,0q�6K   
���   �.@�5      m� $
��Open��6   
�������$��20(�6X    m� Command 6)   �� *	OpenCmd+6h   m� 5      $
��Save��6   
�������$��20(�6Y    6*   �� *	SaveCmd+6i   5       m� 5!      $��Chanel��6   
�������$��20(�6:   $��0,0,7,0q�6H   
���   �.@�5"      m� -   5        $	��RED��5"      
������� m� IsCheckable . $��20(�6�    m� 	IsChecked 6h   �� *IsRedSelected+	 �� Mode$	 OneWay=�6�   m� 5#      -   5        $��GREEN��5#      
������� . $��20(�6�    6j   �� *IsGreenSelected+$	 OneWay=�6�   m� 5$      -   5        $
��BLUE��5$      
������� . $��20(�6�    6g   �� *IsBlueSelected+$	 OneWay=�6�   m� 5%      -   5        $��All channel for mask��5%      
������� . $��20(�6�    6w   �� *IsAllSelected+$	 OneWay=�6�   5&      m� 5'      $��Ekvalize��6   $��0,0,7,0q�6J   
�������$��20(�6o    6*   �� *EkvalizeCmd+6   m� 5(      $
��Mask��6   
�������$��20(�68   $��0,0,7,0q�6F   
���   �.@�5)      m� -   5        $��Sobel��5)      
�������$��20(�6O   6_   m� 5*      -   5        $��Privet��5*      
�������$��20(�6Q   6a   m� 5+      -   5        $��Roberts��5+      
�������$��20(�6S   6c   5,      5-      � 5.   
   $��1��6   .+�5/      #  LiveCharts.Wpf.CartesianChart  $��0��6I   &   LiveCharts.Wpf.Charts.Base.Chart
   Series
 6!   �� *TestSeriesCollection+6I   50      �� 51   
   $��2��6   $��5��6$   $��Stretch=�6/   � 52   
   $��3��6   .+�53        $��0��6C   
 6!   �� *WorkCollection+6C   54      55      56      