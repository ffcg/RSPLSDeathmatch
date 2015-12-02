using System;

using UIKit;
using CoreGraphics;
using Foundation;
using System.Drawing;

namespace FFCG.RSPLS.DeathMatch.iOS
{
	[Register("GameView")]
	public class OneGameView : UIView
	{
		private UIView _iconsContainerView;
		private UIView _loginButtonContainerView;

		private CGPoint[] _iconAnimationStart;
		private CGPoint[] _iconAnimationEnd;

		public OneGameView()
		{
			Initialize();
		}

		public OneGameView(RectangleF bounds) : base(bounds)
		{
			Initialize();
		}

		void Initialize()
		{
			var backgroundImageView = new UIImageView(UIImage.FromBundle("Background.jpg"));
			backgroundImageView.ContentMode = UIViewContentMode.ScaleToFill;
			backgroundImageView.TranslatesAutoresizingMaskIntoConstraints = false;
			this.AddSubview(backgroundImageView);

			_iconsContainerView = new UIView ();
			_iconsContainerView.TranslatesAutoresizingMaskIntoConstraints = false;
			this.AddSubview (_iconsContainerView);

			var killLabel = new UILabel();

			try
			{
				killLabel.Font = UIFont.FromName("Base 02", 40f);
			}
			catch (Exception)
			{
				killLabel.Font = UIFont.FromName("Chalkduster", 25f);
			}
			killLabel.AdjustsFontSizeToFitWidth = true; // gets smaller if it doesn't fit
			killLabel.MinimumFontSize = 12f; // never gets smaller than this size
			killLabel.LineBreakMode = UILineBreakMode.TailTruncation;
			killLabel.Lines = 0; // 0 means unlimited
			killLabel.TextAlignment = UITextAlignment.Center;
			killLabel.TextColor = UIColor.White;
			killLabel.Text = "Kills";
			killLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			this.AddSubview(killLabel);

			var killCountLabel = new UILabel();

			try
			{
				killCountLabel.Font = UIFont.FromName("Base 02", 40f);
			}
			catch (Exception)
			{
				killCountLabel.Font = UIFont.FromName("Chalkduster", 30f);
			}
			killCountLabel.AdjustsFontSizeToFitWidth = true; // gets smaller if it doesn't fit
			killCountLabel.MinimumFontSize = 12f; // never gets smaller than this size
			killCountLabel.LineBreakMode = UILineBreakMode.TailTruncation;
			killCountLabel.Lines = 0; // 0 means unlimited
			killCountLabel.TextAlignment = UITextAlignment.Center;
			killCountLabel.TextColor = UIColor.White;
			killCountLabel.Text = "0";
			killCountLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			this.AddSubview(killCountLabel);

			var deathLabel = new UILabel();

			try
			{
				deathLabel.Font = UIFont.FromName("Base 02", 40f);
			}
			catch (Exception)
			{
				deathLabel.Font = UIFont.FromName("Chalkduster", 25f);
			}
			deathLabel.AdjustsFontSizeToFitWidth = true; // gets smaller if it doesn't fit
			deathLabel.MinimumFontSize = 12f; // never gets smaller than this size
			deathLabel.LineBreakMode = UILineBreakMode.TailTruncation;
			deathLabel.Lines = 0; // 0 means unlimited
			deathLabel.TextAlignment = UITextAlignment.Center;
			deathLabel.TextColor = UIColor.White;
			deathLabel.Text = "Deaths";
			deathLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			this.AddSubview(deathLabel);


			var deathCountLabel = new UILabel();

			try
			{
				deathCountLabel.Font = UIFont.FromName("Base 02", 40f);
			}
			catch (Exception)
			{
				deathCountLabel.Font = UIFont.FromName("Chalkduster", 30f);
			}
			deathCountLabel.AdjustsFontSizeToFitWidth = true; // gets smaller if it doesn't fit
			deathCountLabel.MinimumFontSize = 12f; // never gets smaller than this size
			deathCountLabel.LineBreakMode = UILineBreakMode.TailTruncation;
			deathCountLabel.Lines = 0; // 0 means unlimited
			deathCountLabel.TextAlignment = UITextAlignment.Center;
			deathCountLabel.TextColor = UIColor.White;
			deathCountLabel.Text = "0";
			deathCountLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			this.AddSubview(deathCountLabel);


			AddIconsToContainer ();

			var viewsDictionary = NSDictionary.FromObjectsAndKeys (
				new NSObject[] { backgroundImageView, killLabel, killCountLabel, deathLabel, deathCountLabel, _iconsContainerView }, 
				new NSObject[] { new NSString ("bg"), new NSString ("kill"), new NSString ("killCount"), new NSString ("death"), new NSString ("deathCount"), new NSString ("icons") });
			this.AddConstraints (NSLayoutConstraint.FromVisualFormat ("H:|[bg]|",
				NSLayoutFormatOptions.DirectionLeadingToTrailing, new NSDictionary (), viewsDictionary));
			this.AddConstraints (NSLayoutConstraint.FromVisualFormat ("V:|[bg]|",
				NSLayoutFormatOptions.DirectionLeadingToTrailing, new NSDictionary (), viewsDictionary));

			this.AddConstraints (NSLayoutConstraint.FromVisualFormat ("H:|-(4)-[kill]-(8)-[killCount]-(>=12)-[deathCount]-(8)-[death]-(4)-|",
				NSLayoutFormatOptions.DirectionLeadingToTrailing, new NSDictionary (), viewsDictionary));
			this.AddConstraints (NSLayoutConstraint.FromVisualFormat ("H:|-(>=24)-[icons]-(>=24)-|",
				NSLayoutFormatOptions.DirectionLeadingToTrailing, new NSDictionary (), viewsDictionary));

			this.AddConstraint (NSLayoutConstraint.Create (_iconsContainerView, NSLayoutAttribute.Width,
				NSLayoutRelation.Equal, this, NSLayoutAttribute.Width, 1, -48));
			this.AddConstraint (NSLayoutConstraint.Create (_iconsContainerView, NSLayoutAttribute.Height,
				NSLayoutRelation.Equal, _iconsContainerView, NSLayoutAttribute.Width, 1, 0));	
			
			this.AddConstraints(NSLayoutConstraint.FromVisualFormat("V:|-(>=30,<=20)-[icons]-(>=180,<=120)-|", 0, new NSDictionary(), viewsDictionary));
		}

