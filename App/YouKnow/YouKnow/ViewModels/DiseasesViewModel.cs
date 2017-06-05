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
   public class DiseasesViewModel : BaseViewModel
    {
        private List<DiseasesModel> _diseasesList;

        public List<DiseasesModel> DiseasesList
        {
            get { return _diseasesList; }
            set { _diseasesList = value; NotifyPropertyChanged("DiseasesList"); }
        }
        public DiseasesViewModel()
        {
            _diseasesList = new List<DiseasesModel>();
        }

        public async Task GetList()
        {
            try
            {
                IsBusy = true;
                IsEnabled = false;
               // var position = await GenericPageViewModel.GetCurrentLocation();
                _diseasesList = await restClient.GetAsync<List<DiseasesModel>>(AppConstants.YouKnow_URl +
                                                                  "GetDiseases?lattitude=" +
                                                                             "12.9205979" +
                                                                             "&longitude=" + "77.6845254", false);
                NotifyPropertyChanged("DiseasesList");

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