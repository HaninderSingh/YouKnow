using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Com.OneSignal;
using YouKnow.Helpers;
using YouKnow.Models;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using YouKnow.Constants;

namespace YouKnow.ViewModels
{
    public class GenericPageViewModel : BaseViewModel
    {
        private List<GenericModel> _genericList;

        public List<GenericModel> GenericList
        {
            get { return _genericList; }
            set { _genericList = value; NotifyPropertyChanged("GenericList"); }
        }

        
        public GenericPageViewModel()
        {
            _genericList = new List<GenericModel>();
        }

        //public static async Task<Position> GetCurrentLocation()
        //{
        //    // Capture the current location.
        //    Position position = null;
        //    try
        //    {
        //        var locator = CrossGeolocator.Current;
        //        locator.DesiredAccuracy = 50;

        //        // Get the current device position. Leave it null if geo-location is disabled,
        //        // return position (0, 0) if unable to acquire.
        //        if (locator.IsGeolocationEnabled)
        //        {
        //            // Allow ten seconds for geo-location determination.                    
        //            position = await locator.GetPositionAsync(10000);
        //            OneSignal.Current.SendTag("Lat", position.Latitude.ToString());
        //            OneSignal.Current.SendTag("Lang", position.Longitude.ToString());
        //        }
        //        else
        //        {
        //            Debug.WriteLine("Location could not be acquired, geolocator is disabled.");
        //        }
        //    }
        //    catch (Exception le)
        //    {
        //        // TODO: Log this error.
        //        Debug.WriteLine("Location could not be acquired.");
        //        Debug.WriteLine(le.Message);
        //        Debug.WriteLine(le.StackTrace);
        //        position = new Position() { Latitude = 0, Longitude = 0 };
        //    }

        //    return position;
        //}
        public async Task GetGenericList()
        {
            try
            {
                IsBusy = true;
                IsEnabled = false;
                OneSignal.Current.SendTag("Lat", "12.9205979");
                OneSignal.Current.SendTag("Lang", "77.6845254");
                // var position=  await  GenericPageViewModel.GetCurrentLocation();
                _genericList =
                    await restClient.GetAsync<List<GenericModel>>(AppConstants.YouKnow_URl +
                                                                  "GetCarouselData?lattitude=" + "12.9205979" +
                                                                  "&longitude=" + "77.6845254",false);
                NotifyPropertyChanged("GenericList");
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
