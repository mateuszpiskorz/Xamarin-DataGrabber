using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using XamarinDataGrabber.Interfaces;
using XamarinDataGrabber.Models;
using XamarinDataGrabber.ViewModels;

namespace XamarinDataGrabber
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            //Models
            builder.RegisterType<LedModel>().As<ILedConfiguration>();
            builder.RegisterType<ConfigModel>().As<IConfigurationModel>();

            //ViewModels
            builder.RegisterType<SettingsViewModel>().AsSelf();

            IContainer container = builder.Build();


            //Setting services locator to abstract ViewModels creation using Autofac
            //Due to Xamarin architecture the only way i've found to do this
            AutofacServiceLocator asl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => asl);

            return container;
            
            
        }
    }
}
