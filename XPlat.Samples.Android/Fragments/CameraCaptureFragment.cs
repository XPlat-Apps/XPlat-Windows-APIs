using System;
using Android.Widget;
using CommonServiceLocator;
using MADE.App.Views.Navigation.Pages;
using XPlat.Media.Capture;
using XPlat.Samples.Android.ViewModels;

namespace XPlat.Samples.Android.Fragments
{
    public class CameraCaptureFragment : MvvmPage
    {
        private Button captureImageButton;
        private Button captureVideoButton;

        public CameraCaptureFragment()
        {
            this.DataContext = ServiceLocator.Current.GetInstance<CameraCaptureFragmentViewModel>();
        }

        public override int LayoutId => Resource.Layout.CameraCaptureFragment;

        protected CameraCaptureFragmentViewModel ViewModel => this.DataContext as CameraCaptureFragmentViewModel;

        public override void OnResume()
        {
            if (this.captureImageButton != null)
            {
                this.captureImageButton.Click -= this.OnCaptureImageClick;
            }

            if (this.captureVideoButton != null)
            {
                this.captureVideoButton.Click -= this.OnCaptureVideoClick;
            }

            base.OnResume();

            ViewModel.CameraCaptureUI = new CameraCaptureUI(this.Context);

            this.captureImageButton = this.GetChildView<Button>(Resource.Id.capture_image);
            this.captureVideoButton = this.GetChildView<Button>(Resource.Id.capture_video);

            if (this.captureImageButton != null)
            {
                this.captureImageButton.Click += this.OnCaptureImageClick;
            }

            if (this.captureVideoButton != null)
            {
                this.captureVideoButton.Click += this.OnCaptureVideoClick;
            }
        }

        private async void OnCaptureVideoClick(object sender, EventArgs e)
        {
            await this.ViewModel.CaptureVideoAsync();
        }

        private async void OnCaptureImageClick(object sender, EventArgs e)
        {
            await this.ViewModel.CaptureImageAsync();
        }
    }
}