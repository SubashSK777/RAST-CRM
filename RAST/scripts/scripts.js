
function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

// Validation Scripts //

function validNumbers(e, t, errorElement, AlertMessage) {
    try {
        if (window.event) {
            var charCode = window.event.keyCode;
        }
        else if (e) {
            var charCode = e.which;
        }
        else { return true; }
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
    catch (err) {
        errorelement.innerHTML = "<font color='red'>" + AlertMessage + "</font>";
    }
}

function checkEmail(emailAddress) {
    var sQtext = '[^\\x0d\\x22\\x5c\\x80-\\xff]';
    var sDtext = '[^\\x0d\\x5b-\\x5d\\x80-\\xff]';
    var sAtom = '[^\\x00-\\x20\\x22\\x28\\x29\\x2c\\x2e\\x3a-\\x3c\\x3e\\x40\\x5b-\\x5d\\x7f-\\xff]+';
    var sQuotedPair = '\\x5c[\\x00-\\x7f]';
    var sDomainLiteral = '\\x5b(' + sDtext + '|' + sQuotedPair + ')*\\x5d';
    var sQuotedString = '\\x22(' + sQtext + '|' + sQuotedPair + ')*\\x22';
    var sDomain_ref = sAtom;
    var sSubDomain = '(' + sDomain_ref + '|' + sDomainLiteral + ')';
    var sWord = '(' + sAtom + '|' + sQuotedString + ')';
    var sDomain = sSubDomain + '(\\x2e' + sSubDomain + ')*';
    var sLocalPart = sWord + '(\\x2e' + sWord + ')*';
    var sAddrSpec = sLocalPart + '\\x40' + sDomain; // complete RFC822 email address spec
    var sValidEmail = '^' + sAddrSpec + '$'; // as whole string

    var reValidEmail = new RegExp(sValidEmail);

    return reValidEmail.test(emailAddress);
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
}

function CheckEmpty(element, errorelement, AlertMessage) {
    if (element.value.trim() == "") {
        errorelement.innerHTML = "<font color='red'>" + AlertMessage + "</font>";
        element.focus();
        return false;
    }
    errorelement.innerHTML = "";
    return true;
}

// End Validation Scripts //

/*Ragu Remove 01 May 2022
 function fnAuthenticateLogin() {
    var login = document.getElementById("txtEmail").value;
    var password = document.getElementById("txtPassword").value;
    if (CheckEmpty(document.getElementById("txtEmail"), document.getElementById("spEmail"), "  Error - Email address cannot be empty") == false) {
        return false;
    }
    if (checkEmail(document.getElementById("txtEmail").value) == false) {
        document.getElementById('spEmail').innerHTML = "<font color='red'> Error - Invalid Email address</font>";
        return false;
    }
    if (CheckEmpty(document.getElementById("txtPassword"), document.getElementById("spPassword"), "  Error - Password cannot be empty") == false) {
        return false
    }

    var chkStatus = 0;
    if ($('#chkRemember').is(':checked')) {
        chkStatus = 1;
    }

    document.getElementById('divMsg').innerHTML = "<font color='blue'>Authenticating. Please wait....</font>";

    var xmlhttp = new XMLHttpRequest();

    var strType = getParameterByName("type");

    var url = "/services/authentication.aspx?id=0&login=" + login + "&password=" + password + "&chkbox=" + chkStatus;

    if (strType == "e") {
        params = params + "&type=e";
        xmlhttp.open("GET", url, false);
    }
    else {
        xmlhttp.open("GET", url, false);
    }

    xmlhttp.send();

    var intRetValue = xmlhttp.responseText;

    if (intRetValue == 1) {

        location.href = "Map.aspx";

    }
    else
        if (intRetValue == 0) {
            document.getElementById('divMsg').innerHTML = "";
            document.getElementById('spEmail').innerHTML = "<font color='red'><b>INVALID EMAIL ID</b></font>";

        }
        else
            if (intRetValue == 2) {
                document.getElementById('divMsg').innerHTML = "";
                document.getElementById('spPassword').innerHTML = "<font color='red'><b>INVALID PASSWORD</b></font>";
            }
}*/

// start sites_ui.aspx //
function fnDeleteRow(intRowNo, intHubId) {
    var xmlhttp = new XMLHttpRequest();
    var params = "hubid=" + intHubId;
    xmlhttp.open("GET", "/services/site.aspx?id=4&" + params, false);

    document.getElementById("hidEdit").value = "";
    document.getElementById("hidHubId").value = "";
    document.getElementById("txtHubId").value = "";
    document.getElementById("txtPhoneNumber").value = "";
    document.getElementById("txtHubOnTime").value = "00:01";
    document.getElementById("txtHubOffTime").value = "23:59";

    document.getElementById("SelectMobileProvider").value = "Singtel";
    document.getElementById("SelectTypeOfSim").value = "M2M";


    document.getElementById("btnAddHubDetails").innerHTML = "Add Hub Details";

    xmlhttp.send();
    var intReturnValue = xmlhttp.responseText;
    document.getElementById("ContentPlaceHolder1_tblHubDetails").deleteRow(intRowNo);
}

function fnEditRowHub(intRowNo, intHubId) {
    document.getElementById("hidEdit").value = intRowNo;
    document.getElementById("hidHubId").value = intHubId;

    var table = document.getElementById("ContentPlaceHolder1_tblHubDetails").getElementsByTagName('tbody')[0];
    var strHubId = (table.rows[intRowNo].cells[1].innerHTML);
    var strHubPhoneNumber = (table.rows[intRowNo].cells[2].innerHTML);

    var strHubOnTime = (table.rows[intRowNo].cells[3].innerHTML);
    var strHubOffTime = (table.rows[intRowNo].cells[4].innerHTML);
    var strSelectMobileProvider = (table.rows[intRowNo].cells[5].innerHTML);
    var strSelectTypeOfSim = (table.rows[intRowNo].cells[6].innerHTML);

    document.getElementById("txtHubId").value = strHubId;
    document.getElementById("txtPhoneNumber").value = strHubPhoneNumber;

    document.getElementById("txtHubOnTime").value = strHubOnTime;
    document.getElementById("txtHubOffTime").value = strHubOffTime;
    document.getElementById("SelectMobileProvider").value = strSelectMobileProvider;
    document.getElementById("SelectTypeOfSim").value = strSelectTypeOfSim;

    document.getElementById("btnAddHubDetails").innerHTML = "Update Hub Details";

}

function fnAddHubDetails() {

    var table = document.getElementById("ContentPlaceHolder1_tblHubDetails").getElementsByTagName('tbody')[0];

    //Check if HubId already exists

    if (CheckEmpty(document.getElementById("txtHubId"), document.getElementById("lblHubIdError"), "  Error - Hub Id cannot be empty") == false) {
        return false
    }

    if (CheckEmpty(document.getElementById("txtPhoneNumber"), document.getElementById("lblHubPhoneNumberError"), "  Error - Hub Phone Number cannot be empty") == false) {
        return false
    }

    var hubValid = chkLicenseHub(); // Ragu 13 Jun 2019

    if (hubValid == 0) {
        alert("HUB QR Number not registered. Please contact Administrator");
        return false
    }
    /*else if (hubValid == 1) {
        var preNumber = table.rows[intRowNo].cells[2].innerHTML;
        var recentNumber = document.getElementById("txtPhoneNumber").value;

        if (preNumber == recentNumber) {
            //do
        }
        else {
            alert("HUB QR Number already inside. Please contact Administrator");
        }
        return false
    }*/
   

    if (parseInt(document.getElementById("hidEdit").value) > 0) {
        
        var intRowNo = document.getElementById("hidEdit").value;
        table.rows[intRowNo].cells[1].innerHTML = document.getElementById("txtHubId").value;
        table.rows[intRowNo].cells[2].innerHTML = document.getElementById("txtPhoneNumber").value;
        table.rows[intRowNo].cells[3].innerHTML = document.getElementById("txtHubOnTime").value;
        table.rows[intRowNo].cells[4].innerHTML = document.getElementById("txtHubOffTime").value;
        table.rows[intRowNo].cells[5].innerHTML = document.getElementById("SelectMobileProvider").value;
        table.rows[intRowNo].cells[6].innerHTML = document.getElementById("SelectTypeOfSim").value;

        document.getElementById("hidEdit").value = "0";
        document.getElementById("btnAddHubDetails").innerHTML = "Add Hub Details";

        var intHubId = document.getElementById("hidHubId").value;
        var intSiteId = document.getElementById("ContentPlaceHolder1_strtxtSiteId").value;
        var strPhoneNumber = document.getElementById("txtPhoneNumber").value;
        var strHubId = document.getElementById("txtHubId").value;

        //--- Ragu 21 May 2018
        var strSelectMobileProvider = document.getElementById("SelectMobileProvider").value;
        var strSelectTypeOfSim = document.getElementById("SelectTypeOfSim").value;
        //var strtxtNOfSensor = document.getElementById("txtNOfSensor").value;
        var strtxtHubOnTime = document.getElementById("txtHubOnTime").value;
        var strtxtHubOffTime = document.getElementById("txtHubOffTime").value;
        //--- Ragu 21 May 2018

        var xmlhttp = new XMLHttpRequest();
        //var params = "hubid=" + intHubId + "&siteid=" + intSiteId + "&shubid=" + strHubId + "&phonenumber=" + strPhoneNumber;
        //Ragu Changeds 21 May 2018
        var params = "hubid=" + intHubId + "&siteid=" + intSiteId + "&shubid=" + strHubId + "&phonenumber=" + strPhoneNumber + "&mobileprovider=" + strSelectMobileProvider + "&selectTypeofsim=" + strSelectTypeOfSim + "&txthubonTime=" + strtxtHubOnTime + "&txthuboffTime=" + strtxtHubOffTime;
        xmlhttp.open("GET", "/services/site.aspx?id=5&" + params, false);

        xmlhttp.send();
        var intReturnValue = xmlhttp.responseText;
        document.getElementById("txtHubId").value = "";
        document.getElementById("txtPhoneNumber").value = "";
        document.getElementById("txtHubOnTime").value = "00:01";
        document.getElementById("txtHubOffTime").value = "23:59";

        document.getElementById("SelectMobileProvider").value = "Singtel";
        document.getElementById("SelectTypeOfSim").value = "M2M";

    }
    else
    {
        for (var i = 0; i < table.rows.length; i++)
        {

            // FIX THIS
            var row = 0;

            var strHubId = (table.rows[i].cells[1].innerHTML);
            var strHubPhoneNumber = (table.rows[i].cells[2].innerHTML);

            /*var strHubOnTime = (table.rows[i].cells[3].innerHTML);
            var strHubOffTime = (table.rows[i].cells[4].innerHTML);
            var strSelectMobileProvider = (table.rows[i].cells[5].innerHTML);
            var strSelectTypeOfSim = (table.rows[i].cells[6].innerHTML);*/

           
            if (strHubId == document.getElementById("txtHubId").value) {
                alert("Hub Id already defined for this site");
                return false;
            }

            if (strHubPhoneNumber == document.getElementById("txtPhoneNumber").value) {
                alert("Hub Id already defined for this site");
                return false;
            }

            /*document.getElementById("txtHubId").value = strHubId;
            document.getElementById("txtPhoneNumber").value = strHubPhoneNumber;

            document.getElementById("txtHubOnTime").value = strHubOnTime;
            document.getElementById("txtHubOffTime").value = strHubOffTime;
            document.getElementById("SelectMobileProvider").value = strSelectMobileProvider;
            document.getElementById("SelectTypeOfSim").value = strSelectTypeOfSim;*/
        }


        var row = table.insertRow(table.rows.length);
        var intRowCount = table.rows.length - 1;

        // Insert new cells (<td> elements) at the 1st and 2nd position of the "new" <tr> element:
        var cellSlNo = row.insertCell(0);
        var cellHubNo = row.insertCell(1);
        var cellHubPhoneNo = row.insertCell(2);

        var cellhubOnTime = row.insertCell(3);
        var cellhubOffTime = row.insertCell(4);

        var cellMobileProvider = row.insertCell(5);
        var cellpaymentType = row.insertCell(6);

        var cellRemove = row.insertCell(7);

        //var cellRemove = row.insertCell(3);

        row.id = intRowCount.toString();
        // Add some text to the new cells:
        cellSlNo.innerHTML = intRowCount.toString();
        cellHubNo.innerHTML = document.getElementById("txtHubId").value;
        cellHubPhoneNo.innerHTML = document.getElementById("txtPhoneNumber").value;

        cellhubOnTime.innerHTML = document.getElementById("txtHubOnTime").value;
        cellhubOffTime.innerHTML = document.getElementById("txtHubOffTime").value;
        cellMobileProvider.innerHTML = document.getElementById("SelectMobileProvider").value;
        cellpaymentType.innerHTML = document.getElementById("SelectTypeOfSim").value;
        
        cellRemove.innerHTML = "<i class='fa fa-edit' onclick='fnEditRowHub(" + eval(intRowCount) + ")'></i>&nbsp;<i class='fa fa-minus-square' onclick='fnDeleteRow(" + intRowCount.toString() + ",0)'></i>";
        document.getElementById("txtHubId").value = "";
        document.getElementById("txtPhoneNumber").value = "";
        document.getElementById("txtHubOnTime").value = "00:01";
        document.getElementById("txtHubOffTime").value = "23:59";

        document.getElementById("SelectMobileProvider").value = "Singtel";
        document.getElementById("SelectTypeOfSim").value = "M2M";
    }
}

function chkLicenseHub() {  // Ragu 13 Jun 2019
    //window.alert(m_sensorID);

    var intOrganizationId = document.getElementById("ContentPlaceHolder1_strcmbOrganization").value;
    if ((intOrganizationId == "") || (intOrganizationId == "0")) {
        intOrganizationId = document.getElementById("ContentPlaceHolder1_cmbOrganization").value;
        document.getElementById("ContentPlaceHolder1_strcmbOrganization").value = document.getElementById("ContentPlaceHolder1_cmbOrganization").value;
    }
    var m_hubSerialNumber = document.getElementById("txtPhoneNumber").value;

    var intReturnValue;

    var hubValid = 0;
    $.ajax({
        type: 'GET',
        async: false,
        url: '/services/sensor.aspx?id=15&organizationid=' + intOrganizationId + '&hubserialnumber=' + m_hubSerialNumber,
        success: function (data) {
            hubValid = data;
        }
    });

    return hubValid;
   
}

function chkLicenseSensor() {  // Ragu 13 Jun 2019
    //window.alert(m_sensorID);

    var intOrganizationId = document.getElementById("ContentPlaceHolder1_hidOrgId").value;
    var strsensorQRNumber = document.getElementById('txtQRScanId').value;

    var intReturnValue;

    var sensorValid = 0;
    $.ajax({
        type: 'GET',
        async: false,
        url: '/services/sensor.aspx?id=16&organizationid=' + intOrganizationId + '&sensorqrnumber=' + strsensorQRNumber,
        success: function (data) {
            sensorValid = data;
        }
    });
    return sensorValid;
}
 /*
function fnConfigureSensors() {

    var table = document.getElementById("ContentPlaceHolder1_tblHubDetails").getElementsByTagName('tbody')[0];

    //Check if HubId already exists

    if (CheckEmpty(document.getElementById("txtHubId"), document.getElementById("lblHubIdError"), "  Error - Hub Id cannot be empty") == false) {
        return false
    }

    if (CheckEmpty(document.getElementById("txtPhoneNumber"), document.getElementById("lblHubPhoneNumberError"), "  Error - Hub Phone Number cannot be empty") == false) {
        return false
    }

    if (parseInt(document.getElementById("hidEdit").value) > 0) {
        var intRowNo = document.getElementById("hidEdit").value;
        table.rows[intRowNo].cells[1].innerHTML = document.getElementById("txtHubId").value;
        table.rows[intRowNo].cells[2].innerHTML = document.getElementById("txtPhoneNumber").value;
        document.getElementById("hidEdit").value = "0";
        document.getElementById("btnAddHubDetails").innerHTML = "Add Hub Details";

        var intHubId = document.getElementById("hidHubId").value;
        var intSiteId = document.getElementById("ContentPlaceHolder1_txtSiteId").value;
        var strPhoneNumber = document.getElementById("txtPhoneNumber").value;
        var strHubId = document.getElementById("txtHubId").value;

        var xmlhttp = new XMLHttpRequest();
        var params = "hubid=" + intHubId + "&siteid=" + intSiteId + "&shubid=" + strHubId + "&phonenumber=" + strPhoneNumber;
        xmlhttp.open("GET", "/services/site.aspx?id=5&" + params, false);

        xmlhttp.send();
        var intReturnValue = xmlhttp.responseText;
        document.getElementById("txtHubId").value = "";
        document.getElementById("txtPhoneNumber").value = "";


    }
    else {
        for (var i = 0; i < table.rows.length; i++) {

            // FIX THIS
            var row = 0;

            var strHubId = (table.rows[i].cells[1].innerHTML);
            var strHubPhoneNumber = (table.rows[i].cells[2].innerHTML);

            if (strHubId == document.getElementById("txtHubId").value) {
                alert("Hub Id already defined for this site");
                return false;
            }

            if (strHubPhoneNumber == document.getElementById("txtPhoneNumber").value) {
                alert("Hub Id already defined for this site");
                return false;
            }

        }


        var row = table.insertRow(table.rows.length);
        var intRowCount = table.rows.length - 1;

        // Insert new cells (<td> elements) at the 1st and 2nd position of the "new" <tr> element:
        var cellSlNo = row.insertCell(0);
        var cellHubNo = row.insertCell(1);
        var cellHubPhoneNo = row.insertCell(2);
        var cellRemove = row.insertCell(3);

        row.id = intRowCount.toString();
        // Add some text to the new cells:
        cellSlNo.innerHTML = intRowCount.toString();
        cellHubNo.innerHTML = document.getElementById("txtHubId").value;
        cellHubPhoneNo.innerHTML = document.getElementById("txtPhoneNumber").value;
        cellRemove.innerHTML = "<i class='fa fa-edit' onclick='fnEditRowHub(" + eval(intRowCount) + ")'></i>&nbsp;<i class='fa fa-minus-square' onclick='fnDeleteRow(" + intRowCount.toString() + ",0)'></i>";
        document.getElementById("txtHubId").value = "";
        document.getElementById("txtPhoneNumber").value = "";
    }
}
*/

function fnConfigureSensors() {
    //save the site information
    if (CheckEmpty(document.getElementById("ContentPlaceHolder1_txtSiteName"), document.getElementById("spSiteNameError"), "  Error - Site Name cannot be empty") == false) {
        return false
    }
    if (CheckEmpty(document.getElementById("ContentPlaceHolder1_txtLatitude"), document.getElementById("spSiteLatitude"), "  Error - Latitude cannot be empty") == false) {
        return false
    }
    if (CheckEmpty(document.getElementById("ContentPlaceHolder1_txtLongitude"), document.getElementById("spSiteLongitude"), "  Error - Longitude cannot be empty") == false) {
        return false
    }
    if (CheckEmpty(document.getElementById("ContentPlaceHolder1_txtSiteAddress"), document.getElementById("spSiteAddress"), "  Error - Site Address cannot be empty") == false) {
        return false
    }
   

    var intSiteId = document.getElementById("ContentPlaceHolder1_strtxtSiteId").value;

    var table = document.getElementById("ContentPlaceHolder1_tblHubDetails").getElementsByTagName('tbody')[0];

    var strHubId = "";
    var strHubPhoneNumber = "";
    var intHubIds = "";
    var strHubOnTime = "";
    var strHubOffTime = "";
    var strMobileProvider = "";
    var strPaymentType = "";


    for (var i = 1; i < table.rows.length; i++) {

        row = table.rows[i];

        strHubId = strHubId + row.cells[1].firstChild.nodeValue + ",";
        strHubPhoneNumber = strHubPhoneNumber + row.cells[2].firstChild.nodeValue + ",";

        strHubOnTime = strHubOnTime + row.cells[3].firstChild.nodeValue + ",";
        strHubOffTime = strHubOffTime + row.cells[4].firstChild.nodeValue + ",";
        strMobileProvider = strMobileProvider + row.cells[5].firstChild.nodeValue + ",";
        strPaymentType = strPaymentType + row.cells[6].firstChild.nodeValue + ",";
        
        //intHubIds = intHubIds + row.cells[4].firstChild.nodeValue + ",";
    }

    var strImageName = "";

    var xmlhttp = new XMLHttpRequest();
    var params = "siteid=" + intSiteId + "&sitename=" + document.getElementById("ContentPlaceHolder1_txtSiteName").value + "&lat=" + document.getElementById("ContentPlaceHolder1_txtLatitude").value + "&lon=" + document.getElementById("ContentPlaceHolder1_txtLongitude").value + "&address=" + document.getElementById("ContentPlaceHolder1_txtSiteAddress").value + "&deploymenttechnician=" + document.getElementById("ContentPlaceHolder1_cmbDeploymentTechnicianName").value + "&alerttechnician=" + document.getElementById("ContentPlaceHolder1_cmbAlertTechnicianName").value + "&organization=" + document.getElementById("ContentPlaceHolder1_strcmbOrganization").value + "&status=" + document.getElementById("ContentPlaceHolder1_cmbStatus").value + "&hubid=" + strHubId + "&hubphonenumber=" + strHubPhoneNumber + "&imagename=" + strImageName + "&hubontime" + strHubOnTime + "&hubofftime" + strHubOffTime + "&mobileprovider" + strMobileProvider + "&paymenttype" + strPaymentType;


    var strType = getParameterByName("type");

 


    if (strType == "e") {
        $.ajax({
            url: "/services/site.aspx?id=2",
            type: "POST",
            data: {
                type: "e", siteid: intSiteId, sitename: document.getElementById("ContentPlaceHolder1_txtSiteName").value, lat: document.getElementById("ContentPlaceHolder1_txtLatitude").value, lon: document.getElementById("ContentPlaceHolder1_txtLongitude").value, address: document.getElementById("ContentPlaceHolder1_txtSiteAddress").value, deploymenttechnician: document.getElementById("ContentPlaceHolder1_cmbDeploymentTechnicianName").value, alerttechnician: document.getElementById("ContentPlaceHolder1_cmbAlertTechnicianName").value, organization: document.getElementById("ContentPlaceHolder1_cmbOrganization").value, status: document.getElementById("ContentPlaceHolder1_cmbStatus").value, hubid: strHubId, hubphonenumber: strHubPhoneNumber, imagename: strImageName , hubontime:strHubOnTime, hubofftime:strHubOffTime, mobileprovider:strMobileProvider, paymenttype:strPaymentType},
            dataType: "json",
            success: function (result) {
                
                location.href = "SensorManagement.aspx?id=" + intSiteId + "&organizationid=" + document.getElementById("ContentPlaceHolder1_strcmbOrganization").value
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //alert(xhr.status);
                // alert(thrownError);
            }
        });
    }
    else {
        $.ajax({
            url: "/services/site.aspx?id=1",
            type: "POST",
            data: { id: 1, siteid: intSiteId, sitename: document.getElementById("ContentPlaceHolder1_txtSiteName").value, lat: document.getElementById("ContentPlaceHolder1_txtLatitude").value, lon: document.getElementById("ContentPlaceHolder1_txtLongitude").value, address: document.getElementById("ContentPlaceHolder1_txtSiteAddress").value, deploymenttechnician: document.getElementById("ContentPlaceHolder1_cmbDeploymentTechnicianName").value, alerttechnician: document.getElementById("ContentPlaceHolder1_cmbAlertTechnicianName").value, organization: document.getElementById("ContentPlaceHolder1_cmbOrganization").value, status: document.getElementById("ContentPlaceHolder1_cmbStatus").value, hubid: strHubId, hubphonenumber: strHubPhoneNumber, imagename: strImageName, hubontime: strHubOnTime, hubofftime: strHubOffTime, mobileprovider: strMobileProvider, paymenttype: strPaymentType },
            dataType: "json",
            success: function (result) {
                location.href = "SensorManagement.aspx?id=" + intSiteId + "&organizationid=" + document.getElementById("ContentPlaceHolder1_strcmbOrganization").value
            },
            error: function (xhr, ajaxOptions, thrownError) {
                // alert(xhr.status);
                // alert(thrownError);
            }
        });
    }


    // xmlhttp.send();
    // var intReturnValue = xmlhttp.responseText;

    //save the hub information

    // 


}

function fnShowBuildingMap() {
    var strBuildingFloorMap = document.getElementById("ContentPlaceHolder1_hidUploadFileName").value;

}

// end sites_ui.aspx //

function getMouseXY(e) {

    
    if (document.getElementById("ContentPlaceHolder1_txtFloorMapImageName").value == "") {
        document.getElementById("spError").innerHTML = "<font color='red'>Enter and  Save the Location before adding Sensors</font>"
        return false;
    }
    
  
    var e = (!e) ? window.event : e;
    if (e.pageX || e.pageY) {
        //posX = e.pageX;
        //posY = e.pageY;
        posX = e.offsetX;
        posY = e.offsetY;
    }
    else if (e.clientX || e.clientY) {
        if (document.body.scrollLeft || document.body.scrollTop) {
            posX = e.clientX + document.body.scrollLeft;
            posY = e.clientY + document.body.scrollTop;
        }
        else {
            posX = e.clientX + document.documentElement.scrollLeft;
            posY = e.clientY + document.documentElement.scrollTop;
        }
    }

   /* if (e.clientX || e.screenX)
    {
        posX = posX + (e.offsetX - e.clientX);
    }

    if (e.clientY || e.screenY) {
        posY = posY + (e.clientY - e.offsetY);
    }*/

     
    var img = new Image(27, 18); // width, height values are optional params
    img.id = "imgMarker";
    img.src = '/openlayers/img/0.gif';
    img.style.position = "absolute";

    //Old Concept Need Adjustment
    //img.style.left = (posX + 5) + "px";
    //img.style.top = (posY - 200) + "px";
    img.style.left = posX + "px";
    img.style.top = posY + "px";

    var isMobile = {
        Android: function () {
            return navigator.userAgent.match(/Android/i);
        },
        BlackBerry: function () {
            return navigator.userAgent.match(/BlackBerry/i);
        },
        iOS: function () {
            return navigator.userAgent.match(/iPhone|iPad|iPod/i);
        },
        Opera: function () {
            return navigator.userAgent.match(/Opera Mini/i);
        },
        Windows: function () {
            return navigator.userAgent.match(/IEMobile/i) || navigator.userAgent.match(/WPDesktop/i);
        },
        any: function () {
            return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
        }
    };

    var element = document.getElementById('text');
        
    if (document.body.clientWidth <= screen.width / 2)
    {
        /* Ragu Hide 20 Dec 19 Due to Tablet screen Issue */
        //img.style.left = (posX + 5) + "px";
        //img.style.top = (posY - 430) + "px";
    } 
    else if (isMobile.any())
    {
        //alert("mobile");
        //img.style.left = (posX + document.body.scrollLeft + 5) + "px";
        //img.style.top = (posY - 430) + "px";
    }

    //img.style.left = (posX + 15) + "px";
    //img.style.top = (posY - 225) + "px";

    img.addEventListener("click", function () {
        alert("clicked");
    });
    document.getElementById('ContentPlaceHolder1_image_panel').appendChild(img);

    
    //document.getElementById("txtX").value = img.style.left;
    //document.getElementById("txtY").value = img.style.top;   
    if (isMobile.any()) {
        //alert("mobile");
        //document.getElementById("txtX").value = (posX - 20) + "px";  // pos -10
        //document.getElementById("txtY").value = (posY - 203) + "px";
    }
    else if (document.body.clientWidth <= screen.width / 2)
    {
        /* Ragu Hide 20 Dec 19 Due to Tablet screen Issue */
        //document.getElementById("txtX").value = (posX + document.body.scrollLeft - 20) + "px";  // pos -10
        //document.getElementById("txtY").value = (posY - 203) + "px";
    } 
    else {
        //document.getElementById("txtX").value = (posX - 20) + "px"; //0
        //document.getElementById("txtY").value = (posY + 25) + "px";
    }

    //nEW lAYOUT
    document.getElementById("txtX").value = (posX - 25) + "px"; //0
    document.getElementById("txtY").value = (posY + 225) + "px";
    

    document.getElementById("btnDelete").style.visibility = 'hidden';


    document.getElementById("txtSensorName").value="";
    document.getElementById("txtMin").value = "0";
    document.getElementById("txtMax").value = "2";
    document.getElementById("txtSensorLocation").value = "";
    


    $('#modalSensorSettings').modal();
    

    /*
    document.getElementById("txtSensorName").disabled = false;
    // document.getElementById("cmbRange").disabled = false;
    document.getElementById("txtUnit").disabled = false;
    document.getElementById("txtX").value = img.style.left;
    document.getElementById("txtY").value = img.style.top;

    document.getElementById("txtMin").disabled = false;
    document.getElementById("txtMax").disabled = false;
    */

}
function fnDeleteSensor() {
    var intSensorId = document.getElementById("hidSensorName").value;
    var xmlhttp = new XMLHttpRequest();

    xmlhttp.open("GET", "/services/sensor.aspx?id=2&sensorid=" + intSensorId.toString(), false);
    xmlhttp.send();

    var intReturnValue = xmlhttp.responseText;

    document.location.reload();

}
function fnGetSensorData(intSensorId)
{
    
    var xmlhttp = new XMLHttpRequest();

    xmlhttp.open("GET", "/services/sensor.aspx?id=3&sensorid=" + intSensorId.toString(), false);
    xmlhttp.send();

    var strReturnValue = xmlhttp.responseText;
    var obj = JSON.parse(strReturnValue);

    document.getElementById("txtSensorName").value = obj[0].s_SensorId;
    document.getElementById("txtMin").value = obj[0].i_MinThresholdLevel;
    document.getElementById("txtMax").value = obj[0].i_MaxThresholdLevel;
    document.getElementById("txtX").value = obj[0].i_X;
    document.getElementById("txtY").value = obj[0].i_Y;
    document.getElementById("txtSensorLocation").value = obj[0].s_SensorLocation;
    document.getElementById("hidSensorName").value = obj[0].i_SensorId;

    // Ragu 11 Sep 2017
    document.getElementById("ContentPlaceHolder1_cmbHubId").value = obj[0].i_HubId;

    // Ragu 27 Apr 2018
    document.getElementById("ContentPlaceHolder1_cmbSensorType").value = obj[0].s_SensorType;

    //Ragu 2 AUg 19
    document.getElementById("txtQRScanId").value = obj[0].s_qrCode;

    //

    document.getElementById("btnDelete").style.visibility = 'visible';
    $('#modalSensorSettings').modal();
}
function fnAddSensorDetails() {

    //  var table = document.getElementById("ContentPlaceHolder1_tblSensors").getElementsByTagName('tbody')[0];

    //Check if HubId already exists

    /*if (CheckEmpty(document.getElementById("txtMin")))
    {
        document.getElementById("txtMin").value = "0";
    }
    if (CheckEmpty(document.getElementById("txtMax")))
    {
        document.getElementById("txtMax").value = "2";
    }*/

    if (CheckEmpty(document.getElementById("txtSensorName"), document.getElementById("spSensorName"), "  Error - Sensor Id cannot be empty") == false) {
        return false
    }
    if (CheckEmpty(document.getElementById("txtSensorLocation"), document.getElementById("spSensorLocation"), "  Error - Sensor Location cannot be empty") == false) {
        return false
    }

    if (CheckEmpty(document.getElementById("txtMin"), document.getElementById("spMin"), "  Error - Minimum Threshold Level cannot be empty") == false) {
        return false
    }

    if (CheckEmpty(document.getElementById("txtMax"), document.getElementById("spMax"), "  Error - Maximum Threshold Level cannot be empty") == false) {
        return false
    }

    if (CheckEmpty(document.getElementById("txtQRScanId"), document.getElementById("spSensorQRId"), "  Error - QR Number cannot be Empty") == false) {
        return false
    }

    var sensorValid = chkLicenseSensor(); // Ragu 13 Jun 2019

    if (document.getElementById("ContentPlaceHolder1_cmbSensorType").value == "Trap without Sensor") {
        sensorValid = 1;
    }
    if (document.getElementById("ContentPlaceHolder1_cmbSensorType").value == "Hunting Camera") {
        sensorValid = 1;
    }
    if (document.getElementById("ContentPlaceHolder1_cmbSensorType").value == "Bait Station") {
        sensorValid = 1;
    }
    if (sensorValid == 0) {
        alert("Sensor QR Number not registered. Please contact Administrator");
        lastUpdSensorNumber_minusOne();
        $('#modalSensorSettings').hide();
        document.getElementById('txtQRScanId').value = 0;
        return false;
    }

    var intFloorMapId = document.getElementById("ContentPlaceHolder1_cmbFloorMapImage").value;
    document.getElementById("ContentPlaceHolder1_strcmbFloorMapImage").value = intFloorMapId;
   

    var min = parseInt(document.getElementById("txtMin").value);
    var max = parseInt(document.getElementById("txtMax").value);

    if (min >= max) {
        document.getElementById("spMin").innerHTML = "<font color='red'> Minimum Threshold should be less than Maximum Threshold</font>";
        return false;
    }


    //for (var i = 0; i < table.rows.length; i++) {

    //    // FIX THIS
    //    var row = 0;

    //    var strHubId = (table.rows[i].cells[1].innerHTML);
    //    var strSensorId = (table.rows[i].cells[2].innerHTML);

    //    strHubId = strHubId.replace("<span>", "");
    //    strHubId = strHubId.replace("</span>", "");
    //    strSensorId = strSensorId.replace("<span>", "");
    //    strSensorId = strSensorId.replace("</span>", "");

    //    var strSelectedHubId = document.getElementById('ContentPlaceHolder1_cmbHubId');

    //    var intSensorId = document.getElementById("hidSensorId").value;
    //    if (intSensorId == "") {
    //        if (strHubId == strSelectedHubId.options[strSelectedHubId.selectedIndex].innerHTML) {
    //            if (strSensorId == document.getElementById("txtSensorId").value) {
    //                document.getElementById("spSensorId").innerHTML = "<font color='red'> Error - Sensor already defined</font>";
    //                return false;
    //            }

    //            //        return false;
    //        }
    //    }

    //}
    //-----


    var intSiteId = getParameterByName("id");
    var intSensorId = document.getElementById("hidSensorName").value;
    if (intSensorId == "")
    {
        var intSensorId = 0;
    }

    var status = document.getElementById("ContentPlaceHolder1_ddStatus").value;

    var xmlhttp = new XMLHttpRequest();
    var params = "sensorid=" + intSensorId + "&siteid=" + intSiteId + "&hubid=" + document.getElementById("ContentPlaceHolder1_cmbHubId").value + "&sensorname=" + document.getElementById("txtSensorName").value + "&sensortype=" + document.getElementById("ContentPlaceHolder1_cmbSensorType").value +
        "&min=" + min + "&max=" + max +
        "&th_x=" + document.getElementById("txtMin").value + "&th_y=" + document.getElementById("txtMax").value + "&unit=0&status=" + status + "&XPix=" + document.getElementById("txtX").value + "&YPix=" + document.getElementById("txtY").value + "&floorid=" + intFloorMapId + "&sensorlocation=" + document.getElementById("txtSensorLocation").value + "&qrCode=" + document.getElementById("txtQRScanId").value;

   
    xmlhttp.open("GET", "/services/sensor.aspx?id=1&" + params, false);
    xmlhttp.send();

    var intReturnValue = xmlhttp.responseText;



    if (intReturnValue == "0") {
        document.getElementById("spSensorId").innerHTML = '<font color=red> Error - Sensor already defined</font>';
        return false;
    }

    else {
        document.getElementById("hidLastHubId").value = document.getElementById("ContentPlaceHolder1_cmbHubId").value;
        document.getElementById("hidLastSensorId").value = document.getElementById("txtSensorName").value;
        alert("-sensor is saved");
    }

    document.getElementById("txtSensorName").value = "";
    document.getElementById("txtMin").value = "0";
    document.getElementById("txtMax").value = "2";
   
    document.getElementById("txtX").value = "";
    document.getElementById("txtY").value = "";

    document.location.reload();

}
function fnEditRowSensors(intRowNo) {
    var xmlhttp = new XMLHttpRequest();

    xmlhttp.open("GET", "/services/sensor.aspx?id=3&sensorid=" + intRowNo.toString(), false);
    xmlhttp.send();

    var strReturnValue = xmlhttp.responseText;
    var obj = JSON.parse(strReturnValue);

    document.getElementById("txtSensorName").value = obj[0].s_SensorId;
    document.getElementById("txtMin").value = obj[0].i_MinThresholdLevel;
    document.getElementById("txtMax").value = obj[0].i_MaxThresholdLevel;
    document.getElementById("txtX").value = obj[0].i_X;
    document.getElementById("txtY").value = obj[0].i_Y;
    document.getElementById("hidSensorName").value = obj[0].i_SensorId;

    //Ragu 30 Apr 2018
    document.getElementById("ContentPlaceHolder1_cmbSensorType").value = obj[0].s_SensorType;
    //----

    //Ragu 2 Aug 19
    document.getElementById("ContentPlaceHolder1_txtQRScanId").value = obj[0].s_qrCode;
    

    document.getElementById("btnAddSensor").innerHTML = "Update Sensor";



}
function fnDeleteRowSensors(intRowNo) {
    var xmlhttp = new XMLHttpRequest();

    xmlhttp.open("GET", "/services/sensor.aspx?id=2&sensorid=" + intRowNo.toString(), false);
    xmlhttp.send();

    var intReturnValue = xmlhttp.responseText;

    document.location.reload();

}

function fnDownloadDashboard()
{
    var intSiteId = document.getElementById("ContentPlaceHolder1_cmbSiteName").value;
    var e = document.getElementById("ContentPlaceHolder1_cmbLocation");
    var intFloorMapId = e.options[e.selectedIndex].id;
    //var intFloorMapId = document.getElementById("ContentPlaceHolder1_strcmbLocation");

    if (intFloorMapId == "0") {
        alert("Select the Location Name");
        return false;
    }

    var dtStartDate = document.getElementById("hidStartDate").value;
    var dtEndDate = document.getElementById("hidEndDate").value;

    $.ajax({
        type: 'GET',
        url: '/services/dashboard.aspx?id=8&siteid=' + intSiteId + "&floormapid=" + intFloorMapId + '&startdate=' + dtStartDate + '&enddate=' + dtEndDate,
        success: function (data) {
            document.getElementById("btnDownloadDashboard").style.visibility = "visible";
           
            JSONToCSVConvertor(data, "Export Data", true);

        }
    });


}

function fnGetDashboardDataForSite(strSiteId) {

    if (typeof strSiteId == 'undefined') {
        return false;
    }


    var intSiteId = document.getElementById("ContentPlaceHolder1_cmbSiteName").value;
    document.getElementById("ContentPlaceHolder1_cmbSiteName").selectedIndex = intSiteId;
    if (intSiteId <= 0) {
        intSiteId = strSiteId;
    }
   
    //Ragu 22 Jun 2017
    var e = document.getElementById("ContentPlaceHolder1_cmbLocation");
    var intFloorMapId = e.options[e.selectedIndex].id;
    
    //Ragu 02 Aug 17
    if (intFloorMapId == 0) {
        if (intSiteId == 0 || intSiteId== "") {
            return false;
        }
        document.getElementById("ContentPlaceHolder1_cmbLocation").selectedIndex = "1";
        var e = document.getElementById("ContentPlaceHolder1_cmbLocation");
        var intFloorMapId = e.options[e.selectedIndex].id;
    }
    //-----


    //var e = document.getElementById("ContentPlaceHolder1_cmbLocation").value;
    //var intFloorMapId = e.options[e.selectedIndex].id;
    //var intFloorMapId = e;
    if (intFloorMapId < 0 || intFloorMapId == null || intFloorMapId == "") {
        //var e = document.getElementById("ContentPlaceHolder1_cmbLocation").value;
        //intFloorMapId = e.options[e.selectedIndex].id;
        if (intSiteId == "0" || intSiteId == "") {
            return false;
        }
        document.getElementById("ContentPlaceHolder1_cmbLocation").selectedIndex = "1";
        var e = document.getElementById("ContentPlaceHolder1_cmbLocation");
        var intFloorMapId = e.options[e.selectedIndex].id;
    }

    if (intFloorMapId == "0") {
        alert("Select the Location Name");
        return false;
    }

    var strFloorMap = "";
    $.ajax({
        type: 'GET',
        url: '/services/dashboard.aspx?id=7&floormapid=' + intFloorMapId,
        success: function (data) {
            strFloorMap = data;
            document.getElementById("ContentPlaceHolder1_imgBuildingPlan").src = "/plans/" + strFloorMap;
            document.getElementById("ContentPlaceHolder1_imgBuildingPlan").style.visibility = "visible";
            //document.getElementById("ContentPlaceHolder1_spMessage").innerHTML = "";
        }
    });



    arrSensorDetails = '';

    document.getElementById("ContentPlaceHolder1_cmbSiteName").value = intSiteId;

    /*if (strSiteId != "0") {
        document.getElementById("ContentPlaceHolder1_cmbSiteName").value = strSiteId;
        intSiteId = strSiteId;
    }
    else {
        document.getElementById("ContentPlaceHolder1_cmbSiteName").value = intSiteId;
    }
    */

    $(document).ready(function () {


        var dtStartDate = document.getElementById("hidStartDate").value;
        var dtEndDate = document.getElementById("hidEndDate").value;

        //Ragu 22 Jun 2016
        var tmp_date = new Date();

        var tmp_dtEnd = moment().format("YYYY-MM-DD 23:59");
        //tmp_date.setDate(tmp_date.getDate() - 1);
        var tmp_dtStart = moment().subtract(1, 'days').format("YYYY-MM-DD 00:00");

        if ((dtStartDate == "") && (dtEndDate == "")) {
            document.getElementById("hidStartDate").value = tmp_dtStart;
            document.getElementById("hidEndDate").value = tmp_dtEnd;
            dtStartDate = tmp_dtStart;
            dtEndDate = tmp_dtEnd;

            document.getElementById("spDateRange").innerHTML = " " + tmp_dtStart + '  to  ' + tmp_dtEnd + "";
            $('#reportrange span').html(tmp_dtStart + ' - ' + tmp_dtEnd);
        }
        //-------


        $.ajax({
            type: 'GET',
            url: '/services/dashboard.aspx?id=1&siteid=' + intSiteId + "&floormapid=" + intFloorMapId + '&startdate=' + dtStartDate + '&enddate=' + dtEndDate,
            success: function (data) {
                document.getElementById("btnDownloadDashboard").style.visibility = "visible";
                var sensor_data = JSON.parse(data);

                var element = document.getElementsByName("sensor_image");
                for (index = element.length - 1; index >= 0; index--) {
                    element[index].parentNode.removeChild(element[index]);
                }

                $('label').remove();
                $('line').remove();
                $('div.line').remove();
                $('div.arrow').remove();
                $('div.newarrow').remove();

                var intLblRE = 0, intLblST = 0, intLblGB = 0, intLblCG = 0, intLblSMT = 0, intLblTrap = 0, intLblBait = 0, intLblNoSensor = 0, intLblCamera = 0, intLblBaitStation = 0, intLblSRSBaitStation=0;
                var intLblTriggerCount = 0, intLblCatchCount = 0, varAliveCount = 0, varLiveCount = 0, varLowCount = 0;
                var dthubLastTrigger = "";

                //var batMaxTrigger = document.getElementById("BatteryMaxTrigger").value;
                var batMaxTrigger = 28000;
                var todayDate = new Date();


                for (var i = 0; i < sensor_data.length; i++) {
                    var sensorid = sensor_data[i].i_SensorId;
                    var min = sensor_data[i].i_MinThresholdLevel;
                    var max = sensor_data[i].i_MaxThresholdLevel;
                    var status = sensor_data[i].b_Status;
                    var intSensorCount = sensor_data[i].i_SensorCount;
                    var SensorType = sensor_data[i].s_SensorType;

                    var qrCode = sensor_data[i].s_qrCode;

                    var intSensorCount_CurrentDay = sensor_data[i].i_SensorCount_currentday;   //Ragu 06 Aug 20 - Sensor Red Color Based on Last 24 Hour

                    if (i == 0)
                        dthubLastTrigger = sensor_data[0].hubLastTrigger;

                    //Remove Other Type of Sensor Exelu RE
                    if ((SensorType != "RE*") && (document.getElementById("chkViewSensorOnly").checked == true)) {
                        continue;
                    }


                    // Ragu 15 May Remove Yellow color - If less than 3 Display 0
                    //if (intSensorCount == 0) {
                    //    status = "";
                    //}
                    //if(intSensorCount<3)
                    //{
                    //    intSensorCount = 0;
                    //}

                    if (intSensorCount_CurrentDay >= max) { //Max Threshold
                        status = 2;
                    }
                    else {
                        status = 0;
                    }


                    //------
                    var img = new Image(27, 18);

                    img.name = "sensor_image";
                    img.id = sensorid + "_" + min + "_" + max;

                    //Ragu 27 Aug - View Rod Caught Only Red- Others Green
                    if (document.getElementById("chkViewRodent").checked == true) {
                        if (sensor_data[i].i_rodCount >= 1) {
                            status = 2;
                        }
                        else {
                            status = 0;
                        }

                    }


                    //Ragu 01 May 2018
                    img.src = '/openlayers/img/0.gif';
                    if (status == "") {
                        if ((SensorType == 0) || (SensorType == "RE*"))
                            img.src = '/openlayers/img/0.gif';
                        else if (SensorType == "SRS-ST")
                            img.src = '/openlayers/img/0_Cir_Orange.png';
                        else if (SensorType == "SRS-GB")
                            img.src = '/openlayers/img/0_Cir.png';
                        else if (SensorType == "SRS-CG")
                            img.src = '/openlayers/img/0_Cir_Black.png';
                        else if (SensorType == "SMT")
                            img.src = '/openlayers/img/0_tri.png';
                        else if (SensorType == "SRS-Trap")
                            img.src = '/openlayers/img/0_tri.png';
                        else if (SensorType == "SRS-Bait Station")
                            img.src = '/openlayers/img/0_Bait.jpg';
                        else if (SensorType == "Trap without Sensor")
                            img.src = '/openlayers/img/04_Poly_Blue.png';
                        else if (SensorType == "Hunting Camera")
                            img.src = '/openlayers/img/0_Camera.png';
                        else if (SensorType == "Bait Station")
                            img.src = '/openlayers/img/0_Bait.png';


                    }
                    if (status == "0") {
                        if ((SensorType == 0) || (SensorType == "RE*"))
                            img.src = '/openlayers/img/0.gif';
                        else if (SensorType == "SRS-ST")
                            img.src = '/openlayers/img/0_Cir_Orange.png';
                        else if (SensorType == "SRS-GB")
                            img.src = '/openlayers/img/0_Cir.png';
                        else if (SensorType == "SRS-CG")
                            img.src = '/openlayers/img/0_Cir_Black.png';
                        else if (SensorType == "SMT")
                            img.src = '/openlayers/img/0_tri.png';
                        else if (SensorType == "SRS-Trap")
                            img.src = '/openlayers/img/0_tri.png';
                        else if (SensorType == "SRS-Bait Station")
                            img.src = '/openlayers/img/0_Bait.jpg';
                        else if (SensorType == "Trap without Sensor")
                            img.src = '/openlayers/img/04_Poly_Blue.png';
                        else if (SensorType == "Hunting Camera")
                            img.src = '/openlayers/img/0_Camera.png';
                        else if (SensorType == "Bait Station")
                            img.src = '/openlayers/img/0_Bait.png';
                    }
                    if (status == "1") {
                        // img.src = '/openlayers/img/1.gif';
                        status == "0"
                        //img.src = '/openlayers/img/0.gif';    // Ragu 15 May Remove Yellow color
                        if ((SensorType == 0) || (SensorType == "RE*"))
                            img.src = '/openlayers/img/0.gif';
                        else if (SensorType == "SRS-ST")
                            img.src = '/openlayers/img/0_Cir_Orange.png';
                        else if (SensorType == "SRS-GB")
                            img.src = '/openlayers/img/0_Cir.png';
                        else if (SensorType == "SRS-CG")
                            img.src = '/openlayers/img/0_Cir_Black.png';
                        else if (SensorType == "SMT")
                            img.src = '/openlayers/img/0_tri.png';
                        else if (SensorType == "SRS-Trap")
                            img.src = '/openlayers/img/0_tri.png';
                        else if (SensorType == "SRS-Bait Station")
                            img.src = '/openlayers/img/0_Bait.jpg';
                        else if (SensorType == "Trap without Sensor")
                            img.src = '/openlayers/img/04_Poly_Blue.png';
                        else if (SensorType == "Hunting Camera")
                            img.src = '/openlayers/img/0_Camera.png';
                        else if (SensorType == "Bait Station")
                            img.src = '/openlayers/img/0_Bait.png';
                    }
                    if (status == "2") {
                        //img.src = '/openlayers/img/2.gif';
                        if ((SensorType == 0) || (SensorType == "RE*"))
                            img.src = '/openlayers/img/2.gif';
                        else if (SensorType == "SRS-ST")
                            img.src = '/openlayers/img/2_Cir_Orange.png';
                        else if (SensorType == "SRS-GB")
                            img.src = '/openlayers/img/2_Cir.png';
                        else if (SensorType == "SRS-CG")
                            img.src = '/openlayers/img/2_Cir_Black.png';
                        else if (SensorType == "SMT")
                            img.src = '/openlayers/img/2_tri.png';
                        else if (SensorType == "SRS-Trap")
                            img.src = '/openlayers/img/2_tri.png';
                        else if (SensorType == "SRS-Bait Station")
                            img.src = '/openlayers/img/2_Bait.jpg';
                        else if (SensorType == "Trap without Sensor")
                            img.src = '/openlayers/img/04_Poly_Red.png';   //Red Color
                        else if (SensorType == "Hunting Camera")
                            img.src = '/openlayers/img/2_Camera.png';
                        else if (SensorType == "Bait Station")
                            img.src = '/openlayers/img/2_Bait.jpg';
                    }

                    var x = sensor_data[i].i_X;
                    var y = sensor_data[i].i_Y;
                    x = x + 25;
                    //y = y - 150;   RAGU 20 MAR 2017
                    y = y - 225;
                    img.style.position = "absolute";
                    img.style.left = x + "px";
                    img.style.top = y + "px";


                    img.addEventListener("click", function () {

                        fnGetSensorDataForDashboard(this.id, dtStartDate, dtEndDate);

                    }, false);

                    document.getElementById('ContentPlaceHolder1_image_panel').appendChild(img);

                    var newlabel = document.createElement("Label");
                    newlabel.style.position = "absolute";

                    if (document.getElementById("chkViewSensor").checked == true) {
                        newlabel.style.left = (x + 2) + "px";
                        newlabel.style.fontSize = "small";
                        newlabel.innerHTML = intSensorCount + "<font size=1 color=blue>-" + sensor_data[i].s_SensorId.replace(" Warning", "") + "</font>";
                    }
                    else if (document.getElementById("chkViewRodent").checked == true) {

                        if (sensor_data[i].i_rodCount >= 1) {
                            newlabel.style.left = (x + 2) + "px";
                            //newlabel.style.fontSize = "small";
                            if (((SensorType == 0) || (SensorType == "RE*")))
                                newlabel.innerHTML = "<b><font size=3 color=White>" + intSensorCount + "-" + sensor_data[i].i_rodCount + "</font></b>";
                            else {
                                newlabel.style.left = (x + 7) + "px";
                                newlabel.innerHTML = "<b><font size=3 color=White>" + sensor_data[i].i_rodCount + "</font></b>";
                            }
                        }
                        else {
                            newlabel.style.left = (x + 2) + "px";
                            //newlabel.style.fontSize = "small";

                            /*if (((SensorType == 0) || (SensorType == "RE*")))
                                newlabel.innerHTML = "<b><font size=3 color=black>" + intSensorCount + "-0" + "</font></b>";
                            else {
                                newlabel.style.left = (x + 6) + "px";
                                newlabel.innerHTML = "<b><font size=3 color=black>" + intSensorCount + "</font></b>";
                            }*/
                        }
                    }
                    else {
                        if (intSensorCount <= 9)
                            newlabel.style.left = (x + 8) + "px";
                        else if ((intSensorCount >= 10) && (intSensorCount <= 99))
                            newlabel.style.left = (x + 5) + "px";
                        else if (intSensorCount >= 100)
                            newlabel.style.left = (x + 2) + "px";
                        newlabel.innerHTML = intSensorCount;

                    }
                    newlabel.style.top = y + "px";
                    if (status == "2") {
                        newlabel.style.color = "#FFFFFF";
                    }

                    newlabel.id = sensorid + "_" + min + "_" + max;
                    newlabel.addEventListener("click", function () {
                        fnGetSensorDataForDashboard(this.id, dtStartDate, dtEndDate);
                    }, false);
                    document.getElementById('ContentPlaceHolder1_image_panel').appendChild(newlabel);
                    img = null;

                    //Countting the No of Sensor - 28 May 2019

                    if (SensorType == "RE*")
                        intLblRE++;
                    else if (SensorType == "SRS-ST")
                        intLblST++;
                    else if (SensorType == "SRS-GB")
                        intLblGB++;
                    else if (SensorType == "SRS-CG")
                        intLblCG++;
                    else if (SensorType == "SMT")
                        intLblSMT++;
                    else if (SensorType == "SRS-Trap")
                        intLblTrap++;
                    else if (SensorType == "SRS-Bait Station")
                        intLblBait++;
                    else if (SensorType == "Trap without Sensor")
                        intLblNoSensor++;
                    else if (SensorType == "Hunting Camera")
                        intLblCamera++;
                    else if (SensorType == "Bait Station")
                        intLblBaitStation++;
                    //else if (SensorType == "SRS-Bait Station")
                    //    intLblSRSBaitStation++;
                    //------------

                    intLblTriggerCount = intLblTriggerCount + intSensorCount;
                    intLblCatchCount = intLblCatchCount + sensor_data[i].i_rodCount;

                    //Battery Chart Update

                    //Ragu Battery StandbyTime Update

                    var installation_date = sensor_data[i].dtSensorUpdTime;
                    var t_batPerSms = sensor_data[i].actualCount;
                    var diff = sensor_data[i].date_diff;

                    //SMS Log Batter Percentage
                    var t_batPerSms = 0;
                    t_batPerSms = batMaxTrigger - t_batPerSms;
                    t_batPerSms = Math.round((t_batPerSms / batMaxTrigger) * 100, 0);
                    t_batPerSms = Math.round(t_batPerSms - (diff * 0.25), 0);

                    if (t_batPerSms <= 0)
                        t_batPerSms = 0;

                    if (t_batPerSms == 0) {
                        varAliveCount++;
                    }
                    else if (t_batPerSms >= 30) {
                        varLiveCount++;
                    }
                    else {
                        varLowCount++;
                    }


                }
                document.getElementById('lblType_RE').innerHTML = intLblRE;
                document.getElementById('lblType_ST').innerHTML = intLblST;
                document.getElementById('lblType_GB').innerHTML = intLblGB;
                document.getElementById('lblType_CG').innerHTML = intLblCG;
                document.getElementById('lblType_SMT').innerHTML = intLblSMT;
                document.getElementById('lblNoSensor').innerHTML = intLblNoSensor;
                document.getElementById('lblHuntCamera').innerHTML = intLblCamera;
                document.getElementById('lblBaitStation').innerHTML = intLblBaitStation;
                document.getElementById('lblBait').innerHTML = intLblBait;
                //document.getElementById('lblType_Trap').innerHTML = intLblTrap;
                //document.getElementById('lblType_Bait').innerHTML = intLblBait;


                document.getElementById('lblType_Flashes').innerHTML = intLblTriggerCount;
                document.getElementById('lblType_Catches').innerHTML = intLblCatchCount;
                document.getElementById('lblType_LastUpdate').innerHTML = dthubLastTrigger;

                var arrXAxis = ["Live", "Low", "Alive"];
                var arrValue = new Array();
                arrValue[0] = varLiveCount;
                arrValue[1] = varLowCount;
                arrValue[2] = varAliveCount;    
             
                var ctxP = document.getElementById("pieChart_BatStatus").getContext('2d');
                var myPieChart = new Chart(ctxP, {
                    type: 'doughnut',
                    data: {
                        labels: arrXAxis,
                        datasets: [{
                            data: arrValue,
                            backgroundColor: ["#46BFBD", "#FDB45C", "#F7464A"],
                            hoverBackgroundColor: ["#46BFBD", "#FDB45C", "#F7464A"]
                        }]
                    },
                    options: {
                        responsive: true, legend: {
                            position: 'right'
                        }
                    }
                });
               
            }
            
        });
    });
}
//Ragu 19 Sep 2018

function fnGetDashboardDataForSite_Hourly(strSiteId, dtFrom, dtTo)
{
    if (typeof strSiteId == 'undefined') {
        return false;
    }

    var intSiteId = document.getElementById("ContentPlaceHolder1_cmbSiteName").value;

    //Ragu 22 Jun 2017
    var e = document.getElementById("ContentPlaceHolder1_cmbLocation");
    var intFloorMapId = e.options[e.selectedIndex].id;

    //Ragu 02 Aug 17
    if (intFloorMapId == 0) {
        document.getElementById("ContentPlaceHolder1_cmbLocation").selectedIndex = "1";
        var e = document.getElementById("ContentPlaceHolder1_cmbLocation");
        var intFloorMapId = e.options[e.selectedIndex].id;
    }
    //-----
    //var e = document.getElementById("ContentPlaceHolder1_cmbLocation").value;
    //var intFloorMapId = e;
    if (intFloorMapId < 0 || intFloorMapId == null || intFloorMapId == "") {
        e = document.getElementById("ContentPlaceHolder1_cmbLocation").value;
        intFloorMapId = e;
    }

    if (intFloorMapId == "0") {
        alert("Select the Location Name");
        return false;
    }

    var strFloorMap = "";
    $.ajax({
        type: 'GET',
        url: '/services/dashboard.aspx?id=7&floormapid=' + intFloorMapId,
        success: function (data) {
            strFloorMap = data;
            document.getElementById("ContentPlaceHolder1_imgBuildingPlan").src = "/plans/" + strFloorMap;
            document.getElementById("ContentPlaceHolder1_imgBuildingPlan").style.visibility = "visible";
            //document.getElementById("ContentPlaceHolder1_spMessage").innerHTML = "";
        }
    });
    
    arrSensorDetails = '';

    if (strSiteId != "0") {
        document.getElementById("ContentPlaceHolder1_cmbSiteName").value = strSiteId;
        intSiteId = strSiteId;
    }
    else {
        document.getElementById("ContentPlaceHolder1_cmbSiteName").value = intSiteId;
    }

    $(document).ready(function () {
        var dtStartDate = dtFrom;
        var dtEndDate = dtTo;

        //Ragu 22 Jun 2016
        var tmp_date = new Date();

        var tmp_dtEnd = dtTo;
        //tmp_date.setDate(tmp_date.getDate() - 1);
        var tmp_dtStart = dtFrom;

        if ((dtFrom == "") && (dtTo == "")) {
            document.getElementById("hidStartDate").value = tmp_dtStart;
            document.getElementById("hidEndDate").value = tmp_dtEnd;
            dtFrom = tmp_dtStart;
            dtTo = tmp_dtEnd;

            document.getElementById("spDateRange").innerHTML = " " + tmp_dtStart + '  to  ' + tmp_dtEnd + "";
            $('#reportrange span').html(tmp_dtStart + ' - ' + tmp_dtEnd);
        }

        $.ajax(
            {
                type: 'GET',
                url: '/services/dashboard.aspx?id=9&siteid=' + intSiteId + "&floormapid=" + intFloorMapId + '&startdate=' + dtFrom + '&enddate=' + dtTo,
                success: function (data)
                {
                    if (data != "") {
                        document.getElementById("btnDownloadDashboard").style.visibility = "visible";
                        var sensor_data = JSON.parse(data);

                        var element = document.getElementsByName("sensor_image");
                        for (index = element.length - 1; index >= 0; index--) {
                            element[index].parentNode.removeChild(element[index]);
                        }
                        $('label').remove();
                        $('line').remove();
                        $('div.line').remove();
                        $('div.arrow').remove();
                        $('div.newarrow').remove();



                        for (var i = 0; i < sensor_data.length; i++) {
                            var sensorid = sensor_data[i].i_SensorId;
                            var min = sensor_data[i].i_MinThresholdLevel;
                            var max = sensor_data[i].i_MaxThresholdLevel;
                            var status = sensor_data[i].b_Status;
                            var intSensorCount = sensor_data[i].i_SensorCount;
                            var SensorType = sensor_data[i].s_SensorType;
                            var qrCode = sensor_data[i].s_qrCode;
                            

                            var TiggerMin = sensor_data[i].i_SensorMin;


                            if (intSensorCount == 0) {
                                status = "";
                            }
                            var img = new Image(27, 18);

                            img.name = "sensor_image";
                            img.id = sensorid + "_" + min + "_" + max;

                            //Ragu 01 May 2018
                            img.src = '/openlayers/img/0.gif';
                            if (status == "") {
                                if ((SensorType == 0) || (SensorType == "RE*"))
                                    img.src = '/openlayers/img/0.gif';
                                else if (SensorType == "SRS-ST")
                                    img.src = '/openlayers/img/0_Cir_Orange.png';
                                else if (SensorType == "SRS-GB")
                                    img.src = '/openlayers/img/0_Cir.png';
                                else if (SensorType == "SRS-CG")
                                    img.src = '/openlayers/img/0_Cir_Black.png';
                                else if (SensorType == "SMT")
                                    img.src = '/openlayers/img/0_tri.png';
                                else if (SensorType == "SRS-Trap")
                                    img.src = '/openlayers/img/0_tri.png';
                                else if (SensorType == "SRS-Bait Station")
                                    img.src = '/openlayers/img/0_Bait.jpg';
                                else if (SensorType == "Trap without Sensor")
                                    img.src = '/openlayers/img/04_Poly_Blue.png';
                                else if (SensorType == "Hunting Camera")
                                    img.src = '/openlayers/img/0_Camera.png';
                                else if (SensorType == "Bait Station")
                                    img.src = '/openlayers/img/0_Bait.png';
                            }
                            if (status == "0") {
                                if ((SensorType == 0) || (SensorType == "RE*"))
                                    img.src = '/openlayers/img/0.gif';
                                else if (SensorType == "SRS-ST")
                                    img.src = '/openlayers/img/0_Cir_Orange.png';
                                else if (SensorType == "SRS-GB")
                                    img.src = '/openlayers/img/0_Cir.png';
                                else if (SensorType == "SRS-CG")
                                    img.src = '/openlayers/img/0_Cir_Black.png';
                                else if (SensorType == "SMT")
                                    img.src = '/openlayers/img/0_tri.png';
                                else if (SensorType == "SRS-Trap")
                                    img.src = '/openlayers/img/0_tri.png';
                                else if (SensorType == "SRS-Bait Station")
                                    img.src = '/openlayers/img/0_Bait.jpg';
                                else if (SensorType == "Trap without Sensor")
                                    img.src = '/openlayers/img/04_Poly_Blue.png';
                                else if (SensorType == "Hunting Camera")
                                    img.src = '/openlayers/img/0_Camera.png';
                                else if (SensorType == "Bait Station")
                                    img.src = '/openlayers/img/0_Bait.png';
                            }
                            if (status == "1") {
                                // img.src = '/openlayers/img/1.gif';
                                status == "0"
                                //img.src = '/openlayers/img/0.gif';    // Ragu 15 May Remove Yellow color
                                if ((SensorType == 0) || (SensorType == "RE*"))
                                    img.src = '/openlayers/img/0.gif';
                                else if (SensorType == "SRS-ST")
                                    img.src = '/openlayers/img/0_Cir_Orange.png';
                                else if (SensorType == "SRS-GB")
                                    img.src = '/openlayers/img/0_Cir.png';
                                else if (SensorType == "SRS-CG")
                                    img.src = '/openlayers/img/0_Cir_Black.png';
                                else if (SensorType == "SMT")
                                    img.src = '/openlayers/img/0_tri.png';
                                else if (SensorType == "SRS-Trap")
                                    img.src = '/openlayers/img/0_tri.png';
                                else if (SensorType == "SRS-Bait Station")
                                    img.src = '/openlayers/img/0_Bait.jpg';
                                else if (SensorType == "Trap without Sensor")
                                    img.src = '/openlayers/img/04_Poly_Blue.png';
                                else if (SensorType == "Hunting Camera")
                                    img.src = '/openlayers/img/0_Camera.png';
                                else if (SensorType == "Bait Station")
                                    img.src = '/openlayers/img/0_Bait.png';
                            }
                            if (status == "2") {
                                //img.src = '/openlayers/img/2.gif';
                                if ((SensorType == 0) || (SensorType == "RE*"))
                                    img.src = '/openlayers/img/2.gif';
                                else if (SensorType == "SRS-ST")
                                    img.src = '/openlayers/img/2_Cir_Orange.png';
                                else if (SensorType == "SRS-GB")
                                    img.src = '/openlayers/img/2_Cir.png';
                                else if (SensorType == "SRS-CG")
                                    img.src = '/openlayers/img/2_Cir_Black.png';
                                else if (SensorType == "SMT")
                                    img.src = '/openlayers/img/2_tri.png';
                                else if (SensorType == "SRS-Trap")
                                    img.src = '/openlayers/img/2_tri.png';
                                else if (SensorType == "SRS-Bait Station")
                                    img.src = '/openlayers/img/2_Bait.jpg';
                                else if (SensorType == "Trap without Sensor")
                                    img.src = '/openlayers/img/04_Poly_Red.png';
                                else if (SensorType == "Hunting Camera")
                                    img.src = '/openlayers/img/2_Camera.png';
                                else if (SensorType == "Bait Station")
                                    img.src = '/openlayers/img/2_Bait.png';
                            }
                            var x = sensor_data[i].i_X;
                            var y = sensor_data[i].i_Y;
                            x = x + 25;
                            //y = y - 150;   RAGU 20 MAR 2017
                            y = y - 225;
                            img.style.position = "absolute";
                            img.style.left = x + "px";
                            img.style.top = y + "px";

                            img.addEventListener("click", function () {
                                fnGetSensorDataForDashboard(this.id, dtFrom, dtTo);
                            }, false);

                            document.getElementById('ContentPlaceHolder1_image_panel').appendChild(img);

                            var newlabel = document.createElement("Label");
                            newlabel.style.position = "absolute";
                            if (intSensorCount <= 9) {
                                newlabel.style.left = (x + 8) + "px";
                            }
                            else if ((intSensorCount >= 10) && (intSensorCount <= 99)) {
                                newlabel.style.left = (x + 5) + "px";
                            }
                            else if (intSensorCount >= 100) {
                                newlabel.style.left = (x + 2) + "px";
                            }
                            newlabel.style.top = y + "px";
                            if (status == "2") {
                                newlabel.style.color = "#FFFFFF";
                            }

                            newlabel.innerHTML = intSensorCount;
                            newlabel.id = sensorid + "_" + min + "_" + max;


                            newlabel.addEventListener("click", function () {

                                fnGetSensorDataForDashboard(this.id, dtFrom, dtTo);
                            }, false);
                            document.getElementById('ContentPlaceHolder1_image_panel').appendChild(newlabel);

                            if (intSensorCount >= 1) {
                                var newlabel1 = document.createElement("Label");
                                newlabel1.style.position = "absolute";
                                newlabel1.style.left = (parseInt(x)) + "px";
                                newlabel1.style.top = (parseInt(y) - 18) + "px";
                                newlabel1.style.color = "#0000ff";
                                var value1 = new Date(parseInt(sensor_data[i].i_SensorMin.replace(/(^.*\()|([+-].*$)/g, '')));
                                //var TrigTime1 = value1.getHours() + ":" + value1.getMinutes() + ":" + value1.getSeconds();
                                var TrigTime1 = value1.getHours().toLocaleString('en-US', { minimumIntegerDigits: 2, useGrouping: false })+"" + value1.getMinutes().toLocaleString('en-US', { minimumIntegerDigits: 2, useGrouping: false });
                                newlabel1.innerHTML = TrigTime1;
                                newlabel1.id = x + "_" + y + "_" + sensorid;
                                document.getElementById('ContentPlaceHolder1_image_panel').appendChild(newlabel1);
                            }
                        }
                        //Ragu Path Finding
                        for (var i = 0; i < sensor_data.length; i++) {
                            var sensorid = sensor_data[i].i_SensorId;
                            var min = sensor_data[i].i_MinThresholdLevel;
                            var max = sensor_data[i].i_MaxThresholdLevel;
                            var status = sensor_data[i].b_Status;
                            var intSensorCount = sensor_data[i].i_SensorCount;
                            var SensorType = sensor_data[i].s_SensorType;
                            var qrCode = sensor_data[i].s_qrCode;
                            

                            var TiggerMin = sensor_data[i].i_SensorMin;

                            var nearestDistanceX = 250;
                            var nearestDistanceY = 150;
                            var var_X = 0;
                            var var_X = 0;
                            var var_X1 = 0;
                            var var_Y1 = 0;
                            var flag = 0;


                            if ((TiggerMin != null) & (sensor_data[i].i_SensorCount >= 1)) {
                                var_X = sensor_data[i].i_X;
                                var_Y = sensor_data[i].i_Y;
                                for (var j = i + 1; j < sensor_data.length; j++) {
                                    flag = 0;
                                   
                                        varSensorID1 = sensor_data[j].i_SensorId;
                                        var_X1 = sensor_data[j].i_X;
                                        var_Y1 = sensor_data[j].i_Y;
                                        if ((var_X > var_X1) & (sensor_data[j].i_SensorCount >= 1)) {
                                            if ((var_X - var_X1) <= nearestDistanceX) {
                                                flag = 1;
                                            }
                                        }
                                        if ((var_X1 > var_X) & (sensor_data[j].i_SensorCount >= 1)) {
                                            if ((var_X1 - var_X) <= nearestDistanceX) {
                                                flag = 1;
                                            }
                                        }

                                        if (flag == 1)
                                        {
                                            if ((var_Y > var_Y1) & (sensor_data[j].i_SensorCount >= 1))
                                            {
                                                if ((var_Y - var_Y1) <= nearestDistanceY)
                                                {
                                                    flag = 2;
                                                }
                                            }
                                            if ((var_Y1 > var_Y) & (sensor_data[j].i_SensorCount >= 1))
                                            {
                                                if ((var_Y1 - var_Y) <= nearestDistanceY)
                                                {
                                                    flag = 2;
                                                }
                                            }
                                        }
                                        
                                        
                                        if (flag == 2) {
                                          
                                        //strReturnValue = strReturnValue + var_X + "," + var_Y + "," + var_X1 + "," + var_Y1 + "," + vartriggerTime + ".";
                                        /*var newlabel1 = document.createElement("Label");
                                        newlabel1.style.position = "absolute";
                                        newlabel1.style.left = (parseInt(var_X1)) + "px";
                                        newlabel1.style.top = (parseInt(var_Y1) - 6) + "px";
                                        newlabel1.style.color = "#0000FF";
                                        newlabel1.innerHTML = ">";
                                        newlabel1.id = var_X1 + "_" + var_Y1 + "_Arrow_" + sensorid;
                                        var t_angle = Math.atan2(var_Y1 - var_Y, var_X1 - var_X) * 180 / Math.PI;
                                        //var t_rot_text = "rotate(" + t_angle + "deg)";
                                        //newlabel1.style.transform(t_rot_text);
                                        document.getElementById('ContentPlaceHolder1_image_panel').appendChild(newlabel1);

                                        /*var newlabel2 = document.createElement("Label");
                                        newlabel2.style.position = "absolute";
                                        newlabel2.style.left = (parseInt(var_X1) - 10) + "px";
                                        newlabel2.style.top = (parseInt(var_Y1) - 6) + "px";
                                        newlabel2.style.color = "#0000FF";
                                        newlabel2.innerHTML = sensor_data[j].i_SensorMin;
                                        newlabel2.id = var_X1 + "_" + var_Y1 + "_" + varSensorID1;
                                        document.getElementById('ContentPlaceHolder1_image_panel').appendChild(newlabel2);*/

                                        if (parseInt(getMinutesBetweenDates(sensor_data[i].i_SensorMin, sensor_data[j].i_SensorMin)) <= 15)  //Skip More than 15 Min Timing
                                        {

                                            var elmnt = document.getElementById("ContentPlaceHolder1_image_panel");
                                            elmnt.scrollTop = 0;
                                            elmnt.scrollLeft = 0;

                                            //drawSVGLine(var_X, var_Y, var_X1, var_Y1);  // Draw Path
                                            //drawlineXY(var_X, var_Y, var_X1, var_Y1);  // Draw Path
                                            newArrow(var_X, var_Y, var_X1, var_Y1); // Previous Version
                                            //initDraw(var_X, var_Y, var_X1, var_Y1);

                                            //TEst


                                            //test
                                            elmnt.scrollTop = 0;
                                            elmnt.scrollLeft = 0;

                                            //drawArrow(var_X, var_Y, var_X1, var_Y1)  // Draw Arrow
                                            //drawArrow(var_X, var_Y, var_X1, var_Y1);

                                            j = sensor_data.length; // For Terminate the Path. 1 to 1 only
                                        }
                                    }
                                }
                            }
                        }
                        //tESTING
                        
                        /*var newlabel1 = document.createElement("Label");
                        newlabel1.style.position = "absolute";
                        newlabel1.style.left = 100 + "px";
                        newlabel1.style.top = 100 + "px";
                        newlabel1.style.color = "#0000FF";
                        newlabel1.innerHTML = "SIVARAGURAM";
                        newlabel1.id = x + "_" + y + "_" + "TEST";
                        document.getElementById('ContentPlaceHolder1_image_panel').appendChild(newlabel1);*/

                    } //End For
                    img = null;
                },
            });
    });
 }

function sleep(milliseconds) {
        var start = new Date().getTime();
        for (var i = 0; i < 1e7; i++) {
            if ((new Date().getTime() - start) > milliseconds) {
                break;
            }
        }
    }

function JSONToCSVConvertor(JSONData, ReportTitle, ShowLabel) {
        //If JSONData is not an object then JSON.parse will parse the JSON string in an Object
        var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;

        var CSV = '';
        //Set Report title in first row or line

        CSV += ReportTitle + '\r\n\n';

        //This condition will generate the Label/Header
        if (ShowLabel) {
            var row = "";

            //This loop will extract the label from 1st index of on array
            for (var index in arrData[0]) {

                //Now convert each value to string and comma-seprated
                row += index + ',';
            }

            row = row.slice(0, -1);

            //append Label row with line break
            CSV += row + '\r\n';
        }

        //1st loop is to extract each row
        for (var i = 0; i < arrData.length; i++) {
            var row = "";

            //2nd loop will extract each column and convert it in string comma-seprated
            for (var index in arrData[i]) {
                row += '"' + arrData[i][index] + '",';
            }

            row.slice(0, row.length - 1);

            //add a line break after each row
            CSV += row + '\r\n';
        }

        if (CSV == '') {
            alert("Invalid data");
            return;
        }

        //Generate a file name
        var fileName = "MyReport_";
        //this will remove the blank-spaces from the title and replace it with an underscore
        fileName += ReportTitle.replace(/ /g, "_");

        //Initialize file format you want csv or xls
        var uri = 'data:text/csv;charset=utf-8,' + escape(CSV);

        // Now the little tricky part.
        // you can use either>> window.open(uri);
        // but this will not work in some browsers
        // or you will not get the correct file extension    

        //this trick will generate a temp <a /> tag
        var link = document.createElement("a");
        link.href = uri;

        //set the visibility hidden so it will not effect on your web-layout
        link.style = "visibility:hidden";
        link.download = fileName + ".csv";

        //this part will append the anchor tag and remove it after automatic click
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    }

function fnGetCSVForSensor() {
        var data = document.getElementById("hidDashboardData").value;
        //Ragu
        //data = data1.replace("\\u003cbr/\\u003e\\u003cstrong\\u003e"," - ");
        //data = data.replace("\\u003c/strong\\u003e", " ");
        //data = data.split("\\u003cbr/\\u003e\\u003cstrong\\u003e").join("-");
        //data = data.split("\\u003c/strong\\u003e").join("");

        data = replaceAll("\\u003cbr/\\u003e\\u003cstrong\\u003e", " - ", data);
        data = replaceAll("\\u003c/strong\\u003e", " ", data);

        //window.alert(data);
        //data =data.replace(/"\\u003cbr/\\u003e\\u003cstrong\\u003e"/g, 'x');


        JSONToCSVConvertor(data, "Export Data", true);
    }

function replaceAll(find, replace, str) {
        while (str.indexOf(find) > -1) {
            str = str.replace(find, replace);
        }
        return str;
    }


function fnGetSensorDataForDashboard(strSensorId, dtStartDate, dtEndDate) {


        //spLoading.innerHTML = "<img src='/img/hourglass.gif'/>";
        if ((document.getElementById("hidStartDate").value != "") && (document.getElementById("hidEndDate").value != "")) {
            dtStartDate = document.getElementById("hidStartDate").value;
            dtEndDate = document.getElementById("hidEndDate").value;
        }


        arrSensorDetails = strSensorId.split("_");
        document.getElementById("spMinimumThreshold").innerHTML = arrSensorDetails[1];
        document.getElementById("spMaximumThreshold").innerHTML = arrSensorDetails[2];
        document.getElementById("spSensorId").innerHTML = arrSensorDetails[0];

        document.getElementById("hidSensorId").value = arrSensorDetails[0];
        document.getElementById("hidStartDate").value = dtStartDate;
        document.getElementById("hidEndDate").value = dtEndDate;

        // Video Call---------

        var filename1 = "videos/";
        filename1 = filename1 + arrSensorDetails[0] + ".mp4";

        $.get(filename1)
            .done(function () {
                lightbox_open(filename1);
            }).fail(function () {
                // not exists code
            })
        // Video Call---------

        fnShowRodentCaughtData();
        fnShowCosumedData();

        //alert("Selected Period: " + dtStartDate + ' to ' + dtEndDate + "");
        //document.getElementById("spDateText").innerHTML = "Selected Period: " + dtStartDate + ' to ' + dtEndDate + "";
        $('#modalDashboard').modal();
        $('#tab_chart').show();
        $('#tab_chart1').show();
        sleep(1000);
        document.getElementById("tab_chart").innerHTML = "";
        

        var strLocation = document.getElementById("ContentPlaceHolder1_cmbLocation").value;

        document.getElementById("spLocation").innerHTML = strLocation;

        $(document).ready(function () {
            $.ajax({
                type: 'GET',
                url: '/services/dashboard.aspx?id=3&sensorid=' + arrSensorDetails[0] + '&startdate=' + dtStartDate + '&enddate=' + dtEndDate,
                success: function (data) {

                    /*if(data.search("Sensor Location")>=1)
                    {
                        document.getElementById("spSensorId").innerHTML = data;
                        data = "";
                    }*/

                    document.getElementById("hidDashboardData").value = data;

                    var dv_chart_data = JSON.parse(data);
                    var arrXAxis = new Array();
                    var arrValue = new Array();
                    var arrMin = new Array();
                    var arrMax = new Array();
                    var strSensorIdDisplay = dv_chart_data[0].sensor_id;

                    //if (dv_chart_data.length > 0) //// Ragu 15 May 2017
                    //{

                    var tmp_count = 0;
                    for (var i = 0; i < dv_chart_data.length; i++) {

                        //if (dv_chart_data[i].i_Value >= 3) {// Ragu 15 May 2017   - If Trigger Count more than 3 only display
                        var value = new Date
                            (
                            parseInt(dv_chart_data[i].dt_DateTime_Data.replace(/(^.*\()|([+-].*$)/g, ''))
                            );

                        var date = value.getMonth() +
                            1 +
                            "/" +
                            value.getDate() +
                            "/" +
                            value.getFullYear() + " " + value.getHours(); // + ":" + value.getMinutes();

                        arrXAxis.push(dv_chart_data[i].dt_DateTime_Data);
                        arrValue.push(dv_chart_data[i].i_Value);
                        arrMin.push(dv_chart_data[i].sensor_min);
                        arrMax.push(dv_chart_data[i].sensor_max);

                        tmp_count = tmp_count + dv_chart_data[i].i_Value;// Ragu 15 May 2017
                    }

                    // }

                    var intLatestValue = arrValue[dv_chart_data.length - 1];
                    var dtLastAvailableData = arrXAxis[dv_chart_data.length - 1];

                    document.getElementById("spLastDateReceived").innerHTML = dtLastAvailableData;
                    document.getElementById("spSensorId").innerHTML = strSensorIdDisplay;
                    document.getElementById("spSensorValue").innerHTML = intLatestValue;
                    document.getElementById("btnExport").style.visibility = "visible";
                    document.getElementById("btnData").style.visibility = "visible";
                    document.getElementById("btnRodentCaught").style.visibility = "visible";
                    document.getElementById("btnBaitConsumed").style.visibility = "visible";
                    
                    
                    //if(tmp_count>=3) ///// Ragu 15 May 2017
                    //{
                    if (intLatestValue != 0) {

                        var canvas = document.createElement('canvas');
                        canvas.id = "canvas_chart";
                        canvas.width = 1000;
                        canvas.height = 400;
                        
                        document.getElementById("tab_chart").appendChild(canvas);
        
                        //Ragu Changes at 08 Sep 2020

                        var ctxP = document.getElementById("canvas_chart").getContext('2d');
                        var myPieChart = new Chart(ctxP, {
                            type: 'bar',
                            data: {
                                labels: arrXAxis,
                                datasets: [{ label: 'No. Of. Trigger', data: arrValue, fill: false, backgroundColor : "#FF0000"}]
                                
                            },
                            options: {
                                    scales: {yAxes: [{ticks: {beginAtZero: true, stepSize: 1}}]}, 
                                    responsive: true
                                }
                            });


               
                        //----------------------------------



                        
                        fnShowChartData();
                        //fnShowRodentCaughtData();
                    }
                    //}
                    else {
                        document.getElementById("tab_chart").innerHTML = "No data available";
                        return;
                    }
                }
            });
        });

    }

function fnGetExcelForSensor() {
        var dtStartDate = document.getElementById("hidStartDate").value;
        var dtEndDate = document.getElementById("hidEndDate").value;

        if (typeof (arrSensorDetails) != 'undefined') {
            if (arrSensorDetails != '') {
                $.ajax({
                    type: 'GET',
                    url: '/services/dashboard.aspx?id=4&sensorid=' + arrSensorDetails[0] + '&startdate=' + dtStartDate + '&enddate=' + dtEndDate,
                    success: function (data) {
                        if (data != 'nodata')
                            window.location.href = data;
                        else
                            alert("- Data Not Available")
                    }
                });
            }

        }

    }

function fnUpdateProfile() {
        var strUser = document.getElementById("ContentPlaceHolder1_hidUserDetails").value;
        var splUser = strUser.split("$");
        var userId = splUser[0];
        var username = splUser[1];
        var emailid = splUser[2];

        var password = document.getElementById("ContentPlaceHolder1_txtPassword").value;

        if (CheckEmpty(document.getElementById("ContentPlaceHolder1_txtPassword"), document.getElementById("spPassword"), "  Error - Passwword cannot be empty") == false) {
            return false;
        }
        if (password == username) {
            document.getElementById("spPassword").innerHTML = "<font color='red'> - Password should be different from user-name </font>";
            document.getElementById("ContentPlaceHolder1_txtPassword").focus();
            return false;
        }
        if (password == emailid) {
            document.getElementById("spPassword").innerHTML = "<font color='red'> - Password should be different from EmailId </font>";
            document.getElementById("ContentPlaceHolder1_txtPassword").focus();
            return false;
        }

        if (!fnValidatePassword(password)) {
            return false;
        }

        if (CheckEmpty(document.getElementById("ContentPlaceHolder1_txtPhone"), document.getElementById("spPhone"), "  Error - Phone number cannot be empty") == false) {
            return false;
        }

        var params = "&pwd=" + password + "&phone=" + document.getElementById("ContentPlaceHolder1_txtPhone").value;

        var xmlhttp = new XMLHttpRequest();
        var strType = getParameterByName("type");

        if (strType == "e") {
            params = params + "&type=e";
            xmlhttp.open("GET", "/services/updateprofile.aspx?id=" + userId + "&" + params, false);
        }
        else {
            xmlhttp.open("GET", "/services/updateprofile.aspx?id=" + userId + "&" + params, false);
        }

        xmlhttp.send();

        var intReturnValue = xmlhttp.responseText;

        document.getElementById("spPassword").innerHTML = "<font color='red'> - Password is changed successfully </font>";
    }

function fnValidatePassword(password) {
        if (password.length < 6) {
            document.getElementById("spPassword").innerHTML = "<font color='red'> - Password should be atleast 6 characters </font>";
            document.getElementById("ContentPlaceHolder1_txtPassword").focus();
            return false;
        }

        re = /[0-9]/;
        if (!re.test(password)) {
            document.getElementById("spPassword").innerHTML = "<font color='red'> - Password should contain atleast one number </font>";
            document.getElementById("ContentPlaceHolder1_txtPassword").focus();
            return false;
        }

        re = /[a-z]/;
        if (!re.test(password)) {
            document.getElementById("spPassword").innerHTML = "<font color='red'> - Password should contain atleast one lower case alphabet </font>";
            document.getElementById("ContentPlaceHolder1_txtPassword").focus();
            return false;
        }
        re = /[A-Z]/;
        if (!re.test(password)) {
            document.getElementById("spPassword").innerHTML = "<font color='red'> - Password should contain atleast one upper case alphabet  </font>";
            document.getElementById("ContentPlaceHolder1_txtPassword").focus();
            return false;
        }

        return true;
}

function fnAddUpdateUser() {

        var password = document.getElementById("ContentPlaceHolder1_txtPassword").value;
        var confirmPassword = document.getElementById("ContentPlaceHolder1_txtConfirmPassword").value;
        var username = document.getElementById("ContentPlaceHolder1_txtName").value;
        var emailid = document.getElementById("ContentPlaceHolder1_txtEmail").value;

        if (CheckEmpty(document.getElementById("ContentPlaceHolder1_txtName"), document.getElementById("spName"), "  Error - User Name cannot be empty") == false) {
            return false
        }
        if (CheckEmpty(document.getElementById("ContentPlaceHolder1_txtEmail"), document.getElementById("spEmail"), "  Error - Email cannot be empty") == false) {
            return false
        }
        if (CheckEmpty(document.getElementById("ContentPlaceHolder1_txtPassword"), document.getElementById("spPassword"), "  Error - Passwword cannot be empty") == false) {
            return false
        }

        if (password == username) {
            document.getElementById("spPassword").innerHTML = "<font color='red'> - Password should be different from user-name </font>";
            document.getElementById("ContentPlaceHolder1_txtPassword").focus();
            return false;
        }
        if (password == emailid) {
            document.getElementById("spPassword").innerHTML = "<font color='red'> - Password should be different from EmailId </font>";
            document.getElementById("ContentPlaceHolder1_txtPassword").focus();
            return false;
        }

        if (!fnValidatePassword(password)) {
            return false;
        }

        if (CheckEmpty(document.getElementById("ContentPlaceHolder1_txtConfirmPassword"), document.getElementById("spConfirmPassword"), "  Error - Confirm password cannot be empty") == false) {
            return false
        }
        if (password != confirmPassword) {
            document.getElementById("spConfirmPassword").innerHTML = "<font color='red'>  Error- Confirm password does not match</font>";
            document.getElementById("ContentPlaceHolder1_txtConfirmPassword").focus();
            return false;
        }
        if (CheckEmpty(document.getElementById("ContentPlaceHolder1_txtPhone"), document.getElementById("spPhone"), "  Error - Phone number cannot be empty") == false) {
            return false;
        }

        var xmlhttp = new XMLHttpRequest();
        var params = "&uname=" + document.getElementById("ContentPlaceHolder1_txtName").value + "&password=" + document.getElementById("ContentPlaceHolder1_txtPassword").value + "&email=" + document.getElementById("ContentPlaceHolder1_txtEmail").value + "&phone=" + document.getElementById("ContentPlaceHolder1_txtPhone").value + "&organization=" + document.getElementById("ContentPlaceHolder1_ddOrgLocation").value + "&role=" + document.getElementById("ContentPlaceHolder1_ddRole").value + "&bstatus=" + document.getElementById("ContentPlaceHolder1_ddStatus").value;

        var strType = getParameterByName("type");

        //var userId = window.location.search.substring(1).split("&")[1].split('=')[1];
        var userId = getParameterByName("id");

        if (strType == "e") {
            params = params + "&type=e";
            xmlhttp.open("GET", "/services/user.aspx?id=" + userId + "&" + params, false);
        }
        else {
            xmlhttp.open("GET", "/services/user.aspx?id=" + userId + "&" + params, false);
        }

        xmlhttp.send();

        var intReturnValue = xmlhttp.responseText;

        location.href = "users_list.aspx";// + intSiteId
    }
function fnDeleteUser() {

        //bootbox.confirm("Are you sure you want to delete this user", function (result) {
         //   if (result == true) {

                var xmlhttp = new XMLHttpRequest();
                var strType = getParameterByName("type");

                //var userId = window.location.search.substring(1).split("&")[1].split('=')[1];
                var userId = getParameterByName("id");
                if (document.getElementById("ContentPlaceHolder1_hidUserId").value == userId) {
                    document.getElementById("spdelMsg").innerHTML = "<font color='red'><b>-- You cannot delete your own account</b></font>";
                    return false;
                }
                var params = "&uname=delete";
                if (strType == "e") {
                    params = params + "&type=e";
                    xmlhttp.open("GET", "/services/user.aspx?id=" + userId + "&" + params, false);
                }
                else {
                    xmlhttp.open("GET", "/services/user.aspx?id=" + userId + "&" + params, false);
                }

                xmlhttp.send();

                var intReturnValue = xmlhttp.responseText;

                if (intReturnValue > 0) {

                    document.getElementById("spdelMsg").innerHTML = "<font color='red'><b>User deleted successfull</b></font>";
                    location.href = "user_list.aspx";
                }
                else {
                    document.getElementById("spdelMsg").innerHTML = "<font color='red'><b>-- User can't be deleted because it is assigned to site</b></font>";
                }
          //  }

        //}).find("div.modal-content").addClass("confirmWidth");

}

function fnAddUpdateOrganization() {
        //save the site information
        if (CheckEmpty(document.getElementById("ContentPlaceHolder1_txtOrganizationName"), document.getElementById("spOrganizationName"), "  Error - Organization Name cannot be empty") == false) {
            return false
        }
        if (CheckEmpty(document.getElementById("ContentPlaceHolder1_txtContactPerson"), document.getElementById("spContactPerson"), "  Error - Contact Person cannot be empty") == false) {
            return false
        }
        if (CheckEmpty(document.getElementById("ContentPlaceHolder1_txtEmail"), document.getElementById("spEmailAddress"), "  Error - Email Address cannot be empty") == false) {
            return false
        }
        if (CheckEmpty(document.getElementById("ContentPlaceHolder1_txtPhoneNumber"), document.getElementById("spPhoneNumber"), "  Error - Phone Number cannot be empty") == false) {
            return false
        }
        if (CheckEmpty(document.getElementById("ContentPlaceHolder1_txtAddress"), document.getElementById("spAddress"), "  Error - Address cannot be empty") == false) {
            return false
        }

        if (CheckEmpty(document.getElementById("ContentPlaceHolder1_txtOrgCode"), document.getElementById("spOrgCode"), "  Error - Organization Code cannot be empty") == false) {
            return false
        }

        if (checkEmail(document.getElementById("ContentPlaceHolder1_txtEmail").value) == false) {
            document.getElementById('spEmailAddress').innerHTML = "<font color='red'>Invalid Email address</font>";
            return false;
        }
        else {
            document.getElementById('spEmailAddress').innerHTML = "";
        }

        var strLogo = document.getElementById("ContentPlaceHolder1_hidUploadFileName").value

        var xmlhttp = new XMLHttpRequest();
        var params = "orgname=" + document.getElementById("ContentPlaceHolder1_txtOrganizationName").value + "&contactperson=" + document.getElementById("ContentPlaceHolder1_txtContactPerson").value + "&email=" + document.getElementById("ContentPlaceHolder1_txtEmail").value + "&phone=" + document.getElementById("ContentPlaceHolder1_txtPhoneNumber").value + "&address=" + document.getElementById("ContentPlaceHolder1_txtAddress").value + "&status=" + document.getElementById("ContentPlaceHolder1_cmbStatus").value + "&orgcode=" + document.getElementById("ContentPlaceHolder1_txtOrgCode").value + "&logo=" + strLogo;

        var strType = getParameterByName("type");

        var intOrganizationId = getParameterByName("id");

        if (strType == "e") {
            params = params + "&type=e";
            xmlhttp.open("GET", "/services/organization.aspx?id=" + intOrganizationId + "&" + params, false);
        }
        else {
            xmlhttp.open("GET", "/services/organization.aspx?id=" + intOrganizationId + "&" + params, false);
        }
        xmlhttp.send();

        
        location.href = "organization_list.aspx";// 

        /*var intReturnValue = xmlhttp.responseText;
        
        if (strType == "e") {
            $.ajax({
                url: "/services/organization.aspx?",
                type: "POST",
                data: { type: "e", orgname: document.getElementById("ContentPlaceHolder1_txtOrganizationName").value, contactperson: document.getElementById("ContentPlaceHolder1_txtContactPerson").value, email: document.getElementById("ContentPlaceHolder1_txtEmail").value, phone: document.getElementById("ContentPlaceHolder1_txtPhoneNumber").value, address: document.getElementById("ContentPlaceHolder1_txtAddress").value, status: document.getElementById("ContentPlaceHolder1_cmbStatus").value, orgcode: document.getElementById("ContentPlaceHolder1_txtOrgCode").value, logo: strLogo },
                dataType: "json",
                success: function (result) {

                    location.href = "organization_list.aspx";
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        //alert(xhr.status);
                        // alert(thrownError);
                    }
            });
        }
        else {
            $.ajax({
                url: "/services/organization.aspx?",
                type: "POST",
                data: { id: intOrganizationId, orgname: document.getElementById("ContentPlaceHolder1_txtOrganizationName").value, contactperson: document.getElementById("ContentPlaceHolder1_txtContactPerson").value, email: document.getElementById("ContentPlaceHolder1_txtEmail").value, phone: document.getElementById("ContentPlaceHolder1_txtPhoneNumber").value, address: document.getElementById("ContentPlaceHolder1_txtAddress").value, status: document.getElementById("ContentPlaceHolder1_cmbStatus").value, orgcode: document.getElementById("ContentPlaceHolder1_txtOrgCode").value, logo: strLogo },
                dataType: "json",
                success: function (result) {

                    location.href = "organization_list.aspx";
                   },
                error: function (xhr, ajaxOptions, thrownError) {
                    //alert(xhr.status);
                    // alert(thrownError);
                }
            });
        }*/
        


    }
function fnDeleteOrganization() {
        var xmlhttp = new XMLHttpRequest();
        var strType = getParameterByName("type");

        var strType = getParameterByName("type");

        var intOrganizationId = getParameterByName("id");

        var params = "orgname=delete";
        if (strType == "e") {
            params = params + "&type=e";
            xmlhttp.open("GET", "/services/organization.aspx?id=" + intOrganizationId + "&" + params, false);
        }
        else {
            xmlhttp.open("GET", "/services/organization.aspx?id=" + intOrganizationId + "&" + params, false);
        }

        xmlhttp.send();

        var intReturnValue = xmlhttp.responseText;

        if (intReturnValue == 1)
            location.href = "organization_list.aspx";// 
        else
            if (intReturnValue == 2)
                document.getElementById("spdelMsg").innerHTML = "<font color='red'><b>-- Can't be deleted. Delete sites first which belong to this organization</b></font>";

            else
                document.getElementById("spdelMsg").innerHTML = "<font color='red'><b>-- Can't be deleted. Delete users first who are under this organization</b></font>";


    }

function updRodentCaughtInfo() {  // Ragu 24 Jan 2018 for Sensor Battery Update event
    //window.alert(m_sensorID);

    var intSensorId = document.getElementById("hidSensorId").value;
    var rodcaughtTimeStamp = document.getElementById("rodcaughtTimeStamp").value;
    var rodcaughtrema = document.getElementById("rodcaughtrema").value;
    var noofrodcaught = document.getElementById("noofrodcaught").value;
    var xmlhttp = new XMLHttpRequest();

    xmlhttp.open("GET", "/services/dashboard.aspx?id=23&sensorid=" + intSensorId.toString() + "&rodcaughtTimeStamp=" + rodcaughtTimeStamp + "&rodcaughtrema=" + rodcaughtrema + "&noofrodcaught=" + noofrodcaught , false);
    xmlhttp.send();

    var intReturnValue = xmlhttp.responseText;

    fnShowRodentCaughtData();
    
    

}
function updBaitConsumedInfo() {  // Ragu 07 May 2020 
    //window.alert(m_sensorID);

    var intSensorId = document.getElementById("hidSensorId").value;
    var consumedTimeStamp = document.getElementById("consumedTimeStamp").value;
    var consumedrema = document.getElementById("consumedrema").value;
    var noofconsumed = document.getElementById("noofconsumed").value;
    var xmlhttp = new XMLHttpRequest();

    xmlhttp.open("GET", "/services/dashboard.aspx?id=31&sensorid=" + intSensorId.toString() + "&consumedTimeStamp=" + consumedTimeStamp + "&consumedrema=" + consumedrema + "&noofconsumed=" + noofconsumed, false);
    xmlhttp.send();

    var intReturnValue = xmlhttp.responseText;

    fnShowCosumedData();



}
function fnShowRodentCaughtData() {
        var intSensorId = document.getElementById("hidSensorId").value;
        var dtStartDate = document.getElementById("hidStartDate").value;
        var dtEndDate = document.getElementById("hidEndDate").value;
    
    $.ajax({
        type: 'GET',
        async: false,
        url: '/services/dashboard.aspx?id=24&sensorid=' + intSensorId + '&startdate=' + dtStartDate + '&enddate=' + dtEndDate,
        success: function (data) {


            $('#tblData_rodentCaught').html(data);



            $(function () {
                $("#tblData_rodentCaught").dataTable();

            });

        }
    });
}
function fnShowCosumedData() {
    var intSensorId = document.getElementById("hidSensorId").value;
    var dtStartDate = document.getElementById("hidStartDate").value;
    var dtEndDate = document.getElementById("hidEndDate").value;

    $.ajax({
        type: 'GET',
        async: false,
        url: '/services/dashboard.aspx?id=32&sensorid=' + intSensorId + '&startdate=' + dtStartDate + '&enddate=' + dtEndDate,
        success: function (data) {


            $('#tblData_baitConsumed').html(data);



            $(function () {
                $("#tblData_baitConsumed").dataTable();

            });

        }
    });
}

function updHubActiveInfo() {  // Ragu 24 Jan 2018 for Sensor Battery Update event
    //window.alert(m_sensorID);
    
    var intOrganizationId = getParameterByName("id");

    var m_hubSerialNumber = document.getElementById("hubSerialNumber").value;
    var m_hubactive = document.getElementById("hubactive").value;

    var intReturnValue;

    var hubValid = 0;
    $.ajax({
        type: 'GET',
        async: false,
        url: '/services/sensor.aspx?id=15&organizationid=' + intOrganizationId + '&hubserialnumber=' + m_hubSerialNumber,
        success: function (data) {
            hubValid = data;
        }
    });

    //Print Function
    if (hubValid == 0) {

        var xmlhttp = new XMLHttpRequest();
        xmlhttp.open("GET", "/services/organization.aspx?id=" + intOrganizationId + "&hubserialnumber=" + m_hubSerialNumber + "&hubactive=" + m_hubactive + "&orgname=addHub", false);
        xmlhttp.send();
        intReturnValue = xmlhttp.responseText;
    }
    else {
        alert("HUB QR Number already inside. Please contact Administrator");
    }


    showHubActiveInfo();
}
function showHubActiveInfo()
    {  // Ragu 24 Jan 2018 for Sensor Battery Update event
        //window.alert(m_sensorID);

        var intOrganizationId = getParameterByName("id");

      var xmlhttp = new XMLHttpRequest();
         xmlhttp.open("GET", "/services/organization.aspx?id=" + intOrganizationId + "&orgname=viewHub", false);
        xmlhttp.send();

        var intReturnValue = xmlhttp.responseText;

        $('#tblData_hub').html(intReturnValue);

       

        /*$.ajax({
            type: 'GET',
            async: false,
            url: '/services/organization.aspx?id=' + intOrganizationId + '&orgname=viewHub',
            success: function (data) {


                $('#tblData_hub').html(data);
            
                $(function () {
                    $("#tblData_hub").dataTable();

                });

            }
        }).reload();*/
        
   
}
function updSensorActiveInfo() {  // Ragu 24 Jan 2018 for Sensor Battery Update event
        //window.alert(m_sensorID);

        var intOrganizationId = getParameterByName("id");

        var m_sensorQRNumber = document.getElementById("sensorQRNumber").value;
        var m_sensoractive = document.getElementById("sensoractive").value;


        var xmlhttp = new XMLHttpRequest();
        xmlhttp.open("GET", "/services/organization.aspx?id=" + intOrganizationId + "&sensorqrnumber=" + m_sensorQRNumber + "&sensoractive=" + m_sensoractive + "&orgname=addSensor", false);
        xmlhttp.send();

        var intReturnValue = xmlhttp.responseText;

        showSensorActiveInfo();
    }
function updBulkSensorActiveInfo() {  
        //window.alert(m_sensorID);
            
        var intOrganizationId = getParameterByName("id");

        var i_Start = document.getElementById("txtQRStart").value;
        var i_End = document.getElementById("txtQREnd").value;
        //var t_orgCode = document.getElementById("txtOrgCode").value;
        //var t_orgCode = document.getElementById("hidQRPrefix").value;
        var t_orgCode = arguments[0];
        
        var m_sensoractive = "Active";

        for (var i = i_Start; i <= i_End; i++) {
            var m_sensorQRNumber = t_orgCode + "-" + i;

            var sensorValid = 0;
            $.ajax({
                type: 'GET',
                async: false,
                url: '/services/sensor.aspx?id=16&organizationid=' + intOrganizationId + '&sensorqrnumber=' + m_sensorQRNumber,
                success: function (data) {
                    sensorValid = data;
                }
            });

            if (sensorValid == 0) {

                var xmlhttp = new XMLHttpRequest();
                xmlhttp.open("GET", "/services/organization.aspx?id=" + intOrganizationId + "&sensorqrnumber=" + m_sensorQRNumber + "&sensoractive=" + m_sensoractive + "&orgname=addSensor", false);
                xmlhttp.send();
            }
            else{
                alert("QR Number already inside. Please contact Administrator");
            }
           
            var intReturnValue = xmlhttp.responseText;


            //Print Function

             /*var xhttp = new XMLHttpRequest();
            xhttp.open('GET', "http://192.168.1.54:40006/?QRCode=" + m_sensorQRNumber, true);
            xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            xhttp.send();*/

        }
        showSensorActiveInfo();
    }
function printBulkSensorActiveInfo() {
        //window.alert(m_sensorID);

        //PrinterIP
        var t_printerIP = "http://192.168.1.54:40006";
        t_printerIP = t_printerIP + "/?QRCode="

        var intOrganizationId = getParameterByName("id");

        var i_Start = document.getElementById("txtQRStart").value;
        var i_End = document.getElementById("txtQREnd").value;
        //var t_orgCode = document.getElementById("txtOrgCode").value;
        //var t_orgCode = document.getElementById("hidQRPrefix").value;
        var t_orgCode = arguments[0];

        var m_sensoractive = "Active";

        for (var i = i_Start; i <= i_End; i++) {
            var m_sensorQRNumber = t_orgCode + "-" + i;

            var sensorValid = 0;
            $.ajax({
                type: 'GET',
                async: false,
                url: '/services/sensor.aspx?id=16&organizationid=' + intOrganizationId + '&sensorqrnumber=' + m_sensorQRNumber,
                success: function (data) {
                    sensorValid = data;
                 }
            });

            //Print Function
            if (sensorValid == 1) {
                sleepIteration(5000);
                var xhttp = new XMLHttpRequest();

                xhttp.open('GET', t_printerIP  + m_sensorQRNumber, true);
                xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
                xhttp.send();
                sleepIteration(5000);
                
            }
            else if (sensorValid > 1)
            {
                alert("Duplicate QR Number will not accept. Please contact Administrator");
            }
            else
            {
                alert("Please Generate QR First and Print");
            }
        }  
        alert("Print Success");
        showSensorActiveInfo();
    }

function sleepIteration(milliseconds) {
        let timeStart = new Date().getTime();
        while (true) {
            let elapsedTime = new Date().getTime() - timeStart;
            if (elapsedTime > milliseconds) {
                break;
            }
        }
    } 

function showSensorActiveInfo() {  // Ragu 24 Jan 2018 for Sensor Battery Update event
        //window.alert(m_sensorID);

        var intOrganizationId = getParameterByName("id");

        var xmlhttp = new XMLHttpRequest();
        /* xmlhttp.open("GET", "/services/organization.aspx?id=" + intOrganizationId + "&orgname=viewHub", false);
        xmlhttp.send();

        var intReturnValue = xmlhttp.responseText;

        $('#tblData_hub').html(intReturnValue);*/


        $.ajax({
            type: 'GET',
            async: false,
            url: '/services/organization.aspx?id=' + intOrganizationId + '&orgname=viewSensor',
            success: function (data) {


                $('#tblData_Sensor').html(data);

                $(function () {
                    $("#tblData_Sensor").dataTable();

                });

            }
        });
        

    }

function getSensorActiveInfoList() {  // Ragu 24 Jan 2018 for Sensor Battery Update event
    //window.alert(m_sensorID);

    var intOrganizationId = getParameterByName("id");

    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open("GET", "/services/organization.aspx?id=" + intOrganizationId + "&orgname=getSensor", false);
    xmlhttp.send();

    var intReturnValue = xmlhttp.responseText;
    //document.getElementById("hidSensorQRlist").value = intReturnValue;
    return intReturnValue;

    //$('#hidSensorQRlist').(intReturnValue);
    

}

function fnShowChartData() {
        var dtStartDate = document.getElementById("hidStartDate").value;
        var dtEndDate = document.getElementById("hidEndDate").value;
        var intSensorId = document.getElementById("hidSensorId").value;

        $.ajax({
            type: 'GET',
            async: false,
            url: '/services/dashboard.aspx?id=5&sensorid=' + intSensorId + '&startdate=' + dtStartDate + '&enddate=' + dtEndDate,
            success: function (data) {


                $('#tblData').html(data);



                $(function () {
                    $("#tblData").dataTable();

                });

            }
        });
    }

   
/*$(".modHelp").on("show.bs.modal", function () {
        var height = $(window).width();
        $(this).find(".modal-body").css("max-width", width);
});*/

function fnHelp() {
        var strPage = location.pathname.substring(1);

        $.ajax({
            type: 'GET',
            url: '/help/' + strPage,
            success: function (data) {
                document.getElementById("spHelpContent").innerHTML = data;
            }
        });

        $('#modHelp').modal('show');
    }


function fnViewChartData() {
        $('#modDashboardData').modal('show');
    }

function fnRodentCaught() {
    $('#modRodentCaughtData').modal('show');
}

function fnBaitConsumed() {
    $('#modBaitConsumedData').modal('show');
}

function fnResetSensorNumber() {
        var r = confirm("This will clear all existing sensors and sensor data");
        if (r == true) {
            $.ajax({
                type: 'GET',
                url: '/services/configuration.aspx?id=1',
                success: function (data) {
                    window.location.reload();
                }
            });
        }
    }

function fnSaveConfiguration() {
        if (CheckEmpty(document.getElementById("ContentPlaceHolder1_txtMaxStoragePeriod"), document.getElementById("spConfig"), "  Error - Maximum Data Storage Period cannot be empty") == false) {
            return false
        }

        var strMaxDataStoragePeriod = document.getElementById("ContentPlaceHolder1_txtMaxStoragePeriod").value;
        $.ajax({
            type: 'GET',
            url: '/services/configuration.aspx?id=2&period=' + strMaxDataStoragePeriod,
            success: function (data) {
                window.location.reload();
            }
        });
    }

function fnDeleteSite() {
        var intSiteId = document.getElementById("ContentPlaceHolder1_strtxtSiteId").value;

        bootbox.confirm("Deleting this site will delete all associated Hubs, Sensors and their data.<br/><br/>Are you sure you want to delete this site", function (result) {
            if (result == true) {
                $.ajax({
                    type: 'GET',
                    url: '/services/site.aspx?id=6&siteid=' + intSiteId,
                    success: function (data) {
                        bootbox.alert("Site deleted successfully", function () {
                            window.location.href = "sites_list.aspx";
                        }).find("div.modal-content").addClass("confirmWidth");

                    }
                });

            }

        }).find("div.modal-content").addClass("confirmWidth");


    }

function fnSaveLocation() {
    if (document.getElementById("ContentPlaceHolder1_strcmbFloorMapImage").value == "") {
        var strFloorMap = document.getElementById("ContentPlaceHolder1_cmbFloorMapImage").value;
    }
    else {
        var strFloorMap = document.getElementById("ContentPlaceHolder1_strcmbFloorMapImage").value;
    }
       
        var intSiteId = getParameterByName("id");
        var strLocation = document.getElementById("ContentPlaceHolder1_txtFloorMapImageName").value;
        $.ajax({
            url: "/services/site.aspx?id=7",
            type: "POST",
            data: { siteid: intSiteId, floormap: strFloorMap, location: strLocation },
            dataType: "json",
            success: function (result) {
            },
            error: function (xhr, ajaxOptions, thrownError) {

            }
        });



    }

function fnGetLocations() {
        var intSiteId = document.getElementById("ContentPlaceHolder1_cmbSiteName").value;

        $.ajax({
            type: 'GET',
            async: false,
            url: '/services/dashboard.aspx?id=6&siteid=' + intSiteId,
            success: function (data) {
                var location_data = JSON.parse(data);
                document.getElementById("ContentPlaceHolder1_cmbLocation").options.length = 0;
                var select = document.getElementById("ContentPlaceHolder1_cmbLocation");
                var option0 = document.createElement("option");
                option0.id = "0";
                option0.value = "--Select Location--";
                option0.innerHTML = "--Select Location--";
                select.appendChild(option0);

                for (var i = 0; i < location_data.length; i++) {

                    var option = document.createElement("option");
                    option.id = location_data[i].i_SiteImageId;
                    option.value = location_data[i].s_FloorMapImageName;
                    option.innerHTML = location_data[i].s_FloorMapImageName;
                    select.appendChild(option);
                }

            }
        });

}

function fnGetHub(strSiteId) {
        var intSiteId = document.getElementById("ContentPlaceHolder1_cmbSiteName").value;

        $.ajax({
            type: 'GET',
            async: false,
            url: '/services/sites.aspx?id=8&siteid=' + strSiteId,
            success: function (data) {
                var location_data = JSON.parse(data);
                document.getElementById("ContentPlaceHolder1_cmbHubName").options.length = 0;
                var select = document.getElementById("ContentPlaceHolder1_cmbHubName");
                var option0 = document.createElement("option");
                option0.id = "0";
                option0.value = "--Select Hub Name--";
                option0.innerHTML = "--Select Hub Name--";
                select.appendChild(option0);

                for (var i = 0; i < location_data.length; i++) {

                    var option = document.createElement("option");
                    option.id = location_data[i].s_HubId;
                    option.value = location_data[i].s_HubPhoneNumber;
                    option.innerHTML = location_data[i].s_HubPhoneNumber;
                    select.appendChild(option);


                }
            }
        });

}

function fnViewFloorMap() {
    var intFloorMapId = document.getElementById("ContentPlaceHolder1_cmbFloorMapImage").value;

    /*if (document.getElementById("ContentPlaceHolder1_strcmbFloorMapImage").value = "") {
        document.getElementById("ContentPlaceHolder1_strcmbFloorMapImage").value = intFloorMapId;
    }*/
    //document.getElementById("ContentPlaceHolder1_strcmbFloorMapImage").value = document.getElementById("ContentPlaceHolder1_cmbFloorMapImage").value;
        //var intFloorMapId = document.getElementById("ContentPlaceHolder1_strcmbFloorMapImage").value;
    var intSiteId = getParameterByName("id");
    //alert(intSiteId);

        $.ajax({
            type: 'GET',
            async: false,
            url: '/services/sensor.aspx?id=4&siteid=' + intSiteId + '&floormapid=' + intFloorMapId,
            success: function (data) {
                var floormap_data = JSON.parse(data);
                //var e = document.getElementById("ContentPlaceHolder1_strcmbFloorMapImage");
                var e = document.getElementById("ContentPlaceHolder1_cmbFloorMapImage");
                var strFloorMap = e.options[e.selectedIndex].text;

                //document.getElementById("ContentPlaceHolder1_image_panel").innerHTML = "<span id=\"ContentPlaceHolder1_spBuildingImage\"></span>";
                document.getElementById("ContentPlaceHolder1_imgBuildingPlan").src = "/plans/" + strFloorMap;
                document.getElementById("ContentPlaceHolder1_imgBuildingPlan").style.visibility = "visible";

                document.getElementById("hidLastSensorId").value = floormap_data.length-2;

                var element = document.getElementsByName("sensor_image");
                for (index = element.length - 1; index >= 0; index--) {
                    element[index].parentNode.removeChild(element[index]);
                }

                for (var i = 0; i < floormap_data.length; i++) {

                    if (floormap_data[i].s_SensorType == "RE*") {
                        var strImageUrl = "/openlayers/img/0.gif";
                    }
                    else if (floormap_data[i].s_SensorType == "SRS-Bait Station") {
                        var strImageUrl = "/openlayers/img/0_Bait.jpg";
                    }
                    else if (floormap_data[i].s_SensorType == "SRS-Trap") {
                        var strImageUrl = "/openlayers/img/0_tri.png";
                    }
                    else if (floormap_data[i].s_SensorType == "SRS-ST") {
                        var strImageUrl = "/openlayers/img/0_Cir_Orange.png";
                    }
                    else if (floormap_data[i].s_SensorType == "SRS-GB") {
                        var strImageUrl = "/openlayers/img/0_Cir.png";
                    }
                    else if (floormap_data[i].s_SensorType == "SRS-CG") {
                        var strImageUrl = "/openlayers/img/0_Cir_Black.png";
                    }
                    else if (floormap_data[i].s_SensorType == "SMT") {
                        var strImageUrl = "/openlayers/img/0_tri.png";
                    }
                    else if (floormap_data[i].s_SensorType == "Trap without Sensor") {
                        var strImageUrl = "/openlayers/img/04_Poly_Blue.png";
                    }
                    else if (floormap_data[i].s_SensorType == "Hunting Camera") {
                        var strImageUrl = "/openlayers/img/0_Camera.png";
                    }
                    else if (floormap_data[i].s_SensorType == "Bait Station") {
                        var strImageUrl = "/openlayers/img/0_Bait.png";
                    }
                    else {
                        var strImageUrl = "/openlayers/img/0.gif";
                    }
                    var sensorid = floormap_data[i].i_SensorId;

                    var img = new Image(27, 18);
                    img.ToolTip = "Hub Id:";
                    img.name = "sensor_image";
                    img.id = sensorid;
                    img.src = strImageUrl;
                    img.style.position = "absolute";
                    var x = floormap_data[i].i_X;
                    var y = floormap_data[i].i_Y;

                    // Ragu 15 May 2019 For working in Phone


                    x = x + 25;
                    //y = y - 150;   RAGU 20 MAR 2017
                    y = y - 225;

                    //----

                    img.style.left = x + "px";
                    img.style.top = y + "px";


                    img.addEventListener("click", function () {
                        fnGetSensorData(this.id);
                    }, false);

                    document.getElementById('ContentPlaceHolder1_image_panel').appendChild(img)


                    img = null;
                }
            }
        });
    }

function newArrow(x1, y1, x2, y2) {

    $("#ContentPlaceHolder1_image_panel").scrollLeft(0);
    $("#ContentPlaceHolder1_image_panel").scrollTop(0);

    var length = Math.sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
    var angle = Math.atan2(y2 - y1, x2 - x1) * 180 / Math.PI;
    var angle1 = angle * (-1);
    var transform = 'rotate(' + angle + 'deg)';

    if (navigator.userAgent.indexOf("Chrome") != -1) {


        setTimeout(function () {
            $('body, html').stop().animate({ scrollTop: 0 }, 100);
        }, 500);

        setTimeout(function () {
            $('body, html').stop().animate({ scrollLeft: 0 }, 100);
        }, 500);

        setTimeout(function () {
            $('#ContentPlaceHolder1_image_panel').stop().animate({ scrollTop: 0 }, 100);
        }, 500);

        setTimeout(function () {
            $('#ContentPlaceHolder1_image_panel').stop().animate({ scrollLeft: 0 }, 100);
        }, 500);

        //alert("Chrome");
        //midX = (x1 + x2) / 2
        //midY = (y1 + y2) / 2
        x1 = x1 + 295;
        y1 = y1 + 0;

        x2 = x2 + 295; //left IE
        y2 = y2 + 0; //top IE

    }
    else {
        x1 = x1 + 295;
        y1 = y1 + 0;

        x2 = x2 + 295; //left IE
        y2 = y2 + 0; //top IE
    }


    var line = $('<div>')
        .appendTo('#ContentPlaceHolder1_image_panel')
        .addClass('newarrow')
        .css({
            'transform': transform,
            '-webkit-transform': transform,
            '-moz-transform': transform,
            '-o-transform': transform,
            '-ms-transform': transform,
            'position': 'absolute'
        })
        .width(length)
        .offset({ top: y1, left: x1 });
            //.offset({ top: midY, left: midX });

            //'margin-top': x2 + "px",
            //'margin-left': y2 + "px"

   

}
function drawlineXY(x1, y1, x2, y2) {
        
       

        $("#ContentPlaceHolder1_image_panel").scrollLeft(0);
        $("#ContentPlaceHolder1_image_panel").scrollTop(0);


        /*if (navigator.userAgent.indexOf("Chrome") != -1) {

            x1 = x1 + 45; //left Chrome
            y1 = y1 - 50; //top Chrome

            x2 = x2 + 45; //left Chrome
            y2 = y2 - 50; //top Chrome

        }
        else {
            x1 = x1 + 45;
            y1 = y1 - 50;

            x2 = x2 + 45; //left IE
            y2 = y2 - 50; //top IE
        }*/

        var length = Math.sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        var angle = Math.atan2(y2 - y1, x2 - x1) * 180 / Math.PI;
        var transform = 'rotate(' + angle + 'deg)';

        var line = $('<div>')
            .appendTo('#ContentPlaceHolder1_image_panel')
            .addClass('line')
            .css({
                'position': 'absolute',  //'position': 'absolute',
                '-webkit-transform': transform,
                '-moz-transform': transform,
                'transform': transform,
                '-o-transform': transform,
                '-ms-transform': transform,
                'font-size': '30px'
            })
            //.text(">")
            .width(length)
            .offset({ left: x1, top: y1});
       
        //return line;
        $("#ContentPlaceHolder1_image_panel").scrollLeft(0);
        $("#ContentPlaceHolder1_image_panel").scrollTop(0);
        // Draw Arrow

        angle = Math.abs(angle);


        /*if ((angle >= 0) && (angle <= 29)) {
            y2 = y2 - 15;
            x2 = x2 - 25;
        }
        else if ((angle >= 30) && (angle <= 59)) {
            y2 = y2 - 15;
            x2 = x2 - 25;
        }
        else if ((angle >= 60) && (angle <= 89)) {
            y2 = y2 - 15;
            x2 = x2 - 25;
        }

        else if ((angle >= 90) && (angle <= 119)) {
            y2 = y2 + 20;
            x2 = x2 + 5;
        }
        else if ((angle >= 120) && (angle <= 149)) {
            y2 = y2 + 20;
            x2 = x2 + 5;
        }
        else if ((angle >= 150) && (angle <= 179)) {
            y2 = y2 + 20;
            x2 = x2 + 5;
        }

        else if ((angle >= 180) && (angle <= 209)) {
            y2 = y2 - 5;
            x2 = x2 + 25;
        }
        else if ((angle >= 210) && (angle <= 239)) {
            y2 = y2 - 5;
            x2 = x2 + 25;
        }
        else if ((angle >= 240) && (angle <= 269)) {
            y2 = y2 - 5;
            x2 = x2 + 25;
        }

        else if ((angle >= 270) && (angle <= 299)) {
            y2 = y2 - 5;
            x2 = x2 + 15;
        }
        else if ((angle >= 300) && (angle <= 329)) {
            y2 = y2 - 5;
            x2 = x2 + 15;
        }
        else if ((angle >= 329) && (angle <= 360)) {
            y2 = y2 - 5;
            x2 = x2 + 15;
        }*/

        if (navigator.userAgent.indexOf("Chrome") != -1) {

            x1 = x1 + 50; //left Chrome
            y1 = y1 - 175; //top Chrome

            x2 = x2 + 50; //left Chrome
            y2 = y2 - 175; //top Chrome

        }
        else {
            x1 = x1 + 45;
            y1 = y1 - 50;

            x2 = x2 + 45; //left IE
            y2 = y2 - 50; //top IE
        }

        if (angle <= 0)
        {
            angle = angle + 180;
        }

        if ((angle >= 0) && (angle <= 29)) { //Correct
            y2 = y2 - 15;
            x2 = x2 - 25;
        }
        else if ((angle >= 30) && (angle <= 59)) { //Correct
            y2 = y2 - 10;
            x2 = x2 - 25;
        }
        else if ((angle >= 60) && (angle <= 89)) {  //Correct
            y2 = y2 - 20;
            x2 = x2 - 20;
        }

        else if ((angle >= 90) && (angle <= 119)) { //Correct
            y2 = y2 + 10;
            x2 = x2 + 10;
        }
        else if ((angle >= 120) && (angle <= 149)) { //Correct
            y2 = y2 + 10;
            x2 = x2 + 5;
        }
        else if ((angle >= 150) && (angle <= 179)) { //Correct
            y2 = y2 + 20;
            x2 = x2 + 5;
        }

        else if ((angle >= 180) && (angle >= 209)) {//Correct
            y2 = y2 - 25;
            x2 = x2 + 25;
        }
        else if ((angle >= 210) && (angle <= 239)) { //Correct
            y2 = y2 - 5;
            x2 = x2 - 15;
        }
        else if ((angle >= 240) && (angle <= 269)) {
            y2 = y2 - 5;
            x2 = x2 + 25;
        }

        else if ((angle >= 270) && (angle <= 299)) {
            y2 = y2 - 5;
            x2 = x2 + 15;
        }
        else if ((angle >= 300) && (angle <= 329)) { //Correct
            y2 = y2 + 15;
            x2 = x2 + 15;
        }
        else if ((angle >= 330) && (angle <= 360)) {//Correct
            y2 = y2 + 10;
            x2 = x2 + 15;
        }

       

        var line1 = $('<div>')
            .appendTo('#ContentPlaceHolder1_image_panel')
            .addClass('arrow')
            .css({
                'position': 'absolute',  //'position': 'absolute',
                '-webkit-transform': transform,
                '-moz-transform': transform,
                'transform': transform,
                '-o-transform': transform,
                '-ms-transform': transform,
                'font-size': '30px',
                'background': 'transparent',
                'color': 'blue'
            })
            .text(">")
            .offset({ left: x2, top: y2 });
        
        //drawArrow1(x1, y1, x2, y2, transform);
    }
function drawArrow1(x1, y1, x2, y2, transform) {


    var angle = Math.atan2(y2 - y1, x2 - x1) * 180 / Math.PI;

    /*if (angle < 0) {
        tmp = angle + 180;
        angle = 360 - tmp;

    }*/
    angle = Math.abs(angle);


    if ((angle >= 0) && (angle <= 90)) {
        y2 = y2 - 15;
        x2 = x2 - 15;
    }

    if ((angle >= 90) && (angle <= 180)) {
        y2 = y2 + 20;
        x2 = x2 + 5;
    }

    if ((angle >= 180) && (angle <= 270)) {
        y2 = y2 - 15;
        x2 = x2 + 20;
    }
    if ((angle >= 270) && (angle <= 360)) {
        y2 = y2 - 5;
        x2 = x2 + 15;
    }

    
    $(document).ready(function () {
        $("#ContentPlaceHolder1_image_panel").scrollLeft(0);
        $("#ContentPlaceHolder1_image_panel").scrollTop(0);
    });

    var line = $('<div>')
        .appendTo('#ContentPlaceHolder1_image_panel')
        .addClass('arrow')
        .css({
            'position': 'relative',
            '-webkit-transform': transform,
            '-moz-transform': transform,
            '-o-transform': transform,
            '-ms-transform': transform,
            'transform': transform,
            'font-size': '30px',
            'background': 'transparent',
            'color': 'blue',
            'vertical-align': 'top',  
            'align': 'left'
        })
        .text(">")
        .offset({ left: x2, top: y2 });

   

    return line;
}
function drawArrow(x1, y1, x2, y2) {
 
    $(document).ready(function () {
        $("#ContentPlaceHolder1_image_panel").scrollLeft(0);
        $("#ContentPlaceHolder1_image_panel").scrollTop(0);
    });

    
   /*f (navigator.userAgent.indexOf("Chrome") != -1) {

        x1 = x1 + 45; //left Chrome
        y1 = y1 - 50; //top Chrome

        x2 = x2 + 45; //left Chrome
        y2 = y2 - 50; //top Chrome

    }
    else {
        //x1 = x1 + 45;
        //y1 = y1 - 50;

        //x2 = x2 + 45; //left IE
        //y2 = y2 - 50; //top IE

        x1 = x1 + 45;
        y1 = y1 - 50;

        x2 = x2 + 45; //left IE
        y2 = y2 - 70; //top IE
    }*/
    
    
    var length = Math.sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
    var angle = Math.atan2(y2 - y1, x2 - x1) * 180 / Math.PI;
    var transform = 'rotate(' + angle + 'deg)';

    if(angle < 0)
    {
        tmp = angle + 180;
        angle = 360 - tmp;

    }
  
    if (angle < 45)
    {
        x2 = x2 - 18;
        y2 = y2 - 23;   //2:00 Done
    }
    else if ((angle >= 45) && (angle < 90))
    {
        x2 = x2 - 20;
        y2 = x2 - 25;  //06:00

    }
    else if ((angle >= 90) && (angle < 135))
    {
        x2 = x2 - 10;
        y2 = y2 - 15;

    }
    else if ((angle >= 135) &&(angle < 180))
    {
        x2 = x2 - 5;
        y2 = y2 - 15;  //00:00 Done

    }
    else if ((angle >= 180) &&(angle < 225))
    {
        x2 = x2 - 10;
        y2 = y2 - 20;  //22:00 Done  
    }
    else if ((angle >= 225) &&(angle < 270))
    {
        x2 = x2 - 10; 
        y2 = y2 - 15;  //00:01
    }
    else if ((angle >= 270) && (angle < 315))
    {
        x2 = x2 - 5;
        y2 = y2 - 15;  //00:03

    }
    else if ((angle >= 315) && (angle <= 360))
    {
        x2 = x2 - 5;
        y2 = y2 - 15;  //00:00 Done

    }

    var line = $('<div>')
        .appendTo('#ContentPlaceHolder1_image_panel')
        .addClass('arrow')
        .css({
            'position': 'absolute',  //'position': 'absolute',
            '-webkit-transform': transform,
            '-moz-transform': transform,
            '-o-transform': transform,
            '-ms-transform': transform,
            'transform': transform,
            'font-size': '30px',
            'background': 'transparent',
            'color': 'blue'

            //'padding': '-70px'
            
        })
        //.width(length)
        .text(">")
        .offset({ left: x2, top: y2 });
        
    return line;
}


function initDraw(x1, y1, x2, y2) {
    var canvas = document.createElement('canvas');
    //var canvas = document.getElementById("ContentPlaceHolder1_image_panel");

    var start = { x: x1, y: y1 }
    var end = { x: x2, y: y2 }
    var ctx = canvas.getContext("2d");
    arrow(ctx, start, end, 10);
    var parent = document.getElementById("ContentPlaceHolder1_image_panel");
    parent.appendChild(canvas);
}


function arrow(ctx, p1, p2, size) {
    ctx.save();
    var points = edges(ctx, p1, p2);
    if (points.length < 2)
        return p1 = points[0], p2 = points[points.length - 1];

    // Rotate the context to point along the path
    var dx = p2.x - p1.x, dy = p2.y - p1.y, len = Math.sqrt(dx * dx + dy * dy);
    ctx.translate(p2.x, p2.y);
    ctx.rotate(Math.atan2(dy, dx));

    // line
    ctx.lineCap = 'round';
    ctx.beginPath();
    ctx.moveTo(0, 0);
    ctx.lineTo(-len + 2, 0);
    ctx.closePath();
    ctx.stroke();

    // arrowhead
    ctx.beginPath();
    ctx.moveTo(0, 0);
    ctx.lineTo(-size, -size);
    ctx.lineTo(-size, size);
    ctx.closePath();
    ctx.fill();

    ctx.restore();
}


function edges(ctx, p1, p2, cutoff) {
    if (!cutoff) cutoff = 220; // alpha threshold
    var dx = Math.abs(p2.x - p1.x), dy = Math.abs(p2.y - p1.y),
        sx = p2.x > p1.x ? 1 : -1, sy = p2.y > p1.y ? 1 : -1;
    var x0 = Math.min(p1.x, p2.x), y0 = Math.min(p1.y, p2.y);
    var pixels = ctx.getImageData(x0, y0, dx + 1, dy + 1).data;
    var hits = [], i = 0;
    for (x = p1.x, y = p1.y, e = dx - dy; x != p2.x || y != p2.y;) {
        var red = pixels[((y - y0) * (dx + 1) + x - x0) * 4];
        var green = pixels[((y - y0) * (dx + 1) + x - x0) * 4 + 1];
        var blue = pixels[((y - y0) * (dx + 1) + x - x0) * 4 + 2];
        console.log("r:" + red + "g:" + green + "b:" + blue);
        if (red === 0 && green === 0 && blue === 0) {
            console.log(i);
            i++
            hits.push({ x: x, y: y });
        }
        var e2 = 2 * e;
        if (e2 > -dy) { e -= dy; x += sx }
        if (e2 < dx) { e += dx; y += sy }
        //over = alpha>=cutoff;
    }
    return hits;
}

function getMinutesBetweenDates(startDate, endDate) {

    var value = new Date(parseInt(startDate.replace(/(^.*\()|([+-].*$)/g, '')));
    var value1 = new Date(parseInt(endDate.replace(/(^.*\()|([+-].*$)/g, '')));

    return value1.getMinutes() - value.getMinutes();
}
function jsDateToNormalDate(tmp)
{
    var today = new Date(tmp);

    //Isolate the day, the month and the year from the date
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yy = today.getFullYear().toString();

    //combine the date elements as mm/dd/yy
    //start by creating a leading zero for single digit days and months
    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }

    //today = mm + '/' + dd + '/' + yy;
    today = yy + '/' + mm + '/' + dd;
    return today;
}
function jsonDateToDate(tmp)
    {
    var value = new Date(parseInt(tmp.replace(/(^.*\()|([+-].*$)/g, '')));

    var date = value.getMonth() +
        1 +
        "/" +
        value.getDate() +
        "/" +
        value.getFullYear() + " " + value.getHours() + ":" + value.getMinutes();
    return value;
}
function digitFix(temp) {
        //If date is not passed, get current date
        if (parseInt(temp) <= 9)
            return "0" + temp;
        else
            return temp;
    }
function fnExportPDF() {
        var doc = new jsPDF();

        var specialElementHandlers = {
            'editor': function (element, renderer) {
                return true;
            }
        };

        var html = document.getElementById("ContentPlaceHolder1_image_panel").innerHTML;

        doc.fromHTML(html, 200, 200, {
            'width': 500,
            'elementHandlers': specialElementHandlers
        });
        doc.save("Test.pdf");
    }
function leadingZero(value) {
    if (value < 10) {
        return "0" + value.toString();
    }
    return value.toString();
}
function getNumberOfWeek(tmpDate) {
    const today = new Date(tmpDate);
    const firstDayOfYear = new Date(today.getFullYear(), 0, 1);
    const pastDaysOfYear = (today - firstDayOfYear) / 86400000;
    return Math.ceil((pastDaysOfYear + firstDayOfYear.getDay() + 1) / 7);
}

function fnGetAnalysisData0(intSiteId, dtStartDate, dtEndDate, intFloorId)  //Daily-All Level
{
    if ((document.getElementById("hidStartDate").value != "") && (document.getElementById("hidEndDate").value != "")) {
        dtStartDate = document.getElementById("hidStartDate").value;
        dtEndDate = document.getElementById("hidEndDate").value;
    }
    document.getElementById("hidStartDate").value = dtStartDate;
    document.getElementById("hidEndDate").value = dtEndDate;
    $('#modalDashboard_ActivityAnalysis').modal();
    $('#tab_chartAnalysis').show();
    sleep(1000);
    document.getElementById("tab_chartAnalysis").innerHTML = "";
    var strLocation = document.getElementById("ContentPlaceHolder1_cmbLocation").value;
    document.getElementById("spLocation").innerHTML = strLocation;

    


    $(document).ready(function () {
        $.ajax({
            type: 'GET',
            url: '/services/dashboard.aspx?id=11&siteid=' + intSiteId + '&startdate=' + dtStartDate + '&enddate=' + dtEndDate + '&intFloorId=' + intFloorId,
            success: function (data) {
                document.getElementById("hidDashboardDataForChatAnalysis").value = data;

                var dv_chart_data = JSON.parse(data);
                var arrXAxis = new Array();
                var arrValue = new Array();
                var arrMin = new Array();
                var arrMax = new Array();
                var strSensorIdDisplay = dv_chart_data[0].sensor_id;
                var zeroFlag = 0;


                var dtFrom = new Date(dtStartDate.replace(/-/g, "/"));
                var dtTo = new Date(dtEndDate.replace(/-/g, "/"));
                var duration_period = parseInt((dtTo - dtFrom) / (86400000));

                
                var tmp_count = 0;
                var tot_count = dv_chart_data.length;
                for (var i = 0; i < tot_count; i++) { /// Ragu Changes 31 Aug 2018
                    if ((i == 0)) {
                        var startDate_tmp = dtFrom.getFullYear() + "/" + leadingZero((dtFrom.getMonth() + 1)) + "/" + leadingZero(dtFrom.getDate());
                        if ((dv_chart_data[i].triggerDate) == startDate_tmp) {
                            arrXAxis.push(dv_chart_data[i].triggerDate);
                            arrValue.push(dv_chart_data[i].dayCount);
                        }
                        else
                        {
                            var cur_date = new Date(dv_chart_data[0].triggerDate);
                            var date_diff = parseInt((cur_date - pre_date) / (86400000)); 

                            for (var k = date_diff; k > 0; k--) {
                                var addOnDate = new Date(dtFrom);
                                var newdate = new Date(dv_chart_data[i].triggerDate);
                                newdate.setDate(newdate.getDate() - k);

                                arrXAxis.push(newdate.getFullYear() + "/" + leadingZero((newdate.getMonth() + 1)) + "/" + leadingZero(newdate.getDate()));
                                arrValue.push(0);
                                
                            }
                            arrXAxis.push(dv_chart_data[i].triggerDate);
                            arrValue.push(dv_chart_data[i].dayCount);
                        }
                    }
                    else {
                        var cur_date = new Date(dv_chart_data[i].triggerDate);
                        var pre_date = new Date(dv_chart_data[i - 1].triggerDate);
                        if (zeroFlag == 1) {
                            //pre_date.setDate(pre_date.getDate() + zeroFlag);
                            zeroFlag = 0;
                        }
                        
                        var date_diff = parseInt((cur_date - pre_date) / (86400000)); 
                        if (date_diff == 1) {
                            arrXAxis.push(dv_chart_data[i].triggerDate);
                            arrValue.push(dv_chart_data[i].dayCount);
                        }
                        else {  
                            for (var k = 1; k < date_diff; k++) {
                                var addOnDate = new Date(pre_date);
                                var newdate = new Date(addOnDate);
                                newdate.setDate(addOnDate.getDate() + k);

                                arrXAxis.push(newdate.getFullYear() + "/" + leadingZero((newdate.getMonth() + 1)) + "/" + leadingZero(newdate.getDate()));
                                arrValue.push(0);
                                zeroFlag = zeroFlag + 1;
                                
                            }
                            //Ragu added Last date value
                            arrXAxis.push(dv_chart_data[i].triggerDate);
                            arrValue.push(dv_chart_data[i].dayCount);
                        }

                        

                        if ((i == tot_count-1)) {
                            var cur_date = new Date(dtTo);
                            var pre_date = new Date(dv_chart_data[i].triggerDate);
                            var date_diff = parseInt((cur_date - pre_date) / (86400000));

                            for (var k = 1; k <= date_diff; k++) {
                                var addOnDate = new Date(dtFrom);
                                var newdate = new Date(dv_chart_data[i].triggerDate);
                                newdate.setDate(newdate.getDate() + k);

                                arrXAxis.push(newdate.getFullYear() + "/" + leadingZero((newdate.getMonth() + 1)) + "/" + leadingZero(newdate.getDate()));
                                arrValue.push(0);
                            }
                        }
                    }   
                    
                }

                /*var canvas = document.createElement('canvas');
                canvas.id = "canvas_chart";
                canvas.width = 900;
                canvas.height = 300;
                document.getElementById("tab_chartAnalysis").appendChild(canvas);

                var barChartData =
                    {
                        labels: arrXAxis,
                        datasets: [
                             {
                                    type: "line",
                                    fillColor: "rgba(220,220,220,0)",
                                    strokeColor: "#ff1200",
                                    pointColor: "#ff1200",
                                    pointStrokeColor: "#ff1200",
                                    pointHighlightFill: "#ff1200",
                                    pointHighlightStroke: "rgba(220,220,220,1)",
                                    data: arrValue
                                },
                        ]
                    }
                var ctx = document.getElementById("canvas_chart").getContext("2d");
                var myLineBarChart = new Chart(ctx).Overlay(barChartData);
                document.getElementById("spChartHead").innerHTML = "Daily Activity for All Levels";
                txtLegend = "<ul class='legend'><li><span class='red'></span>All Floors</li></ul>";
                document.getElementById("spLegend").innerHTML = txtLegend;*/

                document.getElementById("spChartHead").innerHTML = "Daily Activity for All Levels";
                document.getElementById("spLegend").innerHTML = "";

                var canvas = document.createElement('canvas');
                canvas.id = "canvas_chart";
                canvas.width = 1000;
                canvas.height = 400;

                document.getElementById("tab_chartAnalysis").appendChild(canvas);

                var ctxP = document.getElementById("canvas_chart").getContext('2d');
                var myPieChart = new Chart(ctxP, {
                    type: 'line',
                    data: {
                        labels: arrXAxis,
                        datasets: [{ label: 'No. Of. Trigger', data: arrValue, fill: false, borderColor: "#FF0000" }]

                    },
                    options: {
                        scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                        responsive: true
                    }
                });
                

            }
        });
    });
}
function fnGetAnalysisData1(intSiteId, dtStartDate, dtEndDate, intFloorId)  //Daily- Individual
{
    if ((document.getElementById("hidStartDate").value != "") && (document.getElementById("hidEndDate").value != "")) {
        dtStartDate = document.getElementById("hidStartDate").value;
        dtEndDate = document.getElementById("hidEndDate").value;
    }
    document.getElementById("hidStartDate").value = dtStartDate;
    document.getElementById("hidEndDate").value = dtEndDate;
    $('#modalDashboard_ActivityAnalysis').modal();
    $('#tab_chartAnalysis').show();
    sleep(1000);
    document.getElementById("tab_chartAnalysis").innerHTML = "";
    var strLocation = document.getElementById("ContentPlaceHolder1_cmbLocation").value;
    document.getElementById("spLocation").innerHTML = strLocation;

    $(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: '/services/dashboard.aspx?id=20&siteid=' + intSiteId,
        success: function (data) {
            //responce
            document.getElementById("hidDashboardDataForFloorGroup").value = data;
            }
        });
    });

  
    $(document).ready(function () {
        $.ajax({
            type: 'GET',
            url: '/services/dashboard.aspx?id=12&siteid=' + intSiteId + '&startdate=' + dtStartDate + '&enddate=' + dtEndDate + '&intFloorId=' + intFloorId,
            success: function (data) {
               
                document.getElementById("hidDashboardDataForChatAnalysis").value = data;

                var dv_chart_data = JSON.parse(data);
                
                var arrXAxis = new Array();
                var arrValue = new Array();
                var arrValue1 = new Array();
                var arrValue2 = new Array();
                var arrValue3 = new Array();
                var arrValue4 = new Array();
                var arrValue5 = new Array();
                var arrValue6= new Array();
                var arrValue7 = new Array();
                var arrValue8 = new Array();
                var arrValue9 = new Array();
                

                var arrMin = new Array();
                var arrMax = new Array();
                var arrGroupPlan = new Array();

                var strNewData;
                //var strSensorIdDisplay = dv_chart_data[0].sensor_id;
                var dv_chart_groupData = JSON.parse(document.getElementById("hidDashboardDataForFloorGroup").value);
                x_flag = 0;

                var max_count = 0;
                

                var dtFrom = new Date(dtStartDate.replace(/-/g, "/"));
                var dtTo = new Date(dtEndDate.replace(/-/g, "/"));
                var duration_period = parseInt((dtTo - dtFrom) / (86400000));
                
                var floorCount = dv_chart_groupData.length;

                for (var a = 0; a <= duration_period; a++) {
                    var newdate = new Date(dtStartDate.replace(/-/g, "/"));
                    newdate.setDate(newdate.getDate() + a);
                    arrXAxis.push(jsDateToNormalDate(newdate));
                    arrValue.push(0);
                    arrValue1.push(0);
                    arrValue2.push(0);
                    arrValue3.push(0);
                    arrValue4.push(0);
                    arrValue5.push(0);
                    arrValue6.push(0);
                    arrValue7.push(0);
                    arrValue8.push(0);
                    arrValue9.push(0);
                    
                    
                }
                    
                for (var i = 0; i < dv_chart_data.length; i++)
                { /// Ragu Changes 31 Aug 2018
                    //arrXAxis.push(jsDateToNormalDate(newdate));
                    for (vartmp = 0; vartmp < floorCount; vartmp++) {
                        if (dv_chart_groupData[vartmp].s_FloorMapImageName == dv_chart_data[i].s_FloorMapImageName)
                            x = vartmp;
                    }

                    var dt1 = new Date(dv_chart_data[i].triggerDate);
                    var dt2 = new Date(dtStartDate.replace(/-/g, "/"));
                    var pos = parseInt((dt1 - dt2) / (86400000));

                    if (x == 0)
                        arrValue[pos] = dv_chart_data[i].dayCount;
                    else if (x == 1)
                        arrValue1[pos] = dv_chart_data[i].dayCount;
                    else if (x == 2)
                        arrValue2[pos] = dv_chart_data[i].dayCount;
                    else if (x == 3)
                        arrValue3[pos] = dv_chart_data[i].dayCount;
                    else if (x == 4)
                        arrValue4[pos] = dv_chart_data[i].dayCount;
                    else if (x == 5)
                        arrValue5[pos] = dv_chart_data[i].dayCount;
                    else if (x == 6)
                        arrValue6[pos] = dv_chart_data[i].dayCount;
                    else if (x == 7)
                        arrValue7[pos] = dv_chart_data[i].dayCount;
                    else if (x == 8)
                        arrValue8[pos] = dv_chart_data[i].dayCount;
                    else if (x == 9)
                        arrValue9[pos] = dv_chart_data[i].dayCount;
                }

                var uniquearrXAxis = removeDups(arrXAxis);
                
                var canvas = document.createElement('canvas');
                canvas.id = "canvas_chart";
                canvas.width = 900;
                canvas.height = 300;
                document.getElementById("tab_chartAnalysis").appendChild(canvas);

                document.getElementById("spChartHead").innerHTML = "Daily Activity - Group By Individual Level";
                document.getElementById("spLegend").innerHTML = "";

                var canvas = document.createElement('canvas');
                canvas.id = "canvas_chart";
                canvas.width = 1000;
                canvas.height = 400;

                var ctxP = document.getElementById("canvas_chart").getContext('2d');

                if (floorCount == 1) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{
                                label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false,  borderColor: "#ff0000"
                            }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });

                }
                else if (floorCount == 2) {
                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{
                                label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false,  borderColor: "#ff0000"
                            },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false,  borderColor: "#00ff00" }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });


                }

                else if (floorCount == 3) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false,  borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false,  borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false,  borderColor: "#0000ff" }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });

                }
                else if (floorCount == 4) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false,  borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false,  borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false,  borderColor: "#0000ff" },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, data: arrValue3, fill: false,  borderColor: "#ff00ff" }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });
                }

                else if (floorCount == 5) {
                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false,  borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false,  borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false,  borderColor: "#0000ff" },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, data: arrValue3, fill: false,  borderColor: "#ff00ff" },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, data: arrValue4, fill: false,  borderColor: "#00ffff" }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });
                }

                else if (floorCount == 6) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false,  borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false,  borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false,  borderColor: "#0000ff" },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, data: arrValue3, fill: false,  borderColor: "#ff00ff" },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, data: arrValue4, fill: false,  borderColor: "#00ffff" },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, data: arrValue5, fill: false,  borderColor: "#ffa500" }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });

                }

                else if (floorCount == 7) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false,  borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false,  borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false,  borderColor: "#0000ff" },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, data: arrValue3, fill: false,  borderColor: "#ff00ff" },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, data: arrValue4, fill: false,  borderColor: "#00ffff" },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, data: arrValue5, fill: false,  borderColor: "#ffa500" },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, data: arrValue6, fill: false,  borderColor: "#008080" }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });

                }

                else if (floorCount == 8) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false,  borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false,  borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false,  borderColor: "#0000ff" },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, data: arrValue3, fill: false,  borderColor: "#ff00ff" },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, data: arrValue4, fill: false,  borderColor: "#00ffff" },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, data: arrValue5, fill: false,  borderColor: "#ffa500" },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, data: arrValue6, fill: false,  borderColor: "#008080" },
                            { label: dv_chart_groupData[7].s_FloorMapImageName, data: arrValue7, fill: false,  borderColor: "#a52a2a" }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });

                }

                else if (floorCount == 9) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false,  borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false,  borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false,  borderColor: "#0000ff" },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, data: arrValue3, fill: false,  borderColor: "#ff00ff" },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, data: arrValue4, fill: false,  borderColor: "#00ffff" },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, data: arrValue5, fill: false,  borderColor: "#ffa500" },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, data: arrValue6, fill: false,  borderColor: "#008080" },
                            { label: dv_chart_groupData[7].s_FloorMapImageName, data: arrValue7, fill: false,  borderColor: "#a52a2a" },
                            { label: dv_chart_groupData[8].s_FloorMapImageName, data: arrValue8, fill: false,  borderColor: "#e6e6fa" }
                            ]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });


                }

                else if (floorCount == 9) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false,  borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false,  borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false,  borderColor: "#0000ff" },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, data: arrValue3, fill: false,  borderColor: "#ff00ff" },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, data: arrValue4, fill: false, borderColor: "#00ffff" },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, data: arrValue5, fill: false, borderColor: "#ffa500" },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, data: arrValue6, fill: false, borderColor: "#008080" },
                            { label: dv_chart_groupData[7].s_FloorMapImageName, data: arrValue7, fill: false, borderColor: "#a52a2a" },
                            { label: dv_chart_groupData[8].s_FloorMapImageName, data: arrValue8, fill: false, borderColor: "#e6e6fa" },
                            { label: dv_chart_groupData[9].s_FloorMapImageName, data: arrValue8, fill: false, borderColor: "#808080" }
                            ]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });
                }
            }
        });
    });
}
function removeDups(names) {
    let unique = {};
    names.forEach(function (i) {
        if (!unique[i]) {
            unique[i] = true;
        }
    });
    return Object.keys(unique);
}
function fnGetAnalysisData2(intSiteId, dtStartDate, dtEndDate, intFloorId)  //Weekly-All Level
{
    if ((document.getElementById("hidStartDate").value != "") && (document.getElementById("hidEndDate").value != "")) {
        dtStartDate = document.getElementById("hidStartDate").value;
        dtEndDate = document.getElementById("hidEndDate").value;
    }
    document.getElementById("hidStartDate").value = dtStartDate;
    document.getElementById("hidEndDate").value = dtEndDate;
    $('#modalDashboard_ActivityAnalysis').modal();
    $('#tab_chartAnalysis').show();
    sleep(1000);
    document.getElementById("tab_chartAnalysis").innerHTML = "";
    var strLocation = document.getElementById("ContentPlaceHolder1_cmbLocation").value;
    document.getElementById("spLocation").innerHTML = strLocation;
    
    $(document).ready(function () {
        $.ajax({
            type: 'GET',
            url: '/services/dashboard.aspx?id=13&siteid=' + intSiteId + '&startdate=' + dtStartDate + '&enddate=' + dtEndDate + '&intFloorId=' + intFloorId,
            success: function (data) {
                document.getElementById("hidDashboardDataForChatAnalysis").value = data;

                var dv_chart_data = JSON.parse(data);
                var arrXAxis = new Array();
                var arrValue = new Array();
                var arrMin = new Array();
                var arrMax = new Array();
                //var strSensorIdDisplay = dv_chart_data[0].sensor_id;

                var tmp_count = 0;
                for (var i = 0; i < dv_chart_data.length; i++) {

                    var tmp = dv_chart_data[i].triggerWeek;
                    var tmpYear = Math.round(parseInt(tmp) / 100);
                    var tmpWeek = parseInt(tmp) % 100;
                    if (tmpWeek<=9)
                        arrXAxis.push(tmpYear + "-(W0" + tmpWeek + ")");
                    else
                        arrXAxis.push(tmpYear + "-(W" + tmpWeek + ")");
                    arrValue.push(dv_chart_data[i].weekCount);
                }

                var canvas = document.createElement('canvas');
                canvas.id = "canvas_chart";
                canvas.width = 900;
                canvas.height = 300;
                document.getElementById("tab_chartAnalysis").appendChild(canvas);

                document.getElementById("spChartHead").innerHTML = "Weekly Activity - All Level";
                document.getElementById("spLegend").innerHTML = "";

               
                var ctxP = document.getElementById("canvas_chart").getContext('2d');
                var myPieChart = new Chart(ctxP, {
                    type: 'line',
                    data: {
                        labels: arrXAxis,
                        datasets: [{ label: 'No. Of. Trigger', data: arrValue, fill: false, borderColor: "#FF0000" }]

                    },
                    options: {
                        scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                        responsive: true
                    }
                });

                
                document.getElementById("spLegend").innerHTML = "";

            }
        });
    });
}
function fnGetAnalysisData3(intSiteId, dtStartDate, dtEndDate, intFloorId)  //Weekly- Individual
{
    if ((document.getElementById("hidStartDate").value != "") && (document.getElementById("hidEndDate").value != "")) {
        dtStartDate = document.getElementById("hidStartDate").value;
        dtEndDate = document.getElementById("hidEndDate").value;
    }
    document.getElementById("hidStartDate").value = dtStartDate;
    document.getElementById("hidEndDate").value = dtEndDate;
    $('#modalDashboard_ActivityAnalysis').modal();
    $('#tab_chartAnalysis').show();
    sleep(1000);
    document.getElementById("tab_chartAnalysis").innerHTML = "";
    var strLocation = document.getElementById("ContentPlaceHolder1_cmbLocation").value;
    document.getElementById("spLocation").innerHTML = strLocation;

    

    $(document).ready(function () {
        $.ajax({
            type: 'GET',
            url: '/services/dashboard.aspx?id=20&siteid=' + intSiteId,
            success: function (data) {
                //responce
                document.getElementById("hidDashboardDataForFloorGroup").value = data;
            }
        });
    });

    $(document).ready(function () {
        $.ajax({
            type: 'GET',
            url: '/services/dashboard.aspx?id=21&siteid=' + intSiteId,
            success: function (data) {
                //responce
                document.getElementById("hidDashboardDataForFirstTirgger").value = data;
            }
        });
    });
    $(document).ready(function () {
        $.ajax({
            type: 'GET',
            url: '/services/dashboard.aspx?id=14&siteid=' + intSiteId + '&startdate=' + dtStartDate + '&enddate=' + dtEndDate + '&intFloorId=' + intFloorId,
            success: function (data) {
                document.getElementById("hidDashboardDataForChatAnalysis").value = data;

                var dv_chart_data = JSON.parse(data);

                var arrXAxis = new Array();
                var arrValue = new Array();
                var arrValue1 = new Array();
                var arrValue2 = new Array();
                var arrValue3 = new Array();
                var arrValue4 = new Array();
                var arrValue5 = new Array();
                var arrValue6 = new Array();
                var arrValue7 = new Array();
                var arrValue8 = new Array();
                var arrValue9 = new Array();


                var arrMin = new Array();
                var arrMax = new Array();
                var arrGroupPlan = new Array();

                var strNewData;
                //var strSensorIdDisplay = dv_chart_data[0].sensor_id;
                var dv_chart_groupData = JSON.parse(document.getElementById("hidDashboardDataForFloorGroup").value);
                x_flag = 0;

                var max_count = 0;

                var dtFrom = new Date(dtStartDate.replace(/-/g, "/"));
                var dtTo = new Date(dtEndDate.replace(/-/g, "/"));
                var weekDiff = parseInt((dtTo - dtFrom) / (86400000)/7);

                var floorCount = dv_chart_groupData.length;

                for (var a = getNumberOfWeek(dtFrom) - 1; a < getNumberOfWeek(dtFrom) + weekDiff; a++)
                {
                    var tmpYear = dtFrom.getFullYear();
                    var triggerWeek = a;
                    if (a > 52)
                    {
                        tmpYear = tmpYear + 1;
                        triggerWeek = triggerWeek % 52;
                    }

                    if (triggerWeek <= 9) {
                        arrXAxis.push(tmpYear + "-(W" + "0" + triggerWeek + ")");
                    }
                    else
                    {
                        arrXAxis.push(tmpYear + "-(W" + triggerWeek + ")");
                    }
                    arrValue.push(0);
                    arrValue1.push(0);
                    arrValue2.push(0);
                    arrValue3.push(0);
                    arrValue4.push(0);
                    arrValue5.push(0);
                    arrValue6.push(0);
                    arrValue7.push(0);
                    arrValue8.push(0);
                    arrValue9.push(0);
                }

                for (var i = 0; i < dv_chart_data.length; i++) { /// Ragu Changes 31 Aug 2018
                    
                    /*var dt1 = dv_chart_data[i].triggerWeek;
                    var dt2 = getNumberOfWeek(dtFrom);
                    var pos = 0;

                    pos = dt1 - dt2;

                    if (pos<=-1)
                    {
                        pos = (dt1 + 5) - dt2;
                    }*/


                    for (var arr_pos = 0; arr_pos < arrXAxis.length; arr_pos++)
                    {
                        var tmpTriggerWeek1 = "";
                        tmpTriggerWeek1= ""+dv_chart_data[i].triggerWeek;
                        var tmpTriggerWeek2= tmpTriggerWeek1;
                        tmpTriggerWeek2 = tmpTriggerWeek1.slice(0, 4);
                        tmpTriggerWeek2 = tmpTriggerWeek2 + "-(W";
                        tmpTriggerWeek2 = tmpTriggerWeek2 + tmpTriggerWeek1.slice(4, 6);
                        tmpTriggerWeek2 = tmpTriggerWeek2 + ")";
                        if (arrXAxis[arr_pos] == tmpTriggerWeek2)
                        {
                            pos = arr_pos;  // Which Week slot
                            for (floor_pos = 0; floor_pos < dv_chart_groupData.length; floor_pos++) {
                                if (dv_chart_groupData[floor_pos].s_FloorMapImageName == dv_chart_data[i].s_FloorMapImageName) {
                                    x = floor_pos; // Which Floor
                                }
                            }

                            if (x == 0)
                                arrValue[pos] = dv_chart_data[i].weekCount;
                            else if (x == 1)
                                arrValue1[pos] = dv_chart_data[i].weekCount;
                            else if (x == 2)
                                arrValue2[pos] = dv_chart_data[i].weekCount;
                            else if (x == 3)
                                arrValue3[pos] = dv_chart_data[i].weekCount;
                            else if (x == 4)
                                arrValue4[pos] = dv_chart_data[i].weekCount;
                            else if (x == 5)
                                arrValue5[pos] = dv_chart_data[i].weekCount;
                            else if (x == 6)
                                arrValue6[pos] = dv_chart_data[i].weekCount;
                            else if (x == 7)
                                arrValue7[pos] = dv_chart_data[i].weekCount;
                            else if (x == 8)
                                arrValue8[pos] = dv_chart_data[i].weekCount;
                            else if (x == 9)
                                arrValue9[pos] = dv_chart_data[i].weekCount;
                         
                        }
                    }
                }

                var uniquearrXAxis = removeDups(arrXAxis);

                var canvas = document.createElement('canvas');
                canvas.id = "canvas_chart";
                canvas.width = 900;
                canvas.height = 300;
                document.getElementById("tab_chartAnalysis").appendChild(canvas);

                document.getElementById("spChartHead").innerHTML = "Weekly Activity - Group By Individual Level";
                document.getElementById("spLegend").innerHTML = "";

                var ctxP = document.getElementById("canvas_chart").getContext('2d');

                if (floorCount == 1) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{
                                label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false, borderColor: "#ff0000"
                            }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });

                }
                else if (floorCount == 2) {
                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{
                                label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false, borderColor: "#ff0000"
                            },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false, borderColor: "#00ff00" }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });


                }

                else if (floorCount == 3) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false, borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false, borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false, borderColor: "#0000ff" }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });

                }
                else if (floorCount == 4) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false, borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false, borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false, borderColor: "#0000ff" },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, data: arrValue3, fill: false, borderColor: "#ff00ff" }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });
                }

                else if (floorCount == 5) {
                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false, borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false, borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false, borderColor: "#0000ff" },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, data: arrValue3, fill: false, borderColor: "#ff00ff" },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, data: arrValue4, fill: false, borderColor: "#00ffff" }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });
                }

                else if (floorCount == 6) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false, borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false, borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false, borderColor: "#0000ff" },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, data: arrValue3, fill: false, borderColor: "#ff00ff" },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, data: arrValue4, fill: false, borderColor: "#00ffff" },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, data: arrValue5, fill: false, borderColor: "#ffa500" }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });

                }

                else if (floorCount == 7) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false, borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false, borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false, borderColor: "#0000ff" },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, data: arrValue3, fill: false, borderColor: "#ff00ff" },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, data: arrValue4, fill: false, borderColor: "#00ffff" },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, data: arrValue5, fill: false, borderColor: "#ffa500" },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, data: arrValue6, fill: false, borderColor: "#008080" }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });

                }

                else if (floorCount == 8) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false, borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false, borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false, borderColor: "#0000ff" },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, data: arrValue3, fill: false, borderColor: "#ff00ff" },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, data: arrValue4, fill: false, borderColor: "#00ffff" },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, data: arrValue5, fill: false, borderColor: "#ffa500" },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, data: arrValue6, fill: false, borderColor: "#008080" },
                            { label: dv_chart_groupData[7].s_FloorMapImageName, data: arrValue7, fill: false, borderColor: "#a52a2a" }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });

                }

                else if (floorCount == 9) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false, borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false, borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false, borderColor: "#0000ff" },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, data: arrValue3, fill: false, borderColor: "#ff00ff" },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, data: arrValue4, fill: false, borderColor: "#00ffff" },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, data: arrValue5, fill: false, borderColor: "#ffa500" },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, data: arrValue6, fill: false, borderColor: "#008080" },
                            { label: dv_chart_groupData[7].s_FloorMapImageName, data: arrValue7, fill: false, borderColor: "#a52a2a" },
                            { label: dv_chart_groupData[8].s_FloorMapImageName, data: arrValue8, fill: false, borderColor: "#e6e6fa" }
                            ]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });


                }

                else if (floorCount == 9) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false, borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false, borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false, borderColor: "#0000ff" },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, data: arrValue3, fill: false, borderColor: "#ff00ff" },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, data: arrValue4, fill: false, borderColor: "#00ffff" },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, data: arrValue5, fill: false, borderColor: "#ffa500" },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, data: arrValue6, fill: false, borderColor: "#008080" },
                            { label: dv_chart_groupData[7].s_FloorMapImageName, data: arrValue7, fill: false, borderColor: "#a52a2a" },
                            { label: dv_chart_groupData[8].s_FloorMapImageName, data: arrValue8, fill: false, borderColor: "#e6e6fa" },
                            { label: dv_chart_groupData[9].s_FloorMapImageName, data: arrValue8, fill: false, borderColor: "#808080" }
                            ]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });
                }

            }
        });
    });
}
function fnGetAnalysisData4(intSiteId, dtStartDate, dtEndDate, intFloorId)  //Monthly-All Level
{
    if ((document.getElementById("hidStartDate").value != "") && (document.getElementById("hidEndDate").value != "")) {
        dtStartDate = document.getElementById("hidStartDate").value;
        dtEndDate = document.getElementById("hidEndDate").value;
    }
    document.getElementById("hidStartDate").value = dtStartDate;
    document.getElementById("hidEndDate").value = dtEndDate;
    $('#modalDashboard_ActivityAnalysis').modal();
    $('#tab_chartAnalysis').show();
    sleep(1000);
    document.getElementById("tab_chartAnalysis").innerHTML = "";
    var strLocation = document.getElementById("ContentPlaceHolder1_cmbLocation").value;
    document.getElementById("spLocation").innerHTML = strLocation;
    
    
    $(document).ready(function () {
        $.ajax({
            type: 'GET',
            url: '/services/dashboard.aspx?id=15&siteid=' + intSiteId + '&startdate=' + dtStartDate + '&enddate=' + dtEndDate + '&intFloorId=' + intFloorId,
            success: function (data) {
                document.getElementById("hidDashboardDataForChatAnalysis").value = data;

                var dv_chart_data = JSON.parse(data);
                var arrXAxis = new Array();
                var arrValue = new Array();
                var arrMin = new Array();
                var arrMax = new Array();
                //var strSensorIdDisplay = dv_chart_data[0].sensor_id;

                var tmp_count = 0;
                for (var i = 0; i < dv_chart_data.length; i++) {

                    arrXAxis.push(dv_chart_data[i].triggerMonth);
                    arrValue.push(dv_chart_data[i].monthCount);
                }

                var canvas = document.createElement('canvas');
                canvas.id = "canvas_chart";
                canvas.width = 900;
                canvas.height = 300;
                document.getElementById("tab_chartAnalysis").appendChild(canvas);

                document.getElementById("spChartHead").innerHTML = "Monthly Activity - All Level";
                document.getElementById("spLegend").innerHTML = "";

                var ctxP = document.getElementById("canvas_chart").getContext('2d');
                var myPieChart = new Chart(ctxP, {
                    type: 'line',
                    data: {
                        labels: arrXAxis,
                        datasets: [{ label: 'No. Of. Trigger', data: arrValue, fill: false, borderColor: "#FF0000" }]

                    },
                    options: {
                        scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                        responsive: true
                    }
                });


                document.getElementById("spLegend").innerHTML = "";

            }
        });
    });
}
function fnGetAnalysisData5(intSiteId, dtStartDate, dtEndDate, intFloorId)  //Monthly- Individual
{
    if ((document.getElementById("hidStartDate").value != "") && (document.getElementById("hidEndDate").value != "")) {
        dtStartDate = document.getElementById("hidStartDate").value;
        dtEndDate = document.getElementById("hidEndDate").value;
    }
    document.getElementById("hidStartDate").value = dtStartDate;
    document.getElementById("hidEndDate").value = dtEndDate;
    $('#modalDashboard_ActivityAnalysis').modal();
    $('#tab_chartAnalysis').show();
    sleep(1000);
    document.getElementById("tab_chartAnalysis").innerHTML = "";
    var strLocation = document.getElementById("ContentPlaceHolder1_cmbLocation").value;
    document.getElementById("spLocation").innerHTML = strLocation;

    var txtLegend;
    $(document).ready(function () {
        $.ajax({
            type: 'GET',
            url: '/services/dashboard.aspx?id=20&siteid=' + intSiteId,
            success: function (data) {
                //responce
                document.getElementById("hidDashboardDataForFloorGroup").value = data;
            }
        });
    });

    

    $(document).ready(function () {
        $.ajax({
            type: 'GET',
            url: '/services/dashboard.aspx?id=16&siteid=' + intSiteId + '&startdate=' + dtStartDate + '&enddate=' + dtEndDate + '&intFloorId=' + intFloorId,
            success: function (data) {
                document.getElementById("hidDashboardDataForChatAnalysis").value = data;

                var dv_chart_data = JSON.parse(data);

                var arrXAxis = new Array();
                var arrValue = new Array();
                var arrValue1 = new Array();
                var arrValue2 = new Array();
                var arrValue3 = new Array();
                var arrValue4 = new Array();
                var arrValue5 = new Array();
                var arrValue6 = new Array();
                var arrValue7 = new Array();
                var arrValue8 = new Array();
                var arrValue9 = new Array();
                
                var arrMin = new Array();
                var arrMax = new Array();

                var arrGroupPlan = new Array();

                var strNewData;
                //var strSensorIdDisplay = dv_chart_data[0].sensor_id;
                var dv_chart_groupData = JSON.parse(document.getElementById("hidDashboardDataForFloorGroup").value);
                x_flag = 0;

                var floorCount = dv_chart_groupData.length;

                var dtFrom = new Date(dtStartDate.replace(/-/g, "/"));
                var dtTo = new Date(dtEndDate.replace(/-/g, "/"));

                var st1;
                if (dtFrom.getMonth() + 1 <= 9) {
                    st1 = dtFrom.getFullYear() + "0" + (dtFrom.getMonth()+1);
                }
                else {
                    st1 = dtFrom.getFullYear() + "" + (dtFrom.getMonth()+1);
                }
               
                var i_st1 = parseInt(st1);

                var st2;
                if (dtTo.getMonth() + 1 <= 9) {
                    st2 = dtTo.getFullYear() + "0" + (dtTo.getMonth()+1);
                }
                else {
                    st2 = dtTo.getFullYear() + "" + (dtTo.getMonth()+1);
                }
                var i_st2 = parseInt(st2);

                
                for (var a = i_st1; a <= i_st2; a++)
                {
                    if (a % 100 == 13)
                    {
                        a = a + 88;
                    }

                    var tmpYear = Math.round(a / 100);
                    var tmpMonth = a % 100;

                    if (tmpMonth <= 9) {
                        arrXAxis.push(tmpYear + "-" + "0" + tmpMonth + "");
                    }
                    else {
                        arrXAxis.push(tmpYear + "-" + tmpMonth + "");
                    }
                    arrValue.push(0);
                    arrValue1.push(0);
                    arrValue2.push(0);
                    arrValue3.push(0);
                    arrValue4.push(0);
                    arrValue5.push(0);
                    arrValue6.push(0);
                    arrValue7.push(0);
                    arrValue8.push(0);
                    arrValue9.push(0);
                }

                var x;
                for (var i = 0; i < dv_chart_data.length; i++) {

                    for (var arr_pos = 0; arr_pos < arrXAxis.length; arr_pos++) {

                        if (arrXAxis[arr_pos] == dv_chart_data[i].triggerMonth) {

                            pos = arr_pos;  // Which Month slot
                            for (floor_pos = 0; floor_pos < dv_chart_groupData.length; floor_pos++) {
                                if (dv_chart_groupData[floor_pos].s_FloorMapImageName == dv_chart_data[i].s_FloorMapImageName) {
                                    x = floor_pos; // Which Floor
                                }
                            }
  
                            if (x == 0)
                                arrValue[pos]=dv_chart_data[i].monthCount;
                            else if (x == 1)
                                arrValue1[pos]=dv_chart_data[i].monthCount;
                            else if (x == 2)
                                arrValue2[pos]=dv_chart_data[i].monthCount;
                            else if (x == 3)
                                arrValue3[pos]=dv_chart_data[i].monthCount;
                            else if (x == 4)
                                arrValue4[pos]=dv_chart_data[i].monthCount;
                            else if (x == 5)
                                arrValue5[pos]=dv_chart_data[i].monthCount;
                            else if (x == 6)
                                arrValue6[pos]=dv_chart_data[i].monthCount;
                            else if (x == 7)
                                arrValue7[pos]=dv_chart_data[i].monthCount;
                            else if (x == 8)
                                arrValue8[pos]=dv_chart_data[i].monthCount;
                            else if (x == 9)
                                arrValue9[pos]=dv_chart_data[i].monthCount;

                        }
                    }
                }
                var uniquearrXAxis = removeDups(arrXAxis);

                var canvas = document.createElement('canvas');
                canvas.id = "canvas_chart";
                canvas.width = 900;
                canvas.height = 300;
                document.getElementById("tab_chartAnalysis").appendChild(canvas);

                document.getElementById("spChartHead").innerHTML = "Monthly Activity - Group By Individual Level";
                document.getElementById("spLegend").innerHTML = "";
                
                var ctxP = document.getElementById("canvas_chart").getContext('2d');

                if (floorCount == 1) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{
                                label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false, borderColor: "#ff0000"
                            }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });

                }
                else if (floorCount == 2) {
                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{
                                label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false, borderColor: "#ff0000"
                            },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false, borderColor: "#00ff00" }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });


                }

                else if (floorCount == 3) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false, borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false, borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false, borderColor: "#0000ff" }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });

                }
                else if (floorCount == 4) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false, borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false, borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false, borderColor: "#0000ff" },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, data: arrValue3, fill: false, borderColor: "#ff00ff" }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });
                }

                else if (floorCount == 5) {
                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false, borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false, borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false, borderColor: "#0000ff" },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, data: arrValue3, fill: false, borderColor: "#ff00ff" },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, data: arrValue4, fill: false, borderColor: "#00ffff" }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });
                }

                else if (floorCount == 6) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false, borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false, borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false, borderColor: "#0000ff" },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, data: arrValue3, fill: false, borderColor: "#ff00ff" },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, data: arrValue4, fill: false, borderColor: "#00ffff" },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, data: arrValue5, fill: false, borderColor: "#ffa500" }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });

                }

                else if (floorCount == 7) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false, borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false, borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false, borderColor: "#0000ff" },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, data: arrValue3, fill: false, borderColor: "#ff00ff" },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, data: arrValue4, fill: false, borderColor: "#00ffff" },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, data: arrValue5, fill: false, borderColor: "#ffa500" },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, data: arrValue6, fill: false, borderColor: "#008080" }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });

                }

                else if (floorCount == 8) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false, borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false, borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false, borderColor: "#0000ff" },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, data: arrValue3, fill: false, borderColor: "#ff00ff" },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, data: arrValue4, fill: false, borderColor: "#00ffff" },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, data: arrValue5, fill: false, borderColor: "#ffa500" },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, data: arrValue6, fill: false, borderColor: "#008080" },
                            { label: dv_chart_groupData[7].s_FloorMapImageName, data: arrValue7, fill: false, borderColor: "#a52a2a" }]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });

                }

                else if (floorCount == 9) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false, borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false, borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false, borderColor: "#0000ff" },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, data: arrValue3, fill: false, borderColor: "#ff00ff" },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, data: arrValue4, fill: false, borderColor: "#00ffff" },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, data: arrValue5, fill: false, borderColor: "#ffa500" },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, data: arrValue6, fill: false, borderColor: "#008080" },
                            { label: dv_chart_groupData[7].s_FloorMapImageName, data: arrValue7, fill: false, borderColor: "#a52a2a" },
                            { label: dv_chart_groupData[8].s_FloorMapImageName, data: arrValue8, fill: false, borderColor: "#e6e6fa" }
                            ]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });


                }

                else if (floorCount == 9) {

                    var myPieChart = new Chart(ctxP, {
                        type: 'line',
                        data: {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, data: arrValue, fill: false, borderColor: "#ff0000" },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, data: arrValue1, fill: false, borderColor: "#00ff00" },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, data: arrValue2, fill: false, borderColor: "#0000ff" },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, data: arrValue3, fill: false, borderColor: "#ff00ff" },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, data: arrValue4, fill: false, borderColor: "#00ffff" },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, data: arrValue5, fill: false, borderColor: "#ffa500" },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, data: arrValue6, fill: false, borderColor: "#008080" },
                            { label: dv_chart_groupData[7].s_FloorMapImageName, data: arrValue7, fill: false, borderColor: "#a52a2a" },
                            { label: dv_chart_groupData[8].s_FloorMapImageName, data: arrValue8, fill: false, borderColor: "#e6e6fa" },
                            { label: dv_chart_groupData[9].s_FloorMapImageName, data: arrValue8, fill: false, borderColor: "#808080" }
                            ]

                        },
                        options: {
                            scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
                            responsive: true
                        }
                    });
                }
            }
        });
    });
}

