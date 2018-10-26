namespace XPlat.Samples.Android.Fragments
{
    using System;
    using System.Threading.Tasks;

    using global::Android.Preferences;
    using global::Android.Widget;

    using MADE.App.Views;
    using MADE.App.Views.Navigation.Pages;

    using XPlat.Device.Geolocation;

    public class GeolocatorFragment : MvvmPage
    {
        private IGeolocator geolocator;

        private IHeaderedTextBlock locationAccessStatus;

        private Button requestLocationAccessButton;

        private HeaderedTextBlock locatorStatus;

        private HeaderedTextBlock currentPosition;

        public override int LayoutId => Resource.Layout.GeolocatorFragment;

        public override void OnResume()
        {
            if (this.geolocator != null)
            {
                this.geolocator.StatusChanged -= this.OnGeolocatorStatusChanged;
                this.geolocator.PositionChanged -= this.OnGeolocatorPositionChanged;
            }

            if (this.requestLocationAccessButton != null)
            {
                this.requestLocationAccessButton.Click -= this.OnRequestLocationAccessClick;
            }

            base.OnResume();

            this.geolocator = new Geolocator(this.Context)
                                  {
                                      DesiredAccuracy = PositionAccuracy.Default, MovementThreshold = 25
                                  };

            if (this.geolocator != null)
            {
                this.geolocator.StatusChanged += this.OnGeolocatorStatusChanged;
                this.geolocator.PositionChanged += this.OnGeolocatorPositionChanged;
            }

            this.requestLocationAccessButton = this.GetChildView<Button>(Resource.Id.request_location_access);
            this.locationAccessStatus = this.GetChildView<HeaderedTextBlock>(Resource.Id.location_access_status);
            this.locatorStatus = this.GetChildView<HeaderedTextBlock>(Resource.Id.location_provider_status);
            this.currentPosition = this.GetChildView<HeaderedTextBlock>(Resource.Id.current_position);

            if (this.requestLocationAccessButton != null)
            {
                this.requestLocationAccessButton.Click += this.OnRequestLocationAccessClick;
            }
        }

        private void OnGeolocatorPositionChanged(IGeolocator sender, PositionChangedEventArgs args)
        {
            if (args?.Position != null)
            {
                this.currentPosition.Text = $"{args.Position.Coordinate.Latitude}, {args.Position.Coordinate.Longitude}";
            }
        }

        private void OnGeolocatorStatusChanged(IGeolocator sender, StatusChangedEventArgs args)
        {
            if (args != null)
            {
                if (this.locatorStatus != null)
                {
                    this.locatorStatus.Text = args.Status.ToString();
                }
            }
        }

        private async void OnRequestLocationAccessClick(object sender, EventArgs e)
        {
            await this.RequestLocationAccessAsync();
        }

        private async Task RequestLocationAccessAsync()
        {
            if (this.geolocator != null)
            {
                GeolocationAccessStatus access = await this.geolocator.RequestAccessAsync();

                if (this.locationAccessStatus != null)
                {
                    this.locationAccessStatus.Text = access.ToString();
                }

                if (access == GeolocationAccessStatus.Allowed)
                {
                    Geoposition currentPos = await this.geolocator.GetGeopositionAsync();

                    this.currentPosition.Text = currentPos != null ? $"{currentPos.Coordinate.Latitude}, {currentPos.Coordinate.Longitude}" : "NULL location information returned.";
                }
            }
        }
    }
}