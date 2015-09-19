using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct TQSubjectsSPKeys
	{
		public const string SpecEdCore = "Special Education Core Subjects";
		public const string Core = "Core Subjects Summary";
		public const string SpecEdSumm = "Special Education Summary";
		public const string All = "Summary All Subjects";
	}

	public class TQSubjectsSP : ParameterValues
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

			range.Add(TQSubjectsSPKeys.SpecEdCore, "SPCORE");
			range.Add(TQSubjectsSPKeys.Core, "CORESUM");
			range.Add(TQSubjectsSPKeys.SpecEdSumm, "SPSUM");
			range.Add(TQSubjectsSPKeys.All, "SUMALL");

			return range;
		}
	}
}
