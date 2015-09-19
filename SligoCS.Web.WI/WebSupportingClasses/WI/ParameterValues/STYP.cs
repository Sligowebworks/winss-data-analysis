using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct STYPKeys
	{
		public const string AllTypes = "All Types";
		public const string Elem = "Elem";
		public const string Mid = "Mid/Jr Hi";
		public const string Hi = "High";
		public const string ElSec = "El/Sec";
		public const string StateSummary = "School Summary";
	}

	public class STYP : ParameterValues
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

			range.Add(STYPKeys.AllTypes, "1");
			range.Add(STYPKeys.Elem, "6");
			range.Add(STYPKeys.Mid, "5");
			range.Add(STYPKeys.Hi, "3");
			range.Add(STYPKeys.ElSec, "7");
			range.Add(STYPKeys.StateSummary, "9");

			return range;
		}
	}
}
