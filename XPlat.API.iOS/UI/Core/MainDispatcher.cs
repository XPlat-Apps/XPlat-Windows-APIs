namespace XPlat.API.UI.Core
{
    using Foundation;

    using System;

    public static class MainDispatcher
    {
        private static NSObject obj;

        private static NSObject Obj
        {
            get
            {
                return obj ?? (obj = new NSObject());
            }
        }

        public static void Run(Action action)
        {
            if (NSThread.Current.IsMainThread)
            {
                action();
                return;
            }

            Obj.BeginInvokeOnMainThread(action);
        }
    }
}