using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using YouKnow.Constants;
using YouKnow.Models;

namespace YouKnow.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StaticMapCard : ContentView
    {
        public event EventHandler SendMaps;
        public StaticMapCard()
        {
            InitializeComponent();
            
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            var s = sender as Button;

            CongestionModel model = BindingContext as CongestionModel;
            //SendMaps?.Invoke(model, e);
            AppConstants.lat = model.Latitude;
            AppConstants.lang = model.Longitude;
            SendMaps?.Invoke(model, e);
        }
    }
}
