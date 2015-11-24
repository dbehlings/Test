using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Eid.Elvis.Messages.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace test.consoleapp {
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

      

        private static void TestJson() {
            var body = File.ReadAllText("json1.json");
            //Console.WriteLine(JsonConvert.DeserializeAnonymousType(body, new { RequestId = Guid.Empty }));
            //var obj = JsonConvert.DeserializeObject(body, new JsonSerializerSettings { TraceWriter = new DiagnosticsTraceWriter { LevelFilter = TraceLevel.Info } });
            var obj = JsonConvert.DeserializeAnonymousType(body, new { ReferenceId = default(string) });
            //var obj = JObject.Parse(body, );
        }

        private static void TestAutomapper() {





            Mapper.CreateMap<ExpandoObject, Interface2>().ForAllMembers((options) => options.ResolveUsing((resolution) => {
                var dictionary = (IDictionary<string, object>)resolution.Context.SourceValue;
                return dictionary[resolution.Context.MemberName];
            }));


            Mapper.CreateMap<JObject, Interface2>().ForAllMembers(options => options.ResolveUsing(resolution => {
                var dictionary = (IDictionary<string, object>)resolution.Context.SourceValue;
                return dictionary[resolution.Context.MemberName];
            }));


            Class1 data = new Class1 { Name = "Jogn", Message = "Hello World!" };

            var str = JsonConvert.SerializeObject(data);



            var mapped = Mapper.Map<Interface2>(JsonConvert.DeserializeObject(str));



            Console.WriteLine(str);


        }
    }
}
