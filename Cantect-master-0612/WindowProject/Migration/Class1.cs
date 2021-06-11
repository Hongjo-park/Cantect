using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Migration
{
    
    public class Class1
    {
        [DllImport("User32.dll")]
        static extern void Keybd_event(byte vk, byte scan, int flag, int extra);

        public static void KeyDown(int Keycode)
        {
            Keybd_event((byte)Keycode, 0, 0, 0);
        }

        public static void KeyUp(int Keycode)
        {
            Keybd_event((byte)Keycode, 0, 2, 0);
        }

      
    }
}
