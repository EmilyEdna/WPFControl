using CandyControls.ControlsModel.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using XExten.Advance.LinqFramework;

namespace CandyControls
{
    public class CandyTabControl : TabControl
    {
        static CandyTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CandyTabControl), new FrameworkPropertyMetadata(typeof(CandyTabControl)));
        }

        public Brush BorderBackgroud
        {
            get { return (Brush)GetValue(BorderBackgroudProperty); }
            set { SetValue(BorderBackgroudProperty, value); }
        }
        public static readonly DependencyProperty BorderBackgroudProperty =
            DependencyProperty.Register("BorderBackgroud", typeof(Brush), typeof(CandyTabControl), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#20B3B3B3"))));

        public Brush HeadBackgroud
        {
            get { return (Brush)GetValue(HeadBackgroudProperty); }
            set { SetValue(HeadBackgroudProperty, value); }
        }
        public static readonly DependencyProperty HeadBackgroudProperty =
            DependencyProperty.Register("HeadBackgroud", typeof(Brush), typeof(CandyTabControl), new PropertyMetadata(Brushes.White));

        public Brush MoveBackgroud
        {
            get { return (Brush)GetValue(MoveBackgroudProperty); }
            set { SetValue(MoveBackgroudProperty, value); }
        }
        public static readonly DependencyProperty MoveBackgroudProperty =
            DependencyProperty.Register("MoveBackgroud", typeof(Brush), typeof(CandyTabControl), new PropertyMetadata(new LinearGradientBrush(
            [
                new GradientStop((Color)ColorConverter.ConvertFromString("#337AE2FD"), 0),
                new GradientStop((Color)ColorConverter.ConvertFromString("#33F1C6C6"), 1)
            ])));

        public Brush SelectBackgroud
        {
            get { return (Brush)GetValue(SelectBackgroudProperty); }
            set { SetValue(SelectBackgroudProperty, value); }
        }
        public static readonly DependencyProperty SelectBackgroudProperty =
            DependencyProperty.Register("SelectBackgroud", typeof(Brush), typeof(CandyTabControl), new PropertyMetadata(new LinearGradientBrush(
            [
                new GradientStop((Color)ColorConverter.ConvertFromString("#33F1C6C6"), 0),
                new GradientStop((Color)ColorConverter.ConvertFromString("#33A5F9C1"), 1)
            ])));
        public ECatagory BorderType
        {
            get { return (ECatagory)GetValue(BorderTypeProperty); }
            set { SetValue(BorderTypeProperty, value); }
        }
        public static readonly DependencyProperty BorderTypeProperty =
            DependencyProperty.Register("BorderType", typeof(ECatagory), typeof(CandyTabControl), new PropertyMetadata(ECatagory.Primary));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(CandyTabControl), new PropertyMetadata(new CornerRadius(3)));

        public EPlacement Placement
        {
            get { return (EPlacement)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }
        public static readonly DependencyProperty PlacementProperty =
            DependencyProperty.Register("Placement", typeof(EPlacement), typeof(CandyTabControl), new PropertyMetadata(EPlacement.Top));

        public bool UseViewBox
        {
            get { return (bool)GetValue(UseViewBoxProperty); }
            set { SetValue(UseViewBoxProperty, value); }
        }
        public static readonly DependencyProperty UseViewBoxProperty =
            DependencyProperty.Register("UseViewBox", typeof(bool), typeof(CandyTabControl), new PropertyMetadata(false));

        public ICommand OpenCommand
        {
            get { return (ICommand)GetValue(OpenCommandProperty); }
            set { SetValue(OpenCommandProperty, value); }
        }
        public static readonly DependencyProperty OpenCommandProperty =
            DependencyProperty.Register("OpenCommand", typeof(ICommand), typeof(CandyTabControl), new PropertyMetadata(default));

        private int _MenuItemSelectIndex = -1;

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            InitTabItemHandle();
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            if (e.Action == NotifyCollectionChangedAction.Add)
                this.SelectedIndex = Items.Count - 1;
            InitTabItemHandle();
        }

        private void InitTabItemHandle()
        {
            List<TabItem> TabItems = this.FindChildren<TabItem>();
            if (TabItems.Count > 0)
            {
                foreach (TabItem Item in TabItems)
                {
                    Item.Loaded -= TabItemLoadEvent;
                    Item.Loaded += TabItemLoadEvent;

                    Item.MouseRightButtonDown -= RightClickEvent;
                    Item.MouseRightButtonDown += RightClickEvent;

                    Item.ContextMenu.Items.OfType<MenuItem>().ForEnumerEach(Menu =>
                    {
                        var Flag = Menu.Tag.AsString().AsInt();
                        Menu.CommandParameter = Item;

                        if (Flag == 1)
                        {
                            Menu.Click -= CloseAllEvent;
                            Menu.Click += CloseAllEvent;
                        }
                        if (Flag == 2)
                        {
                            Menu.Click -= CloseOtherEvent;
                            Menu.Click += CloseOtherEvent;
                        }
                        if (Flag == 3)
                        {
                            Menu.Click -= OpenWindowEvent;
                            Menu.Click += OpenWindowEvent;
                        }
                    });

                }
            }
        }


        private void OpenWindowEvent(object sender, RoutedEventArgs e)
        {
            var Item = ((TabItem)((MenuItem)sender).CommandParameter);
            if (ItemsControl.ItemsControlFromItemContainer(Item) is not CandyTabControl parent) return;
            var current = parent.ItemContainerGenerator.ItemFromContainer(Item);
            parent.OpenCommand?.Execute(current);
        }

        private void CloseOtherEvent(object sender, RoutedEventArgs e)
        {
            var Item = ((TabItem)((MenuItem)sender).CommandParameter);
            if (ItemsControl.ItemsControlFromItemContainer(Item) is not CandyTabControl parent) return;
            if (parent.ItemsSource is IList list)
            {
                var Current = list[_MenuItemSelectIndex];
                for (var i = 0; i < list.Count; i++)
                {
                    var item = list[i];
                    if (!Equals(item, Current) && item != null)
                    {
                        if (ItemContainerGenerator.ContainerFromItem(item) is not TabItem tabItem) continue;
                        list.Remove(item);
                        i--;
                    }
                }
                parent.SelectedIndex = 0;
            }
        }

        private void CloseAllEvent(object sender, RoutedEventArgs e)
        {
            var Item = ((TabItem)((MenuItem)sender).CommandParameter);
            if (ItemsControl.ItemsControlFromItemContainer(Item) is not CandyTabControl parent) return;
            if (parent.ItemsSource is IList list)
                list.Clear();
            parent.SelectedIndex = -1;
        }

        private void RightClickEvent(object sender, MouseButtonEventArgs e)
        {
            TabItem Item = sender as TabItem;
            if (ItemsControl.ItemsControlFromItemContainer(Item) is not CandyTabControl parent) return;
            var current = parent.ItemContainerGenerator.ItemFromContainer(Item);
            if (parent.ItemsSource is IList list)
                _MenuItemSelectIndex = list.IndexOf(current);
        }

        private void TabItemLoadEvent(object sender, RoutedEventArgs e)
        {
            TabItem Item = sender as TabItem;
            Button Btn = (Button)Item.Template.FindName("PART_CLOSE", Item);
            Btn.Click -= CloseSingleEvent;
            Btn.Click += CloseSingleEvent;
        }

        private void CloseSingleEvent(object sender, RoutedEventArgs e)
        {
            var Item = ((TabItem)((Button)sender).CommandParameter);
            if (ItemsControl.ItemsControlFromItemContainer(Item) is not CandyTabControl parent) return;
            var current = parent.ItemContainerGenerator.ItemFromContainer(Item);
            if (parent.ItemsSource is IList list)
                list.Remove(current);
        }
    }
}
