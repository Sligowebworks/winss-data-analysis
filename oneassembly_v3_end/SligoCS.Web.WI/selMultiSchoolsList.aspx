<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="selMultiSchoolsList.aspx.cs" Inherits="SligoCS.Web.WI.selMultiSchoolsList" Title="Selecting multiple schools" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table cellpadding="0" cellspacing="0" border="0" width="600">
	<tr valign="TOP">
		<td>
		<table width="600">
		<tr>
			<td bgcolor="#cccccc" width="490" height="35" class="title">
			<b>Selecting Multiple Schools</b>
			</td>
		</tr>
		<tr>
			<td class="title">
				<b><font color="Red"><asp:panel ID="MessagePanel" runat="server">Only four (4) schools can be selected at a time. You must remove a currently selected school in the list on the right by clicking on the name before you can add another school. </asp:panel></font></b>
			</TD>
		</TR>
		</table>
			<table width="490" border=0 valign="top">
				<TR>
					<TD valign='top' align='left'>
						
						<table width="200" border=1 valign="top">
							<tr>
								<td valign="top" class="smtext" bgcolor="#cccccc">
								
									<b>Available Schools<BR/>(click on school to add to selected schools):</b>
								</td>
							</tr>
							<tr>		
								<td valign=top class="smtext">
<asp:GridView ID="gvSchools" runat="server" AutoGenerateColumns="False" 
   BorderStyle="None" GridLines="None" ShowHeader="false"  
   RowStyle-HorizontalAlign="Left"  HeaderStyle-Height="1">
     <Columns >
        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>
            <asp:HyperLink ID="SchoolLink" runat="server"  
            NavigateUrl='<%# "~/selMultiSchoolsList.aspx" 
            + GetQueryStringForAdding () + "&Add=" + Eval("fullkey").ToString() %>' 
            Text='<%# Eval("DistrictName").ToString() + " - "+Eval("SchoolName").ToString() %>' />
        
            </ItemTemplate>
        </asp:TemplateField> 
    </Columns>
    <RowStyle Font-Size="8pt" HorizontalAlign="Left" />
</asp:GridView> 
								</td>
							</tr>
						</table>
					</TD>
					<TD valign='top' width='200' align='left'> &nbsp;
					</TD>					
					<TD valign='top' align='right'>
						<table width="200" border=1 bordercolor="#D82058" valign="top">
							<tr>
								<td valign="top" class="smtext" >
									<b>Currently Selected Schools<BR>(click on school to remove):</B>
								</td>
							</tr>
							<tr>		
								<td valign=top align='left' class="smtext">
<asp:Panel ID="PanelNoChosen" runat="server">
none chosen
</asp:Panel>
<asp:Panel ID="PanelSelectedSchools" runat="server">
<asp:GridView ID="gvSelectedSchools"  runat="server" AutoGenerateColumns="False" 
   BorderStyle="None" GridLines="None" ShowHeader="false" 
   RowStyle-HorizontalAlign="Left"  HeaderStyle-Height="1">
     <Columns >
        <asp:TemplateField>
                <ItemTemplate>
                  <%# (Container.DataItemIndex + 1).ToString() + ". " %>
                </ItemTemplate>
        </asp:TemplateField>

     
        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>
            <asp:HyperLink ID="SchoolLink" runat="server"  
            NavigateUrl='<%# "~/selMultiSchoolsList.aspx" 
            + GetQueryStringForAdding () + "&Rem=" + Eval("fullkey").ToString() %>' 
            Text='<%# Eval("DistrictName").ToString() + " - "+Eval("SchoolName").ToString() %>' />
        
            </ItemTemplate>
        </asp:TemplateField> 
    </Columns>
    <RowStyle Font-Size="8pt" HorizontalAlign="Left" />
</asp:GridView> 
</asp:Panel>
								</td>
							</tr>
						</table>
					
					<p/>
						<asp:button ID="ChooseFromAnother" OnClick="ChooseFromAnother_Click" runat="server" Text="Choose from another location" /> 

					<P/>If you made a mistake or would like to choose schools from a different location click on the button above.
						

                        <p>
                        </p>
                        <asp:button ID="BackToGraph" OnClick="BackToGraph_Click" runat="server" Text="Back to Graph" /> 

					
					<P>Click on the "Back to Graph" button when you are done.
					</TD>
				</TR>
			</TABLE>
		</td>
	</tr>
</table>



</asp:Content>
