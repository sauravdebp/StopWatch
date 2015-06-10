using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace StopWatch.ViewModels
{
    public class InfiStopWatch : INotifyPropertyChanged
    {
        CountDownTimer dnTimer;
        CountUpTimer upTimer;

        CountTimer _timer;
        public CountTimer Timer
        { 
            get
            {
                return _timer;
            }
            set
            {
                if(value != _timer)
                {
                    _timer = value;
                    NotifyPropertyChanged("Timer");
                }
            }
        }

        public InfiStopWatch(CoreDispatcher d)
        {
            upTimer = new CountUpTimer(d);
            dnTimer = new CountDownTimer(new TimeSpanCustom(0, 0, 0, 10, 0) ,d);
        }

        public void UseStopWatch()
        {
            Timer = upTimer;
        }

        public void UseCountDownTimer()
        {
            Timer = dnTimer;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(null, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
