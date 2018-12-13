using SimpleFileSystem;
using System;
using Xunit;

namespace Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var fs = new FileSystem(48, 4);

            var expected = "hello world";

            fs.Write("file1", expected);
            var content = fs.Read("file1");

            Assert.Equal(expected, content);

            
        }
    }
}
