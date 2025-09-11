using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Planning.Infrastructure.ExcelParser.Extension;

namespace Planning.Infrastructure.Extension
{
    public class ConfigLoader
    {
        public static ExcelConfig Load(string configPath)
        {
            var serializer = new XmlSerializer(typeof(ExcelConfig));
            using var reader = new StreamReader(configPath);
            return (ExcelConfig)serializer.Deserialize(reader);
        }
    }
}
