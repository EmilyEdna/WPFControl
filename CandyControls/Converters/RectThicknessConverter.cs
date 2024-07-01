using CandyControls.ControlsModel.Thicks;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace CandyControls.Converters
{
    public class RectThicknessConverter: TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value.ToString().Contains(","))
            {
                var wh = value.ToString().Split(",");
                return new RectThickness(int.Parse(wh.First()), int.Parse(wh.Last()));
            }
            else
                return new RectThickness(int.Parse(value.ToString()));
        }
    }
}
