<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" Codebehind="SchoolScript.aspx.cs"
    Inherits="SligoCS.Web.WI.SchoolScript" Title="WI Select a School - Data Analysis" %>

<%@ Register Src="WebUserControls/CESA_Map_1.ascx" TagName="CESA_Map_1" TagPrefix="uc1" %>
<%@ Register Src="WebUserControls/CESA_Map_2.ascx" TagName="CESA_Map_2" TagPrefix="uc2" %>
<%@ Register Src="WebUserControls/CESA_Map_3.ascx" TagName="CESA_Map_3" TagPrefix="uc3" %>
<%@ Register Src="WebUserControls/CESA_Map_4.ascx" TagName="CESA_Map_4" TagPrefix="uc4" %>
<%@ Register Src="WebUserControls/CESA_Map_5.ascx" TagName="CESA_Map_5" TagPrefix="uc5" %>
<%@ Register Src="WebUserControls/CESA_Map_6.ascx" TagName="CESA_Map_6" TagPrefix="uc6" %>
<%@ Register Src="WebUserControls/CESA_Map_7.ascx" TagName="CESA_Map_7" TagPrefix="uc7" %>
<%@ Register Src="WebUserControls/CESA_Map_8.ascx" TagName="CESA_Map_8" TagPrefix="uc8" %>
<%@ Register Src="WebUserControls/CESA_Map_9.ascx" TagName="CESA_Map_9" TagPrefix="uc9" %>
<%@ Register Src="WebUserControls/CESA_Map_10.ascx" TagName="CESA_Map_10" TagPrefix="uc10" %>
<%@ Register Src="WebUserControls/CESA_Map_11.ascx" TagName="CESA_Map_11" TagPrefix="uc11" %>
<%@ Register Src="WebUserControls/CESA_Map_12.ascx" TagName="CESA_Map_12" TagPrefix="uc12" %>
<%@ Register Src="WebUserControls/CESA_Map_1_2.ascx" TagName="CESA_Map_1_2" TagPrefix="uc13" %>
<%@ Register Src="WebUserControls/CESA_Map_2_2.ascx" TagName="CESA_Map_2_2" TagPrefix="uc14" %>
<%@ Register Src="WebUserControls/CESA_Map_6_2.ascx" TagName="CESA_Map_6_2" TagPrefix="uc15" %>
<%@ Register Src="WebUserControls/CESA_Map_9_2.ascx" TagName="CESA_Map_9_2" TagPrefix="uc16" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    &nbsp;
    <!-- Schools Display -->
    <table style="padding: 0px 0px 0px 0px; width: 390px;">
        <tr>
            <td align="left">
                <slx:SligoDataGrid ID="SligoDataGrid1" runat="server" AutoGenerateColumns="False"
                    ShowSuperHeader="True" BorderStyle="None" HeaderStyle-BackColor="#EFEFEF" HeaderStyle-Font-Underline="true"
                    HeaderStyle-HorizontalAlign="Left" UseAccessibleHeader="False" CellPadding="4"
                    HeaderStyle-Font-Bold="true" RowStyle-Font-Size="8pt" GridLines="None" RowStyle-HorizontalAlign="Left"
                    SuperHeaderText="">
                    <Columns>
                        <asp:BoundField DataField="CountyName" HeaderText="COUNTY" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="DISTRICT" ItemStyle-HorizontalAlign="Left">
                            <itemtemplate>
           <asp:HyperLink ID="DistrictDetails1" runat="server"  
            NavigateUrl='<%# "~/questions.aspx" 
            + GetQueryStringForMultipleParams ( 
                        Eval("fullkey").ToString(),
                        HttpUtility.UrlEncode(Eval("SchoolName").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("DistrictName").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("SchoolType").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("LowGrade").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("HighGrade").ToString().Trim()),
                        "District"
                        ) %>'                
            
            Text='<%# Eval("DistrictName").ToString() %>' />
        
            </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SCHOOL" ItemStyle-HorizontalAlign="Left">
                            <itemtemplate>
            <asp:HyperLink ID="SchoolDetails1" runat="server"  
            NavigateUrl='<%# "~/questions.aspx" 
            + GetQueryStringForMultipleParams ( 
                        Eval("fullkey").ToString(),
                        HttpUtility.UrlEncode(Eval("SchoolName").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("DistrictName").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("SchoolType").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("LowGrade").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("HighGrade").ToString().Trim()),
                        "School"
                        ) %>' 
            Text='<%# Eval("SchoolName").ToString() %>' />
        
            </itemtemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle Font-Size="8pt" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#EFEFEF" Font-Bold="True" Font-Underline="True" HorizontalAlign="Left" />
                </slx:SligoDataGrid>
                <!-- / Schools Display -->
                <!-- District Display -->
                <slx:SligoDataGrid ID="SligoDataGrid2" runat="server" AutoGenerateColumns="False"
                    ShowSuperHeader="true" BorderStyle="None" CellSpacing="0" HeaderStyle-BackColor="#EFEFEF"
                    HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Left" UseAccessibleHeader="False"
                    CellPadding="4" ShowHeader="true" HeaderStyle-Font-Bold="true" RowStyle-Font-Size="8pt"
                    GridLines="None" RowStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:BoundField DataField="CountyName" HeaderText="COUNTY" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="DISTRICT" ItemStyle-HorizontalAlign="Left">
                            <itemtemplate>
            <asp:HyperLink ID="DistrictDetails2" runat="server"  
            NavigateUrl='<%# "~/questions.aspx" 
            + GetQueryStringForMultipleParams ( 
                        Eval("fullkey").ToString(),
                        HttpUtility.UrlEncode(Eval("SchoolName").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("DistrictName").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("SchoolType").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("LowGrade").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("HighGrade").ToString().Trim()),
                        "District"
                        ) %>'   
            
            Text='<%# Eval("DistrictName").ToString() %>' />
            </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SCHOOL" ItemStyle-HorizontalAlign="Left">
                            <itemtemplate>
                <asp:HyperLink ID="ShowSchoolInDistrictDetails2" runat="server"  
                NavigateUrl='<%# "~/SchoolScript.aspx" 
                + UserValues.GetQueryString(
                    new String[] { 
                        "SEARCHTYPE=SC", 
                        "L=0",  
                        "FULLKEY=" + Eval("fullkey").ToString()
                        }) %>'               
                Text='Show Schools' />
            </itemtemplate>
                        </asp:TemplateField>
                    </Columns>
                </slx:SligoDataGrid>
                <!-- / District Display -->
                <!--//This is the show school lins for County (Click 
        on a county letter or Search by County) -->
                <slx:SligoDataGrid ID="SligoDataGrid3" runat="server" AutoGenerateColumns="False"
                    ShowSuperHeader="true" BorderStyle="None" CellSpacing="0" HeaderStyle-BackColor="#EFEFEF"
                    HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Left" UseAccessibleHeader="False"
                    CellPadding="4" ShowHeader="true" HeaderStyle-Font-Bold="true" RowStyle-Font-Size="8pt"
                    GridLines="None" RowStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:BoundField DataField="CountyName" HeaderText="COUNTY" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="DISTRICT" ItemStyle-HorizontalAlign="Left">
                            <itemtemplate>
            <asp:HyperLink ID="DistrictDetails3" runat="server"  
            NavigateUrl='<%# "~/questions.aspx" 
            + GetQueryStringForMultipleParams ( 
                        Eval("fullkey").ToString(),
                        HttpUtility.UrlEncode(Eval("SchoolName").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("DistrictName").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("SchoolType").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("LowGrade").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("HighGrade").ToString().Trim()),
                        "District"
                        ) %>'              
            
            Text='<%# Eval("DistrictName").ToString() %>' />
        </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SCHOOL" ItemStyle-HorizontalAlign="Left">
                            <itemtemplate>
                <asp:HyperLink ID="ShowSchoolInDistrictDetails3" runat="server"  
                NavigateUrl='<%# "~/SchoolScript.aspx" 
                + UserValues.GetQueryString(
                    new String[] { 
                        "SEARCHTYPE=SC", 
                        "L=0",  
                        "FULLKEY=" + Eval("fullkey").ToString()
                        }) %>' 
                Text='Show Schools' />
            </itemtemplate>
                        </asp:TemplateField>
                    </Columns>
                </slx:SligoDataGrid>
                <!-- / District Display -->
                <!-- Schools Display 2-->
                <slx:SligoDataGrid ID="SligoDataGrid4" runat="server" AutoGenerateColumns="False"
                    ShowSuperHeader="true" BorderStyle="None" CellSpacing="0" HeaderStyle-BackColor="#EFEFEF"
                    HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Left" UseAccessibleHeader="False"
                    CellPadding="4" ShowHeader="true" HeaderStyle-Font-Bold="true" RowStyle-Font-Size="8pt"
                    GridLines="None" RowStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:BoundField DataField="CountyName" HeaderText="COUNTY" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="DISTRICT" ItemStyle-HorizontalAlign="Left">
                            <itemtemplate>
            <asp:HyperLink ID="DistrictDetails4" runat="server"  
            NavigateUrl='<%# "~/questions.aspx" 
            + GetQueryStringForMultipleParams ( 
                        Eval("fullkey").ToString(),
                        HttpUtility.UrlEncode(Eval("SchoolName").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("DistrictName").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("SchoolType").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("LowGrade").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("HighGrade").ToString().Trim()),
                        "District"
                        ) %>' 
            Text='<%# Eval("DistrictName").ToString() %>' />
            </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SCHOOL" ItemStyle-HorizontalAlign="Left">
                            <itemtemplate>
            <asp:HyperLink ID="SchoolDetails2" runat="server"  
            NavigateUrl='<%# "~/questions.aspx" 
            + GetQueryStringForMultipleParams ( 
                        Eval("fullkey").ToString(),
                        HttpUtility.UrlEncode(Eval("SchoolName").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("DistrictName").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("SchoolType").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("LowGrade").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("HighGrade").ToString().Trim()),
                        "School"
                        ) %>'             
            Text='<%# Eval("SchoolName").ToString() %>' />
            </itemtemplate>
                        </asp:TemplateField>
                    </Columns>
                </slx:SligoDataGrid>
                <!-- / Schools Display 2 -->
                <asp:Label ID="Label1" Visible="false" runat="server" Text=""></asp:Label>
                <asp:Panel ID="pnl_Map_1" Visible="false" runat="server">
                    <uc1:CESA_Map_1 ID="CESA_Map_1_1" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnl_Map_1_2" Visible="false" runat="server">
                    <uc13:CESA_Map_1_2 ID="CESA_Map_1_2_1" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnl_Map_2" Visible="false" runat="server">
                    <uc2:CESA_Map_2 ID="CESA_Map_2_1" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnl_Map_2_2" Visible="false" runat="server">
                    <uc14:CESA_Map_2_2 ID="CESA_Map_2_2" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnl_Map_3" Visible="false" runat="server">
                    <asp:Label ID="Map_3_Head_Label" CssClass="title" Text="CESA 3: Click on a School District or hot link below"
                        BackColor="#EFEFEF" runat="server" /><br />
                    <uc3:CESA_Map_3 ID="CESA_Map_3_1" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnl_Map_4" Visible="false" runat="server">
                    <asp:Label ID="Map_4_Head_Label" CssClass="title" Text="CESA 4: Click on a School District or hot link below"
                        BackColor="#EFEFEF" runat="server" /><br />
                    <uc4:CESA_Map_4 ID="CESA_Map_4_1" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnl_Map_5" Visible="false" runat="server">
                    <asp:Label ID="Map_5_Head_Label" CssClass="title" Text="CESA 5: Click on a School District or hot link below"
                        BackColor="#EFEFEF" runat="server" /><br />
                    <uc5:CESA_Map_5 ID="CESA_Map_5_1" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnl_Map_6" Visible="false" runat="server" Width="400px">
                    <asp:Label ID="Map_6_Head_Label" CssClass="title" Text="CESA 6: Click on a School District or hot link below"
                        BackColor="#EFEFEF" runat="server" /><br />
                    <br />
                    <asp:HyperLink ID="Map_6_Link_1" BackColor="#EFEFEF" Text="View map that includes union high school districts"
                        Font-Size="12px" NavigateUrl="~/SchoolScript.aspx?SEARCHTYPE=CE&L=6&HS=1" runat="server" /><br />
                    <uc6:CESA_Map_6 ID="CESA_Map_6_1" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnl_Map_6_2" Visible="false" runat="server" Width="400px">
                    <asp:Label ID="Map_6_2_Head_Label" CssClass="title" Text="CESA 6: Click on a School District or hot link below"
                        BackColor="#EFEFEF" runat="server" /><br />
                    <asp:HyperLink ID="Map_6_Link_2" BackColor="#EFEFEF" Text="View map that includes elementary school districts"
                        Font-Size="12px" NavigateUrl="~/SchoolScript.aspx?SEARCHTYPE=CE&L=6&HS=0" runat="server" /><br />
                    <br />
                    <uc15:CESA_Map_6_2 ID="CESA_Map_6_2" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnl_Map_7" Visible="false" runat="server">
                    <asp:Label ID="Map_7_Head_Label" CssClass="title" Text="CESA 7: Click on a School District or hot link below"
                        BackColor="#EFEFEF" runat="server" Width="130px" /><br />
                    <uc7:CESA_Map_7 ID="CESA_Map_7_1" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnl_Map_8" Visible="false" runat="server">
                    <asp:Label ID="Map_8_Head_Label" CssClass="title" Text="CESA 8: Click on a School District or hot link below"
                        BackColor="#EFEFEF" runat="server" /><br />
                    <uc8:CESA_Map_8 ID="CESA_Map_8_1" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnl_Map_9" Visible="false" runat="server">
                    <asp:Label ID="Map_9_Head_Label" CssClass="title" Text="CESA 9: Click on a School District or hot link below"
                        BackColor="#EFEFEF" runat="server" /><br />
                    <asp:HyperLink ID="Map_9_Link" BackColor="#EFEFEF" Text="View map that includes union high  school districts"
                        Font-Size="12px" NavigateUrl="~/SchoolScript.aspx?SEARCHTYPE=CE&L=9&HS=1" runat="server" />
                    <uc9:CESA_Map_9 ID="CESA_Map_9_1" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnl_Map_9_2" Visible="false" runat="server">
                    <asp:Label ID="Map_9_2_Head_Label" CssClass="title" Text="CESA 9: Click on a School District or hot link below"
                        BackColor="#EFEFEF" runat="server" /><br />
                    <asp:HyperLink ID="Map_9_2_Link" BackColor="#EFEFEF" Text="View map that includes elementary school districts"
                        Font-Size="12px" NavigateUrl="~/SchoolScript.aspx?SEARCHTYPE=CE&L=9&HS=0" runat="server" />
                    <uc16:CESA_Map_9_2 ID="CESA_Map_9_2_1" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnl_Map_10" Visible="false" runat="server">
                    <asp:Label ID="Map_10_Head_Label" CssClass="title" Text="CESA 10: Click on a School District or hot link below"
                        BackColor="#EFEFEF" runat="server" /><br />
                    <uc10:CESA_Map_10 ID="CESA_Map_10_1" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnl_Map_11" Visible="false" runat="server">
                    <asp:Label ID="Map_11_Head_Label" CssClass="title" Text="CESA 11: Click on a School District or hot link below"
                        BackColor="#EFEFEF" runat="server" /><br />
                    <uc11:CESA_Map_11 ID="CESA_Map_11_1" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnl_Map_12" Visible="false" runat="server">
                    <asp:Label ID="Map_12_Head_Label" CssClass="title" Text="CESA 12: Click on a School District or hot link below"
                        BackColor="#EFEFEF" runat="server" /><br />
                    <uc12:CESA_Map_12 ID="CESA_Map_12_1" runat="server" />
                </asp:Panel>
                <!--//CESA -->
                <slx:SligoDataGrid ID="SligoDataGrid5" runat="server" AutoGenerateColumns="False"
                    ShowSuperHeader="true" BorderStyle="None" CellSpacing="0" HeaderStyle-BackColor="#EFEFEF"
                    HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Left" UseAccessibleHeader="False"
                    CellPadding="4" ShowHeader="true" HeaderStyle-Font-Bold="true" RowStyle-Font-Size="8pt"
                    GridLines="None" RowStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:BoundField DataField="CountyName" HeaderText="COUNTY">
                            <itemstyle horizontalalign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="DISTRICT" ItemStyle-Width="125" ItemStyle-HorizontalAlign="Left">
                            <itemtemplate>
            <asp:HyperLink ID="DistrictDetails5" runat="server" 
            NavigateUrl='<%# "~/questions.aspx"
              + UserValues.GetQueryString(
                    new String[] { 
                    "FULLKEY=" + Eval("fullkey"),
                    "DN=" + HttpUtility.UrlEncode(Eval("DistrictName").ToString().Trim())  
                    }) %>' 
            Text='<%# Eval("DistrictName").ToString() %>' />
        </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SCHOOL" ItemStyle-HorizontalAlign="Left">
                            <itemtemplate>
                <asp:HyperLink ID="ShowSchoolInDistrictDetails5" runat="server"  
                NavigateUrl='<%# "~/SchoolScript.aspx" 
                + UserValues.GetQueryString(
                    new String[] { 
                        "SEARCHTYPE=SC", 
                        "L=0",  
                        "FULLKEY=" + Eval("fullkey").ToString()
                        }) %>' 
                Text='Show Schools' />
            </itemtemplate>
                        </asp:TemplateField>
                    </Columns>
                </slx:SligoDataGrid>
                <!-- / District Display -->
            </td>
        </tr>
    </table>
</asp:Content>
