namespace CodeBlockDiagram {
    using System.Diagnostics;
    using System.Text;
    using System.Threading.Tasks;
    using System;

    ///
    public class Program {
        ///
        static async Task Main (string[] args) {
            Encoding.RegisterProvider (CodePagesEncodingProvider.Instance);
#if DEBUG
            Console.WriteLine ($@"
    exePath: {Extentions.ExePath}");
#endif
            Console.WriteLine ($@"
    args:
        {string.Join ($@"{Environment.NewLine}        ", args)}");

            var workspace = new Workspace (args);
            //workspace.WriteResult();
            Console.WriteLine (">---Finish---<");
        }
    }
}