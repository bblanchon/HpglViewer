using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hpgl
{
    public static class HpglUnit
    {
        public static double PluToMm(double value)
        {
            return value / 40;
        }

        public static string PluToReadableString(double value)
        {
            var mm = PluToMm(value);
            var factor = 1.0;
            var unit = "mm";

            if (mm > 1000000)
            {
                factor = 1000000;
                unit = "km";
            }
            else if (mm > 1000)
            {
                factor = 1000;
                unit = "m";
            }
            else if (mm > 100)
            {
                factor = 10;
                unit = "cm";
            }

            return string.Format("{0:0.##} {1}", mm/factor, unit);
        }
    }
}
