using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct WkceWsasKeys
	{
		public const string Wsas = "[WSAS: WKCE and WAA Combined";
		public const string Wkcs = "WKCE Only]";
	}

	public class WkceWsas : ParameterValues
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

			range.Add(WkceWsasKeys.Wsas, "ws");
			range.Add(WkceWsasKeys.Wkcs, "wk");

			return range;
		}
	}
}
