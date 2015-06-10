using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace StopWatch.ViewModels
{
    public class CountUpTimer : CountTimer
    {
        public ObservableCollection<TimeSnap> RecTimes { get; set; }

        public override int Hours
        {
            get { return stopWatch.Elapsed.Hours; }
        }

        public override int Minutes
        {
            get { return stopWatch.Elapsed.Minutes; }
        }

        public override int Seconds
        {
            get { return stopWatch.Elapsed.Seconds; }
        }

        public override int Milliseconds
        {
            get { return stopWatch.Elapsed.Milliseconds; }
        }

        public CountUpTimer(CoreDispatcher d) : base(d)
        {
            RecTimes = new ObservableCollection<TimeSnap>();
        }

        public void Lap()
        {
            TimeSpanCustom splitTime = new TimeSpanCustom(0, Hours, Minutes, Seconds, Milliseconds);
            TimeSpanCustom lapTime = new TimeSpanCustom(0, Hours, Minutes, Seconds, Milliseconds);
            if (RecTimes.Count > 0)
            {
                TimeSnap lastTime = RecTimes.Last<TimeSnap>();
                TimeSpanCustom lastSplit = lastTime.SplitTime;
                lapTime = splitTime.Subtract(lastSplit);
            }
            TimeSnap newItem = new TimeSnap
            {
                LapTime = lapTime,
                SplitTime = splitTime
            };
            RecTimes.Add(newItem);
        }

        public override void Stop()
        {
            base.Stop();
            RecTimes.Clear();
        }
    }
}
