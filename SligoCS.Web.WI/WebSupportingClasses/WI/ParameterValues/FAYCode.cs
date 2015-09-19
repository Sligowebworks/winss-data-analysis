using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct FAYCodeKeys
	{
		public const string CompareFayALL = "Compare";
		public const string NonFAY = "All Students";
		public const string FAY = "Full Academic Year";
	}

	public class FAYCode : ParameterValues
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

			range.Add(FAYCodeKeys.CompareFayALL, "0");
			range.Add(FAYCodeKeys.NonFAY, "9");
			range.Add(FAYCodeKeys.FAY, "1");

			return range;
		}
	}
}
