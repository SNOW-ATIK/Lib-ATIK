using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATIK
{
    public partial class DataHandling
    {
		public static double StandardDeviation(List<double> data)
		{
			if (data.Count > 1)
			{
				var mean = data.Average();
				var sum = data.Sum(x => Math.Pow(x - mean, 2));
				var var = sum / (data.Count - 1);

				return Math.Sqrt(var);
			}
			else
			{
				return 0.0;
			}
		}
	}
}
