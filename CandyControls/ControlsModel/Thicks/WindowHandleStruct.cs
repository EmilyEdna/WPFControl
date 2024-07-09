using CandyControls.Converters;
using System.ComponentModel;

namespace CandyControls.ControlsModel.Thicks
{
    [TypeConverter(typeof(WindowHandleConverter))]
    public class WindowHandleStruct
    {
        public bool IsSettingHidden { get; set; }
        public bool IsMinimizeHidden { get; set; }
        public bool IsMaximizeHidden { get; set; }
        public bool IsCloseHidden { get; set; }

        public WindowHandleStruct(bool Setting, bool Min, bool Max, bool Close)
        {
            this.IsSettingHidden = Setting;
            this.IsMinimizeHidden = Min;
            this.IsMaximizeHidden = Max;
            this.IsCloseHidden = Close;
        }
        public WindowHandleStruct(bool All)
        {
            this.IsSettingHidden = this.IsMinimizeHidden = this.IsMaximizeHidden = this.IsCloseHidden = All;
        }
    }
}
