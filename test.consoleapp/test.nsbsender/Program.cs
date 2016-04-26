using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Eid.Elvis.Messages.Commands;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using NServiceBus;

namespace test.nsbsender {
    class Program {
        static void Main(string[] args) {
            var timer = Stopwatch.StartNew();
            try {

               

                timer.Stop();
            } catch(Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            } finally {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Done in {0} seconds", timer.Elapsed.TotalSeconds);
                Console.WriteLine("<enter> to quit");
                Console.ReadLine();
                Console.ResetColor();
            }

        }


        private static void ReadNsbMessage() {
            var endpoint = QueueClient.CreateFromConnectionString("Endpoint=sb://dan-elvis-local.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=9PgKDcOIyqCKBbmK3tS43Ak74NtslUBx/HQ7Th8fAIE=", "eid.elvis");
            var message = endpoint.Peek();

            var stream = new MemoryStream(message.GetBody<byte[]>());
            var reader = new JsonTextReader(new StreamReader(stream));
            var serializer = new Newtonsoft.Json.JsonSerializer();

            dynamic id = serializer.Deserialize(reader);

            var s = "";
        }

        private static void PublishNSBMessage() {

            var config = new BusConfiguration();
            //config.OverrideLocalAddress("eid.elvis.manual");
            config.EndpointName("eid.elvis.manual");


            // config.AssembliesToScan(
            //     Assembly.GetAssembly(typeof(IRequestBackgroundCheck))
            //);



            config.UseTransport<AzureServiceBusTransport>()
                .ConnectionString("Endpoint=sb://dan-elvis-local.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=9PgKDcOIyqCKBbmK3tS43Ak74NtslUBx/HQ7Th8fAIE=");

            config.UseSerialization<NServiceBus.JsonSerializer>();

            config.Conventions()
                .DefiningCommandsAs(el => {
                    var ret = Eid.Elvis.Messages.MessageTypes.Commands.Any(t => t.IsAssignableFrom(el));
                    Console.WriteLine("Command Type = {0} : {1}", el, ret);
                    return ret;
                })
                .DefiningEventsAs(el => {
                    var ret = Eid.Elvis.Messages.MessageTypes.Events.Any(t => t.IsAssignableFrom(el));
                    Console.WriteLine("Event Type = {0} : {1}", el, ret);
                    return ret;
                })
                .DefiningEncryptedPropertiesAs(prop => false);
            config.PurgeOnStartup(false);

            config.UsePersistence<AzureStoragePersistence>();

            var bus = NServiceBus.Bus.CreateSendOnly(config);

            bus.Send<IPassBackgroundCheck>(c => {
                c.ProviderId = new Guid("A4F38E3C-33BB-44B8-81AA-3BAD3DEB0A15");
                c.ReferenceId = "saga009";
                c.RequestId = new Guid("E5C1986F-AA2B-4EF7-9FC4-C0B1784657C8");
                c.WorkOrder = "GISPushMock_saga009";
                c.RawResponse = "";
                c.UpdateReason = "";


            });
        }



    }
}
