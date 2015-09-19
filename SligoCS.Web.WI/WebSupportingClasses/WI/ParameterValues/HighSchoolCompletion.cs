using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct HighSchoolCompletionKeys
	{
		public const string All = "All Types";
		public const string Certificate = "Certificate";
		public const string HSED = "HSED";
		public const string Regular = "Regular Diploma";
		public const string Summary = "Combined";
	}

	public class HighSchoolCompletion : ParameterValues
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

			range.Add(HighSchoolCompletionKeys.All, "All");
			range.Add(HighSchoolCompletionKeys.Certificate, "CERT");
			range.Add(HighSchoolCompletionKeys.HSED, "HSED");
			range.Add(HighSchoolCompletionKeys.Regular, "REG");
			range.Add(HighSchoolCompletionKeys.Summary, "COMB");

			return range;
		}
	}
}
