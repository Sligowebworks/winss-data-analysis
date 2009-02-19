using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.BLL.WI
{
    public enum S4orALL 
    {
        FourSchoolsOrDistrictsIn = 1,
        AllSchoolsOrDistrictsIn = 2
    }

    public enum SRegion
    {
        County = 1,
        AthleticConf = 2,
        CESA = 3
    }
 
    public enum ViewByGroup
    {
        AllStudentsFAY,
        Gender,
        RaceEthnicity,
        Grade,
        Disability,
        EconDisadv,
        ELP
    }

    public enum CompareTo
    {
        PRIORYEARS,
        DISTSTATE,
        SELSCHOOLS,
        CURRENTONLY,
        SELDISTRICTS
    }

    public enum OrgLevel
    {
        School,
        District,
        State
    }

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
    public enum CommonColumnNamesForGraph
    {
        YearFormatted,
        SchooltypeLabel,
        RaceShortLabel,
        SexLabel,
        GradeShortLabel,
        DisabilityLabel,
        ShortEconDisadvLabel,
        ShortELPLabel,
        OrgLevelLabel,
        District_Name,
        School_Name,
        AllStudents
    }

    //Notes for graph
    public enum StackedType
    {
        No,
        Normal,
        Stacked100
    }

}
