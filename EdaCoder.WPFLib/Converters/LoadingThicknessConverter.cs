using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdaCoder.WPFLib.ControlsModel;

namespace EdaCoder.WPFLib.Converters
{
    public class LoadingThicknessConverter: TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value.ToString().Contains(","))
            {
                var wh = value.ToString().Split(",");
                return new ImageThickness(int.Parse(wh.First()), int.Parse(wh.Last()));
            }
            else
                return new ImageThickness(int.Parse(value.ToString()));
        }
    }
}
