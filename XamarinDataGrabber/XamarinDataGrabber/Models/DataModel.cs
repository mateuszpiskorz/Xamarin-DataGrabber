using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;
using XamarinDataGrabber.Interfaces;

namespace XamarinDataGrabber.Models
{
    class DataModel
    {
        #region Fields
        private string _ipAddress;
        private string _ipPort;
        private string _apiVersion;
        private int _maxSamples;
        private int _sampleTime;

        private string filePath;
        private bool doesExist;
        #endregion
        #region Properties
        public string IpAddress { get; set; }
        public string IpPort { get; set; }
        public string ApiVersion { get; set; }
        public int MaxSamples { get; set; }
        public int SampleTime { get; set; }
        #endregion


        public DataModel()
        {
            //filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "config.json");
            var folderPath = DependencyService.Get<IFileSystem>().GetExternalStorage();
            filePath = Path.Combine(folderPath, "config.json");
            doesExist = File.Exists(filePath);

            //Checking if file exists
            if (doesExist)
            {
                //If it does. Read values
                this.ReadConfig();
                //this.SetDefaultConfig();
               
            }
            else //If doesn't assign default values
            {
                //Initializing default values for data.
                this.SetDefaultConfig();
                

               
            }
            
        }

        //Constructor used when deserializing json string to object using Newtonsoft.Json
        [JsonConstructor]
        public DataModel(string ipAddress, string ipPort, string apiVersion, int maxSamples, int sampleTime)
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
            IpAddress = DefaultConfigParams.defaultIpAdress;
            IpPort = DefaultConfigParams.defaultIpPort;
            ApiVersion = DefaultConfigParams.defaultApiVersion;
            MaxSamples = DefaultConfigParams.defaultMaxSamples;
            SampleTime = DefaultConfigParams.defaultSampleTime;

            this.WriteConfig();
        }

        //Writing class fields values to json configuration file
        public void WriteConfig()
        {
            try
            {
                using (var writer = new StreamWriter(filePath))
                {
                    string jsonString = JsonConvert.SerializeObject(this);
                    writer.WriteLine(jsonString);
                    Debug.WriteLine(filePath);
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
                using (var reader = new StreamReader(filePath))
                {
                    output = reader.ReadToEnd();
                }
                DataModel obj = JsonConvert.DeserializeObject<DataModel>(output);
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
