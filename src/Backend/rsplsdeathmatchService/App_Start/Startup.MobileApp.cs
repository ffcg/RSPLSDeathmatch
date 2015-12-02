﻿using System.Configuration;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using Owin;

namespace FFCG.RSPLSDeathMatch.Server
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //For more information on Web API tracing, see http://go.microsoft.com/fwlink/?LinkId=620686 
            config.EnableSystemDiagnosticsTracing();

            new MobileAppConfiguration()
                .AddMobileAppHomeController() // from the Home package
                .MapApiControllers()
                //.AddTables(                               // from the Tables package
                //    new MobileAppTableConfiguration()
                //        .MapTableControllers()
                //        .AddEntityFramework()     // from the Entity package
                //    )
                //.AddAppServiceAuthentication()            // from the Authentication package
                .AddPushNotifications() // from the Notifications package
                //.MapLegacyCrossDomainController()         // from the CrossDomain package
                .ApplyTo(config);

            // To prevent Entity Framework from modifying your database schema, use a null database initializer
            // Database.SetInitializer<rsplsdeathmatchContext>(null);

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                // This middleware is intended to be used locally for debugging. By default, HostName will
                // only have a value when running in an App Service application.
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }
            app.UseWebApi(config);
        }
    } 
}

