using System;
using FFCG.RSPLS.DeathMatch.Client.App.Shared;
using UIKit;

namespace FFCG.RSPLS.DeathMatch.iOS.Services
{
    public class PresentationManager : IPresentationManager
    {
        private readonly AppDelegate _app;
        private UIViewController _currentController;

        public PresentationManager(AppDelegate app)
        {
            _app = app;
        }

        public void ShowView(Views view)
        {
            UIViewController viewController = null;
            switch (view)
            {
                case (Views.Start):
                    viewController = new StartViewController();
                    break;
                case (Views.Lobby):
                    viewController = new LobbyViewController();
                    break;
                default:
                    break;
            }
            if (viewController == null)
            {
                throw  new NotSupportedException($"Cannot create a view of type {view}");
            }

            if (_currentController == null)
            {
                // create a new window instance based on the screen size
                _app.Window = new UIWindow(UIScreen.MainScreen.Bounds);

                _app.Window.RootViewController = viewController;
                _app.Window.MakeKeyAndVisible();
            }
            else
            {
                _currentController.PresentModalViewController(viewController, animated: true);
            }

            _currentController = viewController;
        }
    }
}