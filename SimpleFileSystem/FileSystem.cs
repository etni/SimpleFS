using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFileSystem
{

    public class Meta
    {
        public Meta()
        {
            Init();
        }

        public Segment Head { get; set; }
        public Segment Tail { get; set; }
        public int SegmentCount { get; set; }

        public void Init()
        {
            Head = null;
            Tail = null;
            SegmentCount = 0;
        }

        public Segment Pull()
        {
            if (Head == null) throw new ApplicationException("No more nodes");

            var node = Head;
            Head = Head.Next;
            node.Next = null;
            SegmentCount--;
            return node;
        }

        public void Add(Segment node)
        {
            if (Head == null)
            {
                Head = node;
                Tail = node;
            }
            else
            {
                Tail.Next = node;
                Tail = node;
            }
            SegmentCount++;
        }

        public void Add(Meta file)
        {
            if(Head == null)
            {
                Head = file.Head;
                Tail = file.Tail;
            }
            else
            {
                Tail.Next = file.Head;
                Tail = file.Tail;
            }
            SegmentCount += file.SegmentCount;
        }
    }

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

        public void Write(string filename, string content)
        {
            if (FileExists(filename))
            {
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
            catch(ApplicationException e)
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

    public class Segment
    {
        private char[] _data;
        public Segment Next { get; set; }

        public Segment(int size)
        {
            _data = new char[size];
        }

        public Segment Write(char[] data)
        {
            Array.Clear(_data,0, _data.Length);
            Array.Copy(data, _data, data.Length);
            return this;
        }

        public string Read()
        {
            return new string(_data);
        }

         
    }
}
