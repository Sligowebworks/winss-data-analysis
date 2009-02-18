<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="QStringDemo2.aspx.cs" Inherits="QStringDemo_QStringDemo2" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table summary="All possible Querystring values">
        <caption>
        </caption>
        <tr>
            <td style="width: 100px" colspan="2">
                All Querystring values.
            </td>
        </tr>                               
        <tr>
            <td style="width: 100px">FULLKEY
            </td>
            <td style="width: 100px">
                <asp:Label ID="lblFULLKEY" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">GraphFile
            </td>
            <td style="width: 100px">
                <asp:Label ID="lblGraphFile" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">CompareTo
            </td>
            <td style="width: 100px">
                <asp:Label ID="lblCompareTo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">ORGLEVEL
            </td>
            <td style="width: 100px">
                <asp:Label id="lblORGLEVEL" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">Group
            </td>
            <td style="width: 100px">
            <asp:Label id="lblGroup" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">STYP
            </td>
            <td style="width: 100px">
                <asp:Label id="lblSTYP" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px">
                    DN
            </td>
            <td style="width: 100px">
                <asp:Label id="lblDN" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">SN</td>
            <td style="width: 100px">
                <asp:Label id="lblSN" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px">
                    DETAIL
        

            </td>
            <td style="width: 100px">
                <asp:Label id="lblDETAIL" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
            COUNTY
        
            </td>
            <td style="width: 100px">
                <asp:Label id="lblCOUNTY" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px">
            YearLocal
      
            </td>
            <td style="width: 100px">
                <asp:Label id="lblYearLocal" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px; height: 31px;">
              TrendStartYearLocal
       
            </td>
            <td style="width: 100px; height: 31px;">
                <asp:Label id="lblTrendStartYearLocal" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px">
             WhichSchool
        
            </td>
            <td style="width: 100px">
                <asp:Label id="lblWhichSchool" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px">
            NumSchools</td>
            <td style="width: 100px">
                <asp:Label id="lblNumSchools" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px">ConferenceKey</td>
            <td style="width: 100px">
                <asp:Label id="lblConferenceKey" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px">DistrictID</td>
            <td style="width: 100px">
                <asp:Label id="lblDistrictID" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px">zBackTo</td>
            <td style="width: 100px">
                <asp:Label id="lblzBackTo" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px">FileQuery</td>
            <td style="width: 100px">
                <asp:Label id="lblFileQuery" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px">FileName</td>
            <td style="width: 100px">
                <asp:Label id="lblFileName" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px">SchoolWebaddress</td>
            <td style="width: 100px">
                <asp:Label id="lblSchoolWebaddress" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px">DistrictWebaddress</td>
            <td style="width: 100px">
                <asp:Label id="lblDistrictWebaddress" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px">Grade Breakout Normal</td>
            <td style="width: 100px">
                <asp:Label id="lblGradeBreakout" runat="server"></asp:Label></td>
        </tr>        
        <tr>
            <td style="width: 100px">Grade Breakout LAG</td>
            <td style="width: 100px">
                <asp:Label id="lblLAG" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px"> Grade Breakout EDISA</td>
            <td style="width: 100px">
                <asp:Label id="lblEDISA" runat="server"></asp:Label></td>
        </tr>
    </table>
</asp:Content>

