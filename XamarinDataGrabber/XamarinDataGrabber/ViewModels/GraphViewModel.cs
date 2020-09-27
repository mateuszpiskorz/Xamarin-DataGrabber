using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows.Input;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Xamarin.Forms;
using XamarinDataGrabber.Interfaces;
using XamarinDataGrabber.Enums;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Microsoft.CSharp.RuntimeBinder;
using XamarinDataGrabber.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace XamarinDataGrabber.ViewModels
{
    public class GraphViewModel:BaseViewModel
    {

        #region Fields
        IGraphServiceProvider _graphService;
        IDataServiceProvider _dataService;
        IServerService _server;
        private Timer _requestTimer;
        private int _timeStamp;
        private PlotModel _temperaturePlotModel;
        private PlotModel _humidityPlotModel;
        private PlotModel _pressurePlotModel;
        #endregion
        #region Properties

        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public PlotModel TemperatureModel {
            get {
                return _temperaturePlotModel;
            }
            set {
                _temperaturePlotModel = value;
                OnPropertyChanged("TemperatureModel");
            } }
        public PlotModel HumidityModel
        {
            get
            {
                return _humidityPlotModel;
            }
            set
            {
                _humidityPlotModel = value;
                OnPropertyChanged("HumidityModel");
            }
        }
        public PlotModel PressureModel
        {
            get
            {
                return _pressurePlotModel;
            }
            set
            {
                _pressurePlotModel = value;
                OnPropertyChanged("PressureModel");
            }
        }
        #endregion

        public GraphViewModel(IGraphServiceProvider graph, IDataServiceProvider dataService, IServerService server)
        {

            _graphService = graph;
            _dataService = dataService;
            _server = server;
            TemperatureModel = _graphService.CreateTimePlot("Temperature", "Temperature Value", "C", 0.0,40.0,"Temperature", OxyColor.FromRgb(255,0,0), 10);
            HumidityModel = _graphService.CreateTimePlot("Humidity", "Humidity Value", "%", 0.0, 100.0, "Humidity", OxyColor.FromRgb(0, 0, 255), 10);
            PressureModel = _graphService.CreateTimePlot("Pressure", "Pressure Value", "hPa", 900.0, 1200.0, "Pressure", OxyColor.FromRgb(0, 255, 0), 100);
            StartCommand = new Command(() => StartTransfer());
            StopCommand = new Command(() => StopTransfer());
        }

        public void StartTransfer()
        {
            TemperatureModel.ResetAllAxes();
            HumidityModel.ResetAllAxes();
            PressureModel.ResetAllAxes();

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

        private async void RequestTimerElaped(object sender, ElapsedEventArgs e)
        {
            string responseText = await _server.HandleGetRequest(HttpRequestsTypes.HttpGetSensorData);
            Debug.WriteLine(responseText);

            try
            {
                 var responseJson = await GetResponseCollection(responseText);
                 TemperatureModel =_graphService.UpdateChart(TemperatureModel, _timeStamp / 1000.0, responseJson.Find(item => item.Name == "Temperature").Value);
                 HumidityModel = _graphService.UpdateChart(HumidityModel, _timeStamp / 1000.0, responseJson.Find(item => item.Name == "Humidity").Value);
                 PressureModel = _graphService.UpdateChart(PressureModel, _timeStamp / 1000.0, responseJson.Find(item => item.Name == "Pressure").Value);
            }
            catch(Exception exc)
            {
                Debug.WriteLine("Json parsing error: ");
                Debug.WriteLine(exc);
            }
            _timeStamp += _dataService.GetConfigurationInstance().SampleTime;
        }

        private async  Task<List<SensorDataModel>> GetResponseCollection(string responseString)
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
        
    }
}
