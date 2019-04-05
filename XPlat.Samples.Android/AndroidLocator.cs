using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MADE.App.Diagnostics;
using MADE.App.Diagnostics.Logging;
using MADE.App.Views.Dialogs;
using MADE.App.Views.Navigation;
using MADE.App.Views.Threading;
using XPlat.Samples.Android.ViewModels;

namespace XPlat.Samples.Android
{
    public class AndroidLocator
    {
        static AndroidLocator()
        {
            if (!ServiceLocator.IsLocationProviderSet)
            {
                ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            }

            SimpleIoc.Default.Register<IMessenger, Messenger>();
            SimpleIoc.Default.Register<IEventLogger, EventLogger>();
            SimpleIoc.Default.Register<IAppDiagnostics>(
                () => new AppDiagnostics(ServiceLocator.Current.GetInstance<IEventLogger>()));
            SimpleIoc.Default.Register<IUIDispatcher, UIDispatcher>();
            SimpleIoc.Default.Register<IAppDialog, AppDialog>();

            SimpleIoc.Default.Register<MainFragmentViewModel>();
            SimpleIoc.Default.Register<CameraCaptureFragmentViewModel>();
            SimpleIoc.Default.Register<FileCaptureFragmentViewModel>();
        }
    }
}