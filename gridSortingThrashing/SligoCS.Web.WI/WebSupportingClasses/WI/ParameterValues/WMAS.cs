using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct WMASKeys
	{
		public const string Agricultur = "Agricultural Education";
		public const string Art = "Art and Design";
		public const string Business = "Business";
		public const string English = "English Language Arts";
		public const string Family = "Family and Consumer Ed";
		public const string WorldLang = "World Languages";
		public const string Marketing = "Marketing Education";
		public const string Mathematics = "Mathematics";
		public const string Music = "Music";
		public const string Science = "Science";
		public const string SocialStudies = "Social Studies";
		public const string TechEd = "Technology Education";
		public const string Other = "Other Subjects";
	}

	public class WMAS : ParameterValues
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

			range.Add(WMASKeys.Agricultur, "1");
			range.Add(WMASKeys.Art, "2");
			range.Add(WMASKeys.Business, "3");
			range.Add(WMASKeys.English, "4");
			range.Add(WMASKeys.Family, "5");
			range.Add(WMASKeys.WorldLang, "6");
			range.Add(WMASKeys.Marketing, "7");
			range.Add(WMASKeys.Mathematics, "8");
			range.Add(WMASKeys.Music, "9");
			range.Add(WMASKeys.Science, "10");
			range.Add(WMASKeys.SocialStudies, "11");
			range.Add(WMASKeys.TechEd, "12");
			range.Add(WMASKeys.Other, "13");

			return range;
		}
	}
}
