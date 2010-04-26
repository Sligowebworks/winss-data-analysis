using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct NoChceKeys
	{
		public const string On = "No Criteria  Chosen";
		public const string Off = "No Criteria Chosen";
	}

	public class NoChce : ParameterValues
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

			range.Add(NoChceKeys.On, "Y");
			range.Add(NoChceKeys.Off, "N");

			return range;
		}
	}
}
