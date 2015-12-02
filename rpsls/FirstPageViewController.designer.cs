// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace rpsls
{
	[Register ("FirstPageViewController")]
	partial class FirstPageViewController
	{
		[Outlet]
		UIKit.UIButton FacebookConnectButton { get; set; }

		[Outlet]
		UIKit.UILabel RpslsLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (FacebookConnectButton != null) {
				FacebookConnectButton.Dispose ();
				FacebookConnectButton = null;
			}

			if (RpslsLabel != null) {
				RpslsLabel.Dispose ();
				RpslsLabel = null;
			}
		}
	}
}
