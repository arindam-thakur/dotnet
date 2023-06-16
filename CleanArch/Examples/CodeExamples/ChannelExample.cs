using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CodeExamples
{
    internal record struct DtoRecordStruct(int Id, string Name);

    internal class ChannelExample
    {
        static Channel<DtoRecordStruct> channel = Channel.CreateUnbounded<DtoRecordStruct>();

        static ChannelWriter<DtoRecordStruct> channelWriter = channel.Writer;

        internal static async ValueTask SpawnProducers()
        {
            var count = 5;
            var tasks = new List<Task>(count);
            for (var c = 0; c < count; c++)
            {
                var task = Task.Factory.StartNew(async () =>
                {
                    var counter = 10;
                    while (counter > 0)
                    {
                        await channelWriter.WriteAsync(new DtoRecordStruct { Id = 1, Name = "abc" });
                        counter--;
                    }
                }).Unwrap();

                tasks.Add(task);
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);

            channelWriter.Complete();
        }

        internal static async ValueTask SpawnConsumer()
        {
            while (await channel.Reader.WaitToReadAsync())
            {
                while (channel.Reader.TryRead(out var dto))
                {
                    Console.WriteLine(dto);
                }
            }
        }

        internal static async ValueTask SpawnProducersAndConsumer()
        {
            var tasks = new Task[2];
            tasks[0] = Task.Run(async () =>
            {
                await SpawnProducers().ConfigureAwait(false);
            });

            tasks[1] = Task.Run(async () =>
            {
                await SpawnConsumer().ConfigureAwait(false);
            });

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }
}
