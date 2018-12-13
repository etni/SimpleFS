using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFileSystem
{

    public class FileSystem
    {
        private int _segmentSize;

        public  Dictionary<string, Meta> Drive;

        public Meta FreeSpace { get; set; } 

        public FileSystem(int size, int segmentSize)
        {
            _segmentSize = segmentSize;
            Initialize(size, segmentSize);
        }

        public void Initialize(int size, int segmentsize)
        {
            Drive = new Dictionary<string, Meta>();

            FreeSpace = new Meta();
            for(var i=0;i < size; i++)
            {
                FreeSpace.Add(new Segment(segmentsize));
            }
        }

        public string Read(string filename)
        {
            if (!FileExists(filename))
            {
                throw new ApplicationException("File Doesn't Exist");
            }

            var content = new StringBuilder();
            var node = Drive[filename].Head;

            while(node != null)
            {
                content.Append(node.Read());
                node = node.Next;
            }

            return content.ToString();

        }

        public void Write(string filename, string content, bool overwrite = false)
        {
            if (FileExists(filename))
            {
                if (overwrite)
                    Delete(filename);
                else 
                    throw new ApplicationException("File Already Exists");
            }
           

            var file = new Meta();
            var index = 0;
            var length = content.Length;

            try
            {
                while (index < length)
                {
                    var cut = content.ToCharArray(index, Math.Min(_segmentSize, length - index));
                    file.Add(FreeSpace.Pull().Write(cut));
                    index = index + _segmentSize;
                }
                
                Drive.Add(filename, file);
            }
            catch(ApplicationException)
            {
                // there's no more free space:
                // put allocated space back into free space
                FreeSpace.Add(file);
                throw;
            }

        }

        public void Delete(string filename)
        {
            if (!FileExists(filename)) throw new ApplicationException("File Does not Exist");

            var file = Drive[filename];
            Drive.Remove(filename);
            FreeSpace.Add(file);
        }

        public bool FileExists(string filename)
        {
            return Drive.ContainsKey(filename);
        }
    }
}
