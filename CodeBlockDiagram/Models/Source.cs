namespace CodeBlockDiagram {
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    ///
    public class Source {

        RegexOptions rop => RegexOptions.Singleline | RegexOptions.Compiled;
        Regex rNamespace => new Regex (@"namespace[ ]+([^ 　]+)*[ ]*{", rop);
        ///
        public FileInfo Fi { get; set; }
        ///
        public List<string> Namespaces { get; set; }
        ///コンストラクタ
        public Source (FileInfo srcFi) {
            loadSource (srcFi);
        }
        void loadSource (FileInfo srcFi) {
            Fi = srcFi;
            Namespaces = getNameSpaces (srcFi).ToList ();
        }

        IEnumerable<string> getNameSpaces (FileInfo fi) {
            var buf = File.ReadAllText (fi.FullName);
            var mats = rNamespace.Matches (buf.ToString ());
            foreach (Match mat in mats) {
                yield return mat.Groups[1].Value.Trim ();
            }
        }
    }
}