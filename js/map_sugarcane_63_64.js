var flagIsPng = true;

function GetBoundingBox(coord, zoom) {
    var proj = map.getProjection();
    var zfactor = Math.pow(2, zoom);

    //Get Latitude Longitude Coordinates
    //Start Projection อันเก่า
    var top = proj.fromPointToLatLng(new google.maps.Point(coord.x * 256 / zfactor, coord.y * 256 / zfactor));
    var bot = proj.fromPointToLatLng(new google.maps.Point((coord.x + 1) * 256 / zfactor, (coord.y + 1) * 256 / zfactor));
    //End Projection อันเก่า

    //Start Projection อันใหม่
    //var top = proj.fromPointToLatLng(new google.maps.Point(coord.x * 256 / zfactor, (coord.y + 1) * 256 / zfactor));
    //var bot = proj.fromPointToLatLng(new google.maps.Point((coord.x + 1) * 256 / zfactor, coord.y * 256 / zfactor));
    //End Projection อันใหม่

    //var gBl = proj.fromPointToLatLng(new google.maps.Point(coord.x * twidth / s, (coord.y + 1) * theight / s)); // bottom left / SW
    //var gTr = proj.fromPointToLatLng(new google.maps.Point((coord.x + 1) * twidth / s, coord.y * theight / s)); // top right / NE

    //Corrections for the Slight Shift of the SLP (MapServer)
    var deltaX = 0.0000015; //อันเก่า: 0.00001 อันใหม่: 0.0000015
    var deltaY = 0.0000056; //อันเก่า: 0.00005 อันใหม่: 0.0000056

    //Create the Bounding Box String
    var boundingBox = (top.lng() + deltaX) + "," + (bot.lat() + deltaY) + "," + (bot.lng() + deltaX) + "," + (top.lat() + deltaY);
    return boundingBox;
}