		private void AddIconsToContainer()
		{
			var icons = new[]
			{
				UIImage.FromBundle("Rock.png"),
				UIImage.FromBundle("Paper.png"),
				UIImage.FromBundle("Scissor.png"),
				UIImage.FromBundle("Lizard.png"),
				UIImage.FromBundle("Spock.png"),
			};
			var count = icons.Length;

			_iconAnimationStart = new CGPoint[count];
			_iconAnimationEnd = new CGPoint[count];
			for (var i = 0; i < count; i++)
			{
				var moveButton = new UIButton(new CGRect(10*i, 10*i, 80, 80));
				moveButton.SetImage(icons[i], UIControlState.Normal);
				moveButton.ContentMode = UIViewContentMode.ScaleAspectFit;
				_iconsContainerView.AddSubview(moveButton);

			}
		}

		public void AnimateIconsIn()
		{
			UIView.Animate(0.25, 0, UIViewAnimationOptions.CurveEaseOut,
				() => {
					for (var i = 0; i < _iconsContainerView.Subviews.Length; i++)
					{
						var iconView = _iconsContainerView.Subviews[i];
						iconView.Center = _iconAnimationEnd[i];
					}
				},
				() => {
				}
			);
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			LayoutIcons();
		}

		private void LayoutIcons()
		{
			var count = _iconsContainerView.Subviews.Length;
			var w = _iconsContainerView.Bounds.Width;
			var h = _iconsContainerView.Bounds.Height;
			var t = 2 * Math.PI / (float)count;

			var w2 = w / 3;
			var r = w/2 - w2/2;
			var r2 = r*3;

			for (var i = 0; i < count; i++)
			{
				_iconAnimationStart[i] = new CGPoint(w/2 + Math.Cos(t*i)*r2, h/2 + Math.Sin(t*i)*r2);
				_iconAnimationEnd[i] = new CGPoint(w/2 + Math.Cos(t*i)*r, h/2 + Math.Sin(t*i)*r);

				var moveButton = _iconsContainerView.Subviews[i];
				moveButton.Frame = new CGRect(0, 0, w2, w2);
				moveButton.Center = _iconAnimationStart[i];
			}
		}

