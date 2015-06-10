using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace StopWatch.ViewModels
{
    public enum TimerStates { INITIAL, RUNNING, PAUSED, STOPPED };

    public abstract class CountTimer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected System.Diagnostics.Stopwatch stopWatch;
        CoreDispatcher UIDispatcher;
        Task taskHndl;

        TimerStates state;
        public TimerStates State 
        {
            get { return state; }
            set
            {
                state = value;
                NotifyPropertyChanged("State");
            }
        }

        public abstract int Hours { get; }

        public abstract int Minutes { get; }

        public abstract int Seconds { get; }

        public abstract int Milliseconds { get; }

        public CountTimer(CoreDispatcher d)
        {
            State = TimerStates.INITIAL;
            stopWatch = new System.Diagnostics.Stopwatch();
            UIDispatcher = d;
        }

        public virtual void Start()
        {
            stopWatch.Start();
            taskHndl = Task.Run(async() => 
            {
                while (stopWatch.IsRunning)
                {
                    await UIDispatcher.RunIdleAsync((args) =>
                    {
                        NotifyAllPropertyChanged();
                    });
                }
            });
            State = TimerStates.RUNNING;
        }

        public virtual void Stop()
        {
            stopWatch.Reset();
            State = TimerStates.STOPPED;
            NotifyAllPropertyChanged();            
        }

        public virtual void Pause()
        {
            stopWatch.Stop();
            State = TimerStates.PAUSED;
        }

        public virtual void Reset()
        {
            Stop();
        }
        
        public virtual void Resume()
        {
            Start();
        }

        void NotifyPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        void NotifyAllPropertyChanged()
        {
            NotifyPropertyChanged("Hours");
            NotifyPropertyChanged("Minutes");
            NotifyPropertyChanged("Seconds");
            NotifyPropertyChanged("Milliseconds");
        }
    }
}
