namespace CodeBlockDiagram {
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using System;
    using SQLite;

    ///
    public class Defintion {
        ///
        [PrimaryKey, AutoIncrement]
        public string source { get; set; }
        ///
        public string xml { get; set; }
        ///
        public string Namespace { get; set; }
        ///
        public string Class { get; set; }
        ///
        public List<string> defStrs { get; set; }
        ///
        public bool MyCode { get; set; }
        ///<summary>コンストラクタ</summary>
        public Defintion (FileInfo xmlFi, IEnumerable<string> userNamespaces) {
            loadXml (xmlFi, userNamespaces);
        }
        void loadXml (FileInfo xmlFi, IEnumerable<string> userNamespaces) {
            var root = XElement.Load (xmlFi.FullName);
            Namespace = root.Elements ("assembly")?.Elements ("name")?.FirstOrDefault ()?.Value.ToString ();
            MyCode = userNamespaces.Contains (Namespace);
            if (MyCode) {
                defStrs = root.Elements ("members")?.Elements ("member")?.Select (s => s.ToString ()).ToList ();
            }
            //else {
            //    throw new Exception ($@"{Namespace} is NoUserNamespace");
            //}
        }

    }
}