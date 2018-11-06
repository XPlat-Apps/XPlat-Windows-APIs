#if WINDOWS_UWP
namespace XPlat.UI.Popups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using XPlat.UI.Popups.Extensions;

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

        public MessageDialog(Windows.UI.Popups.MessageDialog dialog)
        {
            if (dialog != null)
            {
                this.Content = dialog.Content;
                this.Title = dialog.Title;
                this.CancelCommandIndex = dialog.CancelCommandIndex;
                this.DefaultCommandIndex = dialog.DefaultCommandIndex;

                this.Commands = new List<IUICommand>();

                foreach (Windows.UI.Popups.IUICommand command in dialog.Commands)
                {
                    this.Commands.Add(command.ToInternalIUICommand());
                }
            }
        }

        public static implicit operator MessageDialog(Windows.UI.Popups.MessageDialog dialog)
        {
            return new MessageDialog(dialog);
        }

        public static implicit operator Windows.UI.Popups.MessageDialog(MessageDialog dialog)
        {
            Windows.UI.Popups.MessageDialog windowsDialog =
                new Windows.UI.Popups.MessageDialog(dialog.Content, dialog.Title)
                    {
                        CancelCommandIndex = dialog.CancelCommandIndex, DefaultCommandIndex = dialog.DefaultCommandIndex
                    };

            if (dialog.Commands != null)
            {
                foreach (IUICommand command in dialog.Commands)
                {
                    windowsDialog.Commands.Add(command.ToWindowsIUICommand());
                }
            }

            return windowsDialog;
        }

        /// <summary>Gets or sets the title to display on the dialog, if any.</summary>
        public string Title { get; set; }

        /// <summary>Gets an array of commands that appear in the command bar of the message dialog. These commands makes the dialog actionable.</summary>
        public IList<IUICommand> Commands { get; }

        /// <summary>Gets or sets the index of the command you want to use as the default. This is the command that fires by default when users perform positive actions such as accept.</summary>
        public uint DefaultCommandIndex { get; set; }

        /// <summary>Gets or sets the index of the command you want to use as the cancel command. This is the command that fires when users perform negative actions such as cancel.</summary>
        public uint CancelCommandIndex { get; set; }

        /// <summary>Gets or sets the message to be displayed to the user.</summary>
        public string Content { get; set; }

        /// <summary>Begins an asynchronous operation showing a dialog.</summary>
        /// <returns>An object that represents the asynchronous operation.</returns>
        public async Task<IUICommand> ShowAsync()
        {
            Windows.UI.Popups.MessageDialog messageDialog = this;
            Windows.UI.Popups.IUICommand result = await messageDialog.ShowAsync();
            IUICommand expected = result.ToInternalIUICommand();
            return this.Commands.FirstOrDefault(x => x.Id.Equals(expected.Id) && x.Label.Equals(expected.Label));
        }
    }
}
#endif