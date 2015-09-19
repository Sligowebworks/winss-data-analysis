using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.DAL.WI
{
    public class DALAthleticConf : DALWIBase
    {
        public override string  BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            return GetAthleticConferenceListSQL();
        }
        public String GetAthleticConferenceListSQL()
        {
            return ("select distinct Name,ConferenceKey, year  FROM v_Athletic_Conf  where year = (select max(year) from v_Athletic_Conf)  ORDER BY year, Name");
        }

        public string GetAthleticConfNameByID(int conferenceKey)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select distinct Name,ConferenceKey "
                + "FROM v_Athletic_Conf where [year]=  (select max(year) from v_Athletic_Conf) and ConferenceKey= {0} ",
                 conferenceKey);
            SQL = sql.ToString();
            Query();

            return GetScalarStringByID(DataSet, v_Athletic_Conf.Name);
        }
    }
}


/// 
//For the dropdowns:
//select distinct countyname FROM [Wisconsin].[dbo].[AgencyFull] where [year]=2007

//SELECT distinct [paramName],[ConferenceKey
//  FROM [Wisconsin].[dbo].[v_Athletic_Conf] where [year]=2008

//select distinct cesa, CesaName FROM [Wisconsin].[dbo].[AgencyFull] where [year]=2007 and cesa is not null


//For all schools in a county 
//(vACT.county = '47' or vACT.fullkey = '032527040040' )
//(vACT.county = '47' or vACT.fullkey = '03252703XXXX' )


//For all schools in a Athletic Conference
//(ConferenceKey = '45' or vACT.fullkey =

//For all schools in a cesa
//(vACT.CESA = '05' or vACT.fullkey = '032527040040' )

///