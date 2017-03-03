namespace XPlat.API.UI.Popups
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Windows.ApplicationModel.Core;
    using Windows.UI.Core;
    using Windows.UI.Popups;

    public sealed class MessageDialog : IMessageDialog
    {
        private readonly SemaphoreSlim dialogSemaphore = new SemaphoreSlim(1, 1);

        public MessageDialog(string content)
            : this(content, string.Empty)
        {
        }

        public MessageDialog(string content, string title)
        {
            this.Content = content;
            this.Title = title;
            this.Commands = new List<DialogCommand>();
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public List<DialogCommand> Commands { get; }

        public async Task ShowAsync()
        {
            var tcs = new TaskCompletionSource<bool>();

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                async () =>
                    {
                        await this.dialogSemaphore.WaitAsync();

                        var dialog = new Windows.UI.Popups.MessageDialog(this.Content) { Title = this.Title };

                        if (this.Commands != null)
                        {
                            foreach (var command in this.Commands)
                            {
                                var uiCommand = new UICommand(command.Label);
                                if (command.Invoked != null)
                                {
                                    uiCommand.Invoked = cmd => command.Invoked();
                                }

                                dialog.Commands.Add(uiCommand);
                            }
                        }

                        await dialog.ShowAsync();
                        this.dialogSemaphore.Release();
                        tcs.SetResult(true);
                    });

            await tcs.Task;
        }
    }
}