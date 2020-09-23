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
        #endregion
        #region Properties

        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public PlotModel TemperatureModel { get; set; }
        public PlotModel HumidityModel { get; set; }
        public PlotModel PressureModel { get; set; }
        #endregion

        public GraphViewModel(IGraphServiceProvider graph, IDataServiceProvider dataService, IServerService server)
        {

            _graphService = graph;
            _dataService = dataService;
            _server = server;
            TemperatureModel = _graphService.CreateTimePlot("Temperature", "Temperature Value", "C", 0.0,40.0,"Temperature",OxyColor.FromRgb(255,0,0));
            HumidityModel = _graphService.CreateTimePlot("Humidity", "Humidity Value", "%", 0.0, 100.0, "Humidity", OxyColor.FromRgb(0, 0, 255));
            PressureModel = _graphService.CreateTimePlot("Pressure", "Pressure Value", "hPa", 900.0, 1200.0, "Pressure", OxyColor.FromRgb(0, 255, 0));
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

            try
            {
                dynamic responseJson = JObject.Parse(responseText);
                _graphService.UpdateChart(TemperatureModel, _timeStamp / 1000.0, (double)responseJson.temp);
                _graphService.UpdateChart(HumidityModel, _timeStamp / 1000.0, (double)responseJson.hum);
                _graphService.UpdateChart(PressureModel, _timeStamp / 1000.0, (double)responseJson.press);
            }
            catch(Exception exc)
            {
                Debug.WriteLine("Json parsing error: ");
                Debug.WriteLine(exc);
            }
            _timeStamp += _dataService.GetConfigurationInstance().SampleTime;
        }
        
    }
}
