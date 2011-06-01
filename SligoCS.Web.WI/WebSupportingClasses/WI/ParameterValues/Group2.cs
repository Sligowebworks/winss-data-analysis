using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct Group2Keys
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
		public const string None = "No Option Selected";
	}

	public class Group2 : ParameterValues
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

			range.Add(Group2Keys.DistrictSpending, "Spending");
			range.Add(Group2Keys.DistrictSize, "Size");
			range.Add(Group2Keys.EconDisadvantaged, "EconomicStatus");
			range.Add(Group2Keys.LEP, "EnglishProficiency");
			range.Add(Group2Keys.Disabilities, "Disability");
			range.Add(Group2Keys.Native, "Native");
			range.Add(Group2Keys.Asian, "Asian");
			range.Add(Group2Keys.Black, "Black");
			range.Add(Group2Keys.Hispanic, "Hispanic");
			range.Add(Group2Keys.White, "White");
			range.Add(Group2Keys.None, "None");

			return range;
		}
	}
}
