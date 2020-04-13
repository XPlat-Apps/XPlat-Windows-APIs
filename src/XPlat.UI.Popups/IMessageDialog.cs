namespace XPlat.UI.Popups
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>Represents a dialog for showing messages to the user.</summary>
    public interface IMessageDialog
    {
        /// <summary>Gets or sets the title to display on the dialog, if any.</summary>
        string Title { get; set; }

        /// <summary>Gets an array of commands that appear in the command bar of the message dialog. These commands makes the dialog actionable.</summary>
        IList<IUICommand> Commands { get; }

        /// <summary>Gets or sets the index of the command you want to use as the default. This is the command that fires by default when users perform positive actions such as accept.</summary>
        uint DefaultCommandIndex { get; set; }

        /// <summary>Gets or sets the index of the command you want to use as the cancel command. This is the command that fires when users perform negative actions such as cancel.</summary>
        uint CancelCommandIndex { get; set; }

        /// <summary>Gets or sets the message to be displayed to the user.</summary>
        string Content { get; set; }

        /// <summary>Begins an asynchronous operation showing a dialog.</summary>
        /// <returns>An object that represents the asynchronous operation.</returns>
        Task<IUICommand> ShowAsync();
    }
}