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
		public const string NativeAm = "&#37; Am Indian";
		public const string Asian = "&#37; Asian";
		public const string Black = "&#37; Black";
		public const string Hispanic = "&#37; Hispanic";
		public const string White = "&#37; White";
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

			range.Add(TQRelateToKeys.Spending, "Spending");
			range.Add(TQRelateToKeys.DistrictSize, "DistSize");
			range.Add(TQRelateToKeys.SchoolSize, "SchoolSize");
			range.Add(TQRelateToKeys.EconomicStatus, "EconomicStatus");
			range.Add(TQRelateToKeys.EnglishProficiency, "EnglishProficiency");
			range.Add(TQRelateToKeys.Disability, "Disability");
			range.Add(TQRelateToKeys.NativeAm, "Native");
			range.Add(TQRelateToKeys.Asian, "Asian");
			range.Add(TQRelateToKeys.Black, "Black");
			range.Add(TQRelateToKeys.Hispanic, "Hispanic");
			range.Add(TQRelateToKeys.White, "White");

			return range;
		}
	}
}
