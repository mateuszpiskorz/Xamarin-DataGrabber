using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinDataGrabber.Interfaces;
using XamarinDataGrabber.Models;
using XamarinDataGrabber.Enums;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;

namespace XamarinDataGrabber.ViewModels
{
    
    public class DataListViewModel : BaseViewModel
    {
        private IServerService _server;
        private IDataServiceProvider _dataService;
        private ObservableCollection<SensorDataModel> _dataList;
        private Timer _requestTimer;

        #region Properties
        public ObservableCollection<SensorDataModel> DataList
        {
            get
            {
                return _dataList;
            }
            private set
            {
                _dataList = value;
                OnPropertyChanged("DataList");
            }
        }

        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }
        #endregion
        public DataListViewModel(IDataServiceProvider dataService, IServerService server)
        {
            _server = server;
            _dataService = dataService;
            _dataList = null;
            StartCommand = new Command(() => StartTransfer());
            StopCommand = new Command(() => StopTransfer());
        }

        

        private async void RequestTimerElaped(object sender, ElapsedEventArgs e)
        {
            string responseText = await _server.HandleGetRequest(HttpRequestsTypes.HttpGetSensorData);
           // Debug.WriteLine(responseText);

            try
            {
                var responseJson = await GetResponseCollection(responseText);
                DataList = new ObservableCollection<SensorDataModel>(responseJson);
            }
            catch (Exception exc)
            {
                Debug.WriteLine("Json parsing error: ");
                Debug.WriteLine(exc);
            }
            
        }

        private async Task<List<SensorDataModel>> GetResponseCollection(string responseString)
        {
            List<SensorDataModel> data = null;

            try
            {
                data = await Task.Run(() => JsonConvert.DeserializeObject<List<SensorDataModel>>(responseString));

            }
            catch (JsonSerializationException e)
            {
                Debug.WriteLine("Err: Json Collection deserializing");
                Debug.WriteLine(e);
            }

            return data;
        }


        public void StartTransfer()
        {
            if (_requestTimer == null)
            {
                _requestTimer = new Timer(_dataService.GetConfigurationInstance().SampleTime);
                _requestTimer.Elapsed += new ElapsedEventHandler(RequestTimerElaped);
                _requestTimer.Enabled = true;
            }
        }

        public void StopTransfer()
        {
            if (_requestTimer != null)
            {
                _requestTimer.Enabled = false;
                _requestTimer = null;
            }
        }

    }
}
