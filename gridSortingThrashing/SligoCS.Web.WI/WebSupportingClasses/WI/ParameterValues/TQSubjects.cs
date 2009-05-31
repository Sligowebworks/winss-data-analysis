using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct TQSubjectsKeys
	{
		public const string EngLangArt = "English Language Arts";
		public const string Mathematics = "Mathematics";
		public const string Science = "Science";
		public const string SoclStd = "Social Studies";
		public const string FrgnLang = "Foreign Languages";
		public const string Arts = "The Arts Art And Design Dance Music Theatre";
		public const string Elem = "Elementary All Subjects";
		public const string SpecEdCore = "Special Education Core Subjects";
		public const string SpecEdSumm = "Special Education Summary";
		public const string Core = "Core Subjects Summary";
		public const string All = "Summary All Subjects";
	}

	public class TQSubjects : ParameterValues
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

			range.Add(TQSubjectsKeys.EngLangArt, "ELA");
			range.Add(TQSubjectsKeys.Mathematics, "MATH");
			range.Add(TQSubjectsKeys.Science, "SCI");
			range.Add(TQSubjectsKeys.SoclStd, "SOC");
			range.Add(TQSubjectsKeys.FrgnLang, "FLANG");
			range.Add(TQSubjectsKeys.Arts, "ARTS");
			range.Add(TQSubjectsKeys.Elem, "ELSUBJ");
			range.Add(TQSubjectsKeys.SpecEdCore, "SPCORE");
			range.Add(TQSubjectsKeys.SpecEdSumm, "SPSUM");
			range.Add(TQSubjectsKeys.Core, "CORESUM");
			range.Add(TQSubjectsKeys.All, "SUMALL");

			return range;
		}
	}
}
