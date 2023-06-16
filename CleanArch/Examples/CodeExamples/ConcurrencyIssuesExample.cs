using System;
using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CodeExamples
{
    internal static class ConcurrentClass
    {

        static List<int> sharedCollection = new List<int>();

        internal static List<int> GetCollection()
        {
            return sharedCollection;
        }

        internal static ReadOnlyCollection<int> GetReadOnlyCollection()
        {
            var readonlyCol = sharedCollection.AsReadOnly();

            return readonlyCol;
        }

        internal static ImmutableList<int> GetImmutableCollection()
        {
            return sharedCollection.ToImmutableList();
        }

        internal static FrozenSet<int> GetFrozenCollection()
        {
            return sharedCollection.ToFrozenSet();
        }

        internal static ReadOnlySpan<int> GetReadOnlySpan()
        {
            return CollectionsMarshal.AsSpan(sharedCollection);
        }

        internal static void AddToCollection(int item)
        {
            sharedCollection.Add(item);
        }
    }

    internal static class MultiThreadingProcessor
    {
        internal static void Producers()
        {
            Parallel.Invoke(() =>
            {
                ConcurrentClass.AddToCollection(10);
            },
            () =>
            {
                ConcurrentClass.AddToCollection(10);
            });
        }

        internal static void Consumers()
        {
            Parallel.Invoke(() =>
            {
                foreach (var item in ConcurrentClass.GetReadOnlyCollection())
                {
                    Console.WriteLine(item);
                }
            },
            () =>
            {
                foreach (var item in ConcurrentClass.GetReadOnlyCollection())
                {
                    Console.WriteLine(item);
                }
            });
        }

        internal static void Execute()
        {
            Producers();
            Consumers();
        }
    }
}
