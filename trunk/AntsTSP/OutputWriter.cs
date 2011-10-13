using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntsTSP
{
    public static class OutputWriter
    {
        public static void Write(OutputData outputData, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(OutputData));
            FileStream stream = new FileStream(@path, FileMode.Create);
            serializer.Serialize(stream, outputData);
        }
    }
}
