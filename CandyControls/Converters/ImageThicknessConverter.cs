using System.ComponentModel;
using System.Globalization;
using System.Linq;
using CandyControls.ControlsModel.Thicks;

namespace CandyControls.Converters
{
    public class ImageThicknessConverter: TypeConverter
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