function fnCallViewPathHttp(intSiteId, dtStartDate, dtEndDate, intFloorId)  //Monthly- Individual
{
    if ((document.getElementById("hidStartDate").value != "") && (document.getElementById("hidEndDate").value != "")) {
        dtStartDate = document.getElementById("hidStartDate").value;
        dtEndDate = document.getElementById("hidEndDate").value;
    }
    document.getElementById("hidStartDate").value = dtStartDate;
    document.getElementById("hidEndDate").value = dtEndDate;
    
    var strLocation = document.getElementById("ContentPlaceHolder1_cmbLocation").value;
    document.getElementById("spLocation").innerHTML = strLocation;

    $(document).ready(function () {
        $.ajax({
            type: 'GET',
            url: '/services/dashboard.aspx?id=22&siteid=' + intSiteId + '&startdate=' + dtStartDate + '&enddate=' + dtEndDate + '&intFloorId=' + intFloorId,
            success: function (data) {
                //responce
                
                var dv_pathWay = JSON.parse(data);
                //pathwayPost('http://pestech.ddns.net:8087/harbourfront/lap/index.php/site/data', dv_pathWay);  //Old Code
                pathwayPost('http://pestech.ddns.net:8087/Pestech/lap/index.php/site/data', dv_pathWay);
                
            }
        });
    });

}

function pathwayPost(path, params, method) {
    method = method || "post"; // Set method to post by default if not specified.

    // The rest of this code assumes you are not using a library.
    // It can be made less wordy if you use one.
    var form = document.createElement("form");
    form.setAttribute("method", method);
    form.setAttribute("action", path);

    for (var key in params) {
        if (params.hasOwnProperty(key)) {
            var hiddenField = document.createElement("input");
            hiddenField.setAttribute("type", "hidden");
            hiddenField.setAttribute("name", key);
            hiddenField.setAttribute("value", JSON.stringify(params[key]));

            form.appendChild(hiddenField);
        }
    }

    document.body.appendChild(form);
    form.submit();
}

