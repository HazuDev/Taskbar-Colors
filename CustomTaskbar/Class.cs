using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.Win32;
using System.Runtime.CompilerServices;

namespace Taskbar
{
    internal class TaskbarColors
    {
        public static System.Drawing.Color GetTaskbarColor(string color)
        {
            switch (color)
            {
                case "Pink": return System.Drawing.Color.Pink;
                case "White": return System.Drawing.Color.White;
                case "Orange": return System.Drawing.Color.Orange;
                case "Blue": return System.Drawing.Color.Blue;
                case "Red": return System.Drawing.Color.Red;
                case "Violet": return System.Drawing.Color.BlueViolet;
                case "Black": return System.Drawing.Color.Black;
                default: return System.Drawing.Color.Black;
            }
        }
    }
    
    internal class CustomTaskbar
    {
        private const int COLOR_ACT_CAPTION = 2;
        private const int COLOR_INACT_CAPTION = 3;
        private const int WM_SYSCOLOR = 0x0015;

        [DllImport("user32.dll")]
        private static extern bool SetSysColors(int cElements, int[] lpaElements, uint[] lpaRgbValues);

        public static void ChangeTaskbarColor(System.Drawing.Color color)
        {
            int[] elements = { COLOR_ACT_CAPTION, COLOR_INACT_CAPTION };
            uint[] rgbValues = { (uint)color.ToArgb(), (uint)color.ToArgb() };
            SetSysColors(elements.Length, elements, rgbValues);
            SendMessage((IntPtr)0xffff, WM_SYSCOLOR, IntPtr.Zero, IntPtr.Zero);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
    }

    internal class CustomTaskbarWin11
    {
        public static void ChangeTaskbarColor(System.Drawing.Color color)
        {
            string currentTheme = Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", 1).ToString();
            string registryPath = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent";

            if(currentTheme == "0")
            {
                Registry.SetValue(registryPath, "AccentColor", color.ToArgb(), RegistryValueKind.DWord);

            } else
            {
                Registry.SetValue(registryPath, "AccentColorLight", color.ToArgb(), RegistryValueKind.DWord);
            }

            SendMessage((IntPtr)0xffff, WM_SETTING, IntPtr.Zero, IntPtr.Zero);
        }

        private const int WM_SETTING = 0x001A;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
    }
}
