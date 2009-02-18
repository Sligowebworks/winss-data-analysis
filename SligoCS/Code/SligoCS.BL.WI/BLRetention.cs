using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;

namespace SligoCS.BL.WI
{

    



    public class BLRetention : EntityWIBase 
    {

        //default constructor
        public BLRetention()
        {
            //set default yearspan
            _trendStartYear = 1997;
            _currentYear = 2006;
        }

        


        public v_RetentionWWoDisSchoolDistState GetRetentionData()
        {

            List<int> schoolTypes = GetSchoolTypesList(this.SchoolType);

            //create a bunch of Generics Lists, automatically populated with a 9 (the default value).
            List<int> sexCode = GenericsListHelper.GetPopulatedList(9);
            List<int> raceCode = GenericsListHelper.GetPopulatedList(9);
            List<int> disabilityCode = GenericsListHelper.GetPopulatedList(9);

            //year defaults to _currentYear (2006)
            List<int> year = GenericsListHelper.GetPopulatedList(_currentYear);

            //default value for grade code == 98.
            List<int> gradeCodeRange = GenericsListHelper.GetPopulatedList(98);            

            //Special logic for which fullkeys to use.
            _fullKeys = GetFullKeysList(CompareTo, OrigFullKey, CompareToSchools);


            //prepare values in each Generics List.
            //The values below were laid down in the original Wisconsin website.
            //Since the upgrade of the website did NOT include changing the database design, 
            //the Business Layer is an good location to store these literal values.
            if (ViewBy == ViewByGroup.Gender)
                //replace default list with explicit list
                sexCode = GenericsListHelper.GetPopulatedList(1, 2);
            else if (ViewBy == ViewByGroup.RaceEthnicity)
                raceCode = GenericsListHelper.GetPopulatedList(1, 2, 3, 4, 5);
            else if (ViewBy == ViewByGroup.Grade)
            {
                OrgLevel orgLevel = GetOrgLevelFromFullKey(OrigFullKey);
                if (orgLevel == OrgLevel.School)
                    gradeCodeRange = GenericsListHelper.GetPopulatedList(52, 64);
                else
                    gradeCodeRange = GenericsListHelper.GetPopulatedList(16, 64);
            }
            else if (ViewBy == ViewByGroup.Disability)
                disabilityCode = GenericsListHelper.GetPopulatedList(1, 2);


            if (CompareTo == CompareTo.PRIORYEARS)
                year = GenericsListHelper.GetPopulatedList(_trendStartYear, _currentYear);
            

            DALRetention retention = new DALRetention();
            v_RetentionWWoDisSchoolDistState ds = new v_RetentionWWoDisSchoolDistState();
            List<string> orderBy = GetOrderByList(ds._v_RetentionWWoDisSchoolDistState, CompareTo, OrigFullKey);
            ds = retention.GetRetentionData(raceCode, sexCode, disabilityCode, year, _fullKeys, gradeCodeRange, schoolTypes, orderBy);
            this.sql = retention.SQL;
            return ds;
        }


        

        


        

    }
}
