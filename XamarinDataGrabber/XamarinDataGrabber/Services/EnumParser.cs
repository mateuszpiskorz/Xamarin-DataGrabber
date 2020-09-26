using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinDataGrabber.Services
{
    public static class EnumParser
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
        }

    }
}
