using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinDataGrabber.Helpers
{
    public static class EnumParser
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
        }

    }
}
