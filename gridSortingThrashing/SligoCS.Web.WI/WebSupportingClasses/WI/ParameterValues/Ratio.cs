using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct RatioKeys
	{
		public const string Revenue = "Revenue Per Member";
		public const string Expenditure = "CostvPer Member";
	}

	public class Ratio : ParameterValues
	{
		private static SerializableDictionary<String, String> range;

		public override SerializableDictionary<string, string> Range
		{
			get
			{
				if (range == null) range = initRange();
				return range;
			}
		}
		protected  SerializableDictionary<String, String> initRange()
		{
			SerializableDictionary<String, String> range = new SerializableDictionary<String, String>();

			range.Add(RatioKeys.Revenue, "REVENUE");
			range.Add(RatioKeys.Expenditure, "EXPENDITURE");

			return range;
		}
	}
}
