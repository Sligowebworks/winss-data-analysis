using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// This class represents the 2nd tier- the business layer.  In production, 
/// the business layer will probably consist of many classes and reside in its 
/// own .DLL, but for the purposes
/// of this demo it just resides in its own class.
/// 
/// The purpose of the business layer will be to contain rules that are consistent for 
/// the business, regardless of client (web vs. web service vs. desktop app); and 
/// independent of data source (SQL Server 2000 vs. SQL Server 2005 vs. CSV flat file).
/// </summary>
public class BusinessLayer
{
	public BusinessLayer()
	{
		
	}



    /// <summary>
    /// The 1st tier, the client layer, calls only methods in the business layer (2nd tier).
    /// In this case, the business layer is a simple passthrough, but it will frequently 
    /// contain business rule logic.    
    /// </summary>
    /// <returns></returns>
    public DataSet GetSampleData()
    {
        DAL dal = new DAL();
        DataSet ds = dal.GetSampleData();
        return ds;
    }
}
