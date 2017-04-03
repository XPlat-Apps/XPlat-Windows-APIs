namespace XPlat.Storage.Pickers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MimeTypeHelper
    {
        private static Dictionary<string, string> mimeTypes;

        private static Dictionary<string, string> MimeTypes
            =>
                mimeTypes
                ?? (mimeTypes =
                        new Dictionary<string, string>
                            {
                                { ".3g2", "video/3gpp2" },
                                { ".3gp", "video/3gpp" },
                                { ".aif", "audio/x-aiff" },
                                { ".asf", "video/x-ms-asf" },
                                { ".asx", "video/x-ms-asf" },
                                { ".avi", "video/x-msvideo" },
                                { ".bmp", "image/bmp" },
                                { ".doc", "application/msword" },
                                {
                                    ".docx",
                                    "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                                },
                                { ".flv", "video/x-flv" },
                                { ".gif", "image/gif" },
                                { ".jpeg", "image/jpeg" },
                                { ".jpg", "image/jpeg" },
                                { ".jxr", "image/jxr" },
                                { ".m4a", "video/mp4" },
                                { ".m4v", "video/x-m4v" },
                                { ".mid", "audio/midi" },
                                { ".mov", "video/quicktime" },
                                { ".mp4", "video/mp4" },
                                { ".odt", "application/vnd.oasis.opendocument.text" },
                                { ".pdf", "application/pdf" },
                                { ".png", "image/png" },
                                { ".rtf", "application/rtf" },
                                { ".swf", "application/x-shockwave-flash" },
                                { ".tif", "image/tif" },
                                { ".tiff", "image/tiff" },
                                { ".txt", "text/plain" },
                                { ".wav", "audio/x-wav" },
                                { ".wma", "audio/x-ms-wma" },
                                { ".wmv", "video/x-ms-wmv" },
                                { ".wpd", "application/vnd.wordperfect" },
                                { ".wps", "application/vnd.ms-works" },
                                { ".xls", "application/vnd.ms-excel" },
                                {
                                    ".xlsx",
                                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                                }
                            });

        public static IEnumerable<string> GetMimeTypes(IEnumerable<string> fileTypes)
        {
            if (fileTypes == null)
            {
                return null;
            }

            var result = new List<string>();

            foreach (var fileType in fileTypes)
            {
                var mimeType = GetMimeType(fileType);
                if (!string.IsNullOrWhiteSpace(mimeType) && !result.Contains(mimeType))
                {
                    result.Add(mimeType);
                }
            }

            return result;
        }

        public static string GetMimeType(string fileType)
        {
            var result = MimeTypes.FirstOrDefault(
                x => x.Key.Equals(fileType, StringComparison.CurrentCultureIgnoreCase));
            return !string.IsNullOrWhiteSpace(result.Value) ? result.Value : string.Empty;
        }
    }
}