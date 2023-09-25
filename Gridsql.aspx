<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gridsql.aspx.cs" Inherits="ReplicaAX.WebForm1" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<link rel="stylesheet" type="text/css" href=css/bootstrap.min.css media="screen" /> 
    <script type="text/javascript" src="https://cdn.jsdelivr.net/jquery/latest/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/momentjs/latest/moment.min.js"></script>
	<script type="text/javascript" src="https://cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.min.js"></script>
    <link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.css" />
	<script language="javascript"> 
	  $(function () {
        $("#<%=txtDate.ClientID%>").daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            locale: {
                format: "DD/MM/YYYY"
            }
        });
	  });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList ID="ddlCropYear" runat="server"></asp:DropDownList>
            <asp:Button ID="btnSeacrch" runat="server" Text="Search" OnClick="btnSeacrch_Click" />
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
			<asp:TextBox ID="txtDate" class="form-control input-sm" runat="server" ></asp:TextBox>
        </div>
         <div class="list-data" >      
        <h2><a id="aBranch">ข้อมูล</a></h2>       
        <div id="divDetail" style="display:inline;"> 
        <div class="listCustom">
            <asp:Button ID="btnAddDetail" runat="server" Text="เพิ่มรายการ"  CssClass="btn btn-info  text-white btn-sm" onClick="btnAddDetail_Click"/>            
        </div>
        <div style="overflow:auto; max-height:160px;">
            <asp:DataGrid ID="dtgData" runat="server" AutoGenerateColumns="false" Width="97%" CssClass="table table-bordered table-condensed table-responsive table-hover"
                HeaderStyle-CssClass="gridhead" ItemStyle-CssClass="gridrow0" AlternatingItemStyle-CssClass="gridrow1"
                AllowPaging="false" PagerStyle-Visible="false" 
                onitemdatabound="dtgData_ItemDataBound">
                <Columns>
                    <asp:TemplateColumn>
	                    <HeaderTemplate>
                            <%#ColumnTitle[0]%>				                
	                    </HeaderTemplate>
	                    <ItemTemplate>
	                        <%# Container.DataSetIndex +1%>
	                    </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
	                    <HeaderTemplate>
		                    <div class="labelRequireField"><%#ColumnTitle[1]%></div>
	                    </HeaderTemplate>
	                    <ItemTemplate>
	                        <%#Eval(ColumnName[1])%>
	                    </ItemTemplate>
	                    <EditItemTemplate>
                           <asp:TextBox ID="txtDetailCode" runat="server" Width="80" MaxLength="5" Text="<%#Eval(ColumnName[1])%>" ReadOnly="<%#showEdit(Eval(ColumnName[0])) %>" CssClass="<%#showStyleEdit(Eval(ColumnName[0])) %>"></asp:TextBox>
	                    </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
	                    <HeaderTemplate>
		                    <div class="labelRequireField"><%#ColumnTitle[2]%></div>
	                    </HeaderTemplate>
	                    <ItemTemplate>
	                        <%#Eval(ColumnName[2])%>
	                    </ItemTemplate>
	                    <EditItemTemplate>
                            <asp:TextBox ID="txtDetailDesc" runat="server" Width="150" MaxLength="40" Text="<%#Eval(ColumnName[2])%>" ReadOnly="<%#showEdit(Eval(ColumnName[0])) %>" CssClass="<%#showStyleEdit(Eval(ColumnName[0])) %>"></asp:TextBox>
	                    </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
	                    <HeaderTemplate>
		                    <div class="labelRequireField"><%#ColumnTitle[3]%></div>
	                    </HeaderTemplate>
	                    <ItemTemplate>
	                        <%#Eval(ColumnName[3])%> <%#Eval(ColumnName[7])%> จังหวัด<%#(Eval(ColumnName[8]))%><%#Eval(ColumnName[9])%>
	                    </ItemTemplate>
	                    <EditItemTemplate>	
	                       <div style="display:none">
	                            <asp:TextBox ID="txtDetailProvinceCode" runat="server" Width="80" Text="<%#Eval(ColumnName[8])%>" ReadOnly="<%#showEdit(Eval(ColumnName[0])) %>"></asp:TextBox>
	                        </div>	                        
	                        <asp:TextBox ID="txtDetailAddress1" runat="server" Width="200" Text="<%#Eval(ColumnName[3])%>" MaxLength="100" ReadOnly="<%#showEdit(Eval(ColumnName[0])) %>" CssClass="<%#showStyleEdit(Eval(ColumnName[0])) %>"></asp:TextBox><br />
	                        <asp:TextBox ID="txtDetailAddress2" runat="server" Width="200" Text="<%#Eval(ColumnName[7])%>" MaxLength="100" ReadOnly="<%#showEdit(Eval(ColumnName[0])) %>" CssClass="<%#showStyleEdit(Eval(ColumnName[0])) %>"></asp:TextBox><br />
                            <asp:DropDownList ID="ddlDetailProvince" runat="server" >
                            </asp:DropDownList>                            
	                        <asp:TextBox ID="txtDetailPostCode" runat="server" Width="50" Text="<%#Eval(ColumnName[9])%>" MaxLength="5" ReadOnly="<%#showEdit(Eval(ColumnName[0])) %>" CssClass="<%#showStyleEdit(Eval(ColumnName[0])) %>"></asp:TextBox>                        
	                    </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
	                    <HeaderTemplate>
		                    <div class="labelRequireField"><%#ColumnTitle[4]%></div>
	                    </HeaderTemplate>
	                    <ItemTemplate>
	                        <%#(Eval(ColumnName[4]))%>
	                    </ItemTemplate>
	                    <EditItemTemplate>
                            <asp:TextBox ID="txtDetailTel" runat="server" Width="120" Text="<%#Eval(ColumnName[4])%>" MaxLength="20" ReadOnly="<%#showEdit(Eval(ColumnName[0])) %>" CssClass="<%#showStyleEdit(Eval(ColumnName[0])) %>"></asp:TextBox>
	                    </EditItemTemplate>
                    </asp:TemplateColumn>  
                    <asp:TemplateColumn>
	                    <HeaderTemplate>
		                    <%#ColumnTitle[5]%>
	                    </HeaderTemplate>
	                    <ItemTemplate>
	                        <%#(Eval(ColumnName[5]))%>
	                    </ItemTemplate>
	                    <EditItemTemplate>
                            <asp:TextBox ID="txtDetailRemark" runat="server" Width="200" Text="<%#Eval(ColumnName[5])%>" MaxLength="100" ReadOnly="<%#showEdit(Eval(ColumnName[0])) %>" CssClass="<%#showStyleEdit(Eval(ColumnName[0])) %>"></asp:TextBox>
	                    </EditItemTemplate>
                    </asp:TemplateColumn>                      
                    <asp:TemplateColumn>
	                    <HeaderTemplate>
		                    <%#ColumnTitle[6]%>							                    
	                    </HeaderTemplate>
	                    <ItemTemplate>		              
	                        <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="Images/btnEDIT.GIF" ToolTip="Edit" index="<%# Container.DataSetIndex%>" OnClick="imgEdit_Click"/>
	                        <asp:ImageButton ID="imgDelete" runat="server" ImageUrl="Images/btnDelete.GIF" ToolTip="Delete"  index="<%# Container.DataSetIndex%>" OnClick="imgDelete_Click"/>
	                    </ItemTemplate>
	                    <EditItemTemplate>
                            <asp:ImageButton ID="imgSave" runat="server" ImageUrl="Images/img_save.gif" ToolTip="Save"  index="<%# Container.DataSetIndex%>"  OnClick="imgSave_Click" />                                
                            <asp:ImageButton ID="imgCancel" runat="server" ImageUrl="Images/btnDelete.GIF" ToolTip="Cancel"  index="<%# Container.DataSetIndex%>" OnClick="imgCancel_Click"/>
	                    </EditItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>  
        </div>
        </div>
    </div>
		 <div style="display:none">
       
        <asp:Literal ID="litJavaScript" runat="server"></asp:Literal>                
    </div>
    </form>
</body>
</html>
