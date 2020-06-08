﻿using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace TGenWebApp.Services {
    public static class Logger {
        public static bool Enable { get; set; }

        private static string _fileName = "session.log";
        
        private static bool _isOpen = false;

        private static StreamWriter _file;

        private static async Task Initialise() {
            var time = DateTime.Now;
            _fileName = @$"C:\MyLogs\Log-{time.Day}-{time.Month}-{time.Year}_{time.Hour}-{time.Minute}-{time.Second}.log";
            _file = new StreamWriter(_fileName);
            await _file.WriteAsync("Time\t\t\tType\tAction");
            _isOpen = true;
        }

        public static async Task Log(string text, LogMode logMode = LogMode.Verbose) {
            if (!_isOpen) await Initialise();
            var time = DateTime.Now;
            text = $"{time.Day}/{time.Month}/{time.Year},{time.Hour}:" +
                   $"{time.Minute}.{time.Millisecond}\t{logMode}\t" + text;
            await _file.WriteLineAsync(text);
        }
        
    }

    public enum LogMode {
        [Description("V")] Verbose,
        [Description("I")] Information,
        [Description("E")] Error,
        [Description("W")] Warning
        
        
    }
}