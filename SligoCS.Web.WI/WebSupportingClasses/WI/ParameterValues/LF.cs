using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct LFKeys
	{
		public const string State = "Entire State";
		public const string CESA = "My CESA";
		public const string County = "My County";
	}

	public class LF : ParameterValues
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

			range.Add(LFKeys.State, "ST");
			range.Add(LFKeys.CESA, "CE");
			range.Add(LFKeys.County, "CT");

			return range;
		}
	}
}
