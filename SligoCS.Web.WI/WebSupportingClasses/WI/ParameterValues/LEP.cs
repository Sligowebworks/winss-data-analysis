using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct LEPKeys
	{
		public const string On = "%  Limited English Proficient";
		public const string Off = "% Limited English Proficient";
	}

	public class LEP : ParameterValues
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

			range.Add(LEPKeys.On, "Y");
			range.Add(LEPKeys.Off, "N");

			return range;
		}
	}
}
