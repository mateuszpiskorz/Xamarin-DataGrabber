using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinDataGrabber.Interfaces;
using XamarinDataGrabber.Models;

namespace XamarinDataGrabber.Services
{
    public class GraphService : IGraphServiceProvider
    {
        IDataServiceProvider _service;
        
        public GraphService(IDataServiceProvider service)
        {
            _service = service;
            
        }

        public PlotModel CreateTimePlot(string plotTitle,  string yAxisTitle, string yAxisUnit, double yAxisMinimum, double yAxisMaximum, string seriesTitle, OxyColor seriesColor, int majorStep )
        {
            PlotModel plot = new PlotModel() { Title = plotTitle };
            plot.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = _service.GetConfigurationInstance().XAxisMax,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"
            });
            plot.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = yAxisMinimum,
                Maximum = yAxisMaximum,
                MajorStep = majorStep,
                MajorGridlineStyle = LineStyle.Dot,
                MajorGridlineColor = OxyColors.LightGray,
                FontSize = 10,
                Key = "Vertical",
                Unit = yAxisUnit,
                Title = yAxisTitle

            });

            plot.Series.Add(new LineSeries()
            {
                Title = seriesTitle,
                Color = seriesColor,
                XAxisKey = "Horizontal",
                YAxisKey = "Vertical"
            });

            

            return plot;
        }
   
        public PlotModel UpdateChart(PlotModel plotModel, double xValue, double yValue)
        {
            LineSeries plotSeries = (LineSeries)plotModel.Series[0];

            plotSeries.Points.Add(new DataPoint(xValue, yValue));

            if (plotSeries.Points.Count >= _service.GetConfigurationInstance().MaxSamples)
            {
                plotSeries.Points.RemoveAt(0);
            }

            if (xValue >= plotModel.Axes[0].Maximum)
            {
                plotModel.Axes[0].Minimum = (xValue - _service.GetConfigurationInstance().XAxisMax);
                plotModel.Axes[0].Maximum = xValue + _service.GetConfigurationInstance().SampleTime / 1000.0;
            }

            plotModel.InvalidatePlot(true);

            return plotModel;
        }
        
    }
}
