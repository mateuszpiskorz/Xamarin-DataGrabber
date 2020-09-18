using System;
using System.Collections.Generic;
using System.Text;
using XamarinDataGrabber.ViewModels;
using CommonServiceLocator;

namespace XamarinDataGrabber.ViewModels
{
    //Locator for CommonServiceLocator 
    //Uses ViewModels as properties for BindingContext
    //Created because BindingContext for Xaml accepts only parameterless constructors
    //IoC container Autofac needs constructor with interfaces as parameters to wire registered types as dependecies
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public SettingsViewModel SettingsViewModel {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsViewModel>();
            }
        }

        public LedViewModel LedViewModel {
            get
            {
                return ServiceLocator.Current.GetInstance<LedViewModel>();
            }
        }

        public GraphViewModel GraphViewModel {
            get
            {
                return ServiceLocator.Current.GetInstance<GraphViewModel>();
            }
        }
    }
}
