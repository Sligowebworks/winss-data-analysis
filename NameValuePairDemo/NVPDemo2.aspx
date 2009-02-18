<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NVPDemo2.aspx.cs" Inherits="NameValuePairDemo_NVPDemo2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
            <td colspan="2">
                This table displays the session variables' values.
                These values were set on the previous page (NVPDemo1.aspx)                
            </td>
        </tr>
        <tr>
            <td>School Type</td>
            <td><asp:Label ID="lblSchoolType" runat="server" /></td>
        </tr>
        <tr>
            <td>Year</td>
            <td><asp:Label ID="lblYear" runat="server" /></td>
        </tr>
        <tr>
            <td>Attendance Percentage</td>
            <td><asp:Label ID="lblAttendance" runat="server" /></td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
