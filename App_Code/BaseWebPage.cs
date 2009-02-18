using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// This will be the base web page for all pages in the Wisconsin website.
/// This class belongs in the 1st tier, because it is specific to a website.
/// </summary>
public abstract class BaseWebPage : Page 
{
    private const string SESSIONKEY = "sessionKey";
    private Dictionary<SessionVars, string> mySession;



    /// <summary>
    /// This enum represents the set of variables we would keep
    /// throughout each web user's session.  I believe there are currently
    /// around 25; this is meant as a demo.
    /// </summary>
    protected enum SessionVars
    {
        SchoolType,
        Year,
        PercentAttendance
    }

    

    /// <summary>
    /// Retrieves my session variables from the Session object.
    /// We only need to do this once per page load.  After that, 
    /// the page level variable will have already been initialized 
    /// and a call to LoadMySession() will bypass the Session object
    /// and use the local variable directly.
    /// </summary>
    private void LoadMySession()
    {
        if (mySession == null)
        {
            object tmp = Session[SESSIONKEY];
            if (tmp == null)
            {
                //create a new dictionary and stash it in the user's session.
                mySession = new Dictionary<SessionVars, string>();
                Session[SESSIONKEY] = mySession;
            }
            else
                //The user already has a session in progress, so recover it.
                mySession = (Dictionary<SessionVars, string>)tmp;
        }
    }


    /// <summary>
    /// Sets the value of one session variable.
    /// </summary>
    /// <param name="varName"></param>
    /// <param name="value"></param>
	protected void SetSessionVariable(SessionVars varName, string value)
    {
        LoadMySession();
        mySession[varName] = value;        
    }



    protected string GetSessionValue(SessionVars varName)
    {
        LoadMySession();
        string retval = string.Empty;
        if (mySession.ContainsKey(varName))
            retval = mySession[varName];

        return retval;
    }
    
}
