<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fromAX.aspx.cs" Inherits="ReplicaAX.WebForm1" %>

<!DOCTYPE html>
<html>
<head runat="server">
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

    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="Scripts/jquery-ui.1.11.4.min.js"></script>
    <script src="Scripts/jquery.ajax-progress.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="js/Js_ModMain.js"></script>
    <script src="js/Js_Md5.js"></script>
</head>
<body>
     <header>
     <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark bg-dark border-bottom box-shadow mb-3">
         <div class="container">
              <img src="LogoCristalla_v2.png" alt="img" style="max-width: 20px; max-height: 24px"></img> Navbar</a>
             <a class="navbar-brand" asp-area="" asp-page="/Index">ระบบ InterfaceSugracaneSAP</a>
             <button class="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                     aria-expanded="false" aria-label="Toggle navigation">
                 <span class="navbar-toggler-icon"></span>
             </button>
             <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                 <ul class="navbar-nav flex-grow-1">
                     <li class="nav-item">
                         <a class="nav-link text-white" asp-area="" asp-page="/Index">Home</a>
                     </li>
                     <li class="nav-item">
                         <a class="nav-link text-white" asp-area="" asp-page="/FarmareaList/Index">Table Truckoil List</a>
                     </li>
                   
                 </ul>
             </div>
         </div>
     </nav>
 </header>

    <body>

        <div class="container" style="margin-top: 20px;">
            <!-- Text Field -->
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">เงื่อนไขการดึงข้อมูล</h4>

                    <div class="dropdown">
                        <button type="button" class="btn btn-primary dropdown-toggle" data-bs-toggle="dropdown"
                                style="background-color: #495057;" id="ปีฤดูหีบ">
                            ปีฤดูหีบ:
                        </button>
                        <ul class="dropdown-menu">
                            <li><a class="dropdown-item" href="#">Link 1</a></li>
                            <li><a class="dropdown-item" href="#">Link 2</a></li>
                            <li><a class="dropdown-item" href="#">Link 3</a></li>
                        </ul>

                        <button type="button" class="btn btn-primary dropdown-toggle" data-bs-toggle="dropdown"
                                style="background-color: #495057;" id="รหัสรถ">
                            รหัสรถ:
                        </button>
                        <ul class="dropdown-menu">
                            <li><a class="dropdown-item" href="#">Link 1</a></li>
                            <li><a class="dropdown-item" href="#">Link 2</a></li>
                            <li><a class="dropdown-item" href="#">Link 3</a></li>

                        </ul>

                        <button type="button" class="btn btn-primary dropdown-toggle" data-bs-toggle="dropdown"
                                style="background-color: #495057;">
                            วันที่รายการ:
                        </button>
                        <ul class="dropdown-menu">
                            <li><a class="dropdown-item" href="#">Link 1</a></li>
                            <li><a class="dropdown-item" href="#">Link 2</a></li>
                            <li><a class="dropdown-item" href="#">Link 3</a></li>
                        </ul>
                        <a href="#"  onclick="return confirm('คุณมั่นใจที่จะกดดึงข้อมูลใช่หรือไม่?')" class="btn btn-success btn-rounded">ดึงข้อมูล</a>

                    </div>
                </div>
            </div>

            <br>



            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">ข้อมูลการใช้อะไหล่น้ำมัน กับ รถตัดอ้อย</h4>

                    <div class="card">
                        <div class="card-body">
                            <h4 class="card-title">เงื่อนไขการแสดงข้อมูล</h4>
                            <div class="dropdown">
                                <button type="button" class="btn btn-primary dropdown-toggle" data-bs-toggle="dropdown"
                                        style="background-color: #495057;">
                                    Show:
                                </button>
                                <ul class="dropdown-menu">
                                    <li><a class="dropdown-item" href="#">Link 1</a></li>
                                    <li><a class="dropdown-item" href="#">Link 2</a></li>
                                    <li><a class="dropdown-item" href="#">Link 3</a></li>
                                </ul>

                                <button type="button" class="btn btn-primary dropdown-toggle" data-bs-toggle="dropdown"
                                        style="background-color: #495057;" id="รหัสรถ">
                                    รหัสรถ:
                                </button>
                                <ul class="dropdown-menu">
                                    <li><a class="dropdown-item" href="#">Link 1</a></li>
                                    <li><a class="dropdown-item" href="#">Link 2</a></li>
                                    <li><a class="dropdown-item" href="#">Link 3</a></li>
                                </ul>

                                <button type="button" class="btn btn-primary dropdown-toggle" data-bs-toggle="dropdown"
                                        style="background-color: #495057;">
                                    วันที่เริ่มต้น:
                                </button>
                                <ul class="dropdown-menu">
                                    <li><a class="dropdown-item" href="#">Link 1</a></li>
                                    <li><a class="dropdown-item" href="#">Link 2</a></li>
                                    <li><a class="dropdown-item" href="#">Link 3</a></li>
                                </ul>

                                <button type="button" class="btn btn-primary dropdown-toggle" data-bs-toggle="dropdown"
                                        style="background-color: #495057;">
                                    ถึงวันที่:
                                </button>
                                <ul class="dropdown-menu">
                                    <li><a class="dropdown-item" href="#">Link 1</a></li>
                                    <li><a class="dropdown-item" href="#">Link 2</a></li>
                                    <li><a class="dropdown-item" href="#">Link 3</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 border p-3 mt-3">
                        <form method="post">
                            @if (Model.Farmareas.Count() > 0)
                            {
                                <table class="table table-striped border" id="tblist">
                                    <thead>
                                        <tr class="table-secondary">
                                            <th>
                                                <label>Mark</label>
                                            </th>
                                            <th>
                                                <label asp-for="Farmareas.FirstOrDefault().uid"></label>
                                            </th>
                                            <th>
                                                <label asp-for="Farmareas.FirstOrDefault().truckoil"></label>
                                            </th>
                                            <th>
                                                <label asp-for="Farmareas.FirstOrDefault().oilamoun"></label>
                                            </th>
                                            <th>
                                                <label asp-for="Farmareas.FirstOrDefault().transdate"></label>
                                            </th>
                                            <th>
                                                <label asp-for="Farmareas.FirstOrDefault().approvestatus"></label>
                                            </th>
                                            <th>
                                                <label asp-for="Farmareas.FirstOrDefault().approver"></label>
                                            </th>
                                            <th>
                                                <label asp-for="Farmareas.FirstOrDefault().vehicle_code"></label>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        @foreach (var item in Model.Farmareas)
                                        {
                                            <tr>
                                                <td>
                                                    <input type="checkbox" id="vehicle1" name="vehicle1" value="Bike">
                                                </td>
                                                <td>
                                                    @Html.DisplayFor(m => item.uid)
                                                </td>
                                                <td>
                                                    @Html.DisplayFor(m => item.truckoil)
                                                </td>
                                                <td>
                                                    @Html.DisplayFor(m => item.oilamoun)
                                                </td>
                                                <td>
                                                    @Html.DisplayFor(m => item.transdate)
                                                </td>
                                                <td>
                                                    @Html.DisplayFor(m => item.approvestatus)
                                                </td>
                                                <td>
                                                    @Html.DisplayFor(m => item.approver)
                                                </td>
                                                <td>
                                                    @Html.DisplayFor(m => item.vehicle_code)
                                                </td>
                                                @*  <td>
                                        <button asp-page-handle="Delete" onclick="return confirm('Are you sure you want to delete?')" class="btn btn-danger btn-sm">Delete</button>

                                        </td> *@
                                            </tr>
                                        }
                                    </tbody>
                                </table>

                            }
                            else
                            {
                                <p>No books available.</p>
                            }
                        </form>
                    </div>

                    <br>

                    <div>
                        <div class="input-group mb-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">รายการ</span>
                            </div>
                            <div>
                                <input type="text" placeholder="กรอกจำนวนรายการ" class="form-control">
                            </div>


                            <div class="input-group-prepend">
                                <span class="input-group-text">ยอดรวม</span>
                            </div>
                            <div>
                                <input type="text" placeholder="กรอกจำนวนยอดรวม" class="form-control">
                            </div>
                        </div>
                        <div>
                            <a href="#" id="Mark All" class="btn btn-success">Mark All</a>
                            <a href="#" id="Un-Mark All" class="btn btn-success">Un-Mark All</a>
                            <a href="#" id="Un-Confirm" class="btn btn-success">Un-Confirm</a>
                            <a href="#" id="Confirm" onclick="return confirm('Are you sure you want to Confirm?')" class="btn btn-success">Confirm</a>

                        </div>



                    </div>
                </div>
            </div>
            <br />

            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">ข้อมูลการโอนอะไหล่ น้ำมัน คืนพัสดุ</h4>

                    <div class="card">
                        <div class="card-body">
                            <h4 class="card-title">เงื่อนไขการแสดงข้อมูล</h4>
                            <div class="dropdown">
                                <button type="button" class="btn btn-primary dropdown-toggle" data-bs-toggle="dropdown"
                                        style="background-color: #495057;">
                                    Show:
                                </button>
                                <ul class="dropdown-menu">
                                    <li><a class="dropdown-item" href="#">Link 1</a></li>
                                    <li><a class="dropdown-item" href="#">Link 2</a></li>
                                    <li><a class="dropdown-item" href="#">Link 3</a></li>
                                </ul>

                                <button type="button" class="btn btn-primary dropdown-toggle" data-bs-toggle="dropdown"
                                        style="background-color: #495057;">
                                    รหัสรถ:
                                </button>
                                <ul class="dropdown-menu">
                                    <li><a class="dropdown-item" href="#">Link 1</a></li>
                                    <li><a class="dropdown-item" href="#">Link 2</a></li>
                                    <li><a class="dropdown-item" href="#">Link 3</a></li>
                                </ul>

                                <button type="button" class="btn btn-primary dropdown-toggle" data-bs-toggle="dropdown"
                                        style="background-color: #495057;">
                                    วันที่เริ่มต้น:
                                </button>
                                <ul class="dropdown-menu">
                                    <li><a class="dropdown-item" href="#">Link 1</a></li>
                                    <li><a class="dropdown-item" href="#">Link 2</a></li>
                                    <li><a class="dropdown-item" href="#">Link 3</a></li>
                                </ul>

                                <button type="button" class="btn btn-primary dropdown-toggle" data-bs-toggle="dropdown"
                                        style="background-color: #495057;">
                                    ถึงวันที่:
                                </button>
                                <ul class="dropdown-menu">
                                    <li><a class="dropdown-item" href="#">Link 1</a></li>
                                    <li><a class="dropdown-item" href="#">Link 2</a></li>
                                    <li><a class="dropdown-item" href="#">Link 3</a></li>
                                </ul>
                            </div>
                        </div>
                        <div class="col-12 border p-3 mt-3">
                            <form method="post">
                                @if (Model.Farmareas.Count() > 0)
                                {
                                    <table class="table table-striped border" id="tblist1">
                                        <thead>
                                            <tr class="table-secondary">
                                                <th>
                                                    <label>Mark</label>
                                                </th>
                                                <th>
                                                    <label asp-for="Farmareas.FirstOrDefault().uid"></label>
                                                </th>
                                                <th>
                                                    <label asp-for="Farmareas.FirstOrDefault().truckoil"></label>
                                                </th>
                                                <th>
                                                    <label asp-for="Farmareas.FirstOrDefault().oilamoun"></label>
                                                </th>
                                                <th>
                                                    <label asp-for="Farmareas.FirstOrDefault().transdate"></label>
                                                </th>
                                                <th>
                                                    <label asp-for="Farmareas.FirstOrDefault().approvestatus"></label>
                                                </th>
                                                <th>
                                                    <label asp-for="Farmareas.FirstOrDefault().approver"></label>
                                                </th>
                                                <th>
                                                    <label asp-for="Farmareas.FirstOrDefault().vehicle_code"></label>
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            @foreach (var item in Model.Farmareas)
                                            {
                                                <tr>
                                                    <td>
                                                        <input type="checkbox" id="vehicle1" name="vehicle1" value="Bike">
                                                    </td>
                                                    <td>
                                                        @Html.DisplayFor(m => item.uid)
                                                    </td>
                                                    <td>
                                                        @Html.DisplayFor(m => item.truckoil)
                                                    </td>
                                                    <td>
                                                        @Html.DisplayFor(m => item.oilamoun)
                                                    </td>
                                                    <td>
                                                        @Html.DisplayFor(m => item.transdate)
                                                    </td>
                                                    <td>
                                                        @Html.DisplayFor(m => item.approvestatus)
                                                    </td>
                                                    <td>
                                                        @Html.DisplayFor(m => item.approver)
                                                    </td>
                                                    <td>
                                                        @Html.DisplayFor(m => item.vehicle_code)
                                                    </td>
                                             
                                                </tr>
                                            }
                                        </tbody>
                                    </table>
                                }
                                else
                                {
                                    <p>No books available.</p>
                                }
                            </form>
                        </div>
                        <br>

                        <div>
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">รายการ</span>
                                </div>
                                <br>
                                <div>
                                    <input type="text" placeholder="กรอกจำนวนรายการ" class="form-control">
                                </div>
                                <div>
                                </div>
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ยอดรวม</span>
                                </div>
                                <br>
                                <div>
                                    <input type="text"  placeholder="กรอกจำนวนยอดรวม" class="form-control">
                                </div>
                            </div>

                            <div>
                                <a href="#" id="Mark All" class="btn btn-success">Mark All</a>
                                <a href="#" id="Un-Mark All" class="btn btn-success">Un-Mark All</a>
                                <a href="#" id="Un-Confirm" class="btn btn-success">Un-Confirm</a>
                                <a href="#" id="Confirm" class="btn btn-success">Confirm</a>
                            </div>
                            

                        </div>
                    </div>
                </div>
                <br />

    </body>
</body>
<script src="~/lib/jquery/dist/jquery.min.js"></script>
<script src="~/datatable/jquery.datatables.js"></script>
<script src="~/datatable/jquery.datatables.min.js"></script>

<link rel="stylesheet" href="https://cdn.datatables.net/1.13.6/css/jquery.dataTables.css" />
<script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.js"></script>

<script>
    $(document).ready(function () {
        console.log("Here");
        $('#tblist').DataTable();
    })
</script>

<script>
    $(document).ready(function () {
        console.log("Here");
        $('#tblist1').DataTable();
    })
</script>

