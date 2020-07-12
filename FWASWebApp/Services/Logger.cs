using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace FWASWebApp.Services {
    public static class Logger {
        public static bool Enable { get; set; }

        private static string _fileName = "session.log";
        
        // private static bool _isOpen = false;

        private static StreamWriter _file;

        private static async Task Initialise() {
            var time = DateTime.Now;
            _fileName = @$"C:\MyLogs\Log-{time.Day}-{time.Month}-{time.Year}_{time.Hour}-{time.Minute}-{time.Second}.log";
            _file = new StreamWriter(_fileName);
            await _file.WriteAsync("Time\t\t\tType\tAction");
            // _isOpen = true;
        }

        public static void Log(string text, LogMode logMode = LogMode.Verbose) {
            // if (!_isOpen) await Initialise();
            var time = DateTime.Now;
            var t = $"{time.Day}/{time.Month}/{time.Year},{time.Hour}:" +
                       $"{time.Minute}.{time.Millisecond}";
            PrintLog(t, logMode, text);
            // text = t + $"\t{logMode}\t" + text;
            // await _file.WriteLineAsync(text);
        }

        private static void PrintLog(string time, LogMode logMode, string log) {
            Console.Write(time + "\t");
            Console.ForegroundColor = logMode switch {
                LogMode.Info => ConsoleColor.Green,
                LogMode.Error => ConsoleColor.Red,
                LogMode.Verbose => ConsoleColor.Cyan,
                LogMode.Warning => ConsoleColor.Yellow,
                _ => ConsoleColor.White
            };
            Console.Write(logMode);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"\t{log}\n");
        }
        
    }

    public enum LogMode {
        [Description("V")] Verbose,
        [Description("I")] Info,
        [Description("E")] Error,
        [Description("W")] Warning
        
        
    }
}