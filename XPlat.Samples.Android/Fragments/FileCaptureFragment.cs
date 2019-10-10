namespace XPlat.Samples.Android.Fragments
{
    using CommonServiceLocator;

    using global::Android.Widget;

    using MADE.App.Views.Navigation.Pages;

    using XPlat.Samples.Android.ViewModels;

    public class FileCaptureFragment : MvvmPage
    {
        private Button openFileButton;

        public FileCaptureFragment()
        {
            this.DataContext = ServiceLocator.Current.GetInstance<FileCaptureFragmentViewModel>();
        }

        public override int LayoutId => Resource.Layout.FileCaptureFragment;

        protected FileCaptureFragmentViewModel ViewModel => this.DataContext as FileCaptureFragmentViewModel;

        public override void OnResume()
        {
            if (this.openFileButton != null)
            {
                this.openFileButton.Click -= this.OnOpenFileClick;
            }

            base.OnResume();

            this.openFileButton = this.GetChildView<Button>(Resource.Id.capture_file);

            if (this.openFileButton != null)
            {
                this.openFileButton.Click += this.OnOpenFileClick;
            }
        }

        private async void OnOpenFileClick(object sender, System.EventArgs e)
        {
            object storageFile = await this.ViewModel.CaptureFileAsync();
        }
    }
}