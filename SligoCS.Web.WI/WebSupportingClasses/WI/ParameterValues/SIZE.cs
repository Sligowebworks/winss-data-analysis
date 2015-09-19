using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct SIZEKeys
	{
		public const string On = "District  Size";
		public const string Off = "District Size";
	}

	public class SIZE : ParameterValues
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

			range.Add(SIZEKeys.On, "Y");
			range.Add(SIZEKeys.Off, "N");

			return range;
		}
	}
}
