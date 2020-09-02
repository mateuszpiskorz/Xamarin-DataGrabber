using System;
using System.Collections.Generic;
using System.Text;
using XamarinDataGrabber.Interfaces;

namespace XamarinDataGrabber.Models
{
    class LedModel : ILedConfiguration
    {
        int _posX, _posY, _r, _g, _b;
        public int PosX { get { return _posX; }  set { _posX = value; } }
        public int PosY { get { return _posY; }  set { _posY = value; } }
        public int R { get { return _r; } set { _r = value; } }
        public int G { get { return _g; } set { _g = value; } }
        public int B { get { return _b; } set { _b = value; } }

        public string FilePath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DoesExist { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public LedModel(int posx, int posy, int r, int g, int b)
        {
            PosX = posx;
            PosY = posy;
            R = r;
            G = g;
            B = b;
        }

        public void SetDefaultConfig()
        {
            throw new NotImplementedException();
        }

        public void WriteConfig()
        {
            throw new NotImplementedException();
        }

        public void ReadConfig()
        {
            throw new NotImplementedException();
        }
    }
}
