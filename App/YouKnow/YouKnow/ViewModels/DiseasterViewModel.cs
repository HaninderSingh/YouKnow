using Plugin.Geolocator;
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
    public class DiseasterViewModel : BaseViewModel
    {
        private List<DisasterModel> _disasterList ;

        public List<DisasterModel> DisasterList
        {
            get { return _disasterList; }
            set { _disasterList = value; NotifyPropertyChanged("DisasterList"); }
        }
        public DiseasterViewModel()
        {
         _disasterList = new List<DisasterModel>();   
        }

        public async Task GetList()
        {
            try
            {
                IsBusy = true;
                IsEnabled = false;
                //var position = await GenericPageViewModel.GetCurrentLocation();
                _disasterList = await restClient.GetAsync<List<DisasterModel>>(AppConstants.YouKnow_URl +
                                                                  "GetDisasters?lattitude=" +
                                                                             "12.9205979" +
                                                                             "&longitude=" + "77.6845254", false);
                NotifyPropertyChanged("DisasterList");

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
