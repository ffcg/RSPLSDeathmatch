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
	[Register ("ChooseNickViewController")]
	partial class ChooseNickViewController
	{
		[Outlet]
		UIKit.UILabel ChooseNameLabel { get; set; }

		[Outlet]
		UIKit.UITextField NickTextField { get; set; }

		[Outlet]
		UIKit.UIButton StartGameButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (NickTextField != null) {
				NickTextField.Dispose ();
				NickTextField = null;
			}

			if (StartGameButton != null) {
				StartGameButton.Dispose ();
				StartGameButton = null;
			}

			if (ChooseNameLabel != null) {
				ChooseNameLabel.Dispose ();
				ChooseNameLabel = null;
			}
		}
	}
}
