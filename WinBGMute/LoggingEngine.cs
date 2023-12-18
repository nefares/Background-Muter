/*
 *  Background Muter - Automatically mute background applications
 *  Copyright (C) 2022  Nefares (nefares@protonmail.com) github.com/nefares
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WinBGMuter
{
    internal class LoggingEngine
    {
        public delegate void _LogFunction(object input, object? color = null, object? font = null);
        public delegate void _LogLineFunction(object input, object? color = null, object? font = null);

        public static _LogFunction m_logFunction = DefaultLogFunction;
        public static _LogLineFunction m_logLineFunction = DefaultLogLineFunction;

        public enum LOG_LEVEL_TYPE {LOG_NONE, LOG_ERROR, LOG_WARNING, LOG_INFO, LOG_DEBUG };
        public static bool Enabled { get; set; }

        public static LOG_LEVEL_TYPE LogLevel { get; set; }

        public static bool HasDateTime {  get; set; }    

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeConsole();

        public static void DefaultLogFunction(object input, object? color = null, object? font = null)
        {
            try
            {
                Console.Write(input);
            }
            catch (Exception ex)
            {

            }
        }
        public static void DefaultLogLineFunction(object input, object? color = null, object? font = null)
        {
            try
            {
                Console.WriteLine(input);
            }
            catch (Exception ex)
            {

            }

        }

        public static void RestoreDefault()
        {
            AllocConsole();
            m_logFunction = DefaultLogFunction;
            m_logLineFunction = DefaultLogLineFunction;
            LogLevel = LOG_LEVEL_TYPE.LOG_DEBUG;
        }

        public static void SetEngine(_LogFunction logfn, _LogLineFunction loglinefn)
        {
            FreeConsole();
            m_logFunction = logfn;
            m_logLineFunction = loglinefn;
        }

        private static string FormatInput(object input, LOG_LEVEL_TYPE loglevel = LOG_LEVEL_TYPE.LOG_DEBUG)
        {
            string output = "";
            output += HasDateTime ? DateTime.Now.ToString("dd/MM/yyyy H:mm:ss:fff") : "";
            output += " > ";
            output += input;

            return output;
        }
        public static void Log(object input, object? color = null, object? font = null, LOG_LEVEL_TYPE loglevel = LOG_LEVEL_TYPE.LOG_DEBUG)
        {
            if (!Enabled)
                return;

            //todo: add a separate function which does this instead of duplicating it
            if ((loglevel > LogLevel) || (LogLevel == LOG_LEVEL_TYPE.LOG_NONE))
            {
                return;
            }

            
            m_logFunction(input, color, font);
        }

        public static void LogLine(object input, object? color = null, object? font = null, LOG_LEVEL_TYPE loglevel=LOG_LEVEL_TYPE.LOG_DEBUG)
        {
            if (!Enabled)
                return;

            if ((loglevel > LogLevel) || (LogLevel == LOG_LEVEL_TYPE.LOG_NONE))
            {
                return;
            }

            m_logLineFunction(FormatInput(input), color, font);
        }

    }
}
