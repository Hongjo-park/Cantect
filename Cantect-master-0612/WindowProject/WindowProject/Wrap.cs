using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowProject
{
    /// <summary>
    /// Keyboard Event enum
    /// </summary>
    public enum KeyFlag
    {
        /// <summary>
        /// Key Down
        /// </summary>
        KE_DOWN = 0,
        /// <summary>
        /// Extnded Key
        /// </summary>
        KE__EXTENDEDKEY = 1,
        /// <summary>
        /// Key UP
        /// </summary>
        KE_UP = 2
    }

    /// <summary>
    /// Mouse Event enum
    /// </summary>
    public enum MouseFlag
    {
        /// <summary>
        /// Mouse move
        /// </summary>
        ME_MOVE = 1,
        /// <summary>
        /// Mouse Left button down
        /// </summary>
        ME_LEFTDOWN = 2,
        /// <summary>
        /// Mouse Left button up
        /// </summary>
        ME_LEFTUP = 4,
        /// <summary>
        /// Mouse Right button down
        /// </summary>
        ME_RIGHTDOWN = 8,
        /// <summary>
        /// Mouse Right button up
        /// </summary>
        ME_RIGHTUP = 0x10,
        /// <summary>
        /// Mouse middle button down
        /// </summary>
        ME_MIDDLEDOWN = 0x20,
        /// <summary>
        /// Mouse middle button up
        /// </summary>
        ME_MIDDLEUP = 0x40,
        /// <summary>
        /// Mouse wheel
        /// </summary>
        ME_WHEEL = 0x800,
        /// <summary>
        /// Mouse absoulute
        /// </summary>
        ME_ABSOULUTE = 8000
    }
    /// <summary>
    /// Win32 API 래핑 클래스 ( Key, Mouse Event )
    /// </summary>
    public static class Wrap
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int flag, int dx, int dy, int buttons, int extra);
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point point);
        [DllImport("user32.dll")]
        static extern int SetCursorPos(int x, int y);
        [DllImport("User32.dll")]
        static extern void keybd_event(byte vk, byte scan, int flags, int extra);

        /// <summary>
        /// KeyDown Method
        /// </summary>
        /// <param name="keycode">키</param>
        public static void KeyDown(int keycode)
        {
            keybd_event((byte)keycode, 0, (int)KeyFlag.KE_DOWN, 0);
        }
        /// <summary>
        /// KeyUp Method
        /// </summary>
        /// <param name="keycode">키</param>
        public static void KeyUp(int keycode)
        {
            keybd_event((byte)keycode, 0, (int)KeyFlag.KE_UP, 0);
        }
        /// <summary>
        /// set mouse point method
        /// </summary>
        /// <param name="x">바꿀 X 좌표</param>
        /// <param name="y">바꿀 Y 좌표</param>
        public static void Move(int x, int y)
        {
            SetCursorPos(x, y);
        }
        /// <summary>
        /// Point -> mouse 좌표
        /// </summary>
        /// <param name="pt">바꿀 포인트</param>
        public static void Move(Point pt)
        {
            Move(pt.X, pt.Y);
        }
        /// <summary>
        /// mouse left down
        /// </summary>
        public static void LeftDown()
        {
            mouse_event((int)MouseFlag.ME_LEFTDOWN, 0, 0, 0, 0);
        }
        /// <summary>
        /// mouse left up
        /// </summary>
        public static void LeftUp()
        {
            mouse_event((int)MouseFlag.ME_LEFTUP, 0, 0, 0, 0);
        }
    }
}
