using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct StaffRatioKeys
	{
		public const string Staff = "Staff to Students";
		public const string Student = "Students to Staff";
	}

	public class StaffRatio : ParameterValues
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

			range.Add(StaffRatioKeys.Staff, "STAFFSTUDENT");
			range.Add(StaffRatioKeys.Student, "STUDENTSTAFF");

			return range;
		}
	}
}
