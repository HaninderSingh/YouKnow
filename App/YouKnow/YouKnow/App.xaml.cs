using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using YouKnow.Converters;
using YouKnow.Models;
using YouKnow.Views;
using YouKnow.Views.DetailsPage;

namespace YouKnow
{
    public partial class App : Application
    {
        private List<NotifyModel> notifylist;
        public Guid TypeGuid;
        public int typeId;
        public App()
        {
            InitializeComponent();
            notifylist = new List<NotifyModel>();
            MainPage = new NavigationPage(new GenericPage());
            MessagingCenter.Subscribe<Xamarin.Forms.Application, List<NotifyModel>>(App.Current, "info",
                     (sender, arg) =>
                     {
                         if (arg != null)
                         {
                             foreach (var item in arg)
                             {
                                 NotifyModel model = new NotifyModel()
                                 {
                                     Id = item.Id,
                                     Value = item.Value,
                                 };
                                 notifylist.Add(model);
                             }
                         }
                         foreach (var items in notifylist)
                         {
                             if(items.Id == "TypeId")
                             {
                                 TypeGuid = Guid.Parse(items.Value);
                                 
                             }
                             else if(items.Id == "Type")
                             {
                                 typeId = int.Parse(items.Value);
                             }
                         }
                         if(typeId == 1)
                         {
                             App.Current.MainPage = new NavigationPage(new DetailCong(TypeGuid));
                         }
                         else if(typeId == 3)
                         {
                             App.Current.MainPage = new NavigationPage(new DetailsPages(TypeGuid));
                         }
                     });
        }
                     

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
