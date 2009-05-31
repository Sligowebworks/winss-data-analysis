using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct CompareToKeys
	{
		public const string Years = "Prior Years";
		public const string OrgLevel = "District/State";
		public const string SelSchools = "Selected Schools";
		public const string Current = "Current School Data";
		public const string SelDistricts = "Selected Districts";
	}

	public class CompareTo : ParameterValues
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

			range.Add(CompareToKeys.Years, "PRIORYEARS");
			range.Add(CompareToKeys.OrgLevel, "DISTSTATE");
			range.Add(CompareToKeys.SelSchools, "SELSCHOOLS");
			range.Add(CompareToKeys.Current, "CURRENTONLY");
			range.Add(CompareToKeys.SelDistricts, "SELDISTRICTS");

			return range;
		}
	}
}
