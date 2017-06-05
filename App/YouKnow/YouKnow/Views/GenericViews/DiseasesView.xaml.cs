using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YouKnow.Constants;
using YouKnow.Models;
using YouKnow.ViewModels;

namespace YouKnow.Views.GenericViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DiseasesView : ContentPage
    {
        public DiseasesViewModel _ViewModel;
        public DiseasesView()
        {
            InitializeComponent();
            _ViewModel= new DiseasesViewModel();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _ViewModel.GetList();
            dListView.ItemsSource = _ViewModel.DiseasesList;

        }

        private async void DiseasesCard_SendMaps(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapPage(AppConstants.lat, AppConstants.lang));
        }
    }
}
