<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="SelSchool.aspx.cs" Inherits="SligoCS.Web.WI.SelSchool" Title="Data Analysis" %>

<%@ Register Src="WebUserControls/CESA_Map.ascx" TagName="CESA_Map" TagPrefix="uc3" %>

<%@ Register Src="WebUserControls/CESA_Map_Control.ascx" TagName="CESA_Map_Control"
    TagPrefix="uc2" %>

<%@ Register Src="WebUserControls/AlphaLinkWebUserControl.ascx" TagName="AlphaLinkWebUserControl"
    TagPrefix="uc1" %>

<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- <asp:Label ID="Label1" runat="server" Text="" /><br /> -->
    <table style="padding:0px 0px 0px 0px; width: 390px;"><tr><td align="left">
    <asp:Panel ID="pnl_sdc" runat="server">

    <asp:Label ID="SchoolNameLabel" CssClass="text" Text="<B>By School</B>" runat="server" /><br />
    <asp:TextBox ID="txtSchoolName" CssClass="text" runat="server" Width="130px" />
    <asp:Button ID="btnSchool" CssClass="text" runat="server" OnClick="btnSchool_Click" Text="GO" />
    <uc1:AlphaLinkWebUserControl ID="AlphaLinkWebUserControl1" runat="server" SDC="SC" />
    <br />
    
    <asp:Label ID="DistrictNameLabel" CssClass="text" Text="<B>By District</B>" runat="server" /><br />
    <asp:TextBox ID="DistrictName" CssClass="text" runat="server" Width="130px" />
    <asp:Button ID="btnDistrict" CssClass="text" runat="server" OnClick="btnDistrict_Click" Text="GO" />
    <uc1:AlphaLinkWebUserControl ID="AlphaLinkWebUserControl2" runat="server" SDC="DI" />
    <br />
    
    <asp:Label ID="CountyNameLabel" CssClass="text" Text="<B>By County</B>" runat="server" /><br />
    <asp:TextBox ID="CountyName" CssClass="text" runat="server" Width="130px" />   
    <asp:Button ID="btnCounty" CssClass="text" runat="server" OnClick="btnCounty_Click" Text="GO" />
    <uc1:AlphaLinkWebUserControl ID="AlphaLinkWebUserControl3" runat="server" SDC="CO" />
    <br />

    </asp:Panel>

    <slx:SligoDataGrid ID="SligoSchoolGrid" runat="server" AutoGenerateColumns="False" ShowSuperHeader="true" BorderStyle="None" CellSpacing="0" HeaderStyle-BackColor="#EFEFEF" HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Left" UseAccessibleHeader="False" CellPadding="4" ShowHeader="true" HeaderStyle-Font-Bold="true" RowStyle-Font-Size="8pt" GridLines="None" RowStyle-HorizontalAlign="Left">
     <Columns >
        <asp:BoundField DataField="CountyName" HeaderText="COUNTY">
            <itemstyle horizontalalign="Left" />
        </asp:BoundField>
                      
        <asp:TemplateField HeaderText="DISTRICT">
            <itemstyle horizontalalign="Left" />
            <ItemTemplate>
            <asp:HyperLink ID="DistrictDetails1" runat="server"  
            NavigateUrl='<%# "~/questions.aspx" 
            + GetQueryStringForMultipleParams ( 
                        Eval("fullkey").ToString(),
                        HttpUtility.UrlEncode(Eval("SchoolName").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("DistrictName").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("SchoolType").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("LowGrade").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("HighGrade").ToString().Trim()),
                        SligoCS.Web.WI.WebSupportingClasses.WI.OrgLevelKeys.District.ToString()
                        ) %>'    
                        
            Text='<%# Eval("DistrictName").ToString() %>' />
            </ItemTemplate>
        </asp:TemplateField> 
        
        <asp:TemplateField HeaderText="SCHOOL">
            <itemstyle horizontalalign="Left" />
            <ItemTemplate>
            <asp:HyperLink ID="SchoolDetails1" runat="server"  
            NavigateUrl='<%# "~/questions.aspx" 
            + GetQueryStringForMultipleParams ( 
                        Eval("fullkey").ToString(),
                        HttpUtility.UrlEncode(Eval("SchoolName").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("DistrictName").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("SchoolType").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("LowGrade").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("HighGrade").ToString().Trim()),
                        SligoCS.Web.WI.WebSupportingClasses.WI.OrgLevelKeys.School.ToString()
                        ) %>'             
            Text='<%# Eval("SchoolName").ToString() %>' />
            </ItemTemplate>
         </asp:TemplateField> 
    </Columns>
    
    <RowStyle Font-Size="8pt" HorizontalAlign="Left" />
    <HeaderStyle BackColor="#EFEFEF" Font-Bold="True" Font-Underline="True" HorizontalAlign="Left" />
    <PagerStyle HorizontalAlign="Left" />
    </slx:SligoDataGrid>

    <slx:SligoDataGrid ID="SligoDistrictGrid"  runat="server" AutoGenerateColumns="False" ShowSuperHeader="true" BorderStyle="None" CellSpacing="0" HeaderStyle-BackColor="#EFEFEF" HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Left" UseAccessibleHeader="False" CellPadding="4" ShowHeader="true" HeaderStyle-Font-Bold="true" RowStyle-Font-Size="8pt" GridLines="None" RowStyle-HorizontalAlign="Left">
     <Columns >
        <asp:BoundField DataField="CountyName" HeaderText="COUNTY">    
            <itemstyle horizontalalign="Left" />
        </asp:BoundField>
        
        <asp:TemplateField HeaderText="DISTRICT">
            <itemstyle horizontalalign="Left" />
            <ItemTemplate>
            <asp:HyperLink ID="DistrictDetails3" runat="server"  
            NavigateUrl='<%# "~/questions.aspx" 
            + GetQueryStringForMultipleParams ( 
                        Eval("fullkey").ToString(),
                        HttpUtility.UrlEncode(Eval("SchoolName").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("DistrictName").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("SchoolType").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("LowGrade").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("HighGrade").ToString().Trim()),
                        SligoCS.Web.WI.WebSupportingClasses.WI.OrgLevelKeys.District.ToString()
                        ) %>' 
            Text='<%# Eval("DistrictName").ToString() %>' />   
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="SCHOOL" >
            <itemstyle horizontalalign="Left" />
            <ItemTemplate>
            <asp:HyperLink  ID="DistrictDetailsSchool" runat="server" 
            NavigateUrl='<%# 
            "~/SchoolScript.aspx" + UserValues.GetQueryString(
                new String[] { 
                    "SEARCHTYPE=SC", 
                    "L=0",  
                    "FULLKEY=" + Eval("fullkey").ToString()
                    }) %>' 
                Text="Show Schools" /> 
            </ItemTemplate> 
        </asp:TemplateField>
    </Columns>
    <RowStyle Font-Size="8pt" HorizontalAlign="Left" />
    <HeaderStyle BackColor="#EFEFEF" Font-Bold="True" Font-Underline="True" HorizontalAlign="Left" />
 </slx:SligoDataGrid>

   <slx:SligoDataGrid ID="SligoCountyGrid" runat="server" AutoGenerateColumns="False" ShowSuperHeader="true" BorderStyle="None" CellSpacing="0" HeaderStyle-BackColor="#EFEFEF" HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Left" UseAccessibleHeader="False" CellPadding="4" ShowHeader="true" HeaderStyle-Font-Bold="true" RowStyle-Font-Size="8pt" GridLines="None" RowStyle-HorizontalAlign="Left">
     <Columns>
        <asp:BoundField DataField="CountyName" HeaderText="COUNTY">
            <itemstyle horizontalalign="Left" />   
        </asp:BoundField>
        
        <asp:TemplateField HeaderText="DISTRICT">
            <itemstyle horizontalalign="Left" />
            <ItemTemplate>
            <asp:HyperLink ID="DistrictDetails2" runat="server"  
            NavigateUrl='<%# "~/questions.aspx" 
            + GetQueryStringForMultipleParams ( 
                        Eval("fullkey").ToString(),
                        HttpUtility.UrlEncode(Eval("SchoolName").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("DistrictName").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("SchoolType").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("LowGrade").ToString().Trim()),
                        HttpUtility.UrlEncode(Eval("HighGrade").ToString().Trim()),
                        SligoCS.Web.WI.WebSupportingClasses.WI.OrgLevelKeys.District.ToString()
                        ) %>'             
            
            Text='<%# Eval("DistrictName").ToString() %>' />
            
            </ItemTemplate>
        </asp:TemplateField>             
                      
        <asp:TemplateField HeaderText="SCHOOL" >
            <itemstyle horizontalalign="Left" />
            <ItemTemplate>
                <asp:HyperLink  ID="HyperLink1" runat="server" 
                NavigateUrl='<%# 
                "~/SchoolScript.aspx" + UserValues.GetQueryString(
                    new String[] { 
                        "SEARCHTYPE=SC", 
                        "L=0",  
                        "FULLKEY=" + Eval("fullkey").ToString()
                        }) %>' 
                    Text="Show Schools" /> 
            </ItemTemplate> 
            </asp:TemplateField>
        
    </Columns>
    <RowStyle Font-Size="8pt" HorizontalAlign="Left" />
    <HeaderStyle BackColor="#EFEFEF" Font-Bold="True" Font-Underline="True" HorizontalAlign="Left" />
   </slx:SligoDataGrid>
   <asp:Label ID="No_Records_Found_Label" Visible="false" runat="server" Text=""></asp:Label>
<asp:Panel ID="pnl_CESA_Map_Control" runat="server">
    <uc3:CESA_Map id="CESA_Map1" runat="server" />
    <br />
    <asp:Label ID="CESA_Map_Control_Label" CssClass="text" Text="<B>By CESA</B>" runat="server" />
    <br />        
    <uc2:CESA_Map_Control id="CESA_Map_Control" runat="server" />
</asp:Panel>  
</td></tr></table>    
</asp:Content>
