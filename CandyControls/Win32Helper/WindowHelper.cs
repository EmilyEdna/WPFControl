using System;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace CandyControls
{
    internal class WindowHelper
    {

        [DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        private static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        internal static void SetBlurWindow(Window window, int Opacity = 0)
        {
            var help = new WindowInteropHelper(window);

            // 操作系统版本判定。
            var osVersion = Environment.OSVersion.Version;
            var windows10_1809 = new Version(10, 0, 17763);
            var windows10 = new Version(10, 0);

            // 创建 AccentPolicy 对象。
            var accent = new AccentPolicy();

             if (osVersion > windows10_1809)
            {
                accent.AccentState = AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND;
                accent.GradientColor = Opacity;
            }
            else if (osVersion > windows10)
                accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;
            else
                return;

            // 将托管结构转换为非托管对象。
            var accentPolicySize = Marshal.SizeOf(accent);
            var accentPtr = Marshal.AllocHGlobal(accentPolicySize);
            Marshal.StructureToPtr(accent, accentPtr, false);
            // 设置模糊特效。
            var data = new WindowCompositionAttributeData
            {
                Attribute = 19,
                SizeOfData = accentPolicySize,
                Data = accentPtr,
            };
            SetWindowCompositionAttribute(help.Handle, ref data);
            Marshal.FreeHGlobal(accentPtr);
        }

        internal enum AccentState
        {
            /// <summary>
            /// 完全禁用 DWM 的叠加特效。
            /// </summary>
            ACCENT_DISABLED = 0,
            /// <summary>
            /// GradientColor 颜色（失焦后边框为深色）
            /// </summary>
            ACCENT_ENABLE_GRADIENT = 1,
            /// <summary>
            /// 主题色（失焦后边框为深色）
            /// </summary>
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            /// <summary>
            /// 模糊特效（失焦后边框为灰色）
            /// </summary>
            ACCENT_ENABLE_BLURBEHIND = 3,
            /// <summary>
            /// 与 GradientColor 叠加颜色的亚克力特效
            /// </summary>
            ACCENT_ENABLE_ACRYLICBLURBEHIND = 4,
            /// <summary>
            /// 黑色（边框为纯白色）
            /// </summary>
            ACCENT_INVALID_STATE = 5,
        }

        internal struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

        internal struct WindowCompositionAttributeData
        {
            public int Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }
    }
}
