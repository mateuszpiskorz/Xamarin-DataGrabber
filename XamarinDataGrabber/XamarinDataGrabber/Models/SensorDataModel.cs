﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;

namespace XamarinDataGrabber.Models
{
    public class SensorDataModel
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }
        public SensorDataModel()
        {

        }

        [JsonConstructor]
        public SensorDataModel(string name, string value, string unit)
        {
            Name = name;
            Value = double.Parse(value, CultureInfo.InvariantCulture);
            Unit = unit;
        }

    }
}
