using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using Starship.Core.Windows;
using Starship.Win32.Input;
using Starship.Win32.Native;
using Point = System.Windows.Point;
using Rectangle = Starship.Core.Math.Rectangle;

namespace Starship.Win32 {
    public class WindowInstance {

        public WindowInstance(IntPtr handle) {
            Handle = handle;
        }

        static WindowInstance() {
            Simulate = new InputSimulator();
        }

        public void Click(Point point, MouseButton button = MouseButton.LeftButton) {
            Click(point.X, point.Y, button);
        }

        public void Click(double x, double y, MouseButton button = MouseButton.LeftButton) {
            MoveMouseTo(x, y);
            Simulate.Mouse.LeftButtonDown();
            Thread.Sleep(250);
            Simulate.Mouse.LeftButtonUp();
        }

        public void MoveMouseTo(double x, double y) {
            x += DesktopX;
            y += DesktopY;

            var width = WindowsAPI.GetSystemMetrics(SystemMetrics.SM_CXSCREEN);
            var height = WindowsAPI.GetSystemMetrics(SystemMetrics.SM_CYSCREEN);

            var xOffset = x * (65536.0f / width);
            var yOffset = y * (65536.0f / height);

            Simulate.Mouse.MoveMouseTo(xOffset, yOffset);
        }

        public Image CaptureImage() {
            Update();
            return CaptureImage(0, 0, Width, Height);
        }

        public Image CaptureImage(int x, int y, int width = 0, int height = 0) {
            Update();
            
            var image = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            using (var graphics = Graphics.FromImage(image)) {
                graphics.CopyFromScreen(DesktopX + x, DesktopY + y, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
            }

            return image;
        }

        public WindowsAPI.WindowInfo GetWindowInfo() {
            var info = new WindowsAPI.WindowInfo();
            WindowsAPI.GetWindowInfo(Handle, ref info);
            return info;
        }

        public void Update() {
            var info = GetWindowInfo();
            var state = GetPlacement();
            
            State = (WindowStates) state.showCmd;
            X = info.rcClient.X;
            Y = info.rcClient.Y;
            DesktopX = info.rcWindow.X + (int) info.cxWindowBorders / 2 - 1;
            DesktopY = info.rcWindow.Y + (Y - info.rcWindow.Y);
            Width = info.rcClient.Width;
            Height = info.rcClient.Height;
            //ZIndex = GetZIndex();
        }

        public int GetZIndex() {
            return WindowsAPI.GetWindowZIndex(Handle);
        }

        public void SetPosition(int x, int y) {
            Update();
            X = x;
            Y = y;
            WindowsAPI.MoveWindow(Handle, x, y, Width, Height, true);
        }
        
        public WindowsAPI.WindowPlacement GetPlacement() {
            var placement = new WindowsAPI.WindowPlacement();
            WindowsAPI.GetWindowPlacement(Handle, ref placement);
            return placement;
        }

        public void BringToFront() {
            WindowsAPI.SetForegroundWindow(Handle);
            WindowsAPI.SendMessage(Handle, WindowsAPI.WM_SYSCOMMAND, WindowsAPI.SC_RESTORE, 0);
        }

        public Rectangle GetRect() {
            return GetWindowInfo().rcClient;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int DesktopX { get; set; }

        public int DesktopY { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public WindowStates State { get; set; }

        public bool IsActive { get; set; }

        public int ZIndex { get; set; }

        public IntPtr Handle { get; set; }

        private static InputSimulator Simulate { get; set; }
    }
}