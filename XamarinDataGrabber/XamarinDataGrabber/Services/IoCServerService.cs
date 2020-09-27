using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamarinDataGrabber.Interfaces;
using XamarinDataGrabber.Enums;
using Newtonsoft.Json;
using XamarinDataGrabber.Models;
using XamarinDataGrabber.Helpers;

namespace XamarinDataGrabber.Services
{
    public class IoCServerService : IServerService
    {
        
        private IConfigurationModel _service;
        private HttpClient _client;
        public IoCServerService(IDataServiceProvider service, HttpClient client)
        {
            _service = service.GetConfigurationInstance();
            _client = client;
        }

        //Method handling HTTP GET request
        public async Task<string> HandleGetRequest(HttpRequestsTypes requestType)
        {
            string responseText = null;

            try
            {
                if (requestType == HttpRequestsTypes.HttpGetSensorData)
                    responseText = await _client.GetStringAsync(GetServerUrl(HttpRequestsTypes.HttpGetSensorData));
                else
                    responseText = await _client.GetStringAsync(GetServerUrl(HttpRequestsTypes.HttpGetJoystickData));
                   
            }
            catch(Exception e)
            {
                Debug.WriteLine("Get Request Error: ");
                Debug.WriteLine(e);
            }

            return responseText;
        }

        //Method handling HTTP POST requests containg led configuration data
        public async Task<string> HandlePostRequest(IList<ILedConfiguration> data, HttpRequestsTypes requestType)
        {
            string responseText = null;

            try
            { 
                    var requestDataCollection = new FormUrlEncodedContent(GenerateKeyValuePair(data));
                    var responseMessage = await _client.PostAsync(GetServerUrl(requestType), requestDataCollection );
                    responseText = await responseMessage.Content.ReadAsStringAsync();
                    Debug.WriteLine(responseText);
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
        private Dictionary<string, string> GenerateKeyValuePair(IList<ILedConfiguration> data)
        {
            var jsonLed = JsonConvert.SerializeObject(GenerateIntArrayFromRgb(data));
            return new Dictionary<string, string>() { {"ledData",jsonLed } };      
        }

        private int[] GenerateIntArrayFromRgb(IList<ILedConfiguration> data)
        {
            int[] temp = new int[64];
            for (int i = 0; i <= data.Count - 1; i ++)
            {
                int rgbVal = ((data[i].R & 0x00ff) << 16) | ((data[i].G & 0x00ff) << 8) | (data[i].B & 0x00ff);
                temp[i] = rgbVal;
            }

            return temp;
        }

        //Method providing url for HTTP request taking in account wanted request type
        private string GetServerUrl(HttpRequestsTypes requestType)
        {
            if (requestType == HttpRequestsTypes.HttpGetSensorData)
            {
                return $"http://{_service.IpAddress}:{_service.IpPort}/?request=GETSens";
            }
            else if (requestType == HttpRequestsTypes.HttpGetJoystickData)
            {
                return $"http://{_service.IpAddress}:{_service.IpPort}/?request=GETJoy";
            }
            else
            {
                return $"http://{_service.IpAddress}:{_service.IpPort}/index.php";
            }
            
        }

        
        
    }
}
