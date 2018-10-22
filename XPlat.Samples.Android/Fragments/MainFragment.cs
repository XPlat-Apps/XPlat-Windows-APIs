using System;
using Android.Widget;
using CommonServiceLocator;
using MADE.App.Views.Navigation.Pages;
using XPlat.Samples.Android.ViewModels;

namespace XPlat.Samples.Android.Fragments
{
    public class MainFragment : MvvmPage
    {
        private Button navigateToCameraCaptureButton;

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

            base.OnResume();

            this.navigateToCameraCaptureButton = this.GetChildView<Button>(Resource.Id.navigate_to_camera_capture);

            if (this.navigateToCameraCaptureButton != null)
            {
                this.navigateToCameraCaptureButton.Click += this.OnNavigateToCameraCaptureClick;
            }
        }

        private void OnNavigateToCameraCaptureClick(object sender, EventArgs e)
        {
            this.ViewModel?.NavigateToSample(typeof(CameraCaptureFragment), null);
        }
    }
}