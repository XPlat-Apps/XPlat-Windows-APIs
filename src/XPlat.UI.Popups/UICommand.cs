namespace XPlat.UI.Popups
{
    using System;

    /// <summary>Represents a command in a context menu.</summary>
    public sealed class UICommand : IUICommand
    {
#if WINDOWS_UWP
        private readonly WeakReference originator;
#endif

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

#if WINDOWS_UWP
        public UICommand(Windows.UI.Popups.IUICommand command)
        {
            this.originator = new WeakReference(command);

            if (command != null)
            {
                this.Id = command.Id;
                this.Label = command.Label;
                this.Invoked += uiCommand => { command.Invoked?.Invoke(this.Originator); };
            }
        }
#endif

#if WINDOWS_UWP
        public static implicit operator UICommand(Windows.UI.Popups.UICommand command)
        {
            return new UICommand(command);
        }

        public static implicit operator Windows.UI.Popups.UICommand(UICommand command)
        {
            return new Windows.UI.Popups.UICommand();
        }
#endif

        /// <summary>Gets or sets the label for the command.</summary>
        public string Label { get; set; }

        /// <summary>Gets or sets the handler for the event that is fired when the user selects the UICommand.</summary>
        public UICommandInvokedHandler Invoked { get; set; }

        /// <summary>Gets or sets the identifier of the command.</summary>
        public object Id { get; set; }

#if WINDOWS_UWP
        private Windows.UI.Popups.IUICommand Originator => this.originator != null && this.originator.IsAlive
                                          ? this.originator.Target as Windows.UI.Popups.IUICommand
                                          : null;
#endif
    }
}