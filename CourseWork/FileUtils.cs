using System;
using System.Collections.Generic;
using System.IO;

namespace CourseWork
{
    public class FileUtils {
        // Основной фильтр расширений
        public static readonly HashSet<string> EXTENSIONS_FILTER = new HashSet<string>
        {
            ".jpg", ".png", ".gif", ".psd", ".tga", ".bmp", ".ico",
            ".jpeg", ".svg", ".tiff", ".tif", ".jpe", ".jfif",
            ".raw", ".ai", ".rle", ".mp4", ".avi", ".mpeg", ".mpg",
            ".vcd", ".vid", ".vob", ".swf", ".webm", ".vob",
            ".wm", ".wmv", ".yuv", ".dat", ".f4v", ".asx", ".3g2",
            ".3gp", ".asf", ".mkv", ".rm", ".aif", ".amr",
            ".aob", ".asf", ".aud", ".flac", ".iff", ".m3u", ".m3u8",
            ".m4a", ".m4b", ".mid", ".midi", ".mod", ".mp3",
            ".mpa", ".ogg", ".wav", ".wave", ".ra", ".wma", ".asp",
            ".aspx", ".doc", ".docx", ".docm", ".dot", ".dotm",
            ".dotx", ".epub", ".gpx", ".key", ".mobi", ".djv", ".djvu",
            ".pages", ".pdf", ".pps", ".ppsm", ".ppsx",
            ".ppt", ".pptm", ".pptx", ".rtf", ".xls", ".xml", ".xlsm",
            ".xlsb", ".xlsx", ".xlt", ".xltm", ".xltx",
            ".xps", ".odt", ".indd", ".pif", ".rar", ".zip", ".7z", ".cab",
            ".cbr", ".cbz", ".gz", ".jar", ".gzip", ".arj",
            ".pkg", ".pak", ".tar", ".tar-gz", ".tgz", ".xar", ".zipx",
            ".spl", ".ace", ".tmp", ".shs", ".xps", ".cf",
            ".log", ".txt", ".dt", ".cfu", ".mxl", ".1cd", ".efd", ".mft",
            ".pff", ".st", ".grs", ".erf", ".epf",
            ".elf", ".lgf", ".cdn", ".ps", ".bat", ".cmd", ".yaml",
            ".lock", ".json", ".cfg", ".vbs", ".db", ".data",
            ".ini", ".sig", ".ftl", ".sqlite", ".msg", ".scr", ".theme",
            ".html", ".htm", ".hta", ".pub", ".vss",
            ".bak", ".url", ".cdw", ".dwg", ".dxf", ".jgs", ".sat",
            ".cs", ".py", ".rs", ".js", ".ts", ".bin",
        };

        // Вспомогательная функция для рекурсивного обхода файловой системы
        public static void recursiveWalker (string startPath, Action<string> callback, ICollection<string> filter)
        {
            try
            {                
                foreach (var directory in Directory.GetDirectories(startPath))
                {
                    recursiveWalker(directory, callback, filter);
                }
                
                foreach (var file in Directory.GetFiles(startPath))
                {
                    if (!filter.Contains(new FileInfo(file).Extension.ToLower())) continue;
                    callback(file);
                }           
            }
            catch (Exception) {}
        }
    }
}