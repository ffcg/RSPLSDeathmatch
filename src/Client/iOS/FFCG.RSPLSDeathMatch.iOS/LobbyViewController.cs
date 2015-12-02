using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;

namespace FFCG.RSPLS.DeathMatch.iOS
{
    [Register("LobbyView")]
    public class LobbyView : UIView
    {
        public LobbyView()
        {
            Initialize();
        }

        public LobbyView(RectangleF bounds) : base(bounds)
        {
            Initialize();
        }

        void Initialize()
        {
            BackgroundColor = UIColor.Red;
        }
    }

    [Register("LobbyViewController")]
    public class LobbyViewController : UIViewController
    {
        public LobbyViewController()
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            View = new LobbyView();

            base.ViewDidLoad();

            // Perform any additional setup after loading the view
        }
    }
}