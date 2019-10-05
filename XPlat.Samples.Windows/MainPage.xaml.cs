namespace XPlat.Samples.Windows
{
    using System.Diagnostics;

    using global::Windows.System.Profile;
    using global::Windows.UI.Xaml.Controls;
    using global::Windows.UI.Xaml.Navigation;

    using XPlat.ApplicationModel.DataTransfer;
    using XPlat.UI.Popups;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            AnalyticsVersionInfo versionInfo = AnalyticsInfo.VersionInfo;
            string device = AnalyticsInfo.DeviceForm;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var message = new XPlat.UI.Popups.MessageDialog("Hello, World", "Title")
                          {
                              DefaultCommandIndex = 0, CancelCommandIndex = 1
                          };
            message.Commands.Add(new UICommand("Okay", command => Debug.WriteLine("Said okay!")) { Id = 1 });
            message.Commands.Add(new UICommand("Close", command => Debug.WriteLine("Said close!")) { Id = 2 });
            IUICommand result = await message.ShowAsync();

            Debug.WriteLine(result == null ? "Dismissed without choosing a result" : result.Label);

            var dataPackage = new DataPackage();
            dataPackage.SetText("This was copied to the clipboard. Try pasting in another app.");
            XPlat.ApplicationModel.DataTransfer.Clipboard.SetContent(dataPackage);

            string clipboardText = await XPlat.ApplicationModel.DataTransfer.Clipboard.GetContent().GetTextAsync();

            var clipboardTextDialog = new XPlat.UI.Popups.MessageDialog(clipboardText, "Clipboard dialog");
            await clipboardTextDialog.ShowAsync();
        }
    }
}