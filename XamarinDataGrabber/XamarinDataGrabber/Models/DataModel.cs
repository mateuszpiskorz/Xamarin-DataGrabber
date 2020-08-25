using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinDataGrabber.Models
{
    class DataModel
    {
        private string _ipAddress;
        private string _ipPort;
        private string _apiVersion;
        private int _maxSamples;
        private int _sampleTime;



        public DataModel()
        {
            //Initializing default values for data.
            IpAddress = DefaultConfigParams.defaultIpAdress;
            _ipPort = DefaultConfigParams.defaultIpPort;
            _apiVersion = DefaultConfigParams.defaultApiVersion;
            _maxSamples = DefaultConfigParams.defaultMaxSamples;
            _sampleTime = DefaultConfigParams.defaultSampleTime;
        }

        public string IpAddress { get => _ipAddress; set => _ipAddress = value; }

        //TODO: Writing/Reading data to/from JSON file
    }
}
