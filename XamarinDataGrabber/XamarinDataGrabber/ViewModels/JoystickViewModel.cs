using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinDataGrabber.Interfaces;
using XamarinDataGrabber.Enums;
using System.Threading.Tasks;
using XamarinDataGrabber.Models;
using Newtonsoft.Json;
using XamarinDataGrabber.Services;
using System.Diagnostics;

namespace XamarinDataGrabber.ViewModels
{
    public class JoystickViewModel : BaseViewModel
    {

        private IServerService _server;
        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }
        #region Properties
        public bool IsListening {
            get
            {
                return _isListening;
            }
            set
            {
                _isListening = value;
                OnPropertyChanged("IsListening");
            }
        }
        public Color UpColor {
            get {
                return _upColor;
            }
            set {
                _upColor = value;
                OnPropertyChanged("UpColor");
            }
        }
        public Color LeftColor
        {
            get
            {
                return _leftColor;
            }
            set
            {
                _leftColor = value;
                OnPropertyChanged("LeftColor");
            }
        }
        public Color MiddleColor
        {
            get
            {
                return _middleColor;
            }
            set
            {
                _middleColor = value;
                OnPropertyChanged("MiddleColor");
            }
        }
        public Color RightColor
        {
            get
            {
                return _rightColor;
            }
            set
            {
                _rightColor = value;
                OnPropertyChanged("RightColor");
            }
        }

        public Color DownColor
        {
            get
            {
                return _downColor;
            }
            set
            {
                _downColor = value;
                OnPropertyChanged("DownColor");
            }
        }
        #endregion 
        private CancellationTokenSource _cts;
        private CancellationToken _ct;
        private Color _upColor;
        private Color _leftColor;
        private Color _middleColor;
        private Color _rightColor;
        private Color _downColor;
        private bool _isListening;

        public JoystickViewModel(IServerService server)
        {
            StartCommand = new Command(() => StartListening());
            StopCommand = new Command(() => StopListening());
            _server = server;
            IsListening = false;

            UpColor = Color.LightGray;
            LeftColor = Color.LightGray;
            MiddleColor = Color.LightGray;
            RightColor = Color.LightGray;
            DownColor = Color.LightGray;

            
            
        }
        public async void StartListening()
        {
            
            if (!IsListening)
            {
                _cts = new CancellationTokenSource();
                _ct = _cts.Token;
                IsListening = true;


                await JoystickLoop();
            }
            
                
        }

        private void StopListening()
        {
            
                _cts.Cancel();
                IsListening = false;
              
        }

        //Joystick listening loop run on other threads
        private async Task JoystickLoop()
        {
            while (!_ct.IsCancellationRequested)
            {
                
                var response = await _server.HandleGetRequest(HttpRequestsTypes.HttpGetJoystickData);
                JoystickModel responseJson = null;
                try
                {
                     responseJson = await Task.Run(() => JsonConvert.DeserializeObject<JoystickModel>(response));
                }
                catch (JsonSerializationException e)
                {
                    Debug.WriteLine(e);
                }


                if (responseJson == null)
                    continue;

                IndicateChange(responseJson);
                await Task.Delay(40);

            }
        }

        
        //Method updating BoxView colors based on joystick model
        private void IndicateChange(JoystickModel joystick)
        {
            switch(joystick.Direction)
            {
                case SenseTickDirections.Up:
                    switch (joystick.Action)
                    {
                        case SenseTickActions.Pressed:
                            UpColor = Color.Gray;
                            break;
                        case SenseTickActions.Held:
                            UpColor = Color.DimGray;
                            break;
                        case SenseTickActions.Released:
                            UpColor = Color.LightGray;
                            break;
                    }
                    break;

                case SenseTickDirections.Left:
                    switch (joystick.Action)
                    {
                        case SenseTickActions.Pressed:
                            LeftColor = Color.Gray;
                            break;
                        case SenseTickActions.Held:
                            LeftColor = Color.DimGray;
                            break;
                        case SenseTickActions.Released:
                            LeftColor = Color.LightGray;
                            break;
                    }
                    break;

                case SenseTickDirections.Middle:
                    switch (joystick.Action)
                    {
                        case SenseTickActions.Pressed:
                            MiddleColor = Color.Gray;
                            break;
                        case SenseTickActions.Held:
                            MiddleColor = Color.DimGray;
                            break;
                        case SenseTickActions.Released:
                            MiddleColor = Color.LightGray;
                            break;
                    }
                    break;

                case SenseTickDirections.Right:
                    switch (joystick.Action)
                    {
                        case SenseTickActions.Pressed:
                            RightColor = Color.Gray;
                            break;
                        case SenseTickActions.Held:
                            RightColor = Color.DimGray;
                            break;
                        case SenseTickActions.Released:
                            RightColor = Color.LightGray;
                            break;
                    }
                    break;

                case SenseTickDirections.Down:
                    switch (joystick.Action)
                    {
                        case SenseTickActions.Pressed:
                            DownColor = Color.Gray;
                            break;
                        case SenseTickActions.Held:
                            DownColor = Color.DimGray;
                            break;
                        case SenseTickActions.Released:
                            DownColor = Color.LightGray;
                            break;
                    }
                    break;
            }
        }
    }
}
