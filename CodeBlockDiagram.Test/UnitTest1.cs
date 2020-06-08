namespace CodeBlockDiagram.Test {
    using System;
    using CodeBlockDiagram;
    using Xunit;
    ///
    public class UnitTest1 {
        ///
        [Fact]
        public void Test1 () {
            var path = Extentions.ExePath;
            Console.WriteLine (path);
        }
    }
}