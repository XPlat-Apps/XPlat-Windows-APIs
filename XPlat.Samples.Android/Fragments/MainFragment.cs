namespace XPlat.Samples.Android.Fragments
{
    using System;

    using CommonServiceLocator;

    using global::Android.Widget;

    using MADE.App.Views.Navigation.Pages;

    using XPlat.Samples.Android.ViewModels;

    public class MainFragment : MvvmPage
    {
        private Button navigateToCameraCaptureButton;

        private Button navigateToDisplayRequestButton;

        private Button navigateToGeolocatorButton;

        public MainFragment()
        {
            this.DataContext = ServiceLocator.Current.GetInstance<MainFragmentViewModel>();
        }

        public override int LayoutId => Resource.Layout.MainFragment;

        protected MainFragmentViewModel ViewModel => this.DataContext as MainFragmentViewModel;

        public override void OnResume()
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

            base.OnResume();

            this.navigateToCameraCaptureButton = this.GetChildView<Button>(Resource.Id.navigate_to_camera_capture);
            this.navigateToDisplayRequestButton = this.GetChildView<Button>(Resource.Id.navigate_to_display_request);
            this.navigateToGeolocatorButton = this.GetChildView<Button>(Resource.Id.navigate_to_geolocator);

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