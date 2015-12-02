using System;
using System.Diagnostics;
using System.Drawing;
using Foundation;
using UIKit;

namespace FFCG.RSPLS.DeathMatch.iOS
{
    [Register("UniversalView")]
    public class UniversalView : UIView
    {
        public UniversalView()
        {
            Initialize();
        }

        public UniversalView(RectangleF bounds) : base(bounds)
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

            var iconsContainerView = new UIView();
            iconsContainerView.BackgroundColor = UIColor.DarkGray;
            iconsContainerView.TranslatesAutoresizingMaskIntoConstraints = false;
            this.AddSubview(iconsContainerView);

            var viewsDictionary = NSDictionary.FromObjectsAndKeys(
                new NSObject[] { backgroundImageView, titleLabel, loginButtonContainerView, iconsContainerView}, 
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

            this.AddConstraint(NSLayoutConstraint.Create(iconsContainerView, NSLayoutAttribute.Width,
                NSLayoutRelation.Equal, this, NSLayoutAttribute.Width, 1, -48));
            this.AddConstraint(NSLayoutConstraint.Create(iconsContainerView, NSLayoutAttribute.Height,
                NSLayoutRelation.Equal, iconsContainerView, NSLayoutAttribute.Width, 1, 0));

            this.AddConstraints(NSLayoutConstraint.FromVisualFormat("V:|-(>=24,<=48)-[icons]-(>=24)-[login(48)]-(24)-[title(96)]-(>=24,<=48)-|", 0, new NSDictionary(), viewsDictionary));
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
            View = new UniversalView();

            base.ViewDidLoad();

            // Perform any additional setup after loading the view
        }
    }
}