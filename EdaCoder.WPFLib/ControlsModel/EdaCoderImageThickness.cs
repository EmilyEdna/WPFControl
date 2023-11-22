using System.ComponentModel;
using EdaCoder.WPFLib.Converters;

namespace EdaCoder.WPFLib.ControlsModel
{
    [TypeConverter(typeof(EdaCoderImageThicknessConverter))]
    public struct EdaCoderImageThickness
    {
        public EdaCoderImageThickness(int length) 
        {
            Width = length;
            Height = length;
        }

        public EdaCoderImageThickness(int w, int h)
        {
            Width=w; Height=h;
        }
        
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
