#if WINDOWS_UWP
namespace XPlat.ApplicationModel.DataTransfer
{
    using System;
    using System.Threading.Tasks;

    /// <summary>Gets and sets information from the clipboard object.</summary>
    public static class Clipboard
    {
        public static event EventHandler<object> ContentChanged;

        /// <summary>Gets the current content that is stored in the clipboard object.</summary>
        /// <returns>Contains the content of the Clipboard.</returns>
        public static DataPackageView GetContent()
        {
            return Windows.ApplicationModel.DataTransfer.Clipboard.GetContent();
        }

        /// <summary>Gets the current text content that is stored in the clipboard object.</summary>
        /// <returns>The text.</returns>
        public static async Task<string> GetTextAsync()
        {
            return await GetContent().GetTextAsync();
        }

        /// <summary>Sets the current content that is stored in the clipboard object.</summary>
        /// <param name="content">Contains the content of the clipboard. If NULL, the clipboard is emptied.</param>
        public static void SetContent(DataPackage content)
        {
            Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(content);
            ContentChanged?.Invoke(typeof(Clipboard), content);
        }

        /// <summary>Sets the current text that is stored in the clipboard object.</summary>
        /// <param name="text">The text.</param>
        public static void SetText(string text)
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(text);
            SetContent(dataPackage);
        }

        /// <summary>Removes all data from the Clipboard.</summary>
        public static void Clear()
        {
            Windows.ApplicationModel.DataTransfer.Clipboard.Clear();
            ContentChanged?.Invoke(typeof(Clipboard), null);
        }
    }
}
#endif