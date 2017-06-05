using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YouKnow.ViewModels;
using YouKnow.Views.GenericViews;

namespace YouKnow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GenericPage : ContentPage
    {
        public GenericPageViewModel _ViewModel;
        public GenericPage()
        {
            InitializeComponent();
            _ViewModel = new GenericPageViewModel();
            BindingContext = _ViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
          //  await GenericPageViewModel.GetCurrentLocation();
            await _ViewModel.GetGenericList();
            Carousel.ItemsSource = _ViewModel.GenericList;

        }

        public async void GoToBloodPage(object s, EventArgs e)
        {
            await Navigation.PushAsync(new BloodPage());
        }
        public async void GoToCongestionPage(object s, EventArgs e)
        {
            await Navigation.PushAsync(new CongestionPage());
        }
        public async void GoToDisasterPage(object s, EventArgs e)
        {
            await Navigation.PushAsync(new DiseasterPage());
        }
        public async void GoToDiseasesPage(object s, EventArgs e)
        {
            await Navigation.PushAsync(new DiseasesView());
        }
        public async void GoToMissingsPage(object s, EventArgs e)
        {
            await Navigation.PushAsync(new MissingPage());
        }
        public async void GoToWantedPage(object s, EventArgs e)
        {
            await Navigation.PushAsync(new WantendPage());
        }
    }
}
