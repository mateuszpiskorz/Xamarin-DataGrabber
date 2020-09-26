using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinDataGrabber.Models
{
    static class DefaultParams
    {
        
        public static readonly string defaultIpAdress = "192.168.0.20";
        public static readonly string defaultIpPort = "80";
        public static readonly string defaultApiVersion = "1.0.0";
        public static readonly int defaultSampleTime = 500;
        public static readonly int defaultMaxSamples = 100;
        public static readonly byte[] defaultLedColor = { 0, 0, 0 };
        public static readonly double defaultLedColorAlpha = 0.6;
    }
}
