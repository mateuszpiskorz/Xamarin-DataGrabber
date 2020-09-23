using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinDataGrabber.Services
{
    public static class DimensionsConverter
    {
        public static int ConvertDimensions(int rows, int cols)
        {
            return (rows * 8) + cols;
        }
    }
}
