using System.Xml.Serialization;
namespace Planning.Infrastructure.ExcelParser.Extension
{
    [XmlRoot("ExcelConfig")]
    public class ExcelConfig()
    {
        [XmlElement("SheetName")]
        public string SheetName { get; set; }

        [XmlArray("Columns")]
        [XmlArrayItem("Column")]
        public List<ColumnConfig> Columns { get; set; }

        [XmlElement("FirstDateColumn")]
        public ColumnReference FirstDateColumn { get; set; }

        [XmlArray("AllowedFactories")]
        [XmlArrayItem("Factory")]
        public List<string> AllowedFactories { get; set; }
    }

    public class ColumnConfig
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("A1Notation")]
        public string A1Notation { get; set; }

        [XmlAttribute("Index")]
        public int Index { get; set; }
    }

    public class ColumnReference
    {
        [XmlAttribute("A1Notation")]
        public string A1Notation { get; set; }

        [XmlAttribute("Index")]
        public int Index { get; set; }
    }
}