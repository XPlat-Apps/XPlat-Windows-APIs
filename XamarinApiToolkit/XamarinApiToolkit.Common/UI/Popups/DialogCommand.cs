namespace XamarinApiToolkit.UI.Popups
{
    using System;

    public class DialogCommand
    {
        public DialogCommand()
            : this(string.Empty, null)
        {
        }

        public DialogCommand(string label)
            : this(label, null)
        {
        }

        public DialogCommand(string label, Action action)
        {
            this.Label = label;
            this.Invoked = action;
        }

        public string Label { get; set; }

        public Action Invoked { get; set; }
    }
}