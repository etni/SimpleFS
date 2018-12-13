using SimpleFileSystem;
using System;
using Xunit;

namespace Test
{
    public class SegmentTests
    {

        [Fact]
        public void WrittenDataShouldBeSameIfSameSize()
        {
            var node = new Segment(4);
            var hello = "1234";
            node.Write(hello.ToCharArray());

            var expected = new char[] { '1', '2', '3', '4' };
            Assert.Equal(expected, node._data);
        }

        [Fact]
        public void WrittenDataShouldBeSameIfLessSize()
        {
            var node = new Segment(4);
            var hello = "123";
            node.Write(hello.ToCharArray());

            var expected = new char[] { '1', '2', '3', '\0' };
            Assert.Equal(expected, node._data);
        }

        [Fact]
        public void WrittenDataShouldBeSameIfMoreSize()
        {
            var node = new Segment(4);
            var hello = "123";
            node.Write(hello.ToCharArray());

            var expected = new char[] { '1', '2', '3', '\0' };
            Assert.Equal(expected, node._data);
        }




        [Fact]
        public void ShouldReturnAStringOfSameSize()
        {
            var node = new Segment(4);
            var hello = "Hello!";
            node.Write(hello.ToCharArray());

            var result = node.Read();

            Assert.Equal("Hell", result);

        }


        [Fact]
        public void ShouldReturnAStringOfLessSize()
        {
            var node = new Segment(4);
            var hello = "123";
            node.Write(hello.ToCharArray());

            var result = node.Read();

            Assert.Equal("123", result);

        }


    }
}
