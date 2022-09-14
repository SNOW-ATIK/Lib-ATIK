using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using IniParser;
using IniParser.Model;

namespace ATIK
{
    public class IniCfg
    {
        public string FileName { get; private set; }
        private FileIniDataParser _FileDataParser;
        private IniData _IniData;

        public IniCfg(string cfgFileName)
        {
            if (File.Exists(cfgFileName) == false)
            {
                FileStream fs = new FileStream(cfgFileName, FileMode.CreateNew);
                fs.Close();
                fs.Dispose();

                System.Threading.Thread.Sleep(50);
            }
            FileName = cfgFileName;
            _FileDataParser = new FileIniDataParser();
            _IniData = _FileDataParser.ReadFile(FileName);
            
        }

        public bool Save(string newFileName = "")
        {
            if (_FileDataParser != null)
            {
                if (string.IsNullOrEmpty(newFileName) == true)
                {
                    _FileDataParser.WriteFile(FileName, _IniData);
                }
                else
                {
                    _FileDataParser.WriteFile(newFileName, _IniData);
                }
                return true;
            }
            return false;
        }

        public bool AddSection(string section)
        {
            bool rtn = _IniData.Sections.AddSection(section);
            return rtn;
        }

        public bool AddKey(string section, string key, string initValue)
        {
            bool rtn = _IniData[section].AddKey(key, initValue);
            return rtn;
        }

        public string GetString(string section, string key)
        {
            if (string.IsNullOrEmpty(section) || string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException($"Null Argument. (section={section}, key={key}");
            }
            if (_IniData == null)
            {
                throw new NullReferenceException($"Null Reference. (FileName={FileName})");
            }

            if (_IniData.Sections.ContainsSection(section) == false)
            {
                throw new ArgumentException($"Wrong section name. (section={section})");
            }
            if (_IniData[section].ContainsKey(key) == false)
            {
                throw new ArgumentException($"Wrong key name. (key={key})");
            }
            return _IniData[section][key];
        }

        public void SetString(string section, string key, string value)
        {
            if (string.IsNullOrEmpty(section) || string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException($"Null Argument. (section={section}, key={key}");
            }
            if (_IniData == null)
            {
                throw new NullReferenceException($"Null Reference. (FileName={FileName})");
            }

            if (_IniData.Sections.ContainsSection(section) == false)
            {
                throw new ArgumentException($"Wrong section name. (section={section})");
            }
            if (_IniData[section].ContainsKey(key) == false)
            {
                throw new ArgumentException($"Wrong key name. (key={key})");
            }
            _IniData[section][key] = value;
        }

        public int GetInt32(string section, string key)
        {
            string strValue = GetString(section, key);
            if (int.TryParse(strValue, out int value) == false)
            {
                throw new TypeLoadException($"Wrong type. (Target=Int, Source={strValue})");
            }
            return value;
        }

        public void SetInt32(string section, string key, int value)
        {
            SetString(section, key, value.ToString());
        }

        public double GetDouble(string section, string key)
        {
            string strValue = GetString(section, key);
            if (double.TryParse(strValue, out double value) == false)
            {
                throw new TypeLoadException($"Wrong type. (Target=Double, Source={strValue})");
            }
            return value;
        }

        public void SetDouble(string section, string key, double value)
        {
            SetString(section, key, value.ToString());
        }
    }
}
