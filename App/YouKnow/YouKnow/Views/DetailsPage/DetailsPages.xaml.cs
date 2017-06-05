using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YouKnow.ViewModels;

namespace YouKnow.Views.DetailsPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPages : ContentPage
    {
        public DetailViewModel _viewModel;
        public Guid GID;
        public DetailsPages(Guid Id)
        {
            InitializeComponent();
            _viewModel = new DetailViewModel();
            GID = Id;
            BindingContext = _viewModel;
            ToolbarItem info = new ToolbarItem()
            {
                Icon = "Skip",
                Priority = 0,
                Order = ToolbarItemOrder.Primary

            };

            this.ToolbarItems.Add(info);
            info.Clicked += Info_Clicked;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.GetDetails(GID);
        }
        private async void Info_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GenericPage());
        }
    }
}
