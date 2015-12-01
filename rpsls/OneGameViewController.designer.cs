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
	[Register ("OneGameViewController")]
	partial class OneGameViewController
	{
		[Outlet]
		UIKit.UILabel DeathCount { get; set; }

		[Outlet]
		UIKit.UITextField DeathText { get; set; }

		[Outlet]
		UIKit.UIView GameView { get; set; }

		[Outlet]
		UIKit.UILabel KillCount { get; set; }

		[Outlet]
		UIKit.UITextField KillsText { get; set; }

		[Outlet]
		UIKit.UIButton LizzardButton { get; set; }

		[Outlet]
		UIKit.UIButton PaperButton { get; set; }

		[Outlet]
		UIKit.UICollectionView ParticipantsCollection { get; set; }

		[Outlet]
		UIKit.UIButton RockButton { get; set; }

		[Outlet]
		UIKit.UIButton ScissorButton { get; set; }

		[Outlet]
		UIKit.UIButton SpockButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (GameView != null) {
				GameView.Dispose ();
				GameView = null;
			}

			if (ParticipantsCollection != null) {
				ParticipantsCollection.Dispose ();
				ParticipantsCollection = null;
			}

			if (KillCount != null) {
				KillCount.Dispose ();
				KillCount = null;
			}

			if (DeathCount != null) {
				DeathCount.Dispose ();
				DeathCount = null;
			}

			if (KillsText != null) {
				KillsText.Dispose ();
				KillsText = null;
			}

			if (DeathText != null) {
				DeathText.Dispose ();
				DeathText = null;
			}

			if (SpockButton != null) {
				SpockButton.Dispose ();
				SpockButton = null;
			}

			if (ScissorButton != null) {
				ScissorButton.Dispose ();
				ScissorButton = null;
			}

			if (PaperButton != null) {
				PaperButton.Dispose ();
				PaperButton = null;
			}

			if (RockButton != null) {
				RockButton.Dispose ();
				RockButton = null;
			}

			if (LizzardButton != null) {
				LizzardButton.Dispose ();
				LizzardButton = null;
			}
		}
	}
}
