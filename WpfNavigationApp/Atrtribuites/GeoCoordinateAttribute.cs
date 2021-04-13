using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WpfNavigationApp.Atrtribuites
{
    class GeoCoordinateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string coordinateString)
            {
                var isParsed = double.TryParse(coordinateString, out double cordinate);

                if (!isParsed) return false;


                return cordinate >= -180 && cordinate <= 180;
            }
            return false;
        }
    }
}
