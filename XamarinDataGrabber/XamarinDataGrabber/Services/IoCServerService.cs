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
        private HttpClient _client;
        public IoCServerService(IDataServiceProvider service, HttpClient client)
        {
            _service = service.GetConfigurationInstance();
            _client = client;
        }

        //Method handling HTTP GET request
        public async Task<string> HandleGetRequest()
        {
            string responseText = null;

            try
            {
               
                    responseText = await _client.GetStringAsync(GetServerUrl("GET"));
                
            }
            catch(Exception e)
            {
                Debug.WriteLine("Get Request Error: ");
                Debug.WriteLine(e);
            }

            return responseText;
        }

        //Method handling HTTP POST requests containg led configuration data
        public async Task<string> HandlePostRequest(IList<ILedConfiguration> data)
        {
            string responseText = null;

            try
            { 
                    var requestDataCollection = new FormUrlEncodedContent(GenerateKeyValuePair(data));
                    var responseMessage = await _client.PostAsync(GetServerUrl("POST"), requestDataCollection );
                    responseText = await responseMessage.Content.ReadAsStringAsync();
                    if (String.IsNullOrEmpty(responseText)) Debug.WriteLine("HTTP POST response text is null");    
            }
            catch (Exception e)
            {
                Debug.WriteLine("HTTP POST request error: ");
                Debug.WriteLine(e);
            }

            return responseText;
            

        }

        //Method used to convert list of led configuraiton to keyvaluepair collection
        private List<KeyValuePair<string, string>> GenerateKeyValuePair(IList<ILedConfiguration> data)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    int[] pos = { i, j };
                    byte[] color = { data[DimensionsConverter.ConvertDimensions(i, j)].R, data[DimensionsConverter.ConvertDimensions(i, j)].G, data[DimensionsConverter.ConvertDimensions(i, j)].B };

                    result.Add(new KeyValuePair<string, string>(pos.ToString(), color.ToString()));
                    
                }
            }

            return result;
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

        
        
    }
}
