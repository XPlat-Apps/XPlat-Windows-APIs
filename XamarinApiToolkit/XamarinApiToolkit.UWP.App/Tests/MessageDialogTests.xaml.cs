namespace XamarinApiToolkit.UWP.App.Tests
{
    using Windows.UI.Xaml;

    using XamarinApiToolkit.Tests.Common.TestHelpers;
    using XamarinApiToolkit.UI.Popups;

    public sealed partial class MessageDialogTests
    {
        public MessageDialogTests()
        {
            this.InitializeComponent();
        }

        private async void OnBasicDialogWithContentClicked(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog(MessageDialogHelpers.BasicContent);
            await messageDialog.ShowAsync();
        }
    }
}