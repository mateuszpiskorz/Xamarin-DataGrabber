using System;
using System.Collections.Generic;
using System.Text;
using XamarinDataGrabber.Interfaces;

namespace XamarinDataGrabber.Models
{
    class LedModel : ILedConfiguration
    {
        int _posX, _posY;
        byte _r, _g, _b;
        public int PosX { get { return _posX; }  set { _posX = value; } }
        public int PosY { get { return _posY; }  set { _posY = value; } }
        public byte R { get { return _r; } set { _r = value; } }
        public byte G { get { return _g; } set { _g = value; } }
        public byte B { get { return _b; } set { _b = value; } }


        public LedModel(int posx, int posy)
        {
            PosX = posx;
            PosY = posy;
            R = DefaultParams.defaultLedColor[0];
            G = DefaultParams.defaultLedColor[1];
            B = DefaultParams.defaultLedColor[2];

        }

        public LedModel(int posx, int posy, byte r, byte g, byte b)
        {
            PosX = posx;
            PosY = posy;
            R = r;
            G = g;
            B = b;
        }

        public void SetDefaultSettings()
        {
            //TODO: Set Default Colors for the Diode
        }

        
    }
}
