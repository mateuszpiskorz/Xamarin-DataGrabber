﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinDataGrabber.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinDataGrabber
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            ContainerConfig.Configure();
            MainPage = new MainView();
            
                
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
