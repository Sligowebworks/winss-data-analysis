<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="SligoDataGrid.aspx.cs" Inherits="_Default"%>

<%@ Register Src="SligoDataGrid.ascx" TagName="SligoDataGrid" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Test page</title>
</head>
<body>
    <form id="form1" runat="server">
    
    This is a test web page to demonstrate the Sligo Data Grid.
    <div>
        <uc1:SligoDataGrid ID="SligoDataGrid1" runat="server" />
    
    </div>
    </form>
</body>
</html>
