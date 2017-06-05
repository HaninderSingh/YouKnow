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
using YouKnow.Views.GenericViews;

namespace YouKnow.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DiseasesCard : ContentView
    {
        public event EventHandler SendMaps;
        public DiseasesCard()
        {
            InitializeComponent();
            //MyMap.IsVisible = false;
            //PutSomePinsOnMap();
        }
        //    void PutSomePinsOnMap()
        //    {
        //        DisasterModel model = BindingContext as DisasterModel;
        //        // define a center point and some sample pins


        //        Position tourEiffel = new Position(model.Latitude, model.Longitude);
        //        Pin[] pins =
        //        {
        //    new Pin() {  Label = "",
        //        Position = new Position(model.Latitude, model.Longitude), Type = PinType.Place },
        //    //new Pin() {  Label = "Concorde",
        //    //    Position = new Position(48.865475, 2.321142), Type = PinType.Place },
        //    //new Pin() {  Label = "Étoile",
        //    //    Position = new Position(48.873880, 2.295101), Type = PinType.Place },
        //    //new Pin() {  Label = "La Défense",
        //    //    Position = new Position(48.892418, 2.236180), Type = PinType.Place },
        //};

        //        foreach (Pin p in pins)
        //        {
        //            MyMap.Pins.Add(p);
        //        }

        //        // center the map on Tour Eiffel / set the zoom level
        //        MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(tourEiffel, Distance.FromKilometers(2.5)));
        //    }

        private async void Button_Clicked(object sender, EventArgs e)
        {
           var  s = sender as Button;
            
            DiseasesModel model = BindingContext as DiseasesModel;
            //SendMaps?.Invoke(model, e);
            AppConstants.lat = model.Latitude;
            AppConstants.lang = model.Longitude;
            SendMaps?.Invoke(model, e);
        }
    }
}