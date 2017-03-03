namespace XPlat.API.UI.Popups
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMessageDialog
    {
        string Title { get; set; }

        string Content { get; set; }

        List<DialogCommand> Commands { get; }

        Task ShowAsync();
    }
}