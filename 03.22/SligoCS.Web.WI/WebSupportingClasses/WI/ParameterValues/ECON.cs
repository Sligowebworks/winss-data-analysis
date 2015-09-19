using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct ECONKeys
	{
		public const string On = "%  Economically Disadvantaged";
		public const string Off = "% Economically Disadvantaged";
	}

	public class ECON : ParameterValues
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

			range.Add(ECONKeys.On, "Y");
			range.Add(ECONKeys.Off, "N");

			return range;
		}
	}
}
