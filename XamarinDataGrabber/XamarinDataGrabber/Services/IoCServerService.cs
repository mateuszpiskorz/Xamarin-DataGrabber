using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

        //Method handling HTTP GET request
        public async Task<String> HandleGetRequest()
        {
            string responseText = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    responseText = await client.GetStringAsync(GetServerUrl("GET"));
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine("Get Request Error: ");
                Debug.WriteLine(e);
            }

            return responseText;
        }

        //Method providing url for HTTP request taking in account wanted request type
        private string GetServerUrl(string requestType)
        {
            if (requestType == "GET")
            {
                return $"http://{_service.IpAddress}:{_service.IpPort}?request=GET";
            }
            else
            {
                return $"http://{_service.IpAddress}:{_service.IpPort}/index.php";
            }
            
        }

        
        //TODO: GET and POST methods
    }
}
