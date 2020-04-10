namespace XPlat.UI.Popups.Extensions
{
    public static class UICommandExtensions
    {
#if WINDOWS_UWP
        /// <summary>
        /// Converts the Windows GeolocationAccessStatus enum to the internal XPlat equivalent.
        /// </summary>
        /// <param name="windowsCommand">The Windows GeolocationAccessStatus to convert.</param>
        /// <returns>Returns the equivalent XPlat GeolocationAccessStatus value.</returns>
        public static IUICommand ToInternalIUICommand(
            this Windows.UI.Popups.IUICommand windowsCommand)
        {
            return new UICommand(windowsCommand);
        }

        /// <summary>
        /// Converts the XPlat GeolocationAccessStatus enum to the Windows equivalent.
        /// </summary>
        /// <param name="internalCommand">The XPlat GeolocationAccessStatus to convert.</param>
        /// <returns>Returns the equivalent Windows GeolocationAccessStatus value.</returns>
        public static Windows.UI.Popups.IUICommand ToWindowsIUICommand(
            this IUICommand internalCommand)
        {
            Windows.UI.Popups.UICommand windowsCommand = new Windows.UI.Popups.UICommand();

            if (internalCommand != null)
            {
                windowsCommand.Id = internalCommand.Id;
                windowsCommand.Label = internalCommand.Label;
                windowsCommand.Invoked += command => { internalCommand.Invoked?.Invoke(internalCommand); };
            }

            return windowsCommand;
        }
#endif
    }
}
