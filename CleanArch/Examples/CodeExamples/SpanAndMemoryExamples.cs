using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExamples
{
    internal class SpanAndMemoryExamples
    {
        readonly int[] array;

        public SpanAndMemoryExamples(int[] arr)
        {
            array = arr;
        }

        public Span<int> AsSpan()
        {
            Span<int> span = array;

            span[0] = 10;

            return array.AsSpan();
        }

        public ReadOnlySpan<int> AsReadOnlySpan()
        {
            ReadOnlySpan<int> span = array;

            //span[0] = 10;

            return array.AsSpan();
        }

        //public async Task<Span<int>> AsSpan()
        //{
        //    //int[] arr1 = new int[0];
        //    //Span<int>.Empty

        //    Span<int> span = array;

        //    span[0] = 10;

        //    return array.AsSpan();
        //}

        public async Task<Memory<int>> AsMemory()
        {
            Memory<int> span = array;
            span.Span[0] = 10;

            return array.AsMemory();
        }

        public ReadOnlyMemory<int> AsReadOnlyMemory()
        {
            ReadOnlyMemory<int> span = array;
            //span.Span[0] = 10;

            return array.AsMemory();
        }
    }
}
