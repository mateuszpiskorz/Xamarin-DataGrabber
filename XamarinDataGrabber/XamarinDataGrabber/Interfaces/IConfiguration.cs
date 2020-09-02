using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinDataGrabber.Interfaces
{
    public interface IConfiguration
    {
        string FilePath { get; set; }
        bool DoesExist { get; set; }

        void SetDefaultConfig();
        void WriteConfig();
        void ReadConfig();
    }
}
