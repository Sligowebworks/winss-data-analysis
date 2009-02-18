<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NVPDemo1.aspx.cs" Inherits="NameValuePairDemo_NVPDemo1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Name - value pair demo page 1</title>
</head>
<body>
    <form id="form1" runat="server">
    
    
    <div>
        <table border="1">
        <tr>
            <td colspan="2">
                This table represents a potential way to handle a user's session.
                <br />
                Each session variable is listed in an enumerated type.
                <br />
                The user's session is a single object, a dictionary, 
                which connects each of the enumerated types with its value.
                <br />
                Each page has access to the same code for getting/setting the 
                session, because each page will be derived from the same abstract
                base web page (aptly named BaseWebPage in this demo).                            
            </td>
        </tr>
        <tr>
            <td>School Type</td>
            <td><asp:TextBox ID="txtSchoolType" runat="server" Text="High School"/></td>
        </tr>
        <tr>
            <td>Year</td>
            <td><asp:TextBox ID="txtYear" runat="server" Text="2007" /></td>
        </tr>
        <tr>
            <td>Attendance Percentage</td>
            <td><asp:TextBox ID="txtAttendance" runat="server" Text="98.6" /></td>
        </tr>
        <tr>
            <td><asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" /></td>
            <td></td>
        </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
