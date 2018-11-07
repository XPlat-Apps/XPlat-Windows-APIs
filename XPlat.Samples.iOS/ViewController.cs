using Foundation;
using System;
using System.Diagnostics;
using UIKit;
using XPlat.UI.Popups;

namespace XPlat.Samples.iOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        async partial void UIButton658_TouchUpInside(UIButton sender)
        {
            MessageDialog message = new XPlat.UI.Popups.MessageDialog("Hello, World", "Title")
            {
                Controller = this,
                DefaultCommandIndex = 0,
                CancelCommandIndex = 1
            };

            message.Commands.Add(new UICommand("Okay", command => Debug.WriteLine("Said okay!")) { Id = 1 });
            message.Commands.Add(new UICommand("Close", command => Debug.WriteLine("Said close!")) { Id = 2 });
            IUICommand result = await message.ShowAsync();

            Debug.WriteLine(result == null ? "Dismissed without choosing a result" : result.Label);
        }
    }
}