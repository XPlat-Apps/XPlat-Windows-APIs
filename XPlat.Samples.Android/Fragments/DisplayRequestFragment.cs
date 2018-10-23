namespace XPlat.Samples.Android.Fragments
{
    using System;

    using global::Android.Widget;

    using MADE.App.Views.Navigation.Pages;

    using XPlat.Device.Display;

    public class DisplayRequestFragment : MvvmPage
    {
        private DisplayRequest displayRequest;

        private Button requestLockButton;

        private Button releaseLockButton;

        public override int LayoutId => Resource.Layout.DisplayRequestFragment;

        public override void OnResume()
        {
            if (this.requestLockButton != null)
            {
                this.requestLockButton.Click -= this.OnRequestLockClick;
            }

            if (this.releaseLockButton != null)
            {
                this.releaseLockButton.Click -= this.OnReleaseLockClick;
            }

            base.OnResume();

            this.displayRequest = new DisplayRequest(this.Activity.Window);

            this.requestLockButton = this.GetChildView<Button>(Resource.Id.request_display_lock);
            this.releaseLockButton = this.GetChildView<Button>(Resource.Id.release_display_lock);

            if (this.requestLockButton != null)
            {
                this.requestLockButton.Click += this.OnRequestLockClick;
            }

            if (this.releaseLockButton != null)
            {
                this.releaseLockButton.Click += this.OnReleaseLockClick;
            }
        }

        private void OnReleaseLockClick(object sender, EventArgs e)
        {
            this.displayRequest?.RequestRelease();
        }

        private void OnRequestLockClick(object sender, EventArgs e)
        {
            this.displayRequest?.RequestActive();
        }
    }
}