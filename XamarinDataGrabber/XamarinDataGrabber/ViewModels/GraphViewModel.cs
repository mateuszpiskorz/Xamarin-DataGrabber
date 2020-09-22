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

        public void StartTransfer()
        {
            TemperatureModel.ResetAllAxes();
            HumidityModel.ResetAllAxes();
            PressureModel.ResetAllAxes();

            MessagingCenter.Send<GraphViewModel>(this, "RequestData");
            MessagingCenter.Subscribe<MainViewModel,string>(this, "TransferData", (sender, arg) =>
            {
                
            });
        }

        public void StopTransfer()
        {
            MessagingCenter.Unsubscribe<MainViewModel>(this, "TransferData");
        }

        
    }
}
