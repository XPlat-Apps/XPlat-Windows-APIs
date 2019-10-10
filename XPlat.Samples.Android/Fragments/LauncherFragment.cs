namespace XPlat.Samples.Android.Fragments
{
    using System;

    using global::Android.Widget;

    using MADE.App.Views.Navigation.Pages;

    using XPlat.Device;

    public class LauncherFragment : MvvmPage
    {
        private Button launchUriButton;

        private ILauncher launcher;

        public override int LayoutId => Resource.Layout.LauncherFragment;

        public override void OnResume()
        {
            if (this.launchUriButton != null)
            {
                this.launchUriButton.Click -= this.OnLaunchUriClick;
            }

            base.OnResume();

            this.launcher = new Launcher();

            this.launchUriButton = this.GetChildView<Button>(Resource.Id.launch_uri);

            if (this.launchUriButton != null)
            {
                this.launchUriButton.Click += this.OnLaunchUriClick;
            }
        }

        private async void OnLaunchUriClick(object sender, EventArgs e)
        {
            if (this.launcher != null)
            {
                await this.launcher.LaunchUriAsync(new Uri("http://www.google.com"));
            }
        }
    }
}