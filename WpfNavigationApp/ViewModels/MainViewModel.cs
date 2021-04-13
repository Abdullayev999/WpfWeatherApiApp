using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using WpfNavigationApp.Messages;

namespace WpfNavigationApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
       private  ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel { get => currentViewModel; set => Set(ref currentViewModel, value); }

        public MainViewModel(IMessenger messenger)
        {
            messenger.Register<NavigationMessage>(this, message => 
            {
                    var viewModel = App.ServicesSimpleInjector.GetInstance(message.viewModelType) as ViewModelBase;
                    CurrentViewModel = viewModel;      
            });
        }
    }
}
