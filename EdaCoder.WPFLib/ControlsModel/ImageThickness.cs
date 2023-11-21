using System.ComponentModel;
using EdaCoder.WPFLib.Converters;

namespace EdaCoder.WPFLib.ControlsModel
{
    [TypeConverter(typeof(ImageThicknessConverter))]
    public struct ImageThickness
    {
        public ImageThickness(int length) 
        {
            Width = length;
            Height = length;
        }

        public ImageThickness(int w, int h)
        {
            Width=w; Height=h;
        }
        
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
