using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct RevExpKeys
	{
		public const string Revenue = "Revenue Per Member";
		public const string Expenditure = "Cost Per Member";
	}

	public class RevExp : ParameterValues
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

			range.Add(RevExpKeys.Revenue, "2");
			range.Add(RevExpKeys.Expenditure, "4");

			return range;
		}
	}
}
