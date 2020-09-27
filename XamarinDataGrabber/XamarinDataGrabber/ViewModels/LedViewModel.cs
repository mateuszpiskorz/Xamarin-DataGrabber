using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinDataGrabber.Interfaces;
using XamarinDataGrabber.Models;
using XamarinDataGrabber.Services;
using XamarinDataGrabber.Enums;
using XamarinDataGrabber.Helpers;

namespace XamarinDataGrabber.ViewModels
{
    public class LedViewModel : BaseViewModel
    {

        #region Fields
        private IList<ILedConfiguration> _ledMatrix;
        private IServerService _server;
        private Color _currentColor;
        private double _rBrush, _gBrush, _bBrush;
        #endregion
        #region Properties

        public Grid LedGrid { get; private set; }
        public Color CurrentColor
        {   get
            {
                return _currentColor;
            }
            set
            {
                _currentColor = value;
                OnPropertyChanged("CurrentColor");
            }
        }
        public string RBrush
        {   get
            {
                return (_rBrush * 255).ToString();
            }
            set
            {
                if (double.TryParse(value, out double temp))
                {
                    double doubleVal = temp / 255;
                    if (_rBrush != doubleVal)
                    {
                        if (doubleVal > 1)
                        {
                            _rBrush = 1;
                        }
                        else if (doubleVal < 0)
                        {
                            _rBrush = 0;
                        }
                        else
                        {
                            _rBrush = doubleVal;
                        }
                        
                        CurrentColor = Color.FromRgba(_rBrush, _gBrush, _bBrush, DefaultParams.defaultLedColorAlpha);
                        OnPropertyChanged("RBrush");
                    }
                }
            }
        }

        public string GBrush
        {
            get
            {
                return (_gBrush * 255).ToString();
            }
            set
            {
                if (double.TryParse(value, out double temp))
                {
                    double doubleVal = temp / 255;
                    if (_gBrush != doubleVal)
                    {
                        if (doubleVal > 1)
                        {
                            _gBrush = 1;
                        }
                        else if (doubleVal < 0)
                        {
                            _gBrush = 0;
                        }
                        else
                        {
                            _gBrush = doubleVal;
                        }

                        CurrentColor = Color.FromRgba(_rBrush, _gBrush, _bBrush, DefaultParams.defaultLedColorAlpha);
                        OnPropertyChanged("GBrush");
                    }
                }
            }
        }
        public string BBrush
        {
            get
            {
                return (_bBrush * 255).ToString();
            }
            set
            {
                if (double.TryParse(value, out double temp))
                {
                    double doubleVal = temp / 255;
                    if (_bBrush != doubleVal)
                    {
                        if (doubleVal > 1)
                        {
                            _bBrush = 1;
                        }
                        else if (doubleVal < 0)
                        {
                            _bBrush = 0;
                        }
                        else
                        {
                            _bBrush = doubleVal;
                        }

                        CurrentColor = Color.FromRgba(_rBrush, _gBrush, _bBrush, DefaultParams.defaultLedColorAlpha);
                        OnPropertyChanged("BBrush");
                    }
                }
            }
        }
        public ICommand LedInteraction { get; set; }
        public ICommand SendCommand { get; set; }
        public ICommand DefaultCommand { get; set; }
        #endregion
        public LedViewModel(IDataServiceProvider dataService, IServerService server)
        {
            _ledMatrix = dataService.GetLedMatrix();
            _server = server;
            LedGrid = LedGridInitialzation();
            CurrentColor = SetDefaultColor();
            RBrush = DefaultParams.defaultLedColor[0].ToString();
            GBrush = DefaultParams.defaultLedColor[1].ToString();
            BBrush = DefaultParams.defaultLedColor[2].ToString();
            Debug.WriteLine("Im Created LVM"); //DEBUG
            

            DefaultCommand = new Command(() => ResetToDefault());
            SendCommand = new Command(() => SendData());
        }

        //Initializing Grid which contains all BoxViews(leds) 
        private Grid LedGridInitialzation()
        {
            Grid grid = new Grid()
            {

                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(55,5,5,5)

            };

            for (int i = 0; i <= 7; i++)
            {

                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                for (int j = 0; j <= 7; j++)
                {

                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                    BoxView box = new BoxView()
                    {

                        Color = SetDefaultColor(),
                        HeightRequest = 30,
                        WidthRequest = 30,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center

                    };

                    int[] positionArray = { i, j };
                    LedInteraction = new Command(() =>
                    {

                        LedOnClicked(box, positionArray);

                    });
                    TapGestureRecognizer tapped = new TapGestureRecognizer();
                    tapped.Command = LedInteraction;
                    box.GestureRecognizers.Add(tapped);

                    Grid.SetRow(box, i);
                    Grid.SetColumn(box, j);
                    grid.Children.Add(box);

                }
            }
            return grid;
        }
        

        //Setting Default Color of the led
        public Color SetDefaultColor()
        {
            return Color.FromRgba(DefaultParams.defaultLedColor[0], DefaultParams.defaultLedColor[1], DefaultParams.defaultLedColor[2], DefaultParams.defaultLedColorAlpha);
            
        }

        //Command executed when BoxView(Led) in GUI is tapped
        public void LedOnClicked(BoxView sender, int[] pos)
        {
            sender.Color = CurrentColor;
            var index = DimensionsConverter.ConvertDimensions(pos[0], pos[1]);
            _ledMatrix[DimensionsConverter.ConvertDimensions(pos[0], pos[1])].R = Byte.Parse(RBrush);
            _ledMatrix[DimensionsConverter.ConvertDimensions(pos[0], pos[1])].G = Byte.Parse(GBrush);
            _ledMatrix[DimensionsConverter.ConvertDimensions(pos[0], pos[1])].B = Byte.Parse(BBrush);
        }

        //Command Resetting all BoxViews(Leds) color to defaut. Executed when "Default" button is pressed
        public void ResetToDefault()
        {
            //Resetting color values for boxviews and led data list
            foreach (BoxView view in LedGrid.Children)
            {
                view.Color = SetDefaultColor();
                
                
            }
            foreach (ILedConfiguration led in _ledMatrix)
            {
                led.R = DefaultParams.defaultLedColor[0];
                led.G = DefaultParams.defaultLedColor[1];
                led.B = DefaultParams.defaultLedColor[2];

            }

            RBrush = DefaultParams.defaultLedColor[0].ToString();
            GBrush = DefaultParams.defaultLedColor[1].ToString();
            BBrush = DefaultParams.defaultLedColor[2].ToString();

            SendData();
        }
        

        //Method used to send led data
        public async void SendData()
        {
            var responseText = await _server.HandlePostRequest(_ledMatrix, HttpRequestsTypes.HttpPostLedData);
            Debug.WriteLine(responseText);  
        }

    }
}
