function showOrHidePicTure(imageID) {
    if ($('#' + imageID).css('display') == 'none') {
        $("#" + imageID).show();
    }
    else {
        $("#" + imageID).hide();
    }
}

function Close_TagDivName(DivID) {
    $("#" + DivID).hide();
}
function Show_TagDivName(DivID, txtAlert, StyleName) {
    $("#" + DivID).text(txtAlert);
    $("#" + DivID).removeClass('alert alert-info');
    $("#" + DivID).removeClass('alert alert-success');
    $("#" + DivID).removeClass('alert alert-danger');
    $("#" + DivID).addClass(StyleName)
    $("#" + DivID).show();
}
function CloseDivOutputAlert() {
    document.getElementById("output").style.display = "none";
}
function GetDataAjax(parameter_value, parameter_value2,typee) {
    var result = "";
    var scriptUrl = "frmGetDataByAjax.aspx?parameter_value=" + parameter_value + "&parameter_value2=" + parameter_value2 + "&typee=" + typee;
    $.ajax({
        url: scriptUrl,
        type: 'get',
        dataType: 'html',
        async: false,
        success: function (data) {
            result = data;
        }
    });
    return result;
}


function getCookie(cookieName, document) {
    var name = cookieName + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
}


function setCookie(cname, cvalue, exdays, document) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + "; " + expires;
}

function delete_cookie(name, document) {
    document.cookie = name + '=; expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}


function setNameUserToNav(txtUsername) {
    $("#a_user").html('<span class="glyphicon glyphicon-user"></span>&nbsp;' + txtUsername);
}


//==start=================Javascript ไว้ Getค่าจาาก Parameter url=========================
function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}
//==end=================Javascript ไว้ Getค่าจาาก Parameter url=========================


//=============จัดการ Active Menu===========================================
function setActiveMenu_And_TitlePage(TitlePageName) {
    var ActiveMenu = getParameterByName('ActiveMenu');
    $("#" + ActiveMenu).removeClass();
    $("#" + ActiveMenu).addClass('active');
    $("#nav_title").text(TitlePageName);
}
//====end=========จัดการ Active Menu===========================================
