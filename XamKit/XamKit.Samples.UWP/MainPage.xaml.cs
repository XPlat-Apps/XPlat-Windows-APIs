namespace XamKit.Samples.UWP
{
    using System;

    using Windows.UI.Xaml.Navigation;

    using XamKit.Core.Common.Storage;
    using XamKit.Core.Serialization;
    using XamKit.Core.Storage;
    using XamKit.Samples.UWP.Models;

    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var file = await ApplicationStorage.Current.LocalFolder.CreateFileAsync(
                "test.json",
                FileStoreCreationOption.ReplaceIfExists);

            var data = new TestContainer { Id = Guid.NewGuid(), Name = "Hello, World" };

            await file.SaveDataToFileAsync(data, SerializationService.Json);

            var info = await file.LoadDataFromFileAsync<TestContainer>(SerializationService.Json);
        }
    }
}