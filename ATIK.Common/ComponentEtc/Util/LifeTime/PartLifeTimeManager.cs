using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ATIK
{
    public enum LifeTimeMngType
    { 
        Period,
        Count
    }

    public class PartsLifeTimeManager
    {
        private static List<LifeTimeObj> LifeTimeMngList = new List<LifeTimeObj>();

        public static bool Load(string cfgFilePath, string cfgFileName, string rootName = "")
        {
            string fullName = Path.Combine(cfgFilePath, cfgFileName);
            if (File.Exists(fullName) == false)
            {
                return false;
            }

            XmlCfgPrm cfg = new XmlCfgPrm(cfgFilePath, cfgFileName, rootName);
            if (cfg.XmlLoaded == false)
            {
                return false;
            }

            LifeTimeMngList.Clear();

            int noOfMaints = int.Parse(cfg.Get_Item("NoOfParts"));
            for (int i = 0; i < noOfMaints; i++)
            {
                GenericParam<string> name = new GenericParam<string>(cfg, $"Part_{i}", "Name");
                GenericParam<bool> enabled = new GenericParam<bool>(cfg, $"Part_{i}", "Enabled");
                GenericParam<LifeTimeMngType> type = new GenericParam<LifeTimeMngType>(cfg, $"Part_{i}", "Type");
                GenericParam<string> period_since = new GenericParam<string>(cfg, $"Part_{i}", "PeriodSetting", "Since"); ;
                GenericParam<string> period_current = new GenericParam<string>(cfg, $"Part_{i}", "PeriodSetting", "Current");
                GenericParam<string> period_last = new GenericParam<string>(cfg, $"Part_{i}", "PeriodSetting", "Last");
                GenericParam<int> count_since = new GenericParam<int>(cfg, $"Part_{i}", "CountSetting", "Since"); ;
                GenericParam<int> count_current = new GenericParam<int>(cfg, $"Part_{i}", "CountSetting", "Current");
                GenericParam<int> count_last = new GenericParam<int>(cfg, $"Part_{i}", "CountSetting", "Last");

                if (type.Value == LifeTimeMngType.Period)
                {
                    period_current.Set_Value(DateTime.Now.ToString("yyyy-MM-dd"), false);
                }

                LifeTimeObj info = new LifeTimeObj(name, enabled, type, period_since, period_current, period_last, count_since, count_current, count_last);
                LifeTimeMngList.Add(info);
            }

            return true;
        }

        public static void IncreaseMeasureCount()
        {
            LifeTimeMngList.ForEach(subj =>
            {
                subj.Gen_Count_Current.Set_Value(subj.Gen_Count_Current.Value + 1, true);
            });
        }

        public static List<LifeTimeObj> GetAllMaintParts()
        {
            return LifeTimeMngList;
        }

        public static List<LifeTimeObj> GetEnabledMaintParts()
        {
            return LifeTimeMngList.Where(maint => maint.IsEnabled == true).ToList();
        }

        public static bool ExpiredPartExist()
        {
            if (LifeTimeMngList.Count == 0)
            {
                return false;
            }
            return LifeTimeMngList.Count(part => part.IsEnabled == true && part.IsExpired == true) > 0;
        }

        public static void Clear()
        {
            LifeTimeMngList.Clear();
        }
    }

    public class LifeTimeObj
    {
        public GenericParam<string> Gen_Name;
        public GenericParam<bool> Gen_Enabled;
        public GenericParam<LifeTimeMngType> Gen_Type;
        public GenericParam<string> Gen_Period_Since;
        public GenericParam<string> Gen_Period_Current;
        public GenericParam<string> Gen_Period_Last;
        public GenericParam<int> Gen_Count_Since;
        public GenericParam<int> Gen_Count_Current;
        public GenericParam<int> Gen_Count_Last;

        public double LifeTimeRate
        {
            get
            {
                double rate = 0;
                switch (Gen_Type?.Value)
                {
                    case LifeTimeMngType.Period:
                        Gen_Period_Current.Set_ValueObject(DateTime.Now.ToString("yyyy-MM-dd"), true);
                        DateTime dtSince = DateTime.Parse(Gen_Period_Since.Get_OriginalValue());
                        DateTime dtCurrent = DateTime.Parse(Gen_Period_Current.Get_OriginalValue());
                        DateTime dtLast = DateTime.Parse(Gen_Period_Last.Get_OriginalValue());

                        if ((dtLast - dtSince).TotalSeconds <= 0)
                        {
                            rate = 0;
                        }
                        else
                        {
                            rate = (dtLast - dtCurrent).TotalSeconds / (dtLast - dtSince).TotalSeconds;
                        }
                        break;

                    case LifeTimeMngType.Count:
                        int nSince = Gen_Count_Since.Get_OriginalValue();
                        int nCurrent = Gen_Count_Current.Get_OriginalValue();
                        int nLast = Gen_Count_Last.Get_OriginalValue();

                        if (nLast - nSince <= 0)
                        {
                            rate = 0;
                        }
                        else
                        {
                            rate = (double)(nLast - nCurrent) / (nLast - nSince);
                        }
                        break;
                }
                return rate;
            }
        }

        public bool IsExpired { get => LifeTimeRate <= 0; }

        public bool IsEnabled { get => Gen_Enabled.Get_OriginalValue(); }

        public LifeTimeObj(GenericParam<string> genName, GenericParam<bool> genEnabled, GenericParam<LifeTimeMngType> genType,
                         GenericParam<string> genPeriodSince, GenericParam<string> genPeriodCurrent, GenericParam<string> genPeriodLast,
                         GenericParam<int> genCountSince, GenericParam<int> genCountCurrent, GenericParam<int> genCountLast)
        {
            Gen_Name = genName;
            Gen_Enabled = genEnabled;
            Gen_Type = genType;
            Gen_Period_Since = genPeriodSince;
            Gen_Period_Current = genPeriodCurrent;
            Gen_Period_Last = genPeriodLast;
            Gen_Count_Since = genCountSince;
            Gen_Count_Current = genCountCurrent;
            Gen_Count_Last = genCountLast;
        }
    }
}
