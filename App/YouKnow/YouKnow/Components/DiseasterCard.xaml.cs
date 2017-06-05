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
    public partial class DiseasterCard : ContentView
    {
        public event EventHandler SendMaps;
        public DiseasterCard()
        {
            InitializeComponent();
           
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var s = sender as Button;

            DisasterModel model = BindingContext as DisasterModel;
            //SendMaps?.Invoke(model, e);
            AppConstants.lat = model.Latitude;
            AppConstants.lang = model.Longitude;
            SendMaps?.Invoke(model, e);
        }
    }
}
