using System;
using System.Collections.Generic;
using System.Linq;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Foundation;
using UIKit;
using YouKnow.Models;
using Xamarin.Forms;

namespace YouKnow.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            OneSignal.Current.StartInit("632e82a3-3ff7-49b5-8b65-2005eb3bbb7e")
               .EndInit();
            OneSignal.Current.StartInit("632e82a3-3ff7-49b5-8b65-2005eb3bbb7e")
                .HandleNotificationOpened(HandleNotificationOpened)
                .EndInit();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
        private static void HandleNotificationOpened(OSNotificationOpenedResult result)
        {
            OSNotificationPayload payload = result.notification.payload;
            Dictionary<string, object> additionalData = payload.additionalData;
            List<NotifyModel> payloadList = new List<NotifyModel>();
            foreach (var item in payload.additionalData)
            {
                NotifyModel model = new NotifyModel()
                {
                    Id = item.Key,
                    Value = item.Value.ToString()
                };
                payloadList.Add(model);
            }
            if (payloadList.Count != 0)
            {
                MessagingCenter.Send<Xamarin.Forms.Application, List<NotifyModel>>(App.Current, "info",
                      payloadList);
            }
        }
    }
}
