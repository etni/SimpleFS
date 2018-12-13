using System;

namespace SimpleFileSystem
{
    public class Segment
    {
        public char[] _data;
        public Segment Next { get; set; }

        public Segment(int size)
        {
            _data = new char[size];
        }

        public Segment Write(char[] data)
        {
            Array.Clear(_data,0, _data.Length);
            Array.Copy(data, _data, Math.Min(data.Length, _data.Length));
            return this;
        }

        public string Read()
        {
            var len = Array.IndexOf(_data, '\0');
            len = len < 0 ? _data.Length : len;
            
            return new string(_data,0,len);
        }

         
    }
}
