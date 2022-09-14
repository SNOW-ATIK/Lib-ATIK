using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Xml;

namespace ATIK
{
    public interface IParam
    {        
        Type GetType();
        object ValueObject_Original { get; }
        object ValueObject { get; }
        Control Get_BindingControl();
        void Set_BindingControl(Control ctrl);
        void Set_ValueObject(object value, bool bReSaveDirectly);
        void Save(bool updateOriginal);
    }

    public interface IGenericParam<T>
    {
        (bool convertSuccess, T output) TryConvert(string input);
    }

    public class GenericParam<T> : INotifyPropertyChanged, IParam, IGenericParam<T>
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public new Type GetType()
        {
            return typeof(T);
        }

        //public (bool parseSuccess, T output) TryParse(string input)
        //{
        //    bool parseSuccess = false;
        //    T output;
        //    var tryParseMethod = typeof(T).GetMethod("TryParse",
        //                                   BindingFlags.Static | BindingFlags.Public,
        //                                   null,
        //                                   new Type[] { typeof(string), typeof(T).MakeByRefType() },
        //                                   null);
        //    object[] invokeParams = new object[] { input, null };
        //    parseSuccess = (bool)tryParseMethod.Invoke(null, invokeParams);
        //    if (parseSuccess == true)
        //    {
        //        output = (T)invokeParams[1];
        //    }
        //    return (parseSuccess, (T)invokeParams[1]);
        //}

        public (bool convertSuccess, T output) TryConvert(string input)
        {
            bool parseSuccess = false;
            T output;

            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    // Cast ConvertFromString(string text) : object to (T)
                    output = (T)converter.ConvertFromString(input);
                    parseSuccess = true;
                }
                else
                {
                    output = default;
                }
            }
            catch (NotSupportedException)
            {
                output = default;
            }

            return (parseSuccess, output);
        }

        public object ValueObject_Original { get => Value_Original; }
        public object ValueObject { get => Value; }

        public T Value { get; private set; }
        private T Value_Original;

        private XmlCfgPrm Ref_XML;
        private string[] Ref_NodeNames;
        private string LastName;
        private XmlNode MyNode;

        private Control BindingControl;

        public GenericParam(XmlNode xmlNode)
        {
            MyNode = xmlNode;
            (bool parseSucces, T output) = TryConvert(MyNode.InnerText);
            if (parseSucces == true)
            {
                Value = output;
                Value_Original = output;
            }
        }

        public GenericParam(XmlCfgPrm xml, params string[] nodeNames)
        {
            Ref_XML = xml;
            Ref_NodeNames = nodeNames;
            for (int i = 1; i < nodeNames.Length; i++)
            {
                if (i < nodeNames.Length - 1)
                {
                    LastName += nodeNames[i] + ".";
                }
                else
                {
                    LastName += nodeNames[i];
                }
            }

            string str_Default = Ref_XML.Get_Item(Ref_NodeNames);
            (bool convertSuccess, T outvale) = TryConvert(str_Default);
            if (convertSuccess == true)
            {
                Initialize(outvale);
            }
        }

        public void Initialize(T value)
        {
            Value = value;
            Value_Original = value;
        }

        public string Get_Name()
        {
            return LastName;
        }

        public void Set_BindingControl(Control ctrl)
        {
            BindingControl = ctrl;
        }

        public Control Get_BindingControl()
        {
            return BindingControl;
        }

        public delegate void UIInvoke_SetValueObject(object value, bool bReSaveDirectly);
        public void Set_ValueObject(object value, bool bReSaveDirectly)
        {
            if (BindingControl != null && BindingControl.InvokeRequired == true)
            {
                BindingControl.Invoke(new UIInvoke_SetValueObject(Set_ValueObject), value, bReSaveDirectly);
                return;
            }

            Value = (T)value;

            if (bReSaveDirectly == true && Ref_NodeNames != null && Ref_NodeNames.Length != 0)
            {
                Save(false);
            }

            NotifyPropertyChanged("ValueObject");
        }

        public delegate void UIInvoke_SetTValueObject(T value, bool bReSaveDirectly);
        public void Set_Value(T value, bool bReSaveDirectly = true)
        {
            if (BindingControl != null && BindingControl.InvokeRequired == true)
            {
                BindingControl.Invoke(new UIInvoke_SetTValueObject(Set_Value), value, bReSaveDirectly);
                return;
            }

            Value = value;

            if (bReSaveDirectly == true && Ref_NodeNames != null && Ref_NodeNames.Length != 0)
            {
                Save(bReSaveDirectly);
            }
            else
            {
                Save(false);
            }

            NotifyPropertyChanged("ValueObject");
        }

        public T Get_Value()
        {
            return Value;
        }

        public T Get_OriginalValue()
        {
            return Value_Original;
        }

        public void Set_OriginalValue(T modifiedValue)
        {
            Value_Original = modifiedValue;
        }

        public void Save(bool updateOriginal)
        {
            if (updateOriginal == true)
            {
                Value_Original = Value;
            }

            string[] nodes = new string[Ref_NodeNames.Length + 1];
            for (int i = 0; i < nodes.Length - 1; i++)
            {
                nodes[i] = Ref_NodeNames[i];
            }
            nodes[nodes.Length - 1] = Value.ToString();
            Ref_XML.Set_Item(nodes);
        }

        public T UnDo()
        {
            Value = Value_Original;
            return Value;
        }
    }
}
