using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ATIK
{
    public static class XmlCfg
    {
        public static object Deserialize_XmlCfg(string fileName, Type cfgType)
        {
            object rtn = null;
            XmlSerializer xmlSerializer = new XmlSerializer(cfgType);
            StreamReader sr = new StreamReader(fileName);
            rtn = xmlSerializer.Deserialize(sr);
            sr.Close();
            sr.Dispose();
            return rtn;
        }
    }

    public class XmlCfgPrm
    {
        private object objLock_Access = new object();
        private XmlDocument XmlPrm;
        public bool XmlLoaded { get; private set; }
        public string FileName { get; private set; }
        public string RootName { get; private set; }

        public XmlCfgPrm(string filePath, string fileName, string rootName = "")
        {
            FileName = Path.Combine(filePath, fileName);
            if (rootName != "")
            {
                RootName = rootName;
            }
            else
            {
                RootName = FileName.Substring(0, FileName.Length - 4);
            }
            XmlLoaded = LoadXml(FileName);
        }

        public XmlCfgPrm(string fileName, string rootName = "")
        {
            FileName = fileName;
            if (rootName != "")
            {
                RootName = rootName;
            }
            else
            {
                RootName = FileName.Substring(0, FileName.Length - 4);
            }
            XmlLoaded = LoadXml(FileName);
        }

        private bool LoadXml(string fileName)
        {
            try
            {
                if (File.Exists(fileName) == false)
                {
                    return false;
                }
                lock(objLock_Access)
                {
                    XmlPrm = new XmlDocument();
                    XmlPrm.Load(fileName);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Save()
        {
            if (XmlPrm != null)
            {
                lock (objLock_Access)
                {
                    XmlPrm.Save(FileName);
                }
            }
        }

        public void SaveAs(string filename)
        {
            if (XmlPrm != null)
            {
                lock (objLock_Access)
                {
                    XmlPrm.Save(filename);
                }
            }
        }

        public XmlNode Get_Node(params string[] subj)
        {
            XmlNode ret = null;
            string nodes = RootName + "/";

            try
            {
                if (subj.Length == 1)
                {
                    nodes = RootName;
                }
                else
                {
                    for (int i = 0; i < subj.Length - 1; i++)
                    {
                        nodes += subj[i];
                        if (i < subj.Length - 2) nodes += "/";
                    }
                }
                lock (objLock_Access)
                {
                    XmlNodeList xnList = XmlPrm.SelectNodes(nodes);

                    foreach (XmlNode xn in xnList)
                    {
                        return xn[subj[subj.Length - 1]];
                    }
                }
            }
            catch
            {
                ret = null;
            }
            return ret;
        }

        public string Get_Item(params string[] subj)
        {
            string ret = string.Empty;
            string nodes = RootName + "/";

            try
            {
                if (subj.Length == 1)
                {
                    nodes = RootName;
                }
                else
                {
                    for (int i = 0; i < subj.Length - 1; i++)
                    {
                        nodes += subj[i];
                        if (i < subj.Length - 2) nodes += "/";
                    }
                }
                lock (objLock_Access)
                {
                    XmlNodeList xnList = XmlPrm.SelectNodes(nodes);
                    foreach (XmlNode xn in xnList)
                    {
                        ret = xn[subj[subj.Length - 1]].InnerText;
                    }
                }
            }
            catch (Exception e)
            {
                ret = e.Message;
            }

            if (ret == "") ret = "-1";

            return ret;
        }

        public string Set_Item(params string[] subj)
        {
            string ret = string.Empty;

            try
            {
                string nodes = RootName + "/";
                for (int i = 0; i < subj.Length - 1; i++)
                {
                    nodes += subj[i];
                    if (i < subj.Length - 2) nodes += "/";
                }
                lock (objLock_Access)
                {
                    XmlNodeList xnList = XmlPrm.SelectNodes(nodes);
                    foreach (XmlNode xn in xnList)
                    {
                        xn.InnerText = subj[subj.Length - 1];
                        ret = xn.InnerText;
                    }
                }
                Save();
            }
            catch (Exception e)
            {
                ret = e.Message;
            }

            return ret;
        }

        public string Set_Item(List<string> subj)
        {
            string ret = string.Empty;

            try
            {
                string nodes = RootName + "/";
                for (int i = 0; i < subj.Count - 1; i++)
                {
                    nodes += subj[i];
                    if (i < subj.Count - 2) nodes += "/";
                }
                lock (objLock_Access)
                {
                    XmlNodeList xnList = XmlPrm.SelectNodes(nodes);
                    foreach (XmlNode xn in xnList)
                    {
                        xn.InnerText = subj[subj.Count - 1];
                        ret = xn.InnerText;
                    }
                }
                Save();
            }
            catch (Exception e)
            {
                ret = e.Message;
            }

            return ret;
        }

        public void AppendNode(params string[] subj)
        {
            if (subj.Length < 1) return;

            try
            {
                string nodes = RootName + "/";
                for (int i = 0; i < subj.Length - 1; i++)
                {
                    nodes += subj[i];
                    if (i < subj.Length - 2) nodes += "/";
                }
                lock (objLock_Access)
                {
                    XmlNode xnode = XmlPrm.SelectSingleNode(nodes);
                    XmlElement elem = XmlPrm.CreateElement(subj[subj.Length - 1]);
                    elem.InnerText = "";
                    xnode.AppendChild(elem);
                }
                Save();
            }
            catch
            {
            }
        }

        public void AppendElement(params string[] subj)
        {
            if (subj.Length < 3) return;

            try
            {
                string nodes = RootName + "/";
                for (int i = 0; i < subj.Length - 2; i++)
                {
                    nodes += subj[i];
                    if (i < subj.Length - 3) nodes += "/";
                }
                lock (objLock_Access)
                {
                    XmlNodeList xnList = XmlPrm.SelectNodes(nodes);
                    foreach (XmlNode xn in xnList)
                    {
                        if (xn.InnerXml.Contains(string.Format("<{0}>", subj[subj.Length - 2])) == false)
                        {
                            XmlElement elem = XmlPrm.CreateElement(subj[subj.Length - 2]);
                            elem.InnerText = subj[subj.Length - 1];
                            xn.AppendChild(elem);
                            break;
                        }
                    }
                }
                Save();
            }
            catch
            {
            }
        }

        public string GetAllText(bool trim = false, string filename = "")
        {
            string rtn = string.Empty;
            if (XmlPrm != null)
            {
                if (trim == true)
                {
                    lock (objLock_Access)
                    {
                        rtn = XmlPrm.InnerXml;
                    }
                }
                else
                {
                    if (filename == "")
                    {
                        filename = FileName;
                    }
                    FileStream fs = new FileStream(filename, FileMode.Open);
                    StreamReader st = new StreamReader(fs);
                    rtn = st.ReadToEnd();
                    st.Close();
                    st.Dispose();
                    fs.Close();
                    fs.Dispose();
                }
            }
            else
            {
                rtn = string.Empty;
            }
            return rtn;
        }

        public static (bool ModifiedValueExist, List<IParam> ModifiedValues) CheckModifiedValueExist(List<IParam> valueAll)
        {
            List<IParam> modifiedValue = new List<IParam>();
            valueAll.ForEach(prm =>
            {
                if (prm.ValueObject.Equals(prm.ValueObject_Original) == false)
                {
                    modifiedValue.Add(prm);
                }
            });
            return ((modifiedValue.Count > 0), modifiedValue);
        }

        public static bool IsValidString(string value)
        {
            if (value == "-1" || value.ToUpper() == "NONE")
            {
                return false;
            }
            return true;
        }
    }
}

