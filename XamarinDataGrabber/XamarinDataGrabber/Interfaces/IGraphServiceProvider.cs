﻿using OxyPlot;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinDataGrabber.Interfaces
{
    public interface IGraphServiceProvider
    {
        PlotModel CreateTimePlot(string plotTitle, string yAxisTitle, string yAxisUnit, double yAxisMinimum, double yAxisMaximum, string seriesTitle, OxyColor seriesColor, int majorStep);
        PlotModel UpdateChart(PlotModel plotModel, double xValue, double yValue);
    }
}
