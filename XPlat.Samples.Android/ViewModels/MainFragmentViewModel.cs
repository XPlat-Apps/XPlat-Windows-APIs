using System;
using MADE.App.Views.Navigation;
using MADE.App.Views.Navigation.ViewModels;

namespace XPlat.Samples.Android.ViewModels
{
    public class MainFragmentViewModel : PageViewModel
    {
        private readonly INavigationService navigationService;

        public MainFragmentViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public void NavigateToSample(Type fragmentType, object parameter)
        {
            this.navigationService?.NavigateTo(fragmentType, parameter);
        }
    }
}