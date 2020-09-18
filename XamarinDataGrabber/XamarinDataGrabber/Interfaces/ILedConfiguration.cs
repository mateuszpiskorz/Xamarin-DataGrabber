using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinDataGrabber.Interfaces
{
    public interface ILedConfiguration 
    {
        int PosX { get; set; }
        int PosY { get; set; }
        byte R { get; set; }
        byte G { get; set; }
        byte B { get; set; }
    }
}
