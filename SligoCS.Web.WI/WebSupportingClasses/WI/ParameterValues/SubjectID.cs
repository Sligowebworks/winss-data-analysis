using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct SubjectIDKeys
	{
		public const string AllTested = "All Tested Subjects";
		public const string Reading = "Reading";
		public const string Language = "Language Arts";
		public const string Math = "Mathematics";
		public const string Science = "Science";
		public const string SocialStudies = "Social Studies";
	}

	public class SubjectID : ParameterValues
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

			range.Add(SubjectIDKeys.AllTested, "0AS");
			range.Add(SubjectIDKeys.Reading, "1RE");
			range.Add(SubjectIDKeys.Language, "2LA");
			range.Add(SubjectIDKeys.Math, "3MA");
			range.Add(SubjectIDKeys.Science, "4SC");
			range.Add(SubjectIDKeys.SocialStudies, "5SS");

			return range;
		}
	}
}
