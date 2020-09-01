using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using XamarinDataGrabber.Interfaces;


//Using DenpendencyService to get external storage path from Android OS to use it in Xamarin.Forms
[assembly: Dependency(typeof(XamarinDataGrabber.Droid.FileSystemImplementation))]
namespace XamarinDataGrabber.Droid
{
    class FileSystemImplementation : IFileSystem
    {
        public string GetExternalStorage()
        {
            Context context = Android.App.Application.Context;
            var filePath = context.GetExternalFilesDir("");
            return filePath.Path;
        }
    }
}