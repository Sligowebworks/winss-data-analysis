using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct ACTSubjKeys
	{
		public const string Reading = "Reading";
		public const string English = "English";
		public const string Math = "Math";
		public const string Science = "Science";
		public const string Summary = "Summary";
	}

	public class ACTSubj : ParameterValues
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

			range.Add(ACTSubjKeys.Reading, "1RE");
			range.Add(ACTSubjKeys.English, "2LA");
			range.Add(ACTSubjKeys.Math, "3MA");
			range.Add(ACTSubjKeys.Science, "4SC");
			range.Add(ACTSubjKeys.Summary, "0AS");

			return range;
		}
	}
}
