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
    public class CongestionViewModel : BaseViewModel
    {
        private List<CongestionModel> _congestionList;

        public List<CongestionModel> CongestionList
        {
            get { return _congestionList; }
            set { _congestionList = value;  NotifyPropertyChanged("CongestionList");}
        }

        public CongestionViewModel()
        {
            _congestionList = new List<CongestionModel>();
        }

        public async Task GetCongestions()
        {
            try
            {
                IsBusy = true;
                IsEnabled = false;
               
              //  var  position = await GenericPageViewModel.GetCurrentLocation();
                _congestionList = await restClient.GetAsync<List<CongestionModel>>(AppConstants.YouKnow_URl +
                                                                  "GetCongestions?lattitude=" +
                                                                             "12.9205979" +
                                                                             "&longitude=" + "77.6845254", false);
                NotifyPropertyChanged("CongestionList");

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
