using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using YouKnow.Constants;
using YouKnow.Helpers;
using YouKnow.Models;

namespace YouKnow.ViewModels
{
    public class WantedViewModel : BaseViewModel
    {
        private List<WantedModel> _wantedList;

        public List<WantedModel> WantedList
        {
            get { return _wantedList; }
            set { _wantedList = value; NotifyPropertyChanged("WantedList"); }
        }

        public WantedViewModel()
        {
            _wantedList = new List<WantedModel>();
        }

        public async Task GetList()
        {
            try
            {
                IsBusy = true;
                IsEnabled = false;
              //  var position = await GenericPageViewModel.GetCurrentLocation();
                _wantedList = await restClient.GetAsync<List<WantedModel>>(AppConstants.YouKnow_URl +
                                                                  "GetWanteds?lattitude=" +
                                                                             "12.9205979" +
                                                                             "&longitude=" + "77.6845254", false);
                NotifyPropertyChanged("WantedList");

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