namespace XPlat.UI.Popups
{
    /// <summary>Represents a command in a context menu.</summary>
    public sealed class UICommand : IUICommand
    {
        /// <summary>Creates a new instance of the UICommand class using the specified label.</summary>
        /// <param name="label">The label for the UICommand.</param>
        public UICommand(string label)
        {
            this.Label = label;
        }

        /// <summary>Creates a new instance of the UICommand class using the specified label and event handler.</summary>
        /// <param name="label">The label for the new command.</param>
        /// <param name="action">The event handler for the new command.</param>
        public UICommand(string label, UICommandInvokedHandler action)
        {
            this.Label = label;
            this.Invoked = action;
        }

        /// <summary>Creates a new instance of the UICommand class using the specified label, event handler, and command identifier.</summary>
        /// <param name="label">The label for the new command.</param>
        /// <param name="action">The event handler for the new command.</param>
        /// <param name="commandId">The command identifier for the new command.</param>
        public UICommand(string label, UICommandInvokedHandler action, object commandId)
        {
            this.Label = label;
            this.Invoked = action;
            this.Id = commandId;
        }

        /// <summary>Creates a new instance of the UICommand class.</summary>
        public UICommand()
        {
        }

        /// <summary>Gets or sets the label for the command.</summary>
        public string Label { get; set; }

        /// <summary>Gets or sets the handler for the event that is fired when the user selects the UICommand.</summary>
        public UICommandInvokedHandler Invoked { get; set; }

        /// <summary>Gets or sets the identifier of the command.</summary>
        public object Id { get; set; }
    }
}