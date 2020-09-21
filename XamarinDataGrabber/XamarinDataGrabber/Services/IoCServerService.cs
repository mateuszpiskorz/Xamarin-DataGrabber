using System;
using System.Collections.Generic;
using System.Text;
using XamarinDataGrabber.Interfaces;

namespace XamarinDataGrabber.Services
{
    public class IoCServerService
    {
        
        private IConfigurationModel _service;
        public IoCServerService(IDataServiceProvider service)
        {
            _service = service.GetConfigurationInstance();
        }

        private string GetServerUrl()
        {
            return $"http://{_service.IpAddress}:{_service.IpPort}";
        }
        //TODO: GET and POST methods
    }
}
