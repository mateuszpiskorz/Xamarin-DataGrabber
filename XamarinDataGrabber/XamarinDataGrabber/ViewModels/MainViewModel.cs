using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace XamarinDataGrabber.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        //TODO: Connection handling with server
        //TODO: TabbedPage CurrentPage problem to disconnect from the server in case of switching to Settings tab.
        public MainViewModel()
        {
            Debug.WriteLine("Im Created MVM");
            MessagingCenter.Subscribe<LedViewModel,string>(this, "Hi", (sender,arg) =>
              {
                  Debug.WriteLine("Message Received!"); //DEBUG
                  Debug.WriteLine(arg); // DEBUG
                  //TODO: Implement HTTP REQUEST
              });
        }
        
    }
}
