using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ATIK
{
    public class Util_HandleType
    {
        public static bool ConvertTo(string input, Type targetType, out object output)
        {
            bool parseSuccess = false;
            output = default;

            try
            {
                var converter = TypeDescriptor.GetConverter(targetType);
                if (converter != null)
                {
                    // Cast ConvertFromString(string text) : object to (T)
                    output = converter.ConvertFromString(input);
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

            return parseSuccess;
        }

    }
}
