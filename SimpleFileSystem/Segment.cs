using System;

namespace SimpleFileSystem
{
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
