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
    public partial class CongestionPage : ContentPage
    {
        public CongestionViewModel _ViewModel;
        public CongestionPage()
        {
            InitializeComponent();
            _ViewModel = new CongestionViewModel();
            BindingContext = _ViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _ViewModel.GetCongestions();
            CongestionListView.ItemsSource =  _ViewModel.CongestionList;

        }
        private async void BloodCard_SendMaps(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapPage(AppConstants.lat, AppConstants.lang));
        }
    }
}
