using System;
using System.Diagnostics;
using System.Drawing;
using CoreGraphics;
using Foundation;
using UIKit;

namespace FFCG.RSPLS.DeathMatch.iOS
{
    [Register("StartView")]
    public class StartView : UIView
    {
        private UIButton _iconsContainerView;
        private UIView _loginButtonContainerView;

        public UIButton StartButton => _iconsContainerView;

        private CGPoint[] _iconAnimationStart;
        private CGPoint[] _iconAnimationEnd;

        public StartView()
        {
            Initialize();
        }

        public StartView(RectangleF bounds) : base(bounds)
        {
            Initialize();
        }

        void Initialize()
        {
            var backgroundImageView = new UIImageView(UIImage.FromBundle("Background.jpg"));
            backgroundImageView.ContentMode = UIViewContentMode.ScaleToFill;
            backgroundImageView.TranslatesAutoresizingMaskIntoConstraints = false;
            this.AddSubview(backgroundImageView);

            var titleImageView = new UIImageView(UIImage.FromBundle("Logo.png"));
            titleImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            titleImageView.TranslatesAutoresizingMaskIntoConstraints = false;
            this.AddSubview(titleImageView);

            //foreach (var familyName in UIFont.FamilyNames)
            //{
            //    foreach (var font in UIFont.FontNamesForFamilyName(familyName))
            //    {
            //        Debug.WriteLine($"{familyName}\t{font}");
            //    }
            //}

            //var titleLabel = new UILabel();
            //try
            //{
            //    titleLabel.Font = UIFont.FromName("Base 02", 40f);
            //}
            //catch (Exception)
            //{
            //    titleLabel.Font = UIFont.FromName("Chalkduster", 40f);
            //}
            //titleLabel.AdjustsFontSizeToFitWidth = true; // gets smaller if it doesn't fit
            //titleLabel.MinimumFontSize = 12f; // never gets smaller than this size
            //titleLabel.LineBreakMode = UILineBreakMode.TailTruncation;
            //titleLabel.Lines = 0; // 0 means unlimited
            //titleLabel.TextAlignment = UITextAlignment.Center;
            //titleLabel.TextColor = UIColor.White;
            //titleLabel.Text = "RPSLS DeathMatch";
            //titleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            //this.AddSubview(titleLabel);

            _loginButtonContainerView = new UIView();
            //_loginButtonContainerView.BackgroundColor = UIColor.Blue;
            _loginButtonContainerView.TranslatesAutoresizingMaskIntoConstraints = false;
            _loginButtonContainerView.Alpha = 0.0f;
            this.AddSubview(_loginButtonContainerView);

            _iconsContainerView = new UIButton();
            _iconsContainerView.UserInteractionEnabled = true;
            _iconsContainerView.TranslatesAutoresizingMaskIntoConstraints = false;
            this.AddSubview(_iconsContainerView);
            
            AddIconsToContainer();

            var viewsDictionary = NSDictionary.FromObjectsAndKeys(
                new NSObject[] { backgroundImageView, titleImageView, _loginButtonContainerView, _iconsContainerView }, 
                new NSObject[] { new NSString("bg"), new NSString("title"), new NSString("login"), new NSString("icons") });
            this.AddConstraints(NSLayoutConstraint.FromVisualFormat("H:|[bg]|",
                NSLayoutFormatOptions.DirectionLeadingToTrailing, new NSDictionary(), viewsDictionary));
            this.AddConstraints(NSLayoutConstraint.FromVisualFormat("V:|[bg]|",
                NSLayoutFormatOptions.DirectionLeadingToTrailing, new NSDictionary(), viewsDictionary));

            this.AddConstraints(NSLayoutConstraint.FromVisualFormat("H:|-(24)-[title]-(24)-|",
                NSLayoutFormatOptions.DirectionLeadingToTrailing, new NSDictionary(), viewsDictionary));
            this.AddConstraints(NSLayoutConstraint.FromVisualFormat("H:|-(24)-[login]-(24)-|",
                NSLayoutFormatOptions.DirectionLeadingToTrailing, new NSDictionary(), viewsDictionary));
            this.AddConstraints(NSLayoutConstraint.FromVisualFormat("H:|-(>=24)-[icons]-(>=24)-|",
                NSLayoutFormatOptions.DirectionLeadingToTrailing, new NSDictionary(), viewsDictionary));

            this.AddConstraint(NSLayoutConstraint.Create(_iconsContainerView, NSLayoutAttribute.Width,
                NSLayoutRelation.Equal, this, NSLayoutAttribute.Width, 1, -48));
            this.AddConstraint(NSLayoutConstraint.Create(_iconsContainerView, NSLayoutAttribute.Height,
                NSLayoutRelation.Equal, _iconsContainerView, NSLayoutAttribute.Width, 1, 0));

            this.AddConstraints(NSLayoutConstraint.FromVisualFormat("V:|-(>=24,<=48)-[icons]-(>=24)-[login(48)]-(24)-[title(96)]-(>=24,<=48)-|", 0, new NSDictionary(), viewsDictionary));
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
            var r2 = r*4;

            for (var i = 0; i < count; i++)
            {
                _iconAnimationStart[i] = new CGPoint(w/2 + Math.Cos(t*i)*r2, h/2 + Math.Sin(t*i)*r2);
                _iconAnimationEnd[i] = new CGPoint(w/2 + Math.Cos(t*i)*r, h/2 + Math.Sin(t*i)*r);

                var imageView = _iconsContainerView.Subviews[i];
                imageView.Frame = new CGRect(0, 0, w2, w2);
                imageView.Center = _iconAnimationStart[i];
                imageView.Transform = CGAffineTransform.MakeScale(2.0f, 2.0f);
                imageView.Alpha = 0.0f;

            }
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
                var imageView = new UIImageView(new CGRect(10*i, 10*i, 80, 80));
                imageView.Image = icons[i];
                imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
                _iconsContainerView.AddSubview(imageView);

            }
        }

