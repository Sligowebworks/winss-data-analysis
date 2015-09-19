using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct TQRelateToKeys
	{
		public const string Spending = "District Spending";
		public const string DistrictSize = "District Size";
		public const string SchoolSize = "School Size";
		public const string EconomicStatus = "&#37; Economically Disadvantaged";
		public const string EnglishProficiency = "&#37; Limited English Proficient";
		public const string Disability = "&#37; Students with Disabilities";
		public const string NativeAm = "&#37; Amer Indian";
		public const string Asian = "&#37; Asian";
		public const string Black = "&#37; Black";
		public const string Hispanic = "&#37; Hispanic";
		public const string Pacific = "&#37; Pacific Isle";
		public const string White = "&#37; White";
		public const string TwoPlusRaces = "&#37; Two or More";
	}

	public class TQRelateTo : ParameterValues
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

			range.Add(TQRelateToKeys.Spending, "SPND");
			range.Add(TQRelateToKeys.DistrictSize, "DSZE");
			range.Add(TQRelateToKeys.SchoolSize, "SSZE");
			range.Add(TQRelateToKeys.EconomicStatus, "Econ");
			range.Add(TQRelateToKeys.EnglishProficiency, "LEP");
			range.Add(TQRelateToKeys.Disability, "DISAB");
			range.Add(TQRelateToKeys.NativeAm, "Ntv");
			range.Add(TQRelateToKeys.Asian, "Asn");
			range.Add(TQRelateToKeys.Black, "Blck");
			range.Add(TQRelateToKeys.Hispanic, "Hsp");
			range.Add(TQRelateToKeys.Pacific, "Pac");
			range.Add(TQRelateToKeys.White, "Wht");
			range.Add(TQRelateToKeys.TwoPlusRaces, "2OrMore");

			return range;
		}
	}
}
