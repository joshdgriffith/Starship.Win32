using System;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interop;
using Starship.Core.Extensions;

namespace Starship.Win32.Extensions {
    public static class WPFExtensions {

        public static void Bind<T>(this FrameworkElement element, DependencyProperty property, T target, Expression<Func<T, object>> expression) {
            var member = expression.GetMember().Name;

            element.SetBinding(property, new Binding {
                Path = new PropertyPath(member),
                Source = target
            });
        }

        public static void MakeTransparent(this Window window) {
            if (!window.IsInitialized) {
                throw new Exception("The extension method MakeWindowTransparent can not be called prior to the window being initialized.");
            }

            var hwnd = new WindowInteropHelper(window).Handle;
            var extendedStyle = WindowsAPI.GetWindowLongPtr(hwnd, (int)WindowsAPI.GWL.GWL_EXSTYLE);
            WindowsAPI.SetWindowLongPtr(hwnd, (int)WindowsAPI.GWL.GWL_EXSTYLE, new IntPtr(extendedStyle.ToInt32() | WindowsAPI.WsExTransparent));
        }

        public static void UI(this ContentControl control, Action action) {
            control.Dispatcher.Invoke(action);
        }
        
        public static void SetControlSize(this ContentControl control, double width, double height) {
            control.UI(() => {
                control.Width = width;
                control.Height = height;
            });
        }

        public static void SetWindowPosition(this Window window, double x, double y) {
            window.UI(() => {
                window.Left = x;
                window.Top = y;
            });
        }

        public static void SetWindowParent(this Window window, IntPtr parentHandle) {
            new WindowInteropHelper(window).Owner = parentHandle;
        }
    }
}