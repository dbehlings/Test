using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Serialization;
using AutoMapper;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;


namespace test.consoleapp
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = Stopwatch.StartNew();
            try
            {
                var settings = new JsonSerializerSettings();
                settings.ContractResolver = new DefaultContractResolver { IgnoreSerializableInterface = false };
                settings.TraceWriter = new ConsoleTraceWriter();
                //settings.TypeNameHandling = TypeNameHandling.All;
                settings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
                settings.TypeNameHandling = TypeNameHandling.All;

                settings.Error += (o, err) =>
                {
                    var s = "";
                };

                var json = File.ReadAllText("json1.json");

                //var o = JsonConvert.DeserializeAnonymousType(s, new { Message = default(string), Source = default(string), StackTraceString = default(string) }, settings);

                var ex = JsonConvert.DeserializeObject<Exception>(json, settings);

                timer.Stop();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }
            finally
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Done in {0} seconds", timer.Elapsed.TotalSeconds);
                Console.WriteLine("<enter> to quit");
                Console.ReadLine();
                Console.ResetColor();
            }

        }



        private static void NSBStuff()
        {
            /// 
            QueueClient queue = QueueClient.CreateFromConnectionString("Endpoint=sb://dan-elvis-local.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=9PgKDcOIyqCKBbmK3tS43Ak74NtslUBx/HQ7Th8fAIE=", QueueClient.FormatDeadLetterPath("eid.elvis"));


            IEnumerable<BrokeredMessage> messages = null;
            while ((messages = queue.PeekBatch(messageCount: 1000)).Any())
            {
                foreach (var message in messages)
                {
                    Console.WriteLine("MessageId: {0}", message.MessageId);
                    foreach (var item in message.Properties)
                    {
                        Console.WriteLine("\t{0} : {1}", item.Key, item.Value);
                    }
                }
            }
        }


        private static void Xml()
        {
            var data = new Employee { Id = 1, Name = "Dan behlings" };


            var xml = new XmlSerializer(data.GetType());
            xml.Serialize(Console.Out, data);



        }


        private static void TestJson()
        {
            var body = File.ReadAllText("json1.json");
            //Console.WriteLine(JsonConvert.DeserializeAnonymousType(body, new { RequestId = Guid.Empty }));
            //var obj = JsonConvert.DeserializeObject(body, new JsonSerializerSettings { TraceWriter = new DiagnosticsTraceWriter { LevelFilter = TraceLevel.Info } });
            var obj = JsonConvert.DeserializeAnonymousType(body, new { ReferenceId = default(string) });
            //var obj = JObject.Parse(body, );
        }

        private static void TestAutomapper()
        {





            Mapper.CreateMap<ExpandoObject, Interface2>().ForAllMembers((options) => options.ResolveUsing((resolution) =>
            {
                var dictionary = (IDictionary<string, object>)resolution.Context.SourceValue;
                return dictionary[resolution.Context.MemberName];
            }));


            Mapper.CreateMap<JObject, Interface2>().ForAllMembers(options => options.ResolveUsing(resolution =>
            {
                var dictionary = (IDictionary<string, object>)resolution.Context.SourceValue;
                return dictionary[resolution.Context.MemberName];
            }));


            Class1 data = new Class1 { Name = "Jogn", Message = "Hello World!" };

            var str = JsonConvert.SerializeObject(data);



            var mapped = Mapper.Map<Interface2>(JsonConvert.DeserializeObject(str));



            Console.WriteLine(str);


        }

        private class ConsoleTraceWriter : ITraceWriter
        {
            public TraceLevel LevelFilter {
                get {
                    return TraceLevel.Verbose;
                }
            }

            public void Trace(TraceLevel level, string message, Exception ex)
            {
                Console.WriteLine("{0} --> {1}", message, ex);
            }
        }
    }
}
