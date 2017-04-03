namespace XPlat.Storage.Pickers
{
    using System.Collections.Generic;

    internal class FileOpenPickerFilesReceived
    {
        public FileOpenPickerFilesReceived(int requestId, IEnumerable<IStorageFile> files, bool cancel)
        {
            if (files == null)
            {
                cancel = true;
            }

            this.RequestId = requestId;
            this.Files = files;
            this.Cancel = cancel;
        }

        public bool Cancel { get; }

        public IEnumerable<IStorageFile> Files { get; }

        public int RequestId { get; }
    }
}