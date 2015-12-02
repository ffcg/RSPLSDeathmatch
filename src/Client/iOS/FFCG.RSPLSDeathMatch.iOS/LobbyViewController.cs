using System;
using System.Drawing;

using CoreFoundation;
using FFCG.RSPLS.DeathMatch.ApiModel;
using UIKit;
using Foundation;

namespace FFCG.RSPLS.DeathMatch.iOS
{
    [Register("LobbyView")]
    public class LobbyView : UIView
    {
        public UITableView TableView { get; private set; }

        public LobbyView()
        {
            Initialize();
        }

        public LobbyView(RectangleF bounds) : base(bounds)
        {
            Initialize();
        }

        void Initialize()
        {
            var backgroundImageView = new UIImageView(UIImage.FromBundle("Background.jpg"));
            backgroundImageView.ContentMode = UIViewContentMode.ScaleToFill;
            backgroundImageView.TranslatesAutoresizingMaskIntoConstraints = false;
            this.AddSubview(backgroundImageView);

            TableView = new UITableView();
            TableView.TranslatesAutoresizingMaskIntoConstraints = false;
            TableView.BackgroundColor= UIColor.Clear;
            TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            this.AddSubview(TableView);

            var viewsDictionary = NSDictionary.FromObjectsAndKeys(
                new NSObject[] { backgroundImageView, TableView},
                new NSObject[] { new NSString("bg"), new NSString("table") });
            this.AddConstraints(NSLayoutConstraint.FromVisualFormat("H:|[bg]|",
                NSLayoutFormatOptions.DirectionLeadingToTrailing, new NSDictionary(), viewsDictionary));
            this.AddConstraints(NSLayoutConstraint.FromVisualFormat("V:|[bg]|",
                NSLayoutFormatOptions.DirectionLeadingToTrailing, new NSDictionary(), viewsDictionary));

            this.AddConstraints(NSLayoutConstraint.FromVisualFormat("H:|[table]|",
                NSLayoutFormatOptions.DirectionLeadingToTrailing, new NSDictionary(), viewsDictionary));
            this.AddConstraints(NSLayoutConstraint.FromVisualFormat("V:|-(24)-[table]-(48)-|",
                NSLayoutFormatOptions.DirectionLeadingToTrailing, new NSDictionary(), viewsDictionary));
        }
    }

    [Register("LobbyViewController")]
    public class LobbyViewController : UIViewController
    {
        private AppDelegate App => (AppDelegate)UIApplication.SharedApplication.Delegate;

        public LobbyView LobbyView => (LobbyView) View;

        public LobbyViewController()
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
            View = new LobbyView();

            var games = App.Client.GetGames();
            LobbyView.TableView.Source = new GameTableSource(games);
            
            base.ViewDidLoad();
        }
    }

    public class GameTableSource : UITableViewSource
    {
        public Game[] Games { get; set; }
        private const string CellIdentifier = "GameTableCell";

        public GameTableSource(Game[] games)
        {
            Games = games;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return Games.Length;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
            Game item = Games[indexPath.Row];

            //---- if there are no cells to reuse, create a new one
            if (cell == null)
            { cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier); }

            cell.BackgroundColor = UIColor.Clear;
            cell.TextLabel.Text = item.Name;
            cell.TextLabel.Font = UIFont.FromName("Chalkduster", 24f);
            cell.TextLabel.TextColor = UIColor.White;           

            return cell;
        }
    }
}