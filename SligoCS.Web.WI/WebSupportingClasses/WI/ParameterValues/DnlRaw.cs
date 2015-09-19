using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct DnlRawKeys
	{
		public const string Download = "Download";
		public const string Disable = "Disable";
	}

	public class DnlRaw : ParameterValues
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

			range.Add(DnlRawKeys.Download, "T");
			range.Add(DnlRawKeys.Disable, "F");

			return range;
		}
	}
}
