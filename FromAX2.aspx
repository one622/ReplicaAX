<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FromAX2.aspx.cs" Inherits="ReplicaAX.FromAX2" %>

   <%@ Register src="UserControl/ucPageNavigator.ascx" tagname="ucPageNavigator" tagprefix="uc1" %>
<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">

<head >
    
    <title>ระบบ InterfaceSugracaneSAP</title>
    <%--ต้องใส่เวลาใช้ bootstrap--%>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link type="image/x-icon" rel="shortcut icon" href="images/logo.png" />
    <link rel="icon" sizes="192x192" href="/ic_launcher.png" />
    <link rel="apple-touch-icon" sizes="128x128" href="/ic_launcher.png" />
    <link rel="apple-touch-icon-precomposed" sizes="128x128" href="/ic_launcher.png" />

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-theme.css" rel="stylesheet" />
    <link href="Scripts/jquery-1.11.4.ui.css" rel="stylesheet" />
    <link href="css/login_cristalla.css" rel="stylesheet" />
    <script type="text/javascript" src="https://cdn.jsdelivr.net/jquery/latest/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/momentjs/latest/moment.min.js"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.min.js"></script>
    <link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.css" />

    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="Scripts/jquery-ui.1.11.4.min.js"></script>
    <script src="Scripts/jquery.ajax-progress.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="js/Js_ModMain.js"></script>
    <script src="js/Js_Md5.js"></script>

    <script language="javascript"> 
        $(function () {
            
            $("#<%=txtDate.ClientID%>").daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                locale: {
			        format: "YYYY/MM/DD"
                }
            });
          });
    </script>

</head>
    
<body>
     <form id="form2" runat="server">
    

        <header>
    <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark bg-dark border-bottom box-shadow mb-3">
        <div class="container">
             <img src="Images\logo.png" alt="logo" style="max-width: 50px; max-height: 50px"></img> Navbar</a>
            <a class="navbar-brand"  asp-area="" asp-page="/Index">ระบบ InterfaceSugracaneSAP</a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                    aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                <ul class="navbar-nav flex-grow-1"> 
                </ul>
            </div>
        </div>
    </nav>
