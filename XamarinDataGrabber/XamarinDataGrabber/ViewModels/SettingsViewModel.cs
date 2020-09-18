using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinDataGrabber.Models;
using XamarinDataGrabber.Interfaces;
using System.Diagnostics;

namespace XamarinDataGrabber.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Fields
        string _ipAddress, _ipPort, _apiVersion;
        int _maxSamples, _sampleTime;
        IConfigurationModel _context;
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
        
        public SettingsViewModel(IDataServiceProvider service)
        {
            //Creating Database instance
            _context = service.GetConfigurationInstance();

            //Setting initial values to fields
            _ipAddress = _context.IpAddress;
            _ipPort = _context.IpPort;          //CHECK IF ITS GOOD ???
            _apiVersion = _context.ApiVersion;
            _maxSamples = _context.MaxSamples;
            _sampleTime = _context.SampleTime;


            //Creating Commands for View buttons
            DefaultCommand = new Command(() => { this.SetDefaultSettingsButton(); });
            SaveCommand = new Command(() => { this.SaveSettingsButton(); });
            Debug.WriteLine("Im created SVM!");
        }

        //Default Button method
        private void SetDefaultSettingsButton()
        {
            //Setting default config to datamodel
            _context.SetDefaultConfig();

            IpAddress = _context.IpAddress;
            IpPort = _context.IpPort;
            ApiVersion = _context.ApiVersion;
            MaxSamples = _context.MaxSamples;
            SampleTime = _context.SampleTime;

        }

        //Save Button method
        private void SaveSettingsButton()
        {
            //Saving data to datamodel
            _context.IpAddress = IpAddress;
            _context.IpPort = IpPort;
            _context.ApiVersion = ApiVersion;
            _context.SampleTime = SampleTime;
            _context.MaxSamples = MaxSamples;

            _context.WriteConfig();
        }
         
    }
}
