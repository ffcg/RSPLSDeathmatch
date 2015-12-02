
using System;

using Foundation;
using UIKit;

namespace rpsls
{
	public partial class FirstPageViewController : UIViewController
	{
		public FirstPageViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			FacebookConnectButton.TintColor = UIColor.Red;

			View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Pictures/Background.jpg"));

			RpslsLabel.Lines = 2;
			RpslsLabel.LineBreakMode = UILineBreakMode.WordWrap;

			RpslsLabel.Font = UIFont.FromName ("death", 15);

		}
	}
}
