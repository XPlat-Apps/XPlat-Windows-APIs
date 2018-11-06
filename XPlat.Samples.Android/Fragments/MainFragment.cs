namespace XPlat.Samples.Android.Fragments
{
    using System;
    using System.Diagnostics;

    using CommonServiceLocator;

    using global::Android.Widget;

    using MADE.App.Views.Navigation.Pages;

    using XPlat.Samples.Android.ViewModels;
    using XPlat.UI.Popups;

    public class MainFragment : MvvmPage
    {
        private Button navigateToCameraCaptureButton;

        private Button navigateToDisplayRequestButton;

        private Button navigateToGeolocatorButton;

        private Button navigateToLauncherButton;

        public MainFragment()
        {
            this.DataContext = ServiceLocator.Current.GetInstance<MainFragmentViewModel>();
        }

        public override int LayoutId => Resource.Layout.MainFragment;

        protected MainFragmentViewModel ViewModel => this.DataContext as MainFragmentViewModel;

        public override async void OnResume()
        {
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

            base.OnResume();

            this.navigateToCameraCaptureButton = this.GetChildView<Button>(Resource.Id.navigate_to_camera_capture);
            this.navigateToDisplayRequestButton = this.GetChildView<Button>(Resource.Id.navigate_to_display_request);
            this.navigateToGeolocatorButton = this.GetChildView<Button>(Resource.Id.navigate_to_geolocator);
            this.navigateToLauncherButton = this.GetChildView<Button>(Resource.Id.navigate_to_launcher);

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

            var message = new XPlat.UI.Popups.MessageDialog("Hello, World", "Title")
                              {
                                  Context = this.Context, DefaultCommandIndex = 0, CancelCommandIndex = 1
                              };
            message.Commands.Add(new UICommand("Okay", command => Debug.WriteLine("Said okay!")) { Id = 1 });
            message.Commands.Add(new UICommand("Close", command => Debug.WriteLine("Said close!")) { Id = 2 });
            var result = await message.ShowAsync();

            Debug.WriteLine(result == null ? "Dismissed without choosing a result" : result.Label);
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