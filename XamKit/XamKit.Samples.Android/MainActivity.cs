using Android.App;
using Android.OS;
using Android.Widget;

namespace XamKit.Samples.Android
{
    using XamKit.Core.Common.Storage;
    using XamKit.Core.Storage;

    [Activity(Label = "XamKit.Samples.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            var textFile =
                await
                ApplicationStorage.Current.LocalFolder.CreateFileAsync(
                    "newFile.txt",
                    FileStoreCreationOption.OpenIfExists);

            var folder =
                await
                ApplicationStorage.Current.LocalFolder.CreateFolderAsync(
                    "Extensions",
                    FileStoreCreationOption.OpenIfExists);

            var folderFile = await folder.CreateFileAsync("extensionfile.txt");
        }
    }
}