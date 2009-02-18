using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// This class represents the 3rd tier- Data Access Layer.  In production, this class will most likely reside
/// in its own .DLL, but for the purposes of this project it just resides in its own class.
/// </summary>
public class DAL
{
	public DAL()
	{	        
	}

    public DataSet GetSampleData()
    {

        //string sql = "select * from SampleData";
        //string sql = "select top 100 * FROM V_HSCWWoDisDistState";
        string sql = "select top 100 * from v_RetentionWWoDisSchoolDistState";
        DataSet ds = GetDS(sql);
        return ds;
        

    }

    private DataSet GetDS(string sql)
    {
        string connString = ConfigurationManager.AppSettings["connString"];
        SqlDataAdapter da = new SqlDataAdapter(sql, connString);
        DataSet ds = new DataSet();
        da.Fill(ds);
        return ds;
    }
}
