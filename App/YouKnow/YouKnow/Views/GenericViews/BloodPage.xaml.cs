using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YouKnow.Constants;
using YouKnow.ViewModels;

namespace YouKnow.Views.GenericViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BloodPage : ContentPage
    {
        public BloodViewModel _ViewModel;
        public BloodPage()
        {
            InitializeComponent();
            _ViewModel = new BloodViewModel();
            BindingContext = _ViewModel;
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _ViewModel.GetBloods();
            BloodListView.ItemsSource = _ViewModel.BloodList;

        }

        private async void BloodCard_SendMaps(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapPage(AppConstants.lat, AppConstants.lang));
        }
    }
}
