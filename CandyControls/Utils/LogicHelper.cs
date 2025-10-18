using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using XExten.Advance.LinqFramework;

namespace CandyControls
{
    public static class LogicHelper
    {
        /// <summary>
        /// 查找子控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<T> FindLogicChildren<T>(this DependencyObject obj, string name = "") where T : FrameworkElement
        {
            try
            {
                List<T> TList = [];
                LogicalTreeHelper.GetChildren(obj).OfType<DependencyObject>().ForEnumerEach(child =>
                {
                    if (child is not null and T)
                    {
                        TList.Add((T)child);
                        List<T> childOfChildren = FindLogicChildren<T>(child);
                        if (childOfChildren != null)
                        {
                            TList.AddRange(childOfChildren);
                        }
                    }
                    else
                    {
                        List<T> childOfChildren = FindLogicChildren<T>(child);
                        if (childOfChildren != null)
                        {
                            TList.AddRange(childOfChildren);
                        }
                    }
                });
                if (!string.IsNullOrEmpty(name))
                    return TList.Where(t => t.Name.Equals(name)).ToList();
                return TList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 查找单个子控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T FindLogicChild<T>(this DependencyObject obj, string name) where T : FrameworkElement
        {

          var Element =  LogicalTreeHelper.GetChildren(obj).OfType<DependencyObject>().ToList();

            for (int i = 0; i < Element.Count; i++)
            {
                DependencyObject child = Element.ElementAtOrDefault(i);
                if (child != null && child is T t && t.Name.Equals(name))
                    return t;
                else
                {
                    T childOfChild = FindLogicChild<T>(child, name);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return default;
        }
        /// <summary>
        /// 获得指定元素的父元素
        /// </summary>
        /// <typeparam name="T">指定页面元素</typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T FindLogicParent<T>(this DependencyObject obj) where T : FrameworkElement
        {
            DependencyObject parent = LogicalTreeHelper.GetParent(obj);

            while (parent != null)
            {
                if (parent is T t)
                {
                    return t;
                }
                parent = LogicalTreeHelper.GetParent(parent);
            }
            return null;
        }

        /// <summary>
        /// 获得指定元素的父元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T FindLogicParent<T>(this DependencyObject obj, string name) where T : FrameworkElement
        {
            DependencyObject parent = LogicalTreeHelper.GetParent(obj);
            while (parent != null)
            {
                if (parent is T t && ((T)parent).Name.Equals(name))
                {
                    return t;
                }
                parent = LogicalTreeHelper.GetParent(parent);
            }
            return null;
        }
    }
}
