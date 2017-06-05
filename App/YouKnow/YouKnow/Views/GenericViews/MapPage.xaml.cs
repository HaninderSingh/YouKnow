using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace YouKnow.Views.GenericViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public double Lat;
        public double Lang;
        public MapPage(Double lat, Double lang)
        {
            InitializeComponent();
            Lat = lat;
            Lang = lang;
            PutSomePinsOnMap();

        }
        void PutSomePinsOnMap()
        {
         
            // define a center point and some sample pins
            Position tourEiffel = new Position(Lat ,Lang );
            Pin[] pins =
            {
        new Pin() {  Label = "",
            Position = new Position(Lat ,Lang), Type = PinType.Place },
        //new Pin() {  Label = "Concorde",
        //    Position = new Position(48.865475, 2.321142), Type = PinType.Place },
        //new Pin() {  Label = "Étoile",
        //    Position = new Position(48.873880, 2.295101), Type = PinType.Place },
        //new Pin() {  Label = "La Défense",
        //    Position = new Position(48.892418, 2.236180), Type = PinType.Place },
    };

            foreach (Pin p in pins)
            {
                MyMap.Pins.Add(p);
            }

            // center the map on Tour Eiffel / set the zoom level
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(tourEiffel, Distance.FromKilometers(2.5)));
        }
    }
}