function GetWebMapServiceURL(layer, bounding_box, map_style, code_company, mode, sugarcane_gene, flag_tai, area_layout, soil_type, water_source, water_system, groove_distance, disease, insect, darn, khet, freight_id, finish_cane_cut, rate_null, rate0, rate1, rate2, rate3, rate4, rate5, rate6, rate7, rate8, rate9, rate10, rate11, percent_null, percent0, percent1, percent2, percent3, percent4, percent5, percent6) {//mode, role, darn, sub_darn, company_code, farmer_code, map_style
    //private WMS IP: http://10.4.89.188:8080
    //public WMS IP: http://103.213.207.17:8080
    //http://geoserver.cristalla.co.th:8080
    //var mapServerUrl = "http://geoserver.cristalla.co.th:8080/";
	var mapServerUrl = "https://geoserver.cristalla.co.th/";
    //Base WMS URL

	var webMapServiceURL = mapServerUrl +"geoserver/SugarcaneView/wms?";
    webMapServiceURL += "SERVICE=WMS";
    webMapServiceURL += "&REQUEST=GetMap";
    webMapServiceURL += "&VERSION=1.1.0";
	webMapServiceURL += "&LAYERS=SugarcaneView:view_sugarcane";
    webMapServiceURL += "&FORMAT=image/png";
    webMapServiceURL += "&TRANSPARENT=TRUE";
    webMapServiceURL += "&SRS=EPSG:4326";
    webMapServiceURL += "&BBOX=" + bounding_box;
    webMapServiceURL += "&WIDTH=256";
    webMapServiceURL += "&HEIGHT=256";
	webMapServiceURL += "&STYLES=view_sugarcane_style";
    //if (mode == "province_filter") {
    //    if ((layer == "Sugarcane62-63:thailand_map") && (code_company != "")) {
    //        webMapServiceURL += "&CQL_FILTER=name_1=";
    //        if (code_company == "113") {
    //            webMapServiceURL += "'Sukhothai'";
    //        } else if (code_company == "102") {
    //            webMapServiceURL += "'Kamphaeng Phet'";
    //        } else if (code_company == "104") {
    //            webMapServiceURL += "'Suphan Buri'";
    //        }
    //    }
    //} else
    if (mode === "field_filter") {
        webMapServiceURL += "&CQL_FILTER=code<>'0'";

        if (rate_null === true) {
            webMapServiceURL += " AND (average_ccs IS NULL)";
        }
        if (rate0 === true) {
            webMapServiceURL += " AND (average_ccs BETWEEN 0 AND 0)";
        }
        if (rate1 === true) {
            webMapServiceURL += " AND (average_ccs BETWEEN 0.000001 AND 01.999999)";
        }
        if (rate2 === true) {
            webMapServiceURL += " AND (average_ccs BETWEEN 02.000000 AND 03.999999)";
        }
        if (rate3 === true) {
            webMapServiceURL += " AND (average_ccs BETWEEN 04.000000 AND 05.999999)";
        }
        if (rate4 === true) {
            webMapServiceURL += " AND (average_ccs BETWEEN 06.000000 AND 07.999999)";
        }
        if (rate5 === true) {
            webMapServiceURL += " AND (average_ccs BETWEEN 08.000000 AND 09.999999)";
        }
        if (rate6 === true) {
            webMapServiceURL += " AND (average_ccs BETWEEN 10.000000 AND 11.999999)";
        }
        if (rate7 === true) {
            webMapServiceURL += " AND (average_ccs BETWEEN 12.000000 AND 13.999999)";
        }
        if (rate8 === true) {
            webMapServiceURL += " AND (average_ccs BETWEEN 14.000000 AND 15.999999)";
        }
        if (rate9 === true) {
            webMapServiceURL += " AND (average_ccs BETWEEN 16.000000 AND 17.999999)";
        }
        if (rate10 === true) {
            webMapServiceURL += " AND (average_ccs BETWEEN 18.000000 AND 19.999999)";
        }
        if (rate11 === true) {
            webMapServiceURL += " AND (average_ccs BETWEEN 20.000000 AND 21.999999)";
        }

        if (percent_null === true) {
            webMapServiceURL += " AND (percent IS NULL)";
        }
        if (percent0 === true) {
            webMapServiceURL += " AND (percent BETWEEN 0 AND 0)";
        }
        if (percent1 === true) {
            webMapServiceURL += " AND (percent BETWEEN 0.01 AND 20.00)";
        }
        if (percent2 === true) {
            webMapServiceURL += " AND (percent BETWEEN 20.01 AND 40.00)";
        }
        if (percent3 === true) {
            webMapServiceURL += " AND (percent BETWEEN 40.01 AND 60.00)";
        }
        if (percent4 === true) {
            webMapServiceURL += " AND (percent BETWEEN 60.01 AND 80.00)";
        }
        if (percent5 === true) {
            webMapServiceURL += " AND (percent BETWEEN 80.01 AND 100.00)";
        }
        if (percent6 === true) {
            webMapServiceURL += " AND (percent > 100.00)";
        }

        if (darn !== "DarnEmpty") {
            webMapServiceURL += " AND darn='" + darn + "'";
        } else {
            webMapServiceURL += "";
        }

        if (khet !== "KhetEmpty") {
            webMapServiceURL += " AND khet='" + khet + "'";
        } else {
            webMapServiceURL += "";
        }

        if (sugarcane_gene !== "SugarcaneGeneEmpty") {
            webMapServiceURL += " AND sugarcane_gene='" + sugarcane_gene + "'";
        } else {
            webMapServiceURL += "";
        }

        if (flag_tai !== "TaiProjectEmpty") {
            webMapServiceURL += " AND flag_tai=" + flag_tai;
        } else {
            webMapServiceURL += "";
        }

        if (area_layout !== "AreaLayoutEmpty") {
            webMapServiceURL += " AND area_layout='" + area_layout + "'";
        } else {
            webMapServiceURL += "";
        }

        if (soil_type !== "SoilTypeEmpty") {
            webMapServiceURL += " AND soil_type='" + soil_type + "'";
        } else {
            webMapServiceURL += "";
        }

        if (water_source !== "WaterSourceEmpty") {
            webMapServiceURL += " AND water_source='" + water_source + "'";
        } else {
            webMapServiceURL += "";
        }

        if (water_system !== "WaterSystemEmpty") {
            webMapServiceURL += " AND water_system='" + water_system + "'";
        } else {
            webMapServiceURL += "";
        }

        if (groove_distance !== "GrooveDistanceEmpty") {
            webMapServiceURL += " AND groove_distance='" + groove_distance + "'";
        } else {
            webMapServiceURL += "";
        }

        if (disease !== "DiseaseEmpty") {
            webMapServiceURL += " AND disease='" + disease + "'";
        } else {
            webMapServiceURL += "";
        }

        if (insect !== "InsectEmpty") {
            webMapServiceURL += " AND insect='" + insect + "'";
        } else {
            webMapServiceURL += "";
        }

        if (freight_id !== "FreightIDEmpty") {
            webMapServiceURL += " AND freight_id='" + freight_id + "'";
        } else {
            webMapServiceURL += "";
        }

        if (finish_cane_cut !== "FinishCaneCutEmpty") {
            var intFinishCaneCut = 0;
            if (finish_cane_cut === "FinishCaneCut") {
                intFinishCaneCut = 1;
            }
            else if (finish_cane_cut === "NotFinishCaneCut") {
                intFinishCaneCut = 0;
            }
            webMapServiceURL += " AND finish_cane_cut='" + intFinishCaneCut + "'";
        } else {
            webMapServiceURL += "";
        }
    }
    console.log("webMapServiceURL: " + webMapServiceURL);
    return webMapServiceURL;
}
