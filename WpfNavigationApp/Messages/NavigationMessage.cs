using System;
using System.Collections.Generic;
using System.Text;

namespace WpfNavigationApp.Messages
{
    public class NavigationMessage : IMessage
    {
        public Type viewModelType { get; set; }

    }
}
