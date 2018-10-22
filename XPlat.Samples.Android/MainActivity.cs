using Android.App;
using GalaSoft.MvvmLight.Ioc;
using MADE.App.Views.Navigation;
using XPlat.Samples.Android.Fragments;
using NavigationService = XPlat.Samples.Android.Services.NavigationService;

namespace XPlat.Samples.Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : NavigationFrame
    {
        private bool startup;

        public MainActivity()
        {
            this.FrameLayoutId = Resource.Layout.Main;
            this.FrameFragmentContentId = Resource.Id.MainContent;

            if (SimpleIoc.Default.IsRegistered<INavigationService>())
            {
                SimpleIoc.Default.Unregister<INavigationService>();
            }

            SimpleIoc.Default.Register<INavigationService>(() => new NavigationService(this), true);
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (!this.startup)
            {
                this.Navigate(typeof(MainFragment));
                this.startup = true;
            }
        }
    }
}