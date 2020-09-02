using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;
using XamarinDataGrabber.Interfaces;

namespace XamarinDataGrabber.Models
{
    class ConfigModel : IConfigurationModel
    {
        
        #region Properties
        public string IpAddress { get; set; }
        public string IpPort { get; set; }
        public string ApiVersion { get; set; }
        public int MaxSamples { get; set; }
        public int SampleTime { get; set; }

        public string FilePath { get; set; }
        public bool DoesExist { get; set; }
        #endregion


        public ConfigModel()
        {
            //Getting folder path using DependencyService from XamarinDataGrabber.Droid
            var folderPath = DependencyService.Get<IFileSystem>().GetExternalStorage();
            FilePath = Path.Combine(folderPath, "config.json");
            DoesExist = File.Exists(FilePath);

            //Checking if file exists
            if (DoesExist)
            {
                //If it does. Read values
                this.ReadConfig();
                               
            }
            else //If doesn't assign default values
            {
                //Initializing default values for data.
                this.SetDefaultConfig();
                      
            }
            
        }

        //Constructor used when deserializing json string to XamarinDataGrabber.Models.DataModel object using Newtonsoft.Json
        [JsonConstructor]
        public ConfigModel(string ipAddress, string ipPort, string apiVersion, int maxSamples, int sampleTime)
        {
            IpAddress = ipAddress;
            IpPort = ipPort;
            ApiVersion = apiVersion;
            MaxSamples = maxSamples;
            SampleTime = sampleTime;
            

        }

        //Setting default values to class fields and writing them to json configuration file
        public void SetDefaultConfig()
        {
            IpAddress = DefaultParams.defaultIpAdress;
            IpPort = DefaultParams.defaultIpPort;
            ApiVersion = DefaultParams.defaultApiVersion;
            MaxSamples = DefaultParams.defaultMaxSamples;
            SampleTime = DefaultParams.defaultSampleTime;

            this.WriteConfig();
        }

        //Writing class fields values to json configuration file
        public void WriteConfig()
        {
            try
            {
                using (var writer = new StreamWriter(FilePath))
                {
                    string jsonString = JsonConvert.SerializeObject(this);
                    writer.WriteLine(jsonString);
                    
                }

            }
            catch (Exception exc)
            {
                Debug.WriteLine("Could not create or write to config file: ");
                Debug.Write(exc);
            }
        }

        //Reading json configuration file to populate fields
        public void ReadConfig()
        {
            string output = "";
            try
            {
                using (var reader = new StreamReader(FilePath))
                {
                    output = reader.ReadToEnd();
                }

                ConfigModel obj = JsonConvert.DeserializeObject<ConfigModel>(output);
                IpAddress = obj.IpAddress;
                IpPort = obj.IpPort;
                ApiVersion = obj.ApiVersion;
                MaxSamples = obj.MaxSamples;
                SampleTime = obj.SampleTime;
            }
            catch (Exception exc)
            {
                Debug.WriteLine("Could not read configuration file. Thrown exception: ");
                Debug.Write(exc);
            }

        }

        
    }
}
