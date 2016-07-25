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
using Flurl;
using Flurl.Http;
using System.Net.Http;
using System.Xml.Linq;
using System.Xml;
using Microsoft.WindowsAzure.Storage;

namespace test.consoleapp
{

    public static class FlurlExtensions
    {


        public static Task<HttpResponseMessage> PostContentAsync(this FlurlClient client, string content, string contentType)
        {
            var toPost = new StringContent(content, Encoding.Default, contentType);
            return client.SendAsync(HttpMethod.Post, content: toPost);
        }


        public static Task<HttpResponseMessage> PostContentAsync(this string url, string content, string contentType)
        {

            return new FlurlClient(url).PostContentAsync(content, contentType);

        }


        //public static Task<HttpResponseMessage> PostXmlAsync(this FlurlClient client, object content)
        //{
        //    var doc = new XDocument();

        //    var s = doc.CreateWriter();
        //    s.WriteValue(content);


        //        string xml = "";
        //    var toPost = new StringContent(xml, Encoding.UTF8, "application/xml");

        //    return client
        //}
    }


    //public class SystemMessagesHub : Hub
    //{
    //    private IHubProxy _hub;
    //    private static ConcurrentDictionary<string, IDisposable> subscriptions = new ConcurrentDictionary<string, IDisposable>();

    //    public SystemMessagesHub()
    //    {


    //    }


    //    public override Task OnConnected()
    //    {
    //        subscriptions.TryAdd(Context.ConnectionId, Subscribe(Clients.Caller));
    //        return base.OnConnected();
    //    }

    //    private IDisposable Subscribe(dynamic caller)
    //    {
    //        return _hub.On<IEnumerable<object>>("NewMessage", data =>
    //        {
    //            caller.newMessage(data);
    //        });

    //    }

    //    public override Task OnDisconnected(bool stopCalled)
    //    {
    //        IDisposable sub = null;
    //        if (subscriptions.TryRemove(Context.ConnectionId, out sub))
    //        {
    //            sub.Dispose();
    //        }
    //        return base.OnDisconnected(stopCalled);
    //    }






    //    public async Task<IEnumerable<string>> GetSources()
    //    {
    //        return await _hub.Invoke<IEnumerable<string>>("GetSources");
    //    }




    //}




    class Program
    {
        static void Main(string[] args)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            var timer = Stopwatch.StartNew();
            try
            {
                MainAsync().Wait();

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




        private static async Task MainAsync()
        {
            var json = @"{
  'AccountId': 'ID-EID10_XML',
  // mark for update
  'LogCommunications': 'asdlkmlkmlk',
  'Mock': 'true',
  'MockUrl': 'http://localhost:8140/gisPushdirectpost.aspx',
  'PackageCode': {
    'SevenYear': 'ID-EID10-1',
    'TwoYear': 'ID-EID02-1',
    'Watchdog': 'ID-EIDWD',
    'SureIdStandardCheck': 'ID-EID10-1',
    'SureIdTsaAviationCheck': 'ID-AVI-R-1'
  },
  'Password': 'nWbsE2WV',
  'PayloadPassword': 'nWbsE2WV',
  'RequesterAcctNbr': {
    'SevenYear': 'ID-EID10',
    'TwoYear': 'ID-EID02',
    'Watchdog': 'ID-EIDWD',
    'SureIdStandardCheck': 'ID-EID10',
    'SureIdTsaAviationCheck': 'ID-AVI-R'
  },
  'RequesterOriginSrc': 'ID-EID10_XML',
  'ResponseUrl': 'https://localhost:44301/response/gispush',
  'ServiceUrl': 'http://mocks0:8140/gisPushdirectpost.aspx',
  'ResponseUsername': 'GISPushTest',
  'ResponsePassword': 'Test123',
  'Weight': '1'
}";



            //QueueClient queue = QueueClient.CreateFromConnectionString("Endpoint=sb://dan-elvis-local.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=9PgKDcOIyqCKBbmK3tS43Ak74NtslUBx/HQ7Th8fAIE=", "eid.elvis");

            //var m = queue.Receive();

            //Console.WriteLine(m.MessageId);


            //var url = "https://localhost:44309/";
            //var connection = new HubConnection(url);

            //var hub = connection.CreateHubProxy("HomeHub");

            ////var writer = new StreamWriter(@"c:\temp\systemmessages-trace.txt");
            ////writer.AutoFlush = true;

            ////connection.TraceLevel = TraceLevels.All;
            ////connection.TraceWriter = writer;

            //hub.On<IEnumerable<object>>("NewMessage", data =>
            //{
            //    foreach (var item in data)
            //    {
            //        Console.WriteLine(item);
            //    }
            //});

            //connection.Start().Wait();


            //Console.WriteLine("connected {0}", connection.ConnectionId);




            var d = typeof(Delegate );





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

        private class GenericResult
        {
            public bool IsFail { get; set; }
            public bool IsOngoingVettingResult { get; set; }
            public bool IsPass { get; set; }
            public bool IsRedress { get; set; }

            public bool IsScore { get; set; }
            public IEnumerable<string> Offenses { get; set; }
            public DateTime RedressInitiatedDate { get; set; }
            public Guid RequestId { get; set; }
            public int Score { get; set; }
            public string WorkOrder { get; set; }
        }
    }
}
