using System.ComponentModel;
using System.Globalization;
using System.Linq;
using CandyControls.ControlsModel.Thicks;

namespace CandyControls.Converters
{
    internal class WidthHeightConverter: TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value.ToString().Contains(","))
            {
                var wh = value.ToString().Split(",");
                return new WidthHeightStruct(int.Parse(wh.First()), int.Parse(wh.Last()));
            }
            else
                return new WidthHeightStruct(int.Parse(value.ToString()));
        }
    }
}
