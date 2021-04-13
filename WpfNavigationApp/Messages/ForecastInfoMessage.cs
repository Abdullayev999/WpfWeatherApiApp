using System;
using System.Collections.Generic;
using System.Text;
using WpfNavigationApp.Models;

namespace WpfNavigationApp.Messages
{
    class ForecastDetailsMessage : IMessage
    {
        public Forecast Forecast { get; set; }
    }
}
