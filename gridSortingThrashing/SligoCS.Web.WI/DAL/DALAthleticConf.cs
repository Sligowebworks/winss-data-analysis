using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI.DAL.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALAthleticConf : DALWIBase
    {
        public v_Athletic_Conf GetAthleticConfList(int year)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select distinct Name,ConferenceKey FROM v_Athletic_Conf where [year]= {0} ", year);

            v_Athletic_Conf ds = new v_Athletic_Conf();
            base.GetDS(ds, sql.ToString(), ds._v_Athletic_Conf.TableName);
            return ds;
        }

        public string GetAthleticConfNameByID(int conferenceKey, int year)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select distinct Name,ConferenceKey "
                + "FROM v_Athletic_Conf where [year]= {0} and ConferenceKey= {1} ",
                year, conferenceKey);

            v_Athletic_Conf ds = new v_Athletic_Conf();
            base.GetDS(ds, sql.ToString(), ds._v_Athletic_Conf.TableName);
            return GetScalarStringByID (ds, ds._v_Athletic_Conf.NameColumn.ColumnName);
        }
    }
}


/// 
//For the dropdowns:
//select distinct countyname FROM [Wisconsin].[dbo].[v_AgencyFull] where [year]=2007

//SELECT distinct [paramName],[ConferenceKey
//  FROM [Wisconsin].[dbo].[v_Athletic_Conf] where [year]=2008

//select distinct cesa, CesaName FROM [Wisconsin].[dbo].[v_AgencyFull] where [year]=2007 and cesa is not null


//For all schools in a county 
//(vACT.county = '47' or vACT.fullkey = '032527040040' )
//(vACT.county = '47' or vACT.fullkey = '03252703XXXX' )


//For all schools in a Athletic Conference
//(ConferenceKey = '45' or vACT.fullkey =

//For all schools in a cesa
//(vACT.CESA = '05' or vACT.fullkey = '032527040040' )

///