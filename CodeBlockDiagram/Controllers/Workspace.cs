namespace CodeBlockDiagram {
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using System;

    ///
    public class Workspace {
        System.Text.Encoding Enc = Encoding.UTF8;
        List<Source> sources { get; set; }
        List<Defintion> defintions { get; set; }
        ///
        public string TargetDocType { get; set; } = ".xml";
        ///
        public string TargetSrcType { get; set; } = ".cs";
        ///
        public System.IO.SearchOption SearchOp { get; set; } = SearchOption.AllDirectories;
        List<FileInfo> targetDocs;
        List<FileInfo> targetSrcs;
        List<Exception> exceptions = new List<Exception> ();

        IEnumerable<FileInfo> getAllFiles (IEnumerable<string> args) {
            foreach (var i in args) {
                if (Directory.Exists (i)) {
                    foreach (var j in Directory.EnumerateFiles (i, "*", SearchOp)) {
                        yield return new FileInfo (j);
                    }
                } else if (File.Exists (i)) {
                    yield return new FileInfo (i);
                }
            }
        }

        ///コンストラクタ
        public Workspace (IEnumerable<string> args) {
            targetDocs = getAllFiles (args)
                .Where (a => !a.FullName.Contains ("/packages/") &&
                    a.Extension.ToLower () == TargetDocType.ToLower ())
                .Distinct ().ToList ();
            targetSrcs = getAllFiles (args)
                .Where (a => a.Extension.ToLower () == TargetSrcType.ToLower ())
                .Distinct ().ToList ();

            sources = loadSources ();
            var userNamespaces = sources.SelectMany (s => s.Namespaces).Distinct ().ToList ();
            defintions = loadDocs (userNamespaces);
        }

        List<Source> loadSources () {
            var srcs = new List<Source> ();
            foreach (var i in targetSrcs) {
                try {
                    srcs.Add (new Source (i));
                } catch (Exception ex) {
                    exceptions.Add (ex);
                }
            }
            return srcs;
        }

        List<Defintion> loadDocs (IEnumerable<string> userNamespaces) {
            var defs = new List<Defintion> (targetDocs.Count);
            foreach (var i in targetDocs) {
                try {
                    defs.Add (new Defintion (i, userNamespaces));
                } catch (Exception ex) {
                    exceptions.Add (ex);
                }
            }
            return defs;
        }
        ///
        public void WriteResult () {
            var path = $@"{Environment.GetFolderPath (Environment.SpecialFolder.Desktop)}";
            foreach (var def in defintions.Where (w => w.MyCode)) {
                foreach (var m in def.defStrs) {
                    var fi = new FileInfo ($@"{path}/{def.Namespace}.xml");
                    //$@"{def.Namespace}.{def.Class}.{++no}".Dump (filePath: path);
                    $"{m}".Dump (filePath: fi.FullName);
                }
            }
        }
    }
}