using CandyControls.ControlsModel.Enums;
using CandyControls.ControlsModel.Thicks;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using XExten.Advance.LinqFramework;

namespace CandyControls.Converters
{
    internal class WindowHandleConverter : TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value.ToString().Contains(","))
            {
                var res = value.ToString().Split(",");

                var input = res.FirstOrDefault().AsInt();
                if (input != 0)
                    return new WindowHandleStruct((EWindowHandle)input, res.Last().AsBool());
                else
                    return new WindowHandleStruct(res.First().AsBool(), res.ElementAt(1).AsBool(), res.ElementAt(2).AsBool(), res.Last().AsBool());
            }
            else
                return new WindowHandleStruct(value.ToString().AsBool());
        }
    }
}
