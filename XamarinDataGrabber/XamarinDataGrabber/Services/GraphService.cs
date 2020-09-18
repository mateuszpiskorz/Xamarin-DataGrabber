using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Text;
using XamarinDataGrabber.Interfaces;

namespace XamarinDataGrabber.Services
{
    public class GraphService : IGraphServiceProvider
    {
        IDataServiceProvider _service;
        public GraphService(IDataServiceProvider service)
        {
            _service = service;
        }

        public PlotModel CreateTimePlot(string plotTitle,  string yAxisTitle, string yAxisUnit, double yAxisMinimum, double yAxisMaximum, string seriesTitle, OxyColor seriesColor )
        {
            PlotModel plot = new PlotModel() { Title = plotTitle };
            plot.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = _service.GetConfigurationInstance().MaxSamples,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"
            });
            plot.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = yAxisMinimum,
                Maximum = yAxisMaximum,
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
    }
}
