using System;
using System.Collections.Generic;
using System.Text;
using XamarinDataGrabber.Models;
using XamarinDataGrabber.Interfaces;

namespace XamarinDataGrabber
{
    static class Factory
    {
        public static IConfigurationModel CreateConfiguration()
        {
            return new ConfigModel();
        }

        public static ILedConfiguration CreateLedInstance(int _posx, int _posy, int _r, int _g, int _b)
        {
            return new LedModel(_posx, _posy, _r, _g, _b);
        }
        
        
    }
}
