using System.ComponentModel;
using CandyControls.Converters;

namespace CandyControls.ControlsModel.Thicks
{
    [TypeConverter(typeof(WidthHeightConverter))]
    public struct WidthHeightStruct
    {
        public WidthHeightStruct(int length) 
        {
            Width = length;
            Height = length;
        }

        public WidthHeightStruct(int width, int height)
        {
            Width= width; Height= height;
        }
        
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
