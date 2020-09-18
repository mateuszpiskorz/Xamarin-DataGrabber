using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinDataGrabber.Interfaces
{
    public interface IDataServiceProvider
    {
        IConfigurationModel GetConfigurationInstance();
        IList<ILedConfiguration> GetLedMatrix();
    }
}
