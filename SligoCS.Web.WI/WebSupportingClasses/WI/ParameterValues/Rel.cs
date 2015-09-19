using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct RelKeys
	{
		public const string DistrictSpending = "District Spending";
		public const string DistrictSize = "District Size";
		public const string EconDisadvantaged = "% Economically Disadvantaged";
		public const string LEP = "% Limited English Proficient";
		public const string Disabilities = "% Students with Disabilities";
		public const string Native = "% Am Indian";
		public const string Asian = "% Asian";
		public const string Black = "% Black";
		public const string Hispanic = "% Hispanic";
		public const string White = "% White";
	}

	public class Rel : ParameterValues
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

			range.Add(RelKeys.DistrictSpending, "Spending");
			range.Add(RelKeys.DistrictSize, "Size");
			range.Add(RelKeys.EconDisadvantaged, "EconomicStatus");
			range.Add(RelKeys.LEP, "EnglishProficiency");
			range.Add(RelKeys.Disabilities, "Disability");
			range.Add(RelKeys.Native, "Native");
			range.Add(RelKeys.Asian, "Asian");
			range.Add(RelKeys.Black, "Black");
			range.Add(RelKeys.Hispanic, "Hispanic");
			range.Add(RelKeys.White, "White");

			return range;
		}
	}
}
