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
	[Register ("ChooseGameViewController")]
	partial class ChooseGameViewController
	{
		[Outlet]
		UIKit.UIButton CreateNewGameButton { get; set; }

		[Outlet]
		UIKit.UITableView ListOfGamesTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ListOfGamesTableView != null) {
				ListOfGamesTableView.Dispose ();
				ListOfGamesTableView = null;
			}

			if (CreateNewGameButton != null) {
				CreateNewGameButton.Dispose ();
				CreateNewGameButton = null;
			}
		}
	}
}
