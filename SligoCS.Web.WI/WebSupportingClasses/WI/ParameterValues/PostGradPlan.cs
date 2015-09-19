using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct PostGradPlanKeys
	{
		public const string All = "All Options";
		public const string FourYr = "4-Yr College/University";
		public const string VocTechCollege = "Voc/Tec College";
		public const string Employment = "Employment";
		public const string Military = "Military";
		public const string Training = "Job Training";
		public const string SeekEmploy = "Seeking Employment";
		public const string Other = "Other Plans";
		public const string Undecided = "Undecided";
		public const string NoResponse = "No Response";
	}

	public class PostGradPlan : ParameterValues
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

			range.Add(PostGradPlanKeys.All, "ALL");
			range.Add(PostGradPlanKeys.FourYr, "4-YR");
			range.Add(PostGradPlanKeys.VocTechCollege, "VOC");
			range.Add(PostGradPlanKeys.Employment, "EMP");
			range.Add(PostGradPlanKeys.Military, "MILITARY");
			range.Add(PostGradPlanKeys.Training, "JOB");
			range.Add(PostGradPlanKeys.SeekEmploy, "SEEKEMP");
			range.Add(PostGradPlanKeys.Other, "OTHER");
			range.Add(PostGradPlanKeys.Undecided, "UNDECIDED");
			range.Add(PostGradPlanKeys.NoResponse, "NO-RESP");

			return range;
		}
	}
}
