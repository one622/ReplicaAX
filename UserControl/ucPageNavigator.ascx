<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPageNavigator.ascx.cs" Inherits="ReplicaAX.UserControl.ucPageNavigator" %>
<div class="navigator">
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>		
		<TD align="right" width="100"><asp:label id="Label3" runat="server">หน้าที่ : </asp:label></TD>
		<TD width="20" >&nbsp;<asp:imagebutton id="btnPrev" DESIGNTIMEDRAGDROP="103" runat="server" ImageUrl="~/Images/tab_back.gif" OnClick="btnPrev_Click" Height="14px" Width="14px"></asp:imagebutton></TD>
		<TD align="left" nowrap >
		    <div style="display:none">
		    <table>
		        <tr>
		            <asp:Repeater ID="repPage" runat="server" OnItemDataBound="repPage_ItemDataBound">
                        <ItemTemplate>
                            <td><asp:LinkButton CssClass="textLink" style="color:Blue;" ID="lblPage" runat="server" OnClick="Page_Click" Text="<%#DataBinder.Eval(Container.DataItem, columnName)%>"  title="<%#showPageLabel(DataBinder.Eval(Container.DataItem, columnName).ToString())%>"></asp:LinkButton></td>
                        </ItemTemplate>
                    </asp:Repeater>
                </tr>
		    </table>
		    </div>
        </TD>		
		<TD width="20px">&nbsp;<asp:imagebutton id="btnNext" runat="server" ImageUrl="~/Images/tab_next.gif" OnClick="btnNext_Click" Height="14px" Width="14px"></asp:imagebutton></TD>
		<TD align="left" style="text-align: left;width:87%">&nbsp;&nbsp;
		    <asp:textbox id="txtCurPage" DESIGNTIMEDRAGDROP="80" runat="server" Width="40px" CssClass="inputTextMoney" onkeydown="return isKeyInteger();" MaxLength="5">0</asp:textbox>/
			<asp:label id="lblAllPage" runat="server">0</asp:label>
			<asp:button id="btnGo" runat="server" CssClass="btn btn-info  text-white" Text="GO" OnClick="btnGo_Click" width="60px"></asp:button>            
	    </TD>
        <td align="left" style="text-align: right; width: 50%;" nowrap>&nbsp;&nbsp;
            <asp:label id="Label1" DESIGNTIMEDRAGDROP="22" runat="server">รายการที่  : </asp:label>
            <asp:label id="lblFromItem" DESIGNTIMEDRAGDROP="23" runat="server">0</asp:label>-<asp:label id="lblToItem" DESIGNTIMEDRAGDROP="23" runat="server">0</asp:label>/
			<asp:label id="lblAllItem" runat="server">0</asp:label></td>
	</TR>
</TABLE>
</div>