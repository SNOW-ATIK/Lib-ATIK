using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace ATIK
{
    public class XmlCfgTest
    {
        private string MyFileName = string.Empty;
        private Type MyType = null;

        public object Deserialize_XmlCfg(string fileName, Type cfgType)
        {
            MyFileName = fileName;

            object rtn = null;
            XmlSerializer xmlSerializer = new XmlSerializer(cfgType);
            StreamReader sr = new StreamReader(fileName);
            rtn = xmlSerializer.Deserialize(sr);
            sr.Close();
            sr.Dispose();

            return rtn;
        }

        public bool Serialize_XmlCfg(object source)
        {
            if (string.IsNullOrEmpty(MyFileName) == true)
            {
                return false;
            }

            XmlSerializer xmlSerializer = new XmlSerializer(MyType);
            StreamWriter sw = new StreamWriter(MyFileName);
            xmlSerializer.Serialize(sw, source);
            sw.Close();
            sw.Dispose();

            return true;
        }
    }
}
