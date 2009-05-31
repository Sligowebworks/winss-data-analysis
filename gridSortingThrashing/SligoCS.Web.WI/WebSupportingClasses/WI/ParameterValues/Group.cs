using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct GroupKeys
	{
		public const string All = "All Students";
		public const string Gender = "Gender";
		public const string Race = "Race/Ethnicity";
		public const string Grade = "Grade";
		public const string Disability = "Disability";
		public const string EconDisadv = "Economic Status";
		public const string EngLangProf = "English Proficiency";
		public const string Migrant = "Migrant Status";
	}

	public class Group : ParameterValues
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

			range.Add(GroupKeys.All, "AllStudentsFAY");
			range.Add(GroupKeys.Gender, "Gender");
			range.Add(GroupKeys.Race, "RaceEthnicity");
			range.Add(GroupKeys.Grade, "Grade");
			range.Add(GroupKeys.Disability, "Disability");
			range.Add(GroupKeys.EconDisadv, "EconDisadv");
			range.Add(GroupKeys.EngLangProf, "ELP");
			range.Add(GroupKeys.Migrant, "Mig");

			return range;
		}
	}
}
