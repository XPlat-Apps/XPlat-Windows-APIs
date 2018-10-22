using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using CommonServiceLocator;
using MADE.App.Diagnostics;

namespace XPlat.Samples.Android
{
    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {

        private static AndroidLocator androidLocator;

        private IAppDiagnostics appDiagnostics;

        public MainApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        public static AndroidLocator AndroidLocator => androidLocator ?? (androidLocator = new AndroidLocator());

        public override async void OnCreate()
        {
            base.OnCreate();

            androidLocator = new AndroidLocator();

            this.appDiagnostics = ServiceLocator.Current.GetInstance<IAppDiagnostics>();
            if (this.appDiagnostics != null)
            {
                await this.appDiagnostics.StartRecordingDiagnosticsAsync();
            }

            this.RegisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            this.appDiagnostics?.EventLogger?.WriteInfo("An activity was created");
        }

        public void OnActivityDestroyed(Activity activity)
        {
            this.appDiagnostics?.EventLogger?.WriteInfo("An activity was destroyed");
        }

        public void OnActivityPaused(Activity activity)
        {
            this.appDiagnostics?.EventLogger?.WriteInfo("An activity was paused");
        }

        public void OnActivityResumed(Activity activity)
        {
            this.appDiagnostics?.EventLogger?.WriteInfo("An activity was resumed");
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
            this.appDiagnostics?.EventLogger?.WriteInfo("An activity was saved");
        }

        public void OnActivityStarted(Activity activity)
        {
            this.appDiagnostics?.EventLogger?.WriteInfo("An activity was started");
        }

        public void OnActivityStopped(Activity activity)
        {
            this.appDiagnostics?.EventLogger?.WriteInfo("An activity was stopped");
        }
    }
}