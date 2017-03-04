namespace XPlat.API.UWP.Testing
{
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    using XPlat.API.Device.Power;
    using XPlat.API.Storage;

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("HelloWorld.txt", CreationCollisionOption.OpenIfExists);

            var batteryStatus = PowerManager.Current.BatteryStatus;
            var remainingPercentage = PowerManager.Current.RemainingChargePercent;
        }
    }
}