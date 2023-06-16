using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CodeExamples
{
    internal static class TaskParallelLibrary
    {
        internal static void Parallel_ForEach()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };

            Parallel.ForEach(list, async (item) =>
            {
                //DON'T DO
                Console.WriteLine(item);
                File.WriteAllText("abc.txt", item.ToString());


                //DO
                await File.WriteAllTextAsync("abc.txt", item.ToString()).ConfigureAwait(false);
            });
        }

        internal static void Parallel_Invoke()
        {
            Parallel.Invoke(async () =>
            {
                //ACTION
                DataTable dt = new DataTable();
                dt.Columns.Add("col1", typeof(int));

                for (int c = 0; c < 100; c++)
                    dt.Rows.Add(c);

                var fileName = "abc.xml";
                await WriteDataTableToFile(fileName, dt).ConfigureAwait(false);
            }, async () =>
            {
                //ANOTHER ACTION
                DataTable dt = new DataTable();
                dt.Columns.Add("col2", typeof(int));

                for (int c = 0; c < 100; c++)
                    dt.Rows.Add(c);

                var fileName = "abc.xml";
                await WriteDataTableToFile(fileName, dt).ConfigureAwait(false);
            });
        }

        internal static async Task Task_Factory()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };

            var tasks = new List<Task>(list.Count);
            for (var c = 0; c < list.Count; c++)
            {
                var task = Task.Factory.StartNew(async (itemObject) =>
                {
                    //DO
                    var item = (int)itemObject!;
                    await File.WriteAllTextAsync("abc.txt", item.ToString()).ConfigureAwait(false);
                }, c);

                tasks.Add(task);
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        internal static async Task Task_Run()
        {
            var count = 2;
            var tasks = new List<Task>(count);
            for (var c = 0; c < count; c++)
            {
                var task = Task.Run(async () =>
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("col1", typeof(int));

                    for (int c = 0; c < 100; c++)
                        dt.Rows.Add(c);

                    var fileName = "abc.xml";
                    await WriteDataTableToFile(fileName, dt).ConfigureAwait(false);
                });

                tasks.Add(task);
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        private static async Task WriteDataTableToFile(string fileName, DataTable dt)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (var writer = new StreamWriter(ms))
                {
                    if (string.IsNullOrEmpty(dt.TableName))
                        dt.TableName = "Table";

                    dt.WriteXml(writer, XmlWriteMode.WriteSchema);

                    writer.Close();

                    ms.Position = 0;

                    using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                    {
                        await ms.CopyToAsync(fs).ConfigureAwait(false);
                    }
                }
            }
        }
    }

    internal static class TaskVsValueTask
    {
        internal record DtoRecord(int Id, string Name);

        //RETURNING REFERENCE TYPES
        internal static Task<DtoRecord> ReturnsDtoRecord()
        {
            return Task.FromResult(new DtoRecord(1, "abc"));
        }



        internal record struct DtoRecordStruct(int Id, string Name);

        //RETURNING VALUE TYPES
        internal static ValueTask<DtoRecordStruct> ReturnsDtoRecordStruct()
        {
            return ValueTask.FromResult(new DtoRecordStruct(1, "abc"));
        }

        internal static async ValueTask ReturnsValueTask()
        {
            await File.WriteAllTextAsync("abc.txt", "string").ConfigureAwait(false);
        }
    }
}
