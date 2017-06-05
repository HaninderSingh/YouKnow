using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouKnow.Constants;
using YouKnow.Helpers;
using YouKnow.Models;

namespace YouKnow.ViewModels
{
    public class DetailViewModel : BaseViewModel
    {
        private Detailmodel _detalModel;

        public Detailmodel DetailModel
        {
            get { return _detalModel; }
            set { _detalModel = value; NotifyPropertyChanged("DetailModel"); }
        }
        public DetailViewModel()
        {
            _detalModel = new Detailmodel();
        }
        public async Task GetDetails(Guid id)
        {

            try
            {
                IsBusy = true;
                IsEnabled = false;

                // var position = await GenericPageViewModel.GetCurrentLocation();
                _detalModel = await restClient.GetAsync<Detailmodel>(AppConstants.YouKnow_URl + "GetDiseasesById?id=" + id, false);

                NotifyPropertyChanged("DetailModel");

            }
            catch (RestClientException ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                IsBusy = false;
                IsEnabled = true;
            }
        }
    }
}