		public void AddLoginInfo(UIView profileImageView, string userName, UIView logoutButton)
		{
			foreach (var subview in _loginButtonContainerView.Subviews)
			{
				subview.RemoveFromSuperview();
			}
			profileImageView.TranslatesAutoresizingMaskIntoConstraints = false;
			profileImageView.Layer.CornerRadius = 24;
			profileImageView.Layer.MasksToBounds = true;
			profileImageView.BackgroundColor = UIColor.Green;

			_loginButtonContainerView.AddSubview(profileImageView);

			logoutButton.TranslatesAutoresizingMaskIntoConstraints = false;
			_loginButtonContainerView.AddSubview(logoutButton);

			var nameLabel = new UILabel();
			nameLabel.Font = UIFont.FromName("Chalkduster", 20f);
			nameLabel.MinimumFontSize = 12f; // never gets smaller than this size
			nameLabel.LineBreakMode = UILineBreakMode.WordWrap;
			nameLabel.Lines = 0; // 0 means unlimited
			nameLabel.TextAlignment = UITextAlignment.Center;
			nameLabel.TextColor = UIColor.White;
			nameLabel.Text = userName;
			//nameLabel.BackgroundColor = UIColor.Cyan;
			nameLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			_loginButtonContainerView.AddSubview(nameLabel);

			var viewsDictionary = NSDictionary.FromObjectsAndKeys(
				new NSObject[] { profileImageView, nameLabel, logoutButton },
				new NSObject[] { new NSString("picture"), new NSString("name"), new NSString("logout") });
			_loginButtonContainerView.AddConstraints(NSLayoutConstraint.FromVisualFormat("H:|[picture(48)]-(8)-[name]-(24)-[logout(80)]|",
				NSLayoutFormatOptions.DirectionLeadingToTrailing, new NSDictionary(), viewsDictionary));
			_loginButtonContainerView.AddConstraints(NSLayoutConstraint.FromVisualFormat("V:|-(>=0)-[picture(48)]-(>=0)-|",
				NSLayoutFormatOptions.DirectionLeadingToTrailing, new NSDictionary(), viewsDictionary));
			_loginButtonContainerView.AddConstraints(NSLayoutConstraint.FromVisualFormat("V:|-(>=0)-[name(<=48)]-(>=0)-|",
				NSLayoutFormatOptions.DirectionLeadingToTrailing, new NSDictionary(), viewsDictionary));
			_loginButtonContainerView.AddConstraints(NSLayoutConstraint.FromVisualFormat("V:|-(>=0)-[logout]-(>=0)-|",
				NSLayoutFormatOptions.DirectionLeadingToTrailing, new NSDictionary(), viewsDictionary));

			_loginButtonContainerView.AddConstraint(NSLayoutConstraint.Create(nameLabel, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _loginButtonContainerView, NSLayoutAttribute.CenterY, 1, 0));
			_loginButtonContainerView.AddConstraint(NSLayoutConstraint.Create(logoutButton, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _loginButtonContainerView, NSLayoutAttribute.CenterY, 1, 0));
		}
	}

//	[Register("OneGameViewController")]
	public partial class OneGameViewController : UIViewController
	{

		private AppDelegate App => (AppDelegate) UIApplication.SharedApplication.Delegate;

		private OneGameView OneGameView => (OneGameView) View;
		public OneGameViewController()
		{
		}

		public override void ViewDidLoad ()
		{
			View = new OneGameView();

			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private void UpdateGamer()
		{
			var facebookUser = App.Facebook.GetCurrentUser();
			if (facebookUser != null)
			{
				var logoutButton = App.Facebook.CreateLoginButton();
				var profileView = App.Facebook.CreateProfileView();
				OneGameView.AddLoginInfo(profileView, facebookUser.UserName, logoutButton);
			}
			else
			{
			}
		}


		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			((OneGameView) View).AnimateIconsIn();
		}
	}
}


