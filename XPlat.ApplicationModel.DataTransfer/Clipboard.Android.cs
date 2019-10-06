#if __ANDROID__
namespace XPlat.ApplicationModel.DataTransfer
{
    using System;
    using System.Threading.Tasks;
    using Android.App;
    using Android.Content;

    /// <summary>Gets and sets information from the clipboard object.</summary>
    public static class Clipboard
    {
        private const string PrimaryClipKey = "XPlat.ClipboardText";

        /// <summary>Occurs when the data stored in the Clipboard changes.</summary>
        public static event EventHandler<object> ContentChanged;

        internal static ClipboardManager Originator => Application.Context.GetSystemService(Context.ClipboardService) as ClipboardManager;

        /// <summary>Gets the current content that is stored in the clipboard object.</summary>
        /// <returns>Contains the content of the Clipboard.</returns>
        public static DataPackageView GetContent()
        {
            string text = Originator.PrimaryClip?.GetItemAt(0)?.Text;

            var dataPackageView = new DataPackageView();
            dataPackageView.SetText(text);

            return dataPackageView;
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
            Originator.PrimaryClip = ClipData.NewPlainText(PrimaryClipKey, content.Text);
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
            SetText(string.Empty);
            ContentChanged?.Invoke(typeof(Clipboard), null);
        }
    }
}
#endif