using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Taskbar
{
    class Program
    {
        static void Main(string[] args)
        {
            string? color;

            Console.Write("(Colors: Pink, Black, Orange, Blue, White, Red, Violet)\n> Color : ");
            color = Console.ReadLine();

            if(color != null)
            {
                System.Drawing.Color myColor = TaskbarColors.GetTaskbarColor(color);

                if(Environment.OSVersion.Version.Major == 11)
                {
                    CustomTaskbarWin11.ChangeTaskbarColor(myColor);
                } else
                {
                    CustomTaskbar.ChangeTaskbarColor(myColor);
                }
            }

            Console.Write("> Finished? ");
            Console.ReadKey();
        }
    }
}