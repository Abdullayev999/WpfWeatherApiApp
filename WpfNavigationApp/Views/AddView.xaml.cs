using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfNavigationApp.Messages;
using WpfNavigationApp.ViewModels;

namespace WpfNavigationApp.Views
{
    /// <summary>
    /// Interaction logic for AddView.xaml
    /// </summary>
    public partial class AddView : UserControl
    {
        public AddView()
        {
            InitializeComponent();
        }

        private void MyMap_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var location = MyMap.ViewportPointToLocation(e.GetPosition(MyMap));



            if (DataContext is AddViewModel viewModel)
            {
                viewModel.Latitude = location.Latitude.ToString();
                viewModel.Longitude = location.Longitude.ToString();
            }
            //(DataContext as AddViewModel).Latitude = location.Latitude.ToString();


            //var messenger = App.ServicesSimpleInjector.GetInstance<IMessenger>();
            //var location = MyMap.ViewportPointToLocation(e.GetPosition(MyMap));
            //messenger.Send(new LocationMessage { Latitude = location.Latitude , Longitude = location.Longitude });
        }
    }
}
