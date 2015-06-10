using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace StopWatch.ViewModels
{
    public class CountDownTimer : CountTimer
    {
        public TimeSpanCustom SetTime { get; set; }

        public override int Hours 
        { 
            get { return RemainingTime().Hours; } 
        }

        public override int Minutes
        {
            get { return RemainingTime().Minutes; }
        }

        public override int Seconds
        {
            get { return RemainingTime().Seconds; }
        }

        public override int Milliseconds
        {
            get { return 0; }
        }

        public CountDownTimer(CoreDispatcher d) : base(d) { }

        public CountDownTimer(TimeSpanCustom setTime, CoreDispatcher d) : base(d)
        {
            SetTime = setTime;
        }

        TimeSpanCustom RemainingTime()
        {
            TimeSpanCustom remTime = SetTime.Subtract(new TimeSpanCustom(0, stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes, stopWatch.Elapsed.Seconds, 0));
            if ((remTime.Hours | remTime.Minutes | remTime.Seconds) == 0)
            {
                Stop();
            }
            return remTime;
        }

        public void Start(TimeSpanCustom ts)
        {
            SetTime = ts;
            base.Start();
        }
    }
}
