using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct CourseTypeIDKeys
	{
		public const string AP = "Advanced Placement  rogram&reg;";
		public const string CAPP = "CAPP";
		public const string Other = "Other Courses";
		public const string IB = "International Baccalaureate";
	}

	public class CourseTypeID : ParameterValues
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

			range.Add(CourseTypeIDKeys.AP, "1");
			range.Add(CourseTypeIDKeys.CAPP, "2");
			range.Add(CourseTypeIDKeys.Other, "4");
			range.Add(CourseTypeIDKeys.IB, "6");

			return range;
		}
	}
}
