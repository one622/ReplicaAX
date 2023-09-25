var flagIsPng = true;

function GetBoundingBox(coord, zoom) {
    var proj = map.getProjection();
    var zfactor = Math.pow(2, zoom);

    //Get Latitude Longitude Coordinates
    var top = proj.fromPointToLatLng(new google.maps.Point(coord.x * 256 / zfactor, coord.y * 256 / zfactor));
    var bot = proj.fromPointToLatLng(new google.maps.Point((coord.x + 1) * 256 / zfactor, (coord.y + 1) * 256 / zfactor));

    //Corrections for the Slight Shift of the SLP (MapServer)
    var deltaX = 0.0000015; //อันเก่า: 0.00001 อันใหม่: 0.0000015
    var deltaY = 0.0000056; //อันเก่า: 0.00005 อันใหม่: 0.0000056

    //Create the Bounding Box String
    var boundingBox = (top.lng() + deltaX) + "," + (bot.lat() + deltaY) + "," + (bot.lng() + deltaX) + "," + (top.lat() + deltaY);
    return boundingBox;
}

function GetWebMapServiceURL(mode, role, layer, bounding_box, map_style, darn, sub_darn, company_code, farmer_code) {
    //private WMS IP: http://10.4.89.188:8080
    //public WMS IP: http://103.213.207.17:8080
    //http://geoserver.cristalla.co.th:8080
    //console.log("mode: " + mode + ", role: " + role + ", layer: " + layer + ", bounding_box: " + bounding_box + ", map_style: " + map_style + ", darn: " + darn + ", sub_darn: " + sub_darn + ", company_code: " + company_code + ", farmer_code: " + farmer_code);
    var mapServerPublicUrl = "http://geoserver.cristalla.co.th:8080/";
    var webMapServiceURL = mapServerPublicUrl + "geoserver/";
    if (mode == "GetSugarFactoryInMyCompany") {
        webMapServiceURL += "Sugarcane61-62";
    } else {
        webMapServiceURL += "Sugarcane62-63";
    }
    webMapServiceURL += "/wms?";
    webMapServiceURL += "SERVICE=WMS";
    webMapServiceURL += "&REQUEST=GetMap";
    webMapServiceURL += "&VERSION=1.1.0";
    webMapServiceURL += "&LAYERS=" + layer;
    webMapServiceURL += "&FORMAT=image/png";
    webMapServiceURL += "&TRANSPARENT=TRUE";
    webMapServiceURL += "&SRS=EPSG:4326";
    webMapServiceURL += "&BBOX=" + bounding_box;
    webMapServiceURL += "&WIDTH=256";
    webMapServiceURL += "&HEIGHT=256";
    webMapServiceURL += "&STYLES=" + map_style;

    if (role == 10) {
        webMapServiceURL += "&CQL_FILTER=khet='" + sub_darn + "'";
    } else if (role == 20) {
        if (mode == "GetSubDarn") {
            webMapServiceURL += "&CQL_FILTER=khet='" + sub_darn + "'";
        } else {
            webMapServiceURL += "&CQL_FILTER=darn='" + darn + "'";
        }
    } else if (role > 20) {
        if (mode == "GetDarn") {
            webMapServiceURL += "&CQL_FILTER=darn='" + darn + "'";
        } else if (mode == "GetSubDarn") {
            webMapServiceURL += "&CQL_FILTER=khet='" + sub_darn + "'";
        } else {
            webMapServiceURL += "";
        }
    }

    if (mode == "GetSugarFactoryInMyCompany") {
        webMapServiceURL += "&CQL_FILTER=";
        if (company_code == "113") {
            webMapServiceURL += "fcode='s04'";
        } else if (company_code == "102") {
            webMapServiceURL += "fcode='s02'";
        } else if (company_code == "104") {
            webMapServiceURL += "fcode='s10'";
        }
    } else if (mode == "GetFarmerCode") {
        webMapServiceURL += "&CQL_FILTER=code='" + farmer_code + "'";
    }
    return webMapServiceURL;
}

function PutOtherCompanySugarcaneFactoryLayer() {
    var OtherCompanySugarcaneFactoryLayer = new google.maps.ImageMapType({
        getTileUrl: function (coord, zoom) {
            //Return URL for the tile
            return GetWebMapServiceURL("GetSugarFactoryOtherCompany", "Sugarcane61-62:sugarcane_factory_thailand", GetBoundingBox(coord, zoom), "other_company_sugarcane_factory_style", "", "", "", "", "");
        },
        tileSize: new google.maps.Size(256, 256),
        isPng: flagIsPng
    });
    if (chkCheckboxOtherCompanySugarcaneFactory.checked == true) {
        map.overlayMapTypes.push(null);
        map.overlayMapTypes.setAt(4, OtherCompanySugarcaneFactoryLayer);
    } else if (chkCheckboxOtherCompanySugarcaneFactory.checked == false) {
        map.overlayMapTypes.push(null);
        map.overlayMapTypes.setAt(4, null);
    }
}
