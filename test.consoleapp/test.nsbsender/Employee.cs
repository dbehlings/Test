using System.Xml.Serialization;

namespace test.nsbsender {




    public class Employee {
        public Employee() {
        }

        public int Id { get; set; }

        [XmlText]
        public string Name { get; set; }
    }
}