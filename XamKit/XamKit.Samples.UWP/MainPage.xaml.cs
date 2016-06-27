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

            var tempFolder =
                await
                ApplicationStorage.Current.LocalFolder.CreateFolderAsync("Temp", FileStoreCreationOption.OpenIfExists);

            var tempFile = await tempFolder.CreateFileAsync("TempFile.json", FileStoreCreationOption.ReplaceIfExists);

            var tempData = new TestContainer { Id = Guid.NewGuid(), Name = "Hello, World!" };

            await tempFile.SaveDataToFileAsync(tempData, SerializationService.Json);

            var folders = await ApplicationStorage.Current.LocalFolder.GetFoldersAsync();

            foreach (var folder in folders)
            {
                await folder.CreateFolderAsync("AnotherFolder", FileStoreCreationOption.OpenIfExists);
            }
        }
    }
}