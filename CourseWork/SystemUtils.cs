using System;
using System.Runtime.InteropServices;

namespace CourseWork
{
    public class SystemUtils
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow ();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow (IntPtr hWnd, int nCmdShow);
        
        public static void hideWindow ()
        {
            ShowWindow(GetConsoleWindow(), 0);
        }
        
        public static void showWindow ()
        {
            ShowWindow(GetConsoleWindow(), 1);
        }
    }
}