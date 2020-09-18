using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinDataGrabber.Interfaces;
using XamarinDataGrabber.Models;

namespace XamarinDataGrabber.Services
{
    public class DataService : IDataServiceProvider
    {
        private IList<ILedConfiguration> _ledMatrix = new List<ILedConfiguration>();
        public DataService()
        {
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    _ledMatrix.Add(new LedModel(i, j));
                }
            }
        }
        public IConfigurationModel GetConfigurationInstance()
        {
            return new ConfigModel();
        }

        public IList<ILedConfiguration> GetLedMatrix()
        {

            return _ledMatrix;
        }

        
    }
}
