using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinDataGrabber.Interfaces
{
    public interface IConfigurationModel : IConfiguration
    {
         string IpAddress { get; set; }
         string IpPort { get; set; }
         string ApiVersion { get; set; }
         int MaxSamples { get; set; }
         int SampleTime { get; set; }
    }
}
