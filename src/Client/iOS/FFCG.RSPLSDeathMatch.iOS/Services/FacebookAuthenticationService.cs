using System;
using System.Collections.Generic;
using CoreGraphics;
using Facebook.CoreKit;
using Facebook.LoginKit;
using Foundation;
using Security;
using UIKit;

namespace FFCG.RSPLS.DeathMatch.iOS.Services
{
    public class FacebookAuthenticationLoggedInEventArgs : EventArgs
    {
        public FacebookUser User { get; set; }
    }

    public class FacebookUser
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
    }

    public class FacebookAuthenticationService
    {
        public event EventHandler<FacebookAuthenticationLoggedInEventArgs> LoggedIn;

        // This permission is set by default, even if you don't add it, but FB recommends to add it anyway
        static readonly List<string> ReadPermissions = new List<string> { "public_profile" };

        private const string SecureRecordLabel = "RSPLSFacebookAccessToken";
        private const string SecureRecordDescription = "RSPLSFacebookAccessToken";

        private FacebookUser _currentUser;

        public FacebookAuthenticationService()
        {
            Profile.EnableUpdatesOnAccessTokenChange(true);
            Settings.AppID = "1701374563438405";
            Settings.DisplayName = "RSPLS";

            Profile.Notifications.ObserveDidChange((sender, e) =>
            {

                if (e.NewProfile == null) return;

                _currentUser = new FacebookUser() {UserName = e.NewProfile.Name, UserId = e.NewProfile.UserID};
                LoggedIn?.Invoke(null, new FacebookAuthenticationLoggedInEventArgs() {User = _currentUser});
            });

            _currentUser = null;

            CheckAccessTokenFromKeyChain();

            if (AccessToken.CurrentAccessToken != null)
            {
                var request = new GraphRequest("/me?fields=name", null, AccessToken.CurrentAccessToken.TokenString, null, "GET");
                request.Start((connection, result, error) =>
                {
                    // Handle if something went wrong with the request
                    if (error != null)
                    {
                        new UIAlertView("Error...", error.Description, null, "Ok", null).Show();
                        return;
                    }

                    // Get your profile name
                    var userInfo = result as NSDictionary;
                    var userName = userInfo["name"].ToString();

                    SetNewUser(userName, AccessToken.CurrentAccessToken.UserID);
                });
            }
        }

        private void SetNewUser(string userName, string userId)
        {
            _currentUser = new FacebookUser() { UserName = userName, UserId = userId };
            LoggedIn?.Invoke(null, new FacebookAuthenticationLoggedInEventArgs() { User = _currentUser });
        }

        public UIView CreateLoginButton()
        {
            var loginButton = new LoginButton()
            {
                LoginBehavior = LoginBehavior.Web,
                ReadPermissions = ReadPermissions.ToArray()
            };

            loginButton.Completed += LoginCompleted;
            return loginButton;
        }

        public UIView CreateProfileView()
        {
            var pictureView = new ProfilePictureView(new CGRect(50, 50, 50, 50));
            return pictureView;
        }

        public FacebookUser GetCurrentUser()
        {
            return _currentUser;
        }

        private void LoginCompleted(object sender, LoginButtonCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // Handle if there was an error
                new UIAlertView("Error...", e.Error.LocalizedFailureReason, null, "Ok", null).Show();
                return;
            }

            if (e.Result.IsCancelled)
            {
                // Handle if the user cancelled the login request
                return;
            }

            // Handle your successful login
            StoreAccessTokenInKeyChain(e.Result.Token.UserID, e.Result.Token.AppID, e.Result.Token.TokenString);
        }        

        private static void StoreAccessTokenInKeyChain(string userId, string appId, string tokenString)
        {
            var secureRecord = new SecRecord(SecKind.GenericPassword)
            {
                Label = SecureRecordLabel,
                Description = SecureRecordDescription,
                Account = userId,
                Service = appId,
                ValueData = NSData.FromString(tokenString, NSStringEncoding.UTF8)
            };

            var error = SecKeyChain.Add(secureRecord);
            if (error != SecStatusCode.Success)
            {
                new UIAlertView("Error...", Enum.GetName(typeof(SecStatusCode), error), null, "Ok", null).Show();
            }
        }

        private static void CheckAccessTokenFromKeyChain()
        {
            var secureRecordQuery = new SecRecord(SecKind.GenericPassword)
            {
                Label = SecureRecordLabel
            };

            SecStatusCode status;
            var secureRecord = SecKeyChain.QueryAsRecord(secureRecordQuery, out status);
            if (status == SecStatusCode.Success)
            {
                var accessToken = secureRecord.ValueData.ToString(NSStringEncoding.UTF8);
                AccessToken.CurrentAccessToken = new AccessToken(
                    tokenString: accessToken,
                    permissions: ReadPermissions.ToArray(), 
                    declinedPermissions: new string[] { }, 
                    appID: secureRecord.Service, 
                    userID: secureRecord.Account, 
                    expirationDate: null, 
                    refreshDate: null);
            }
        }
    }
}