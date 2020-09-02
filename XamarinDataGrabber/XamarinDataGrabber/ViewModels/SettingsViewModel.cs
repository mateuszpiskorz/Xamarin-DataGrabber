using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinDataGrabber.Models;
using XamarinDataGrabber.Interfaces;

namespace XamarinDataGrabber.ViewModels
{
    class SettingsViewModel : BaseViewModel
    {
        #region Fields
        string _ipAddress, _ipPort, _apiVersion;
        int _maxSamples, _sampleTime;
        IConfigurationModel context;
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
            //Creating Database instance
            context = Factory.CreateConfiguration();

            //Setting initial values to fields
            _ipAddress = context.IpAddress;
            _ipPort = context.IpPort;
            _apiVersion = context.ApiVersion;
            _maxSamples = context.MaxSamples;
            _sampleTime = context.SampleTime;


            //Creating Commands for View buttons
            DefaultCommand = new Command(() => { this.SetDefaultSettingsButton(); });
            SaveCommand = new Command(() => { this.SaveSettingsButton(); });
        }

        //Default Button method
        private void SetDefaultSettingsButton()
        {
            //Setting default config to datamodel
            context.SetDefaultConfig();

            IpAddress = context.IpAddress;
            IpPort = context.IpPort;
            ApiVersion = context.ApiVersion;
            MaxSamples = context.MaxSamples;
            SampleTime = context.SampleTime;

        }

        //Save Button method
        private void SaveSettingsButton()
        {
            //Saving data to datamodel
            context.IpAddress = IpAddress;
            context.IpPort = IpPort;
            context.ApiVersion = ApiVersion;
            context.SampleTime = SampleTime;
            context.MaxSamples = MaxSamples;

            context.WriteConfig();
        }
         
    }
}
