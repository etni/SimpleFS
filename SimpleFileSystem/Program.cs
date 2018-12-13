using System;

namespace SimpleFileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileSystem = new FileSystem(32, 4);

            fileSystem.Write("hello-world", "peace in the world! x merry xmas!!");
            

            var file = fileSystem.Drive["hello-world"];
            var result = fileSystem.Read("hello-world");


            Console.WriteLine($"Segments: {file.SegmentCount} - Content: {result}");
            
            Console.ReadKey();
        }
    }
}
