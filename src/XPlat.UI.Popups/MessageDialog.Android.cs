#if __ANDROID__
namespace XPlat.UI.Popups
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Android.App;
    using Android.Content;

    /// <summary>Represents a dialog for showing messages to the user.</summary>
    public class MessageDialog : IMessageDialog
    {
        /// <summary>Initializes a new instance of the MessageDialog class to display an untitled message dialog that can be used to ask your user simple questions.</summary>
        /// <param name="content">The message displayed to the user.</param>
        public MessageDialog(string content) : this(content, string.Empty, Android.App.Application.Context)
        {
        }

        /// <summary>Initializes a new instance of the MessageDialog class to display an untitled message dialog that can be used to ask your user simple questions.</summary>
        /// <param name="content">The message displayed to the user.</param>
        /// <param name="context">The Android context.</param>
        public MessageDialog(string content, Android.Content.Context context) : this(content, string.Empty, context)
        {
        }

        /// <summary>Initializes a new instance of the MessageDialog class to display a titled message dialog that can be used to ask your user simple questions.</summary>
        /// <param name="content">The message displayed to the user.</param>
        /// <param name="title">The title you want displayed on the dialog.</param>
        public MessageDialog(string content, string title) : this(content, title, Android.App.Application.Context)
        {
        }

        /// <summary>Initializes a new instance of the MessageDialog class to display a titled message dialog that can be used to ask your user simple questions.</summary>
        /// <param name="content">The message displayed to the user.</param>
        /// <param name="title">The title you want displayed on the dialog.</param>
        /// <param name="context">The Android context.</param>
        public MessageDialog(string content, string title, Android.Content.Context context)
        {
            this.Content = content;
            this.Title = title;
            this.Commands = new List<IUICommand>();
            this.Context = context;
        }

        /// <summary>
        /// Gets the Android context for the dialog.
        /// </summary>
        public Context Context { get; set; }

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
                if (this.Context == null)
                {
                    throw new ArgumentNullException(nameof(this.Context), "The Android Context cannot be null.");
                }

                if (string.IsNullOrEmpty(this.Title))
                {
                    throw new ArgumentNullException(nameof(this.Title), "Title cannot be null.");
                }

                IList<IUICommand> uiCommands = this.Commands;

                if (uiCommands.Count > 3)
                {
                    throw new ArgumentException("The dialog can only contain 3 buttons.", nameof(this.Commands));
                }

                if (uiCommands.Count < 1)
                {
                    throw new ArgumentException("The dialog must contain at least one button.", nameof(this.Commands));
                }

                AlertDialog dialog = new AlertDialog.Builder(this.Context).Create();
                dialog.SetTitle(this.Title);

                if (!string.IsNullOrEmpty(this.Content))
                {
                    dialog.SetMessage(this.Content);
                }

                if (uiCommands != null)
                {
                    for (int i = 0; i < uiCommands.Count; i++)
                    {
                        IUICommand command = uiCommands[i];
                        if (i == this.CancelCommandIndex)
                        {
                            dialog.SetButton(
                                (int)Android.Content.DialogButtonType.Negative,
                                command.Label,
                                (sender, args) =>
                                    {
                                        command.Invoked?.Invoke(command);
                                        tcs.SetResult(command);
                                    });
                        }
                        else if (i == this.DefaultCommandIndex)
                        {
                            dialog.SetButton(
                                (int)Android.Content.DialogButtonType.Positive,
                                command.Label,
                                (sender, args) =>
                                    {
                                        command.Invoked?.Invoke(command);
                                        tcs.SetResult(command);
                                    });
                        }
                        else
                        {
                            dialog.SetButton(
                                (int)Android.Content.DialogButtonType.Neutral,
                                command.Label,
                                (sender, args) =>
                                    {
                                        command.Invoked?.Invoke(command);
                                        tcs.SetResult(command);
                                    });
                        }
                    }
                }

                if (uiCommands[(int)this.CancelCommandIndex] == null)
                {
                    throw new ArgumentException(
                        "The dialog must contain a cancellation/dismiss button corresponding to the CancelCommandIndex.",
                        nameof(this.CancelCommandIndex));
                }

                dialog.CancelEvent += (sender, args) =>
                    {
                        IUICommand command = uiCommands[(int)this.CancelCommandIndex];
                        command?.Invoked?.Invoke(command);
                    };
                dialog.DismissEvent += (sender, args) =>
                    {
                        tcs.TrySetResult(null);
                    };

                dialog.Show();
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