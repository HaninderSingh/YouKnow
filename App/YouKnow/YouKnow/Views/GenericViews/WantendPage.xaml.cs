using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YouKnow.ViewModels;

namespace YouKnow.Views.GenericViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WantendPage : ContentPage
    {
        public WantedViewModel _ViewModel;
        public WantendPage()
        {
            InitializeComponent();
            _ViewModel = new WantedViewModel();
            BindingContext = _ViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _ViewModel.GetList();
            WListView.ItemsSource = _ViewModel.WantedList;

        }
    }
}
