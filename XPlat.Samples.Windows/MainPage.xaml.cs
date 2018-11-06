namespace XPlat.Samples.Windows
{
    using global::Windows.System.Profile;
    using global::Windows.UI.Xaml.Controls;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            var versionInfo = AnalyticsInfo.VersionInfo;
            var device = AnalyticsInfo.DeviceForm;
        }
    }
}