</header>

         <div class="container" style="margin-top: 20px;">
     <!-- Text Field -->
             <div class="card">
                    <div class="card-body">
                        <h4 class="card-title">เงื่อนไขการดึงข้อมูล</h4>
                            <div>
                             
                                <asp:DropDownList ID="ddlCropYear" runat="server" ></asp:DropDownList>

                                <asp:DropDownList ID="ddlVehicle_code" runat="server" ></asp:DropDownList>

                                <asp:TextBox ID="txtDate" href="#" class="form-control input-sm " runat="server" ></asp:TextBox>

                                <asp:Button ID="btnSeacrch" href="#" runat="server" Text="ดึงข้อมูล" OnClick="btnSeacrch_Click" />

                                <asp:GridView ID="GridShowList" runat="server"></asp:GridView>

                            </div>
                        </div>
                 </div>
            </div>
    
    <div class="container" style="margin-top: 20px;">
     <div class="card">
     <div class="card-body">
         <h4 class="card-title">ข้อมูลการใช้อะไหล่น้ำมัน กับ รถตัดอ้อย</h4>

         <div class="card">
             <div class="card-body">
                 <h4 class="card-title">เงื่อนไขการแสดงข้อมูล</h4>
                 <div class="dropdown">

                         <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>

                         <asp:DropDownList ID="รหัสรถ" runat="server"></asp:DropDownList>

                         <asp:DropDownList ID="วันที่เริ่มต้น" runat="server"></asp:DropDownList>

                        <asp:DropDownList ID="ถึงวันที่" runat="server"></asp:DropDownList>
                 </div>
             </div>

             <table width="100%">
	         <tr>
            <td>                            
                <uc1:ucPageNavigator ID="ucPageNavigator" runat="server" OnPageIndexChanged="PageIndexChanged"/>     
                                  
            </td>
	        </tr>
	         <tr>
                 <td>
                     <asp:DataGrid ID="dtgData" runat="server" AutoGenerateColumns="false" Width="97%" CssClass="table table-bordered table-condensed table-responsive table-hover"
    HeaderStyle-CssClass="gridhead" ItemStyle-CssClass="gridrow0" AlternatingItemStyle-CssClass="gridrow1"
    AllowPaging="false" PagerStyle-Visible="false" 
    onitemdatabound="dtgData_ItemDataBound">
    <Columns>
        <asp:TemplateColumn>
            <HeaderTemplate>
                                <%#ColumnName[0]%>					                
                            </HeaderTemplate>
                            <ItemTemplate>
                               <%-- <asp:RadioButton ID="rdoTruck" runat="server" Checked="<%#ShowFlagRdo(Eval(ColumnName[0]))%>" onclick="rdoTruck_Click('R',this.id);" GroupName="rdoTruck<%# Container.DataSetIndex +1%>"/>--%>
					<asp:CheckBox ID="chkTruck" runat="server"  Checked="<%#ShowFlagRdo(Eval(ColumnName[0]))%>" GroupName="chkTruck<%# Container.DataSetIndex +1%>" />
                            </ItemTemplate>
        </asp:TemplateColumn>
		<asp:TemplateColumn>
                            <HeaderTemplate>
                                <%#ColumnName[1]%>					                
                            </HeaderTemplate>
                            <ItemTemplate>
                                
                            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn>
            <HeaderTemplate>
             <div class="labelRequireField"><%#ColumnTitle[2]%></div>
            </HeaderTemplate>
            <ItemTemplate>
                <%#Eval(ColumnName[2])%>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:TextBox ID="txtDetailCode" runat="server" Width="80" MaxLength="5" Text="<%#Eval(ColumnName[2])%>" ReadOnly="<%#showEdit(Eval(ColumnName[0])) %>" CssClass="<%#showStyleEdit(Eval(ColumnName[0])) %>"></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn>
            <HeaderTemplate>
             <div class="labelRequireField"><%#ColumnTitle[3]%></div>
            </HeaderTemplate>
            <ItemTemplate>
                <%#Eval(ColumnName[3])%>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtDetailDesc" runat="server" Width="150" MaxLength="40" Text="<%#Eval(ColumnName[3])%>" ReadOnly="<%#showEdit(Eval(ColumnName[0])) %>" CssClass="<%#showStyleEdit(Eval(ColumnName[0])) %>"></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn>
            <HeaderTemplate>
             <div class="labelRequireField"><%#ColumnTitle[4]%></div>
            </HeaderTemplate>
            <ItemTemplate>
                <%#Eval(ColumnName[4])%> 
            </ItemTemplate>
            <EditItemTemplate>	   
            </EditItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn>
            <HeaderTemplate>
             <div class="labelRequireField"><%#ColumnTitle[5]%></div>
            </HeaderTemplate>
            <ItemTemplate>
                <%#(Eval(ColumnName[5]))%>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtDetailTel" runat="server" Width="120" Text="<%#Eval(ColumnName[5])%>" MaxLength="20" ReadOnly="<%#showEdit(Eval(ColumnName[0])) %>" CssClass="<%#showStyleEdit(Eval(ColumnName[0])) %>"></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateColumn>  
        <asp:TemplateColumn>
            <HeaderTemplate>
             <%#ColumnTitle[6]%>
            </HeaderTemplate>
            <ItemTemplate>
                <%#(Eval(ColumnName[6]))%>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtDetailRemark" runat="server" Width="200" Text="<%#Eval(ColumnName[6])%>" MaxLength="100" ReadOnly="<%#showEdit(Eval(ColumnName[0])) %>" CssClass="<%#showStyleEdit(Eval(ColumnName[0])) %>"></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateColumn> 
  <asp:TemplateColumn>
            <HeaderTemplate>
             <div class="labelRequireField"><%#ColumnTitle[7]%></div>
            </HeaderTemplate>
            <ItemTemplate>
                <%#Eval(ColumnName[7])%> 
            </ItemTemplate>
            <EditItemTemplate>	
                                       
                                          
                         
            </EditItemTemplate>
        </asp:TemplateColumn>
     <%--   <asp:TemplateColumn>
            <HeaderTemplate>
             <%#ColumnTitle[8]%>							                    
            </HeaderTemplate>
            <ItemTemplate>		              
                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="Images/btnEDIT.GIF" ToolTip="Edit" index="<%# Container.DataSetIndex%>" OnClick="imgEdit_Click"/>
                <asp:ImageButton ID="imgDelete" runat="server" ImageUrl="Images/btnDelete.GIF" ToolTip="Delete"  index="<%# Container.DataSetIndex%>" OnClick="imgDelete_Click"/>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:ImageButton ID="imgSave" runat="server" ImageUrl="Images/img_save.gif" ToolTip="Save"  index="<%# Container.DataSetIndex%>"  OnClick="imgSave_Click" />                                
                <asp:ImageButton ID="imgCancel" runat="server" ImageUrl="Images/btnDelete.GIF" ToolTip="Cancel"  index="<%# Container.DataSetIndex%>" OnClick="imgCancel_Click"/>
            </EditItemTemplate>
        </asp:TemplateColumn>--%>
    </Columns>
</asp:DataGrid>
                 </td>
             </tr>
            </table>
               
        
  </div>





             <asp:Button  ID="btnSave" href="#" runat="server" Text="Confirm" OnClick="btnConfirm_Click"  />
         </div>
       </div>
    </div>
</div>
     <div class="col-12 border p-3 mt-3">
     style="display:none">
			 <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        <asp:Literal ID="litJavaScript" runat="server"></asp:Literal>  
        
 </div>
 </form>
</body>

</html>
