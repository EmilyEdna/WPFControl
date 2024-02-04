using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using CandyControls.ControlsModel.Enums;

namespace CandyControls
{
    public class CandyBeveledBorder : Decorator
    {

        static CandyBeveledBorder()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CandyBeveledBorder), new FrameworkPropertyMetadata(typeof(CandyBeveledBorder)));
        }
        public ECatagory BorderType
        {
            get { return (ECatagory)GetValue(BorderTypeProperty); }
            set { SetValue(BorderTypeProperty, value); }
        }

        /// <summary>
        /// [Primary] [Info] [Success] [Warn] [Error]
        /// </summary>
        public static readonly DependencyProperty BorderTypeProperty =
            DependencyProperty.Register("BorderType", typeof(ECatagory), typeof(CandyBeveledBorder), new PropertyMetadata(ECatagory.Primary));

        public Brush BorderBrush
        {
            get { return (Brush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }

        public static readonly DependencyProperty BorderBrushProperty =
            Border.BorderBrushProperty.AddOwner(typeof(CandyBeveledBorder), new PropertyMetadata(Brushes.Transparent, CommonPropertyChanged));

        public Thickness BorderThickness
        {
            get { return (Thickness)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        public static readonly DependencyProperty BorderThicknessProperty =
            Border.BorderThicknessProperty.AddOwner(typeof(CandyBeveledBorder), new PropertyMetadata(new Thickness(), CommonPropertyChanged));

        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public static readonly DependencyProperty BackgroundProperty =
            Control.BackgroundProperty.AddOwner(typeof(CandyBeveledBorder), new PropertyMetadata(Brushes.Transparent, CommonPropertyChanged));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(CandyBeveledBorder), new PropertyMetadata(new CornerRadius(), CommonPropertyChanged));


        private static void CommonPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as CandyBeveledBorder)._isrendersizechanged = true;
            (d as CandyBeveledBorder).InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (_isrendersizechanged)
            {
                _isrendersizechanged = false;
                UpdateGeometry(RenderSize);
            }

            Pen pTop = new Pen(BorderBrush, BorderThickness.Top);
            drawingContext.DrawGeometry(Background, pTop, _currenGeometry);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            _isrendersizechanged = true;
        }

        private void UpdateGeometry(Size nsize)
        {
            if (_currenGeometry == null)
            {
                _currenGeometry = new PathGeometry();
                _currenGeometry.Figures.Add(new PathFigure());
                _currenGeometry.Figures[0].IsClosed = true;
            }
            else
            {
                _currenGeometry.Figures[0].Segments.Clear();
            }

            if (CornerRadius == null)
            {
                _currenGeometry.Figures[0].StartPoint = new Point(0, 0);
                _currenGeometry.Figures[0].Segments.Add(ToLineSegment(new Point(nsize.Width, 0))); _currenGeometry.Figures[0].Segments.Add(ToLineSegment(new Point(nsize.Width, nsize.Height))); _currenGeometry.Figures[0].Segments.Add(ToLineSegment(new Point(0, nsize.Height)));
            }
            else
            {
                _currenGeometry.Figures[0].StartPoint = new Point(CornerRadius.TopLeft, 0);

                if (CornerRadius.TopRight <= 0)
                {
                    _currenGeometry.Figures[0].Segments.Add(ToLineSegment(new Point(nsize.Width, 0)));
                }
                else
                {
                    _currenGeometry.Figures[0].Segments.Add(ToLineSegment(new Point(nsize.Width - CornerRadius.TopRight, 0)));
                    _currenGeometry.Figures[0].Segments.Add(ToLineSegment(new Point(nsize.Width, CornerRadius.TopRight)));
                }

                if (CornerRadius.BottomRight <= 0)
                {
                    _currenGeometry.Figures[0].Segments.Add(ToLineSegment(new Point(nsize.Width, nsize.Height)));
                }
                else
                {
                    _currenGeometry.Figures[0].Segments.Add(ToLineSegment(new Point(nsize.Width, nsize.Height - CornerRadius.BottomRight)));
                    _currenGeometry.Figures[0].Segments.Add(ToLineSegment(new Point(nsize.Width - CornerRadius.BottomRight, nsize.Height)));
                }

                if (CornerRadius.BottomLeft <= 0)
                {
                    _currenGeometry.Figures[0].Segments.Add(ToLineSegment(new Point(0, nsize.Height)));
                }
                else
                {
                    _currenGeometry.Figures[0].Segments.Add(ToLineSegment(new Point(CornerRadius.BottomLeft, nsize.Height)));
                    _currenGeometry.Figures[0].Segments.Add(ToLineSegment(new Point(0, nsize.Height - CornerRadius.BottomLeft)));
                }

                if (CornerRadius.TopLeft > 0)
                {
                    _currenGeometry.Figures[0].Segments.Add(ToLineSegment(new Point(0, CornerRadius.TopLeft)));
                }
            }
        }

        private LineSegment ToLineSegment(Point pt)
        {
            return new LineSegment(pt, true);
        }


        private PathGeometry _currenGeometry = null;
        private bool _isrendersizechanged = true;
    }
}