        public void AnimateIconsIn()
        {
            var count = 0;
            for (var i = 0; i < _iconsContainerView.Subviews.Length; i++)
            {
                var index = i;
                UIView.Animate(0.15, 0.18*(float) i, UIViewAnimationOptions.CurveEaseIn,
                    () =>
                    {
                        var iconView = _iconsContainerView.Subviews[index];
                        iconView.Center = _iconAnimationEnd[index];
                        iconView.Transform = CGAffineTransform.MakeScale(1.0f, 1.0f);
                        iconView.Alpha = 1.0f;
                    },
                    () => {}
                    );
            }
            var delay = 0.15 * ((float)_loginButtonContainerView.Subviews.Length + 1.0f);
            UIView.Animate(0.15, delay, UIViewAnimationOptions.CurveEaseIn,
                () =>
                {
                    _loginButtonContainerView.Alpha = 1.0f;
                },
                () => { }
                );
        }

        public void AnimateIconsOut(Action completed)
        {
            var count = 0;
            for (var i = 0; i < _iconsContainerView.Subviews.Length; i++)
            {
                var index = i;
                UIView.Animate(0.05, 0.08 * (float)i, UIViewAnimationOptions.CurveEaseIn,
                    () =>
                    {
                        var iconView = _iconsContainerView.Subviews[index];
                        iconView.Center = _iconAnimationStart[index];
                        iconView.Transform = CGAffineTransform.MakeScale(2.0f, 2.0f);
                        iconView.Alpha = 0.0f;
                    },
                    () =>
                    {
                        count++;
                        if (count == _iconsContainerView.Subviews.Length)
                        {
                            completed?.Invoke();
                        }
                    }
                    );
            }
            var delay = 0.15 * ((float)_loginButtonContainerView.Subviews.Length + 1.0f);
            UIView.Animate(0.35, delay, UIViewAnimationOptions.CurveEaseIn,
                () =>
                {
                    _loginButtonContainerView.Alpha = 1.0f;
                },
                () => { }
                );
        }

        public void AddLoginButton(UIView loginButton)
        {
            foreach (var subview in _loginButtonContainerView.Subviews)
            {
                subview.RemoveFromSuperview();
            }
            loginButton.TranslatesAutoresizingMaskIntoConstraints = false;
            _loginButtonContainerView.AddSubview(loginButton);

            var viewsDictionary = NSDictionary.FromObjectsAndKeys(
                new NSObject[] { loginButton },
                new NSObject[] { new NSString("login") });
            _loginButtonContainerView.AddConstraints(NSLayoutConstraint.FromVisualFormat("H:|-(>=0)-[login(48)]-(>=0)-|",
                NSLayoutFormatOptions.DirectionLeadingToTrailing, new NSDictionary(), viewsDictionary));
            _loginButtonContainerView.AddConstraints(NSLayoutConstraint.FromVisualFormat("V:|-(>=0)-[login(48)]-(>=0)-|",
                NSLayoutFormatOptions.DirectionLeadingToTrailing, new NSDictionary(), viewsDictionary));
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

    [Register("StartViewController")]
    public class StartViewController : UIViewController
    {
        private AppDelegate App => (AppDelegate) UIApplication.SharedApplication.Delegate;

        private StartView StartView => (StartView) View;
        public StartViewController()
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
            base.ViewDidLoad();

            View = new StartView();
            StartView.StartButton.TouchUpInside += StartButton_TouchUpInside;

            App.Facebook.LoggedIn += Facebook_LoggedIn;
            UpdateUserLogin();
        }

        private void StartButton_TouchUpInside(object sender, EventArgs e)
        {
            StartView.AnimateIconsOut(() =>
            {
                var lobbyViewController = new LobbyViewController();
                this.PresentViewController(lobbyViewController, true, () => { });
            });
        }

        private void UpdateUserLogin()
        {
            var facebookUser = App.Facebook.GetCurrentUser();
            if (facebookUser != null)
            {
                var logoutButton = App.Facebook.CreateLoginButton();
                var profileView = App.Facebook.CreateProfileView();
                StartView.AddLoginInfo(profileView, facebookUser.UserName, logoutButton);
            }
            else
            {
                var loginButton = App.Facebook.CreateLoginButton();
                StartView.AddLoginButton(loginButton);
            }
        }

        private void Facebook_LoggedIn(object sender, Services.FacebookAuthenticationLoggedInEventArgs e)
        {
            UpdateUserLogin();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            ((StartView) View).AnimateIconsIn();
        }
    }
}