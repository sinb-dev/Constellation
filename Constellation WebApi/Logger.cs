using System;
using System.IO;

namespace Constellation_WebApi
{
    enum LogType {None, Info, Warn, Error}
    public static class Logger
    {
        const string filename = "constellation.log";
        public static void Error(string message)
        {
            log(message,LogType.Error);

        }
        public static void Error(string message,Exception exception)
        {
            Error(message+"\\n"+exception.Message+"\\n"+exception.StackTrace);
        }
        public static void Log(string message)
        {
            log(message,LogType.Info);
        }
        public static void Warn(string message)
        {
            log(message,LogType.Warn);
        }
        static void log(string message, LogType type)
        {
            string date = DateTime.Now.ToString();
            string logtype = "";
            switch (type)
            {
                case LogType.Error: logtype = "ERROR"; break;
                case LogType.Warn: logtype = "WARN"; break;
                case LogType.Info: logtype = "INFO"; break;
            }

            string logline = $"[{logtype}][{date}]: {message}";
            File.AppendAllText(filename,logline);
        }
    }
}