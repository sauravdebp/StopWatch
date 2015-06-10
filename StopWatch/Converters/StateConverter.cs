using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using StopWatch.ViewModels;
using Windows.UI.Xaml.Controls;

namespace StopWatch.Converters
{
    public class StateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            TimerStates state = (TimerStates)value;
            SymbolIcon icon = null;
            
            switch (parameter as String)
            {
                case "start_pause":
                    if (state != TimerStates.RUNNING)
                        icon = new SymbolIcon(Symbol.Play);
                    else
                        icon = new SymbolIcon(Symbol.Pause);
                    break;
                case "reset_lap":
                    if (state != TimerStates.RUNNING)
                        icon = new SymbolIcon(Symbol.RepeatAll);
                    else
                        icon = new SymbolIcon(Symbol.Flag);
                    break;
            }
            return icon;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
