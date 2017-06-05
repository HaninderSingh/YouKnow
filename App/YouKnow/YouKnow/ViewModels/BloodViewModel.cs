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
    public class BloodViewModel : BaseViewModel
    {
        private List<BloodModel> _bloodList;

        public List<BloodModel> BloodList
        {
            get { return _bloodList; }
            set { _bloodList = value; NotifyPropertyChanged("BloodList"); }
        }

        public BloodViewModel()
        {
            _bloodList = new List<BloodModel>();
        }

        public async Task GetBloods()
        {
            try
            {
                IsBusy = true;
                IsEnabled = false;

               // var position = await GenericPageViewModel.GetCurrentLocation();
                _bloodList = await restClient.GetAsync<List<BloodModel>>(AppConstants.YouKnow_URl +
                                                                  "GetBloods?lattitude=" +
                                                                             "12.9205979" +
                                                                             "&longitude=" + "77.6845254", false);
                NotifyPropertyChanged("BloodList");

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
