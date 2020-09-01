using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

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

        private string fileName;
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
            fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "config.json");
            doesExist = File.Exists(fileName);

            //Checking if file exists
            if (doesExist)
            {
                //If it does. Read values
                string output = "";
                try
                {
                    using (var reader = new StreamReader(fileName))
                    {
                        output = reader.ReadToEnd();
                    }
                    var obj = JsonConvert.DeserializeObject<DataModel>(output);
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
            else //If doesn't assign default values
            {
                //Initializing default values for data.
                IpAddress = DefaultConfigParams.defaultIpAdress;
                IpPort = DefaultConfigParams.defaultIpPort;
                ApiVersion = DefaultConfigParams.defaultApiVersion;
                MaxSamples = DefaultConfigParams.defaultMaxSamples;
                SampleTime = DefaultConfigParams.defaultSampleTime;

                try
                {

                }
                catch (Exception exc)
                {
                    Debug.WriteLine("Could not create or write to config file: ");
                    Debug.Write(exc);
                }


                using (var writer = File.CreateText(fileName))
                {
                    string jsonString = JsonConvert.SerializeObject(this);
                    writer.WriteLine(jsonString);
                }
            }
            
        }

        public void WriteConfig()
        {

        }

        public void ReadConfig()
        {


        }

        //TODO: DATA READING? Create instance of class or static methods

        //TODO: Writing/Reading data to/from JSON file
    }
}
