using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct WeaponKeys
	{
		public const string Yes = "Weapon/Drug Related";
		public const string No = "Not Weapon/Drug Related";
	}

	public class Weapon : ParameterValues
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

			range.Add(WeaponKeys.Yes, "YES");
			range.Add(WeaponKeys.No, "NO");

			return range;
		}
	}
}
