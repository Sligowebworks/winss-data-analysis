using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct SupDwnldKeys
	{
		public const string True = "True";
		public const string False = "False";
	}

	public class SupDwnld : ParameterValues
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

			range.Add(SupDwnldKeys.True, "1");
			range.Add(SupDwnldKeys.False, "0");

			return range;
		}
	}
}
