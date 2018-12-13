using SimpleFileSystem;
using System;
using System.Collections.Generic;
using Xunit;

namespace Test
{
    public class FileSystemTest
    {

        private Dictionary<string,string> GetFiles()
        {
            return new Dictionary<string, string>
            {
                { "name1", "content1" },
                { "name2", "content2" },
                { "name3", "content3" }
            };
        }


        [Fact]
        public void ShouldWriteReadAllFiles()
        {
            var fs = new FileSystem(48, 4);


            var files = GetFiles();

            foreach(var file in files)
            {
                fs.Write(file.Key, file.Value);
            }


            foreach(var file in files)
            {
                var content = fs.Read(file.Key);
                Assert.Equal(file.Value, content);
            }
            
            
        }

         

    }
}
