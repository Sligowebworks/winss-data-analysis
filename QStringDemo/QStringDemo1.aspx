<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="QStringDemo1.aspx.cs" Inherits="QStringDemo_QStringDemo1" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                <asp:TextBox ID="txtFULLKEY" runat="server" Text="013619040022"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">GraphFile
            </td>
            <td style="width: 100px">
                <asp:TextBox ID="txtGraphFile" runat="server"  Text="HIGHSCHOOLCOMPLETION"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">CompareTo
            </td>
            <td style="width: 100px">
                <asp:TextBox ID="txtCompareTo" runat="server"  Text="PRIORYEARS"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">ORGLEVEL
            </td>
            <td style="width: 100px">
                <asp:TextBox id="txtORGLEVEL" runat="server"  Text="SC"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">Group
            </td>
            <td style="width: 100px">
            <asp:TextBox id="txtGroup" runat="server"  Text="AllStudentsFAY"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">STYP
            </td>
            <td style="width: 100px">
                <asp:TextBox id="txtSTYP" runat="server"  Text="9"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 100px">
                    DN
            </td>
            <td style="width: 100px">
                <asp:TextBox id="txtDN" runat="server"  Text="Milwaukee"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">SN</td>
            <td style="width: 100px">
                <asp:TextBox id="txtSN" runat="server"  Text="Madison Hi"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 100px">
                    DETAIL
        

            </td>
            <td style="width: 100px">
                <asp:TextBox id="txtDETAIL" runat="server"  Text="YES"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
            COUNTY
        
            </td>
            <td style="width: 100px">
                <asp:TextBox id="txtCOUNTY" runat="server"  Text="40"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 100px">
            YearLocal
      
            </td>
            <td style="width: 100px">
                <asp:TextBox id="txtYearLocal" runat="server"  Text="2006"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 100px; height: 31px;">
              TrendStartYearLocal
       
            </td>
            <td style="width: 100px; height: 31px;">
                <asp:TextBox id="txtTrendStartYearLocal" runat="server"  Text="1997"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 100px">
             WhichSchool
        
            </td>
            <td style="width: 100px">
                <asp:TextBox id="txtWhichSchool" runat="server"  Text="4"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 100px">
            NumSchools</td>
            <td style="width: 100px">
                <asp:TextBox id="txtNumSchools" runat="server"  Text="4"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 100px">ConferenceKey</td>
            <td style="width: 100px">
                <asp:TextBox id="txtConferenceKey" runat="server"  Text="27"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 100px">DistrictID</td>
            <td style="width: 100px">
                <asp:TextBox id="txtDistrictID" runat="server"  Text="3619"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 100px">zBackTo</td>
            <td style="width: 100px">
                <asp:TextBox id="txtzBackTo" runat="server"  Text="performance.aspx"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 100px">FileQuery</td>
            <td style="width: 100px">
                <asp:TextBox id="txtFileQuery" runat="server"  Text="select count(*) from
                sys.databases"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 100px">FileName</td>
            <td style="width: 100px">
                <asp:TextBox id="txtFileName" runat="server"  Text="c:\sample.txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 100px">SchoolWebaddress</td>
            <td style="width: 100px">
                <asp:TextBox id="txtSchoolWebaddress" runat="server"  Text="http://mpsportal.milwaukee.k12.wi.us"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 100px">DistrictWebaddress</td>
            <td style="width: 100px">
                <asp:TextBox id="txtDistrictWebaddress" runat="server"  Text="http://www.milwaukee.k12.wi.us"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 100px">Grade Breakout Normal</td>
            <td style="width: 100px">
                <asp:TextBox id="txtGradeBreakout" runat="server"  Text="999"></asp:TextBox></td>
        </tr>        
        <tr>
            <td style="width: 100px">Grade Breakout LAG</td>
            <td style="width: 100px">
                <asp:TextBox id="txtLAG" runat="server"  Text="555"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 100px"> Grade Breakout EDISA</td>
            <td style="width: 100px">
                <asp:TextBox id="txtEDISA" runat="server"  Text="212"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Button runat="server"  Text="Go" ID="btnGo" OnClick="btnGo_Click" /></td>
        </tr>
    </table>
</asp:Content>

