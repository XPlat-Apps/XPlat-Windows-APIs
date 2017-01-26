namespace XamarinApiToolkit.UWP.App
{
    using WinUX.Mvvm.Services;

    using XamarinApiToolkit.UWP.App.Tests;

    using Windows.UI.Xaml;

    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OnMessageDialogTestsClicked(object sender, RoutedEventArgs e)
        {
            NavigationService.Current.Navigate(typeof(MessageDialogTests));
        }
    }
}