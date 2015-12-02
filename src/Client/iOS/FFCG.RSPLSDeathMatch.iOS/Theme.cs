using System;

using Foundation;
using UIKit;

namespace FFCG.RSPLS.DeathMatch.iOS
{
	/// <summary>
	/// Theme class for appling UIAppearance to the app, and holding app level resources (colors and images)
	/// * Notice use of Lazy&lt;T&gt;, so images are not loaded until requested
	/// </summary>
	public static class Theme
	{
		#region Images

		//		static Lazy<UIImage> imagePlaceholder = new Lazy<UIImage> (() => UIImage.FromFile ("Images/iOS7/image_placeholder.png"));
		//
		//		/// <summary>
		//		/// Used for "add image" button on the confirm screen
		//		/// </summary>
		//		public static UIImage ImagePlaceholder {
		//			get { return imagePlaceholder.Value; }
		//		}

		#endregion

		#region Colors

		static Lazy<UIColor> defaultTintColor = new Lazy<UIColor> (() => UIColor.FromRGB(44, 74, 85));

		/// <summary>
		/// Default tint color
		/// </summary>
		public static UIColor DefaultTintColor {
			get { return defaultTintColor.Value; }
		}

		static Lazy<UIColor> navbarBackgroundColor = new Lazy<UIColor> (() => UIColor.FromRGB(44, 74, 85));

		/// <summary>
		/// General background color for the navigation bars
		/// </summary>
		public static UIColor NavbarBackgroundColor {
			get { return navbarBackgroundColor.Value; }
		}

		static Lazy<UIColor> navbarTitleColor = new Lazy<UIColor> (() => UIColor.White);

		/// <summary>
		/// General background color for the navigation bars
		/// </summary>
		public static UIColor NavbarTitleColor {
			get { return navbarTitleColor.Value; }
		}

		static Lazy<UIColor> sectionHeaderColor = new Lazy<UIColor> (() => UIColor.FromRGB(247,247,247));

		/// <summary>
		/// General background color for the sectionheader
		/// </summary>
		public static UIColor SectionHeaderColor {
			get { return sectionHeaderColor.Value; }
		}

		static Lazy<UIColor> cellErrorMarkingBackgroundColor = new Lazy<UIColor> (() => UIColor.FromRGB(255,161,161));

		/// <summary>
		/// The background color for a tableview cell which is marked as having an error (ex. missing a input value)
		/// </summary>
		public static UIColor CellErrorMarkingBackgroundColor {
			get { return cellErrorMarkingBackgroundColor.Value; }
		}

		static Lazy<UIColor> cellDisabledFontColor = new Lazy<UIColor> (() => UIColor.FromRGB(220,220,220));

		/// <summary>
		/// The font color for a cell that indicates that something is disabled
		/// </summary>
		public static UIColor CellDisabledFontColor {
			get { return cellDisabledFontColor.Value; }
		}
		#endregion

		#region Boolean values

		static Lazy<bool> isiOS7 = new Lazy<bool> (() => UIDevice.CurrentDevice.CheckSystemVersion (7, 0));

		/// <summary>
		/// Gets a value indicating if this is iOS 7
		/// </summary>
		public static bool IsiOS7 {
			get { return isiOS7.Value; }
		}

		#endregion

		/// <summary>
		/// Apply UIAppearance to this application, this is iOS's version of "styling"
		/// </summary>
		public static void Apply (UIWindow window)
		{
			window.TintColor = DefaultTintColor;
			UIActivityIndicatorView.Appearance.Color = null;

			UINavigationBar.Appearance.TintColor = UIColor.White;
			UINavigationBar.Appearance.BarTintColor = DefaultTintColor;
			UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes() { ForegroundColor = UIColor.White, Font = SemiboldFontOfSize(19)};
			UIBarButtonItem.Appearance.SetTitleTextAttributes(
				new UITextAttributes { Font = LightFontOfSize (16) }, 
				UIControlState.Normal);
		}

		#region Boolean Fonts

		const string LightFont = "Lato-Light";
		const string MediumFont = "Lato-Medium";
		const string BoldFont = "Lato-Bold";
		const string HairlineFont = "Lato-Hairline";
		const string RegularFont = "Lato-Regular";
		const string SemiboldFont = "Lato-Semibold";
		const string LolFont = "Chalkduster";

		/// <summary>
		/// Returns the default font with a certain size
		/// </summary>
		public static UIFont ThemeFontOfSize (float size)
		{
			return UIFont.FromName (RegularFont, size);
		}

		/// <summary>
		/// Returns the default font with a certain size
		/// </summary>
		public static UIFont FontOfSize (float size)
		{
			return UIFont.FromName (RegularFont, size);
		}

		/// <summary>
		/// Returns the medium font with a certain size
		/// </summary>
		public static UIFont MediumFontOfSize (float size)
		{
			return UIFont.FromName (MediumFont, size);
		}

		/// <summary>
		/// Returns the bold font with a certain size
		/// </summary>
		public static UIFont BoldFontOfSize (float size)
		{
			return UIFont.FromName (BoldFont, size);
		}

		/// <summary>
		/// Returns the hairline font with a certain size, for digits
		/// </summary>
		public static UIFont HairlineFontOfSize (float size)
		{
			return UIFont.FromName (HairlineFont, size);
		}

		/// <summary>
		/// Returns the light font with a certain size
		/// </summary>
		public static UIFont LightFontOfSize (float size)
		{
			return UIFont.FromName (LightFont, size);
		}

		/// <summary>
		/// Returns the semibold font with a certain size
		/// </summary>
		public static UIFont SemiboldFontOfSize (float size)
		{
			return UIFont.FromName (SemiboldFont, size);
		}

		#endregion
		//
		//	Code from Xamarin demo app.
		//	ServiceContainer can be found here: https://github.com/xamarin/prebuilt-apps/blob/98881cd412b21a285980185c08c1bde9738e67a7/FieldService/FieldService.Shared/Utilities/ServiceContainer.cs
		//
		//		/// <summary>
		//		/// Transitions a controller to the rootViewController, for a fullscreen transition
		//		/// </summary>
		//		public static void TransitionController (UIViewController controller, bool animated = true)
		//		{
		//			var window = ServiceContainer.Resolve<UIWindow>();
		//
		//			//Return if it's already the root controller
		//			if (window.RootViewController == controller)
		//				return;
		//
		//			//Set the root controller
		//			window.RootViewController = controller;
		//
		//			//Peform an animation, note that null is not allowed as a callback, so I use delegate { }
		//			if (animated)
		//				UIView.Transition (window, .3, UIViewAnimationOptions.TransitionCrossDissolve, delegate { }, delegate { });
		//		}

		/// <summary>
		/// Converts a path to Images/mypng.png to Images/iOS7/mypng.png on iOS 7
		/// </summary>
		public static string ToiOS7Path(string path)
		{
			return IsiOS7 ? path.Replace ("Images/", "Images/iOS7/") : path;
		}
	}
}
