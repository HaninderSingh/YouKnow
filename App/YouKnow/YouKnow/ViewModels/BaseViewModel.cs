using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouKnow.Helpers;
using YouKnow.Services;

namespace YouKnow.ViewModels
{
   public class BaseViewModel : PropertyNotifier
    {
        public RestClient restClient = RestClient.Instance;
      
        private bool _isConnected;

        private bool _isCnnected;
       
        public bool IsConnected
        {
            get { return _isCnnected; }
            set
            {
                _isCnnected = value;
                NotifyPropertyChanged("IsConnected");
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                NotifyPropertyChanged("IsBusy");
            }
        }
        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {

                _isEnabled = value;
                NotifyPropertyChanged("IsEnabled");
            }
        }
        private bool _isVisble;
        public bool IsVisble
        {
            get { return _isVisble; }
            set
            {
                _isVisble = value;
                NotifyPropertyChanged("IsVisble");
            }
        }
        public void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            IsConnected = e.IsConnected;
        }

        public BaseViewModel()
        {
            
        }
    }
}
