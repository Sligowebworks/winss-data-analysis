<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="ChooseSelectedList.aspx.cs" Inherits="SligoCS.Web.WI.ChooseSelectedList" Title="Selecting Comparable Schools/District" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table cellpadding="0" cellspacing="0" border="0" width="600">
	<tr valign="TOP">
		<td>
		<table width="600">
		<tr>
			<td bgcolor="#cccccc" width="490" height="35" class="title">
			<b>Selecting Multiple <%= (GlobalValues.CompareTo.Key == SligoCS.Web.WI.WebSupportingClasses.WI.CompareToKeys.SelSchools) ? "Schools" : "Districts"%></b>
			</td>
		</tr>
		<tr>
			<td class="title">
				<b><font color="Red"><asp:panel ID="MessagePanel" runat="server"> <asp:Label id="Message" runat="server"></asp:Label></asp:panel></font></b>
			</TD>
		</TR>
		</table>
			<table width="490" border=0 valign="top">
				<TR>
					<TD valign='top' align='left'>
						
						<table width="200" border=1 valign="top">
							<tr>
								<td valign="top" class="smtext" bgcolor="#cccccc">
								
									<b>Available <%= (GlobalValues.CompareTo.Key == SligoCS.Web.WI.WebSupportingClasses.WI.CompareToKeys.SelSchools) ? "Schools" : "Districts"%><BR/>(click to add to selected):</b>
								</td>
							</tr>
							<tr>		
								<td valign="top" class="smtext">
								<asp:Panel  ID="NoAppropriateMessage" runat="server" Visible="false"><asp:Label ID="NoAppropriateLabel" runat="server"></asp:Label></asp:Panel>
<asp:GridView ID="gvAvailable" runat="server" AutoGenerateColumns="False" 
   BorderStyle="None" GridLines="None" ShowHeader="false"  
   RowStyle-HorizontalAlign="Left"  HeaderStyle-Height="1">
     <Columns >
        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>
            <asp:HyperLink ID="AgencyLink" runat="server"  NavigateUrl='<%# "~/ChooseSelectedList.aspx" + GetQueryStringForAdding ("Add", Eval("fullkey").ToString()) %>' Text='<%# (GlobalValues.CompareTo.Key == SligoCS.Web.WI.WebSupportingClasses.WI.CompareToKeys.SelSchools)? (Eval("DistrictName").ToString()) + " - "+ (Eval("SchoolName").ToString()) :  Eval("DistrictName").ToString() %>' />
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
									<b>Currently Selected<BR>(click to remove):</B>
								</td>
							</tr>
							<tr>		
								<td valign=top align='left' class="smtext">
<asp:Panel ID="PanelNoChosen" runat="server">
none chosen
</asp:Panel>
<asp:Panel ID="PanelSelected" runat="server">
<asp:GridView ID="gvSelected"  runat="server" AutoGenerateColumns="False" 
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
            NavigateUrl='<%# "~/ChooseSelectedList.aspx" + GetQueryStringForAdding ("Rem", Eval("fullkey").ToString()) %>' 
            Text='<%# (GlobalValues.CompareTo.Key == SligoCS.Web.WI.WebSupportingClasses.WI.CompareToKeys.SelSchools) ?
             Eval("DistrictName").ToString() + " - "+Eval("SchoolName").ToString() 
            :  Eval("DistrictName").ToString()
             %>' />
        
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
