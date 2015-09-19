using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct OrgLevelKeys
	{
		public const string School = "School";
		public const string District = "District";
		public const string State = "State";
	}

	public class OrgLevel : ParameterValues
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

			range.Add(OrgLevelKeys.School, "sc");
			range.Add(OrgLevelKeys.District, "di");
			range.Add(OrgLevelKeys.State, "st");

			return range;
		}
	}
}
