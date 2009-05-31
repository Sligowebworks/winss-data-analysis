using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct CTKeys
	{
		public const string Cost = "Total Cost";
		public const string TotalEducation = "Total Education Cost";
		public const string CurrentEducation = "Current Education Cost";
	}

	public class CT : ParameterValues
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

			range.Add(CTKeys.Cost, "CT");
			range.Add(CTKeys.TotalEducation, "TEC");
			range.Add(CTKeys.CurrentEducation, "CEC");

			return range;
		}
	}
}
