using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.BL.WI
{
    /*public enum S4orALL 
    {
        FourSchoolsOrDistrictsIn = 1,
        AllSchoolsOrDistrictsIn = 2
    }*/

   /* public enum SRegion
    {
        County = 1,
        AthleticConf = 2,
        CESA = 3
    }*/
 
   /*public enum OrgLevel
    {
        School,
        District,
        State
    }*/

    public enum OrderByCols
    {
        year,
        OrgLevelLabel,
        fullkey,
        Name,
        SchoolTypeLabel,
        SexCode,
        RaceCode,
        GradeCode,
        DisabilityCode
    }

    //Notes for graph
    public enum StackedType
    {
        No,
        Normal,
        Stacked100
    }

}
