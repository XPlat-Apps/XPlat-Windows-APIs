#if __IOS__
namespace XPlat.UI.Popups
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using UIKit;

    /// <summary>Represents a dialog for showing messages to the user.</summary>
    public class MessageDialog : IMessageDialog
    {
        /// <summary>Initializes a new instance of the MessageDialog class to display an untitled message dialog that can be used to ask your user simple questions.</summary>
        /// <param name="content">The message displayed to the user.</param>
        public MessageDialog(string content)
        {
            this.Content = content;
            this.Commands = new List<IUICommand>();
        }

        /// <summary>Initializes a new instance of the MessageDialog class to display a titled message dialog that can be used to ask your user simple questions.</summary>
        /// <param name="content">The message displayed to the user.</param>
        /// <param name="title">The title you want displayed on the dialog.</param>
        public MessageDialog(string content, string title)
        {
            this.Content = content;
            this.Title = title;
            this.Commands = new List<IUICommand>();
        }

        /// <summary>
        /// Gets the iOS controller for the dialog.
        /// </summary>
        public UIViewController Controller { get; set; } = UIApplication.SharedApplication.KeyWindow.RootViewController;

        /// <summary>Gets or sets the title to display on the dialog, if any.</summary>
        public string Title { get; set; }

        /// <summary>Gets an array of commands that appear in the command bar of the message dialog. These commands makes the dialog actionable.</summary>
        public IList<IUICommand> Commands { get; }

        /// <summary>Gets or sets the index of the command you want to use as the default. This is the command that fires by default when users perform positive actions such as accept.</summary>
        public uint DefaultCommandIndex { get; set; } = 0;

        /// <summary>Gets or sets the index of the command you want to use as the cancel command. This is the command that fires when users perform negative actions such as cancel.</summary>
        public uint CancelCommandIndex { get; set; } = 1;

        /// <summary>Gets or sets the message to be displayed to the user.</summary>
        public string Content { get; set; }

        /// <summary>Begins an asynchronous operation showing a dialog.</summary>
        /// <returns>An object that represents the asynchronous operation.</returns>
        public Task<IUICommand> ShowAsync()
        {
            TaskCompletionSource<IUICommand> tcs = new TaskCompletionSource<IUICommand>();

            try
            {
                if (this.Controller == null)
                {
                    throw new ArgumentNullException(nameof(this.Controller), "The iOS Controller cannot be null.");
                }

                if (string.IsNullOrEmpty(this.Title))
                {
                    throw new ArgumentNullException(nameof(this.Title), "Title cannot be null.");
                }

                IList<IUICommand> uiCommands = this.Commands;

                if (uiCommands.Count > 2)
                {
                    throw new ArgumentException("The dialog can only contain 2 buttons.", nameof(this.Commands));
                }

                if (uiCommands.Count < 1)
                {
                    throw new ArgumentException("The dialog must contain at least one button.", nameof(this.Commands));
                }

                UIAlertController alertController = UIAlertController.Create(this.Title, this.Content, UIAlertControllerStyle.Alert);

                if (uiCommands != null)
                {
                    for (int i = 0; i < uiCommands.Count; i++)
                    {
                        IUICommand command = uiCommands[i];
                        if (i == this.CancelCommandIndex)
                        {
                            alertController.AddAction(
                                UIAlertAction.Create(
                                    command.Label,
                                    UIAlertActionStyle.Cancel,
                                    alert =>
                                        {
                                            command.Invoked?.Invoke(command);
                                            tcs.SetResult(command);
                                        }));
                        }
                        else
                        {
                            alertController.AddAction(
                                UIAlertAction.Create(
                                    command.Label,
                                    UIAlertActionStyle.Default,
                                    alert =>
                                        {
                                            command.Invoked?.Invoke(command);
                                            tcs.SetResult(command);
                                        }));
                        }
                    }
                }

                if (uiCommands[(int)this.CancelCommandIndex] == null)
                {
                    throw new ArgumentException(
                        "The dialog must contain a cancellation/dismiss button corresponding to the CancelCommandIndex.",
                        nameof(this.CancelCommandIndex));
                }

                this.Controller.PresentViewController(
                    alertController,
                    true,
                    () =>
                        {
                            bool cancelled = tcs.TrySetResult(null);
                            if (cancelled)
                            {
                                IUICommand command = uiCommands[(int)this.CancelCommandIndex];
                                command?.Invoked?.Invoke(command);
                            }
                        });
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }

            return tcs.Task;
        }
    }
}
#endif