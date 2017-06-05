using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using YouKnow.Constants;
using YouKnow.Models;

namespace YouKnow.ViewModels
{
    public class MissingViewModel : BaseViewModel
    {
        private List<MissingModel> _missingList;

        public List<MissingModel> MissingList
        {
            get { return _missingList; }
            set { _missingList = value; NotifyPropertyChanged("MissingList"); }
        }

        public MissingViewModel()
        {
            _missingList = new List<MissingModel>();
        }

        public async Task GetMissedList()
        {
            try
            {
                IsBusy = true;
                IsEnabled = false;
                //var position = await GenericPageViewModel.GetCurrentLocation();
                _missingList = await restClient.GetAsync<List<MissingModel>>(AppConstants.YouKnow_URl +
                                                                             "GetMissings?lattitude=" +
                                                                             "12.9205979" +
                                                                             "&longitude=" + "77.6845254", false);
                NotifyPropertyChanged("MissingList");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                IsBusy = false;
                IsEnabled =true;
            }
        }
    }
}
