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
        private UIView _iconsContainerView;

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

            var titleLabel = new UILabel();

            try
            {
                titleLabel.Font = UIFont.FromName("Base 02", 40f);
            }
            catch (Exception)
            {
                titleLabel.Font = UIFont.FromName("Helvetica-Bold", 40f);
            }
            titleLabel.AdjustsFontSizeToFitWidth = true; // gets smaller if it doesn't fit
            titleLabel.MinimumFontSize = 12f; // never gets smaller than this size
            titleLabel.LineBreakMode = UILineBreakMode.TailTruncation;
            titleLabel.Lines = 0; // 0 means unlimited
            titleLabel.TextAlignment = UITextAlignment.Center;
            titleLabel.TextColor = UIColor.White;
            titleLabel.Text = "RPSLS DeathMatch";
            titleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            this.AddSubview(titleLabel);

            var loginButtonContainerView = new UIView();
            loginButtonContainerView.BackgroundColor = UIColor.Blue;
            loginButtonContainerView.TranslatesAutoresizingMaskIntoConstraints = false;
            this.AddSubview(loginButtonContainerView);

            _iconsContainerView = new UIView();
            _iconsContainerView.TranslatesAutoresizingMaskIntoConstraints = false;
            this.AddSubview(_iconsContainerView);
            
            AddIconsToContainer();

            var viewsDictionary = NSDictionary.FromObjectsAndKeys(
                new NSObject[] { backgroundImageView, titleLabel, loginButtonContainerView, _iconsContainerView }, 
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
            var r2 = r*3;

            for (var i = 0; i < count; i++)
            {
                _iconAnimationStart[i] = new CGPoint(w/2 + Math.Cos(t*i)*r2, h/2 + Math.Sin(t*i)*r2);
                _iconAnimationEnd[i] = new CGPoint(w/2 + Math.Cos(t*i)*r, h/2 + Math.Sin(t*i)*r);

                var imageView = _iconsContainerView.Subviews[i];
                imageView.Frame = new CGRect(0, 0, w2, w2);
                imageView.Center = _iconAnimationStart[i];
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
    }

    [Register("StartViewController")]
    public class StartViewController : UIViewController
    {
        
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
            View = new StartView();

            base.ViewDidLoad();

            // Perform any additional setup after loading the view
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            ((StartView) View).AnimateIconsIn();
        }
    }
}