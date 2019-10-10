namespace XPlat.Samples.Android.Fragments
{
    using System;
    using System.Diagnostics;

    using CommonServiceLocator;

    using global::Android.Widget;

    using MADE.App.Views.Navigation.Pages;

    using XPlat.ApplicationModel;
    using XPlat.ApplicationModel.DataTransfer;
    using XPlat.Samples.Android.ViewModels;
    using XPlat.UI.Popups;

    public class MainFragment : MvvmPage
    {
        private Button navigateToCameraCaptureButton;

        private Button navigateToDisplayRequestButton;

        private Button navigateToGeolocatorButton;

        private Button navigateToLauncherButton;

        private Button navigateToOpenFileButton;

        private Package package;

        public MainFragment()
        {
            this.DataContext = ServiceLocator.Current.GetInstance<MainFragmentViewModel>();
        }

        public override int LayoutId => Resource.Layout.MainFragment;

        protected MainFragmentViewModel ViewModel => this.DataContext as MainFragmentViewModel;

        public override async void OnResume()
        {
            this.package = Package.Current;

            Uri packageLogo = this.package.Logo;

            if (this.navigateToCameraCaptureButton != null)
            {
                this.navigateToCameraCaptureButton.Click -= this.OnNavigateToCameraCaptureClick;
            }

            if (this.navigateToDisplayRequestButton != null)
            {
                this.navigateToDisplayRequestButton.Click -= this.OnNavigateToDisplayRequestClick;
            }

            if (this.navigateToGeolocatorButton != null)
            {
                this.navigateToGeolocatorButton.Click -= this.OnNavigateToGeolocatorClick;
            }

            if (this.navigateToLauncherButton != null)
            {
                this.navigateToLauncherButton.Click -= this.OnNavigateToLauncherClick;
            }

            if (this.navigateToOpenFileButton != null)
            {
                this.navigateToOpenFileButton.Click -= this.OnNavigateToOpenFileClick;
            }

            base.OnResume();

            this.navigateToCameraCaptureButton = this.GetChildView<Button>(Resource.Id.navigate_to_camera_capture);
            this.navigateToDisplayRequestButton = this.GetChildView<Button>(Resource.Id.navigate_to_display_request);
            this.navigateToGeolocatorButton = this.GetChildView<Button>(Resource.Id.navigate_to_geolocator);
            this.navigateToLauncherButton = this.GetChildView<Button>(Resource.Id.navigate_to_launcher);
            this.navigateToOpenFileButton = this.GetChildView<Button>(Resource.Id.navigate_to_open_file);

            if (this.navigateToCameraCaptureButton != null)
            {
                this.navigateToCameraCaptureButton.Click += this.OnNavigateToCameraCaptureClick;
            }

            if (this.navigateToDisplayRequestButton != null)
            {
                this.navigateToDisplayRequestButton.Click += this.OnNavigateToDisplayRequestClick;
            }

            if (this.navigateToGeolocatorButton != null)
            {
                this.navigateToGeolocatorButton.Click += this.OnNavigateToGeolocatorClick;
            }

            if (this.navigateToLauncherButton != null)
            {
                this.navigateToLauncherButton.Click += this.OnNavigateToLauncherClick;
            }

            if (this.navigateToOpenFileButton != null)
            {
                this.navigateToOpenFileButton.Click += this.OnNavigateToOpenFileClick;
            }

            MessageDialog message = new MessageDialog("Hello, World", "Title")
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

            var clipboardTextDialog =
                new XPlat.UI.Popups.MessageDialog(clipboardText, "Clipboard dialog")
                {
                    DefaultCommandIndex = 0,
                    CancelCommandIndex = 1
                };
            clipboardTextDialog.Commands.Add(new UICommand("Okay", command => Debug.WriteLine("Said okay!")) { Id = 1 });
            clipboardTextDialog.Commands.Add(new UICommand("Close", command => Debug.WriteLine("Said close!")) { Id = 2 });

            await clipboardTextDialog.ShowAsync();
        }

        private void OnNavigateToOpenFileClick(object sender, EventArgs e)
        {
            this.ViewModel?.NavigateToSample(typeof(FileCaptureFragment), null);
        }

        private void OnNavigateToLauncherClick(object sender, EventArgs e)
        {
            this.ViewModel?.NavigateToSample(typeof(LauncherFragment), null);
        }

        private void OnNavigateToDisplayRequestClick(object sender, EventArgs e)
        {
            this.ViewModel?.NavigateToSample(typeof(DisplayRequestFragment), null);
        }

        private void OnNavigateToCameraCaptureClick(object sender, EventArgs e)
        {
            this.ViewModel?.NavigateToSample(typeof(CameraCaptureFragment), null);
        }

        private void OnNavigateToGeolocatorClick(object sender, EventArgs e)
        {
            this.ViewModel?.NavigateToSample(typeof(GeolocatorFragment), null);
        }
    }
}