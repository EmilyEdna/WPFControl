using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using CandyControls.ControlsModel.Thicks;
using XExten.Advance.LinqFramework;

namespace CandyControls
{
    public class CandyContainer : Control
    {
        static CandyContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CandyContainer), new FrameworkPropertyMetadata(typeof(CandyContainer)));
        }
        private WrapPanel PART_CONTENT;
        private Rectangle PART_RECT;
        public override void OnApplyTemplate()
        {
            PART_CONTENT = (WrapPanel)this.Template.FindName("PART_CONTENT", this);
            PART_RECT = (Rectangle)this.Template.FindName("PART_RECT", this);
            PART_RECT.RadiusX = RectRadius.Width;
            PART_RECT.RadiusY = RectRadius.Height;
        }

        #region Property
        /// <summary>
        /// 圆角
        /// </summary>
        public CornerRadius BorderRadius
        {
            get { return (CornerRadius)GetValue(BorderRadiusProperty); }
            set { SetValue(BorderRadiusProperty, value); }
        }
        public static readonly DependencyProperty BorderRadiusProperty =
            DependencyProperty.Register("BorderRadius", typeof(CornerRadius), typeof(CandyContainer), new PropertyMetadata(default));
        /// <summary>
        /// 区域边框
        /// </summary>
        public RectThickness RectRadius
        {
            get { return (RectThickness)GetValue(RectRadiusProperty); }
            set { SetValue(RectRadiusProperty, value); }
        }
        public static readonly DependencyProperty RectRadiusProperty =
            DependencyProperty.Register("RectRadius", typeof(RectThickness), typeof(CandyContainer), new PropertyMetadata(new RectThickness(10, 10)));
        /// <summary>
        /// 标签
        /// </summary>
        public IEnumerable<string> TagSource
        {
            get { return (IEnumerable<string>)GetValue(TagSourceProperty); }
            set { SetValue(TagSourceProperty, value); }
        }
        public static readonly DependencyProperty TagSourceProperty =
            DependencyProperty.Register("TagSource", typeof(IEnumerable<string>), typeof(CandyContainer), new FrameworkPropertyMetadata(OnItemChanged));
        #endregion

        #region 方法
        private static void OnItemChanged(DependencyObject dp, DependencyPropertyChangedEventArgs eve)
        {
            var Ctrl = (CandyContainer)dp;
            Ctrl.Dispatcher.BeginInvoke(async () =>
            {
                await Task.Delay(50);
                Ctrl.TagSource.ForEnumerEach(item => {
                    Ctrl.PART_CONTENT.Children.Add(new CandyTag { Content= item, Margin = new Thickness(5)});
                });
            });
        }
        #endregion
    }
}
