using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinDataGrabber.Interfaces
{
    public interface ILedConfiguration : IConfiguration
    {
        int PosX { get; set; }
        int PosY { get; set; }
        int R { get; set; }
        int G { get; set; }
        int B { get; set; }
    }
}
