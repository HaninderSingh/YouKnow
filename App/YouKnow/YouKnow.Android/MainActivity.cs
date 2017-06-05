using System;
using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using YouKnow.Models;
using Xamarin.Forms;

namespace YouKnow.Droid
{
    [Activity(Label = "YouKnow", Icon = "@drawable/logo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            OneSignal.Current.StartInit("632e82a3-3ff7-49b5-8b65-2005eb3bbb7e")
                 .EndInit();
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::Xamarin.FormsMaps.Init(this, bundle);
            OneSignal.Current.StartInit("632e82a3-3ff7-49b5-8b65-2005eb3bbb7e")
.HandleNotificationOpened(HandleNotificationOpened)
.EndInit();
            LoadApplication(new App());
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
                MessagingCenter.Send<Xamarin.Forms.Application,List<NotifyModel>>(App.Current, "info",
                      payloadList);
            }
        }
    }
}

