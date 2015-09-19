using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
	public struct GradeKeys
	{
		public const string AllDisAgg = "All Tested Grades";
		public const string TBD = "TBD";
		public const string Four_Year_Old_Kindergarten = "Four Year Old Kindergarten";
		public const string Pre_Kindergarten = "Pre-Kindergarten";
		public const string Kinder = "Kinder.";
		public const string Grade_1 = "1";
		public const string Grade_2 = "2";
		public const string Grade_3 = "3";
		public const string Grade_4 = "4";
		public const string Grade_5 = "5";
		public const string Grade_6 = "6";
		public const string Grade_7 = "7";
		public const string Grade_8 = "8";
		public const string Grade_9 = "9";
		public const string Grade_10 = "10";
		public const string Grade_11 = "11";
		public const string Grade_12 = "12";
		public const string GradeCode70 = "Elementary School Type/ Prek-12";
		public const string GradeCode71 = "Middle/Junior High School Type/ Prek-12";
		public const string GradeCode72 = "High School Type/ PreK-12";
		public const string GradeCode73 = "Combined Elementary/Secondary School Type/ Prek-12";
		public const string GradeCode74 = "Elementary School Type/ K-12";
		public const string GradeCode75 = "Middle/Junior High School Type/ K-12";
		public const string GradeCode76 = "High School Type / K-12";
		public const string GradeCode77 = "Combined Elementary/Secondary School Type/K-12";
		public const string GradeCode78 = "Elementary School Type/Grades 7-12";
		public const string GradeCode79 = "Middle/Junior High School Type/Grades 7-12";
		public const string GradeCode80 = "High School Type/Grades 7-12";
		public const string GradeCode81 = "Comb. Elem/Second School Type/Grades 7-12";
		public const string GradeCode82 = "Elementary School Type/Grades 9-12";
		public const string GradeCode83 = "Middle/Junior High School Type/Grades 9-12";
		public const string GradeCode84 = "High School Type/Grades 9-12";
		public const string GradeCode85 = "Combined Elem/Secondary/ Grades 9-12";
		public const string GradeCode86 = "Elementary School Type/Grades 10-12";
		public const string GradeCode87 = "Middle/Junior High School Type/Grades 10-12";
		public const string GradeCode88 = "High School Type/Grades 10-12";
		public const string GradeCode89 = "Combined Elem/Secondary School Type/Grades 10-12";
		public const string GradeCode90 = "Elementary School Type/Grades 6-12";
		public const string GradeCode91 = "Middle/Junior High School Type/Grades 6-12";
		public const string GradeCode92 = "High School Type/Grades 6-12";
		public const string GradeCode93 = "Combined Elem/Secondary School Type/Grades 6-12";
		public const string Grades_6_12_Combined = "Grades 6-12";
		public const string Grades_7_12_Combined = "Grades 7-12 Combined";
		public const string Grades_9_12_Combined = "Grades 9-12 Combined";
		public const string Grades_10_12_Combined = "Grades 10-12 Combined";
		public const string Grades_K_12 = "Grades K-12";
		public const string Combined_PreK_12 = "Combined Grades";
	}

	public class Grade : ParameterValues
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

			range.Add(GradeKeys.AllDisAgg, "0");
			range.Add(GradeKeys.TBD, "9");
			range.Add(GradeKeys.Four_Year_Old_Kindergarten, "10");
			range.Add(GradeKeys.Pre_Kindergarten, "12");
			range.Add(GradeKeys.Kinder, "16");
			range.Add(GradeKeys.Grade_1, "20");
			range.Add(GradeKeys.Grade_2, "24");
			range.Add(GradeKeys.Grade_3, "28");
			range.Add(GradeKeys.Grade_4, "32");
			range.Add(GradeKeys.Grade_5, "36");
			range.Add(GradeKeys.Grade_6, "40");
			range.Add(GradeKeys.Grade_7, "44");
			range.Add(GradeKeys.Grade_8, "48");
			range.Add(GradeKeys.Grade_9, "52");
			range.Add(GradeKeys.Grade_10, "56");
			range.Add(GradeKeys.Grade_11, "60");
			range.Add(GradeKeys.Grade_12, "64");
			range.Add(GradeKeys.GradeCode70, "70");
			range.Add(GradeKeys.GradeCode71, "71");
			range.Add(GradeKeys.GradeCode72, "72");
			range.Add(GradeKeys.GradeCode73, "73");
			range.Add(GradeKeys.GradeCode74, "74");
			range.Add(GradeKeys.GradeCode75, "75");
			range.Add(GradeKeys.GradeCode76, "76");
			range.Add(GradeKeys.GradeCode77, "77");
			range.Add(GradeKeys.GradeCode78, "78");
			range.Add(GradeKeys.GradeCode79, "79");
			range.Add(GradeKeys.GradeCode80, "80");
			range.Add(GradeKeys.GradeCode81, "81");
			range.Add(GradeKeys.GradeCode82, "82");
			range.Add(GradeKeys.GradeCode83, "83");
			range.Add(GradeKeys.GradeCode84, "84");
			range.Add(GradeKeys.GradeCode85, "85");
			range.Add(GradeKeys.GradeCode86, "86");
			range.Add(GradeKeys.GradeCode87, "87");
			range.Add(GradeKeys.GradeCode88, "88");
			range.Add(GradeKeys.GradeCode89, "89");
			range.Add(GradeKeys.GradeCode90, "90");
			range.Add(GradeKeys.GradeCode91, "91");
			range.Add(GradeKeys.GradeCode92, "92");
			range.Add(GradeKeys.GradeCode93, "93");
			range.Add(GradeKeys.Grades_6_12_Combined, "94");
			range.Add(GradeKeys.Grades_7_12_Combined, "95");
			range.Add(GradeKeys.Grades_9_12_Combined, "96");
			range.Add(GradeKeys.Grades_10_12_Combined, "97");
			range.Add(GradeKeys.Grades_K_12, "98");
			range.Add(GradeKeys.Combined_PreK_12, "99");

			return range;
		}
	}
}
