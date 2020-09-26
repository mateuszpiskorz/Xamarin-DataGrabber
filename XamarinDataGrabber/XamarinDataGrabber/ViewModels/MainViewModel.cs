using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using System.Windows.Input;

namespace XamarinDataGrabber.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand ChangeViewEventHandler { get; private set; }
        

        private void ChangedView()
        {
            Debug.WriteLine("View Changed");
        }
        
    }
}
