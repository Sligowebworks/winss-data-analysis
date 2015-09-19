using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct GRSbjKeys
	{
		public const string StateLaw = "Subjects Required by State Law";
		public const string Additional = "Additional Subjects";
	}

	public class GRSbj : ParameterValues
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

			range.Add(GRSbjKeys.StateLaw, "2");
			range.Add(GRSbjKeys.Additional, "4");

			return range;
		}
	}
}
