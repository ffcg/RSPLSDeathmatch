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
	[Register ("CreateNewGameViewController")]
	partial class CreateNewGameViewController
	{
		[Outlet]
		UIKit.UIButton CreateNewGameButton { get; set; }

		[Outlet]
		UIKit.UITextField NewGameName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (NewGameName != null) {
				NewGameName.Dispose ();
				NewGameName = null;
			}

			if (CreateNewGameButton != null) {
				CreateNewGameButton.Dispose ();
				CreateNewGameButton = null;
			}
		}
	}
}
