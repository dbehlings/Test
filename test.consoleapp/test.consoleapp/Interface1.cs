using System;

namespace test.consoleapp
{


    public interface InterfaceA { }


    public interface Interface1 : InterfaceA
    {
        string Message { get; set; }
        string Name { get; set; }

    }

    public interface Interface2 : Interface1
    {

        string Name { get; set; }
    }



    public class Class1 : Interface1
    {
        public string Message { get; set; }
        public string Name { get; set; }

        public string OtherMessage { get; set; }


        public void Notify(DateTime obj, string str)
        {
            var s = "";
        }
    }
}