using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct SPENDKeys
	{
		public const string On = "District  Spending";
		public const string Off = "District Spending";
	}

	public class SPEND : ParameterValues
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

			range.Add(SPENDKeys.On, "Y");
			range.Add(SPENDKeys.Off, "N");

			return range;
		}
	}
}
