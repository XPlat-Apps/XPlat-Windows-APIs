namespace XamKit.Core.Storage
{
    using System;
    using System.Threading;

    using XamKit.Core.Common;
    using XamKit.Core.Common.Storage;

    /// <summary>
    /// Defines the application storage model.
    /// </summary>
    public static class ApplicationStorage
    {
        private static readonly Lazy<IAppData> appData = new Lazy<IAppData>(
            CreateAppData,
            LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Gets the current instance of the application storage.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// Thrown if the library calling ApplicationStorage has not been implemented.
        /// </exception>
        public static IAppData Current
        {
            get
            {
                var data = appData.Value;
                if (data == null)
                {
                    throw new NotImplementedException(
                        "The library you're calling ApplicationStorage from is not support.");
                }

                return data;
            }
        }

        static IAppData CreateAppData()
        {
            return new AppData();
        }
    }
}