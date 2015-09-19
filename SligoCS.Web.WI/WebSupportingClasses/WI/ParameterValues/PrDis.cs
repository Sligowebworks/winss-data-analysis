using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct PrDisKeys
	{
		public const string AllDisabilities = "All Primary Disabilities";
		public const string Cognitive = "Cognitive Disability (CD)";
		public const string Emotional = "Emotional Behavioral Disability (EBD)";
		public const string Learning = "Specific Learning Disability (LD)";
		public const string SpeachLanguage = "Speech or Language Impairment (SL)";
		public const string Autism = "Autism (A)";
		public const string DeafBlind = "Deaf-Blind (DB)";
		public const string Hearing = "Hearing Impairment (HI)";
		public const string OtherHealth = "Other Primary Disability (OPD)";
		public const string Orthopedic = "Orthopedic Impairment (OI)";
		public const string Developmental = "Significant Developmental Delay (SDD)";
		public const string TraumaticBrain = "Traumatic Brain Injury (TBI)";
		public const string Visual = "Visual Impairment (VI)";
		public const string Combined = "Combined (SWD)";
	}

	public class PrDis : ParameterValues
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

			range.Add(PrDisKeys.AllDisabilities, "apd");
			range.Add(PrDisKeys.Cognitive, "cd");
			range.Add(PrDisKeys.Emotional, "ebd");
			range.Add(PrDisKeys.Learning, "ld");
			range.Add(PrDisKeys.SpeachLanguage, "sl");
			range.Add(PrDisKeys.Autism, "a");
			range.Add(PrDisKeys.DeafBlind, "db");
			range.Add(PrDisKeys.Hearing, "hi");
			range.Add(PrDisKeys.OtherHealth, "ohi");
			range.Add(PrDisKeys.Orthopedic, "oi");
			range.Add(PrDisKeys.Developmental, "sdd");
			range.Add(PrDisKeys.TraumaticBrain, "tbi");
			range.Add(PrDisKeys.Visual, "vi");
			range.Add(PrDisKeys.Combined, "swd");

			return range;
		}
	}
}
