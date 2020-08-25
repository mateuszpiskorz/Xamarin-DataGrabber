using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinDataGrabber.Models;

namespace XamarinDataGrabber.ViewModels
{
    class SettingsViewModel : BaseViewModel
    {
        #region Fields
        string _ipAddress, _ipPort, _apiVersion;
        int _maxSamples, _sampleTime;
        #endregion
        #region Properties
        public string IpAddress
        {
            get { return _ipAddress; }
            set
            {
                if (_ipAddress != value)
                {
                    _ipAddress = value;
                    OnPropertyChanged("IpAddress");
                }
            }
        }
        public string IpPort
        {
            get { return _ipPort; }
            set
            {
                if (_ipPort != value)
                {
                    _ipPort = value;
                    OnPropertyChanged("IpPort");
                }
            }
        }
        public string ApiVersion
        {
            get { return _apiVersion; }
            set
            {
                if (_apiVersion != value)
                {
                    _apiVersion = value;
                    OnPropertyChanged("ApiVersion");
                }
            }
        }
        public int MaxSamples
        {
            get { return _maxSamples; }
            set
            {
                if (_maxSamples != value)
                {
                    _maxSamples = value;
                    OnPropertyChanged("MaxSamples");
                }
            }
        }
        public int SampleTime
        {
            get { return _sampleTime; }
            set
            {
                if (_sampleTime != value)
                {
                    _sampleTime = value;
                    OnPropertyChanged("SampleTime");
                }
            }
        }
        public ICommand SaveCommand { get; set; }
        public ICommand DefaultCommand { get; set; }
        #endregion 
        
        public SettingsViewModel()
        {
            //Setting initial values to fields
            _ipAddress = DefaultConfigParams.defaultIpAdress;
            _ipPort = DefaultConfigParams.defaultIpPort;
            _apiVersion = DefaultConfigParams.defaultApiVersion;
            _maxSamples = DefaultConfigParams.defaultMaxSamples;
            _sampleTime = DefaultConfigParams.defaultSampleTime;


            //Creating Commands for View buttons
            DefaultCommand = new Command(() => { this.SetDefaultSettings(); });
            SaveCommand = new Command(() => { this.SaveSettings(); });
        }

        //Default Button method
        private void SetDefaultSettings()
        {
            IpAddress = DefaultConfigParams.defaultIpAdress;
            IpPort = DefaultConfigParams.defaultIpPort;
            ApiVersion = DefaultConfigParams.defaultApiVersion;
            MaxSamples = DefaultConfigParams.defaultMaxSamples;
            SampleTime = DefaultConfigParams.defaultSampleTime;
        }

        //Save Button method
        private void SaveSettings()
        {
            //TODO: Implement Saving Method
            //TOTHINK: using MessagingCenter and sending Message to MainViewModel where Http request are made???
        }
    }
}
