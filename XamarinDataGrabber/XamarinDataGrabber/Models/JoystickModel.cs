using System;
using System.Collections.Generic;
using System.Text;
using XamarinDataGrabber.Enums;
using Newtonsoft.Json;
using XamarinDataGrabber.Services;

namespace XamarinDataGrabber.Models
{
    class JoystickModel
    {
        public SenseTickActions Action { get; set; }
        public SenseTickDirections Direction { get; set; }

        public JoystickModel()
        {

        }

        [JsonConstructor]
        public JoystickModel(string action, string direction)
        {
            Action = EnumParser.ParseEnum<SenseTickActions>(action);
            Direction = EnumParser.ParseEnum<SenseTickDirections>(direction);
        }
    }
}
