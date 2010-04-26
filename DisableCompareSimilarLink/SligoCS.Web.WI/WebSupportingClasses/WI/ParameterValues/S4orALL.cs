using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct S4orALLKeys
	{
		public const string FourSchoolsOrDistrictsIn = "4 Schools/Districts In";
		public const string AllSchoolsOrDistrictsIn = "All In";
	}

	public class S4orALL : ParameterValues
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

			range.Add(S4orALLKeys.FourSchoolsOrDistrictsIn, "1");
			range.Add(S4orALLKeys.AllSchoolsOrDistrictsIn, "2");

			return range;
		}
	}
}
