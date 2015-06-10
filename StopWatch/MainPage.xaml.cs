using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using StopWatch.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.UI.Popups;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace StopWatch
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        InfiStopWatch infiWatch;

        public MainPage()
        {
            this.InitializeComponent();
            infiWatch = new InfiStopWatch(Dispatcher);
            infiWatch.UseStopWatch();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            DataContext = infiWatch;
        }

        private void Button_StartPause_Click(object sender, RoutedEventArgs e)
        {            
            if(infiWatch.Timer.State != TimerStates.RUNNING)
            {
                infiWatch.Timer.Start();
            }
            else
            {
                infiWatch.Timer.Pause();
            }
        }

        private void Button_ResetLap(object sender, RoutedEventArgs e)
        {
            if(infiWatch.Timer.State != TimerStates.RUNNING)
            {
                infiWatch.Timer.Reset();
                list_lapTimes.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                TimerStoryboard.Stop();
            }
            else if(infiWatch.Timer.GetType() == typeof(CountUpTimer))
            {
                (infiWatch.Timer as CountUpTimer).Lap();
                if ((infiWatch.Timer as CountUpTimer).RecTimes.Count == 1)
                {
                    list_lapTimes.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    TimerStoryboard.Begin();
                }
                else
                    list_lapTimes.ScrollIntoView((infiWatch.Timer as CountUpTimer).RecTimes.Last(), ScrollIntoViewAlignment.Leading);
            }
        }

        private async void AppBarToggleButton_aboutApp_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog t = new MessageDialog("This application is developed and maintained by InfiGrail. For feedback send email to infigrail@gmail.com");
            await t.ShowAsync();
        }

        private void but_upTimer_Click(object sender, RoutedEventArgs e)
        {
            infiWatch.UseStopWatch();
        }

        private void but_dnTimer_Click(object sender, RoutedEventArgs e)
        {
            infiWatch.UseCountDownTimer();
        }

        //private void AppBarToggleButton_feedback_Click(object sender, RoutedEventArgs e)
        //{
            
        //}
    }
}
