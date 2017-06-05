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
    public partial class MissingPage : ContentPage
    {
        public MissingViewModel _ViewModel;
        public MissingPage()
        {
            InitializeComponent();
            _ViewModel = new MissingViewModel();
            BindingContext = _ViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _ViewModel.GetMissedList();
            MissingListView.ItemsSource = _ViewModel.MissingList;

        }
    }
}
