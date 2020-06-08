namespace CodeBlockDiagram {
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Text;
    using System.Threading.Tasks;
    using System;

    ///
    public static class Extentions {
        ///
        public static string ExePath => Path.GetDirectoryName (System.Reflection.Assembly.GetExecutingAssembly ().CodeBase);
        ///
        public static FileInfo GetFi (string path) {
            FileInfo fi;
            if (string.IsNullOrEmpty (path)) {
                fi = new FileInfo (Path.GetTempFileName ());
                return fi;
            }
            fi = new FileInfo (path);
            if (!fi.Directory.Exists) {
                fi.Directory.Create ();
            }
            return fi;
        }
        static Encoding Enc => Encoding.GetEncoding (encName);
        static string encName => "utf-8";
        ///
        public static async Task WriteTextAsync (this FileInfo fi, string arg, Encoding _enc = null, string _newLineStr = null) {
            if (!fi.Directory.Exists) {
                fi.Directory.Create ();
            }
            var newLineStr = _newLineStr ?? Environment.NewLine;
            arg = Regex.Replace (arg, "\r\n|\r|\n", newLineStr);
            var enc = _enc??Enc;
            var buf = enc.GetBytes (arg);
            using (var fs = new FileStream (fi.FullName, FileMode.Append, FileAccess.Write, FileShare.None, bufferSize : 4096, useAsync : true)) {
                await fs.WriteAsync (buf, 0, buf.Length).ConfigureAwait (false);
            }
        }
        static object dumpLock = new object ();
        ///
        public static T Dump<T> (this T obj, string msg = "", string filePath = null) {
            var buf = $@"{obj}
";
            FileInfo fi;
            fi = GetFi (filePath);
            lock (dumpLock) {
                fi.WriteTextAsync (buf).Wait ();
            }
            return obj;
        }
    }
}