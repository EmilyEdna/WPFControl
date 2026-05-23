using CandyControls.ControlsModel.Enums;
using CandyControls.Converters;
using System.ComponentModel;

namespace CandyControls.ControlsModel.Thicks
{
    /// <summary>
    /// 窗口按钮控制结构
    /// </summary>
    [TypeConverter(typeof(WindowHandleConverter))]
    public struct WindowHandleStruct
    {
        /// <summary>
        /// 是否隐藏设置按钮
        /// </summary>
        public bool IsSettingHidden { get; set; }
        /// <summary>
        /// 是否隐藏最小化按钮
        /// </summary>
        public bool IsMinimizeHidden { get; set; }
        /// <summary>
        /// 是否隐藏最大化按钮
        /// </summary>
        public bool IsMaximizeHidden { get; set; }
        /// <summary>
        /// 是否隐藏关闭按钮
        /// </summary>
        public bool IsCloseHidden { get; set; }

        public WindowHandleStruct(EWindowHandle Handle, bool Input)
        {
            switch (Handle)
            {
                case EWindowHandle.Set:
                    this.IsSettingHidden = Input;
                    this.IsMinimizeHidden = this.IsMaximizeHidden = this.IsCloseHidden = !Input;
                    break;
                case EWindowHandle.Min:
                    this.IsMinimizeHidden = Input;
                    this.IsSettingHidden = this.IsMaximizeHidden = this.IsCloseHidden = !Input;
                    break;
                case EWindowHandle.Max:
                    this.IsMaximizeHidden = Input;
                    this.IsSettingHidden = this.IsMinimizeHidden = this.IsCloseHidden = !Input;
                    break;
                case EWindowHandle.Exit:
                    this.IsCloseHidden = Input;
                    this.IsSettingHidden = this.IsMinimizeHidden = this.IsMaximizeHidden = !Input;
                    break;
            }
        }
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
