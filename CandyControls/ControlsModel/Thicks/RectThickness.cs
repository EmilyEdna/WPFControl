using System.ComponentModel;
using CandyControls.Converters;

namespace CandyControls.ControlsModel.Thicks
{
    [TypeConverter(typeof(RectThicknessConverter))]
    public struct RectThickness
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public RectThickness(double w, double h)
        {
            this.Width = w;
            this.Height = h;
        }
        public RectThickness(double len)
        {
            this.Height = this.Width = len;
        }
    }
}
