using System.Xml.Serialization;

namespace test.consoleapp {
    
    public  class Employee {
        [XmlAttribute]
        public int Id { get; set; }
        [XmlText]
        public string Name { get; set; }
    }
}