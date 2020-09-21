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


namespace XamarinDataGrabber.ViewModels
{
    public class GraphViewModel:BaseViewModel
    {

        #region Fields
        IGraphServiceProvider _graphService;
        IDataServiceProvider _dataService;
        private Timer RequestTimer;
        #endregion
        #region Properties

        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public PlotModel TemperatureModel { get; set; }
        public PlotModel HumidityModel { get; set; }
        public PlotModel PressureModel { get; set; }
        #endregion

        public GraphViewModel(IGraphServiceProvider graph, IDataServiceProvider dataService)
        {

            _graphService = graph;
            _dataService = dataService;
            TemperatureModel = _graphService.CreateTimePlot("Temperature", "Temperature Value", "C", 0.0,40.0,"Temperature",OxyColor.FromRgb(255,0,0));
            HumidityModel = _graphService.CreateTimePlot("Humidity", "Humidity Value", "%", 0.0, 100.0, "Humidity", OxyColor.FromRgb(0, 0, 255));
            PressureModel = _graphService.CreateTimePlot("Pressure", "Pressure Value", "hPa", 900.0, 1200.0, "Pressure", OxyColor.FromRgb(0, 255, 0));
            StartCommand = new Command(() => StartTransfer());
            StopCommand = new Command(() => StopTransfer());
        }

        public void RequestTimerElapsed(object sender, ElapsedEventArgs e)
        {

        }
        public void StartTransfer()
        {
            if (RequestTimer == null)
            {
                RequestTimer = new Timer(_dataService.GetConfigurationInstance().SampleTime);
                RequestTimer.Elapsed += new ElapsedEventHandler(RequestTimerElapsed);
                RequestTimer.Enabled = true;

                TemperatureModel.ResetAllAxes();
                PressureModel.ResetAllAxes();
                HumidityModel.ResetAllAxes();
                
            }
        }

        public void StopTransfer()
        {
            if (RequestTimer != null)
            {
                RequestTimer.Enabled = false;
                RequestTimer = null;
            }
        }

        
    }
}
