using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct SimKeys
	{
		public const string Default = "Default Similar";
		public const string Outperform = "View list of all 'similar' districts that outperformed my school.";
		public const string AllSimilar = "View list of all 'similar districts.";
	}

	public class Sim : ParameterValues
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

			range.Add(SimKeys.Default, "D");
			range.Add(SimKeys.Outperform, "O");
			range.Add(SimKeys.AllSimilar, "A");

			return range;
		}
	}
}
