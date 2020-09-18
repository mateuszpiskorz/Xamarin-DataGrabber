using System;
using System.Collections.Generic;
using System.Text;
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
        IGraphServiceProvider _graphService;

        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public PlotModel TemperatureModel { get; set; }
        public PlotModel HumidityModel { get; set; }
        public PlotModel PressureModel { get; set; }

        public GraphViewModel(IGraphServiceProvider graph)
        {

            _graphService = graph;
            TemperatureModel = _graphService.CreateTimePlot("Temperature", "Temperature Value", "C", 0.0,40.0,"Temperature",OxyColor.FromRgb(255,0,0));
            HumidityModel = _graphService.CreateTimePlot("Humidity", "Humidity Value", "%", 0.0, 100.0, "Humidity", OxyColor.FromRgb(0, 0, 255));
            PressureModel = _graphService.CreateTimePlot("Pressure", "Pressure Value", "hPa", 900.0, 1200.0, "Pressure", OxyColor.FromRgb(0, 255, 0));
            StartCommand = new Command(() => StartTransfer());
            StopCommand = new Command(() => StopTransfer());
        }
        

        public void StartTransfer()
        {

        }

        public void StopTransfer()
        {

        }

        
    }
}
