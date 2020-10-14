using System;
using System.Configuration;
using System.IO;
using System.Text;

namespace ConsoleChess
{
    /// <summary>
    /// Class to write logs of pieces
    /// </summary>
    public class WriteLog
    {
        /// <summary>
        /// Path logs
        /// </summary>
        private static string PathLog = Path.GetTempPath() + "ChessFolder\\";

        /// <summary>
        /// File name
        /// </summary>
        private static string FileName = "ChessLog";

        /// <summary>
        /// Checks if there is folder
        /// </summary>
        /// <param name="text">Text to write</param>
        public static void ChecksLogs(StringBuilder text)
        {
            int last = 1;
            if (Directory.Exists(PathLog))
            {
                string[] files = Directory.GetFiles(PathLog);

                foreach (string file in files)
                {
                    DateTime today = DateTime.Now;
                    DateTime lastWrite = File.GetLastWriteTime(file);

                    if (today.Subtract(lastWrite).TotalDays >= 2)
                    {
                        string name = Path.GetFileNameWithoutExtension(file);
                        int sequence = int.Parse(name.Replace(FileName, ""));
                        if (sequence > last)
                        {
                            last = sequence+1;
                        }
                    }
                }

                string newFile = string.Concat(@PathLog, FileName, last, ".txt");
                if (files.Length < 1)
                {
                    FileStream f = File.Create(newFile);
                    f.Close();
                }

                WriteLogs(newFile, text);
            }
            else
            {
                Directory.CreateDirectory(string.Concat(PathLog));
                string newFile = string.Concat(@PathLog, FileName, last, ".txt");
                FileStream f = File.Create(newFile);
                f.Close();
                WriteLogs(newFile, text);
            }
        }

        /// <summary>
        /// Write logs
        /// </summary>
        /// <param name="path">Path of file</param>
        /// <param name="text">Text to write</param>
        public static void WriteLogs(string path, StringBuilder text)
        {
            using (StreamWriter sw = File.AppendText(@path))
            {
                sw.WriteLine(text);
            }
        }
    }
}
