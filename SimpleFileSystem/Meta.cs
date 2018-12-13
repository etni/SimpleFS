using System;

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
}
