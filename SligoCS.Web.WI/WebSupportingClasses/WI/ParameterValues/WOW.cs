using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct WOWKeys
	{
		public const string WSASCombined = "WSAS: WKCE and WAA Combined";
		public const string WKCE = "WKCE Only";
	}

	public class WOW : ParameterValues
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

			range.Add(WOWKeys.WSASCombined, "WSAS");
			range.Add(WOWKeys.WKCE, "WKCE");

			return range;
		}
	}
}
