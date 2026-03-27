function fnGetRodentAnalysisData0(intSiteId, dtStartDate, dtEndDate, intFloorId)  //Daily-All Level
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
            url: '/services/dashboard.aspx?id=25&siteid=' + intSiteId + '&startdate=' + dtStartDate + '&enddate=' + dtEndDate + '&intFloorId=' + intFloorId,
            success: function (data) {
                document.getElementById("hidDashboardDataForChatAnalysis").value = data;

                var dv_chart_data = JSON.parse(data);
                var arrXAxis = new Array();
                var arrValue = new Array();
                var arrMin = new Array();
                var arrMax = new Array();
                var strSensorIdDisplay = dv_chart_data[0].sensor_id;


                var dtFrom = new Date(dtStartDate.replace(/-/g, "/"));
                var dtTo = new Date(dtEndDate.replace(/-/g, "/"));
                var duration_period = parseInt((dtTo - dtFrom) / (1000 * 60 * 60 * 24));


                var tmp_count = 0;
                var tot_count = dv_chart_data.length;
                for (var i = 0; i < tot_count; i++) { /// Ragu Changes 31 Aug 2018
                    if ((i == 0)) {
                        var startDate_tmp = dtFrom.getFullYear() + "/" + leadingZero((dtFrom.getMonth() + 1)) + "/" + leadingZero(dtFrom.getDate());
                        if ((dv_chart_data[i].triggerDate) == startDate_tmp) {
                            arrXAxis.push(dv_chart_data[i].triggerDate);
                            arrValue.push(dv_chart_data[i].dayCount);
                        }
                        else {
                            var cur_date = new Date(dv_chart_data[0].triggerDate);
                            var pre_date = new Date(startDate_tmp);
                            var date_diff = parseInt((cur_date - pre_date) / (1000 * 60 * 60 * 24));

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
                        var date_diff = parseInt((cur_date - pre_date) / (1000 * 60 * 60 * 24));
                        if (date_diff == 1) {
                            arrXAxis.push(dv_chart_data[i].triggerDate);
                            arrValue.push(dv_chart_data[i].dayCount);
                        }
                        else {
                            for (var k = 1; k <= date_diff; k++) {
                                var addOnDate = new Date(pre_date);
                                var newdate = new Date(addOnDate);
                                newdate.setDate(addOnDate.getDate() + k);

                                arrXAxis.push(newdate.getFullYear() + "/" + leadingZero((newdate.getMonth() + 1)) + "/" + leadingZero(newdate.getDate()));
                                arrValue.push(0);
                            }
                        }

                        if ((i == tot_count - 1)) {
                            var cur_date = new Date(dtTo);
                            var pre_date = new Date(dv_chart_data[i].triggerDate);
                            var date_diff = parseInt((cur_date - pre_date) / (1000 * 60 * 60 * 24));

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

                var canvas = document.createElement('canvas');
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
                document.getElementById("spLegend").innerHTML = txtLegend;

                

            }
        });
    });
}

function fnGetRodentAnalysisData1(intSiteId, dtStartDate, dtEndDate, intFloorId)  //Daily- Individual
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
            url: '/services/dashboard.aspx?id=26&siteid=' + intSiteId + '&startdate=' + dtStartDate + '&enddate=' + dtEndDate + '&intFloorId=' + intFloorId,
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
                var duration_period = parseInt((dtTo - dtFrom) / (1000 * 60 * 60 * 24));

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

                for (var i = 0; i < dv_chart_data.length; i++) { /// Ragu Changes 31 Aug 2018
                    //arrXAxis.push(jsDateToNormalDate(newdate));
                    for (vartmp = 0; vartmp < floorCount; vartmp++) {
                        if (dv_chart_groupData[vartmp].s_FloorMapImageName == dv_chart_data[i].s_FloorMapImageName)
                            x = vartmp;
                    }

                    var dt1 = new Date(dv_chart_data[i].triggerDate);
                    var dt2 = new Date(dtStartDate.replace(/-/g, "/"));
                    var pos = parseInt((dt1 - dt2) / (1000 * 60 * 60 * 24));

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

                if (floorCount == 1) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li></ul>";
                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }

                    var ctx = document.getElementById("canvas_chart").getContext("2d");
                    var myLineBarChart = new Chart(ctx).Overlay(barChartData);
                }
                else if (floorCount == 2) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li></ul>";
                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }

                    var ctx = document.getElementById("canvas_chart").getContext("2d");
                    var myLineBarChart = new Chart(ctx).Overlay(barChartData);
                }

                else if (floorCount == 3) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li></ul>";


                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 }],
                            options: { legend: { display: true, position: 'bottom' } }

                        }

                    var ctx = document.getElementById("canvas_chart").getContext("2d");
                    var myLineBarChart = new Chart(ctx).Overlay(barChartData);
                }
                else if (floorCount == 4) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li><span class='pink'></span> " + dv_chart_groupData[3].s_FloorMapImageName + "</li></ul>";

                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff00ff", pointColor: "#ff00ff", pointStrokeColor: "#ff00ff", pointHighlightFill: "#ff00ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue3 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }
                }

                else if (floorCount == 5) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li><span class='pink'></span> " + dv_chart_groupData[3].s_FloorMapImageName + "</li><li><span class='lightblue'></span> " + dv_chart_groupData[4].s_FloorMapImageName + "</li></ul>";

                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff00ff", pointColor: "#ff00ff", pointStrokeColor: "#ff00ff", pointHighlightFill: "#ff00ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue3 },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ffff", pointColor: "#00ffff", pointStrokeColor: "#00ffff", pointHighlightFill: "#00ffff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue4 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }
                }

                else if (floorCount == 6) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li><span class='pink'></span> " + dv_chart_groupData[3].s_FloorMapImageName + "</li><li><span class='lightblue'></span> " + dv_chart_groupData[4].s_FloorMapImageName + "</li><li><span class='orange'></span> " + dv_chart_groupData[5].s_FloorMapImageName + "</li></ul>";

                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff00ff", pointColor: "#ff00ff", pointStrokeColor: "#ff00ff", pointHighlightFill: "#ff00ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue3 },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ffff", pointColor: "#00ffff", pointStrokeColor: "#00ffff", pointHighlightFill: "#00ffff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue4 },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ffa500", pointColor: "#ffa500", pointStrokeColor: "#ffa500", pointHighlightFill: "#ffa500", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue5 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }
                }

                else if (floorCount == 7) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li><span class='pink'></span> " + dv_chart_groupData[3].s_FloorMapImageName + "</li><li><span class='lightblue'></span> " + dv_chart_groupData[4].s_FloorMapImageName + "</li><li><span class='orange'></span> " + dv_chart_groupData[5].s_FloorMapImageName + "</li><li><span class='teal'></span> " + dv_chart_groupData[6].s_FloorMapImageName + "</li></ul>";

                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff00ff", pointColor: "#ff00ff", pointStrokeColor: "#ff00ff", pointHighlightFill: "#ff00ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue3 },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ffff", pointColor: "#00ffff", pointStrokeColor: "#00ffff", pointHighlightFill: "#00ffff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue4 },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ffa500", pointColor: "#ffa500", pointStrokeColor: "#ffa500", pointHighlightFill: "#ffa500", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue5 },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#008080", pointColor: "#008080", pointStrokeColor: "#008080", pointHighlightFill: "#008080", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue6 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }
                }

                else if (floorCount == 8) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li><span class='pink'></span> " + dv_chart_groupData[3].s_FloorMapImageName + "</li><li><span class='lightblue'></span> " + dv_chart_groupData[4].s_FloorMapImageName + "</li><li><span class='orange'></span> " + dv_chart_groupData[5].s_FloorMapImageName + "</li><li><span class='teal'></span> " + dv_chart_groupData[6].s_FloorMapImageName + "</li><li><span class='brown'></span> " + dv_chart_groupData[7].s_FloorMapImageName + "</li></ul>";

                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff00ff", pointColor: "#ff00ff", pointStrokeColor: "#ff00ff", pointHighlightFill: "#ff00ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue3 },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ffff", pointColor: "#00ffff", pointStrokeColor: "#00ffff", pointHighlightFill: "#00ffff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue4 },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ffa500", pointColor: "#ffa500", pointStrokeColor: "#ffa500", pointHighlightFill: "#ffa500", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue5 },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#008080", pointColor: "#008080", pointStrokeColor: "#008080", pointHighlightFill: "#008080", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue6 },
                            { label: dv_chart_groupData[7].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#a52a2a", pointColor: "#a52a2a", pointStrokeColor: "#a52a2a", pointHighlightFill: "#a52a2a", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue7 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }
                }

                else if (floorCount == 9) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li><span class='pink'></span> " + dv_chart_groupData[3].s_FloorMapImageName + "</li><li><span class='lightblue'></span> " + dv_chart_groupData[4].s_FloorMapImageName + "</li><li><span class='orange'></span> " + dv_chart_groupData[5].s_FloorMapImageName + "</li><li><span class='teal'></span> " + dv_chart_groupData[6].s_FloorMapImageName + "</li><li><span class='brown'></span> " + dv_chart_groupData[7].s_FloorMapImageName + "</li><li><span class='lavender'></span> " + dv_chart_groupData[8].s_FloorMapImageName + "</li></ul>";

                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff00ff", pointColor: "#ff00ff", pointStrokeColor: "#ff00ff", pointHighlightFill: "#ff00ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue3 },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ffff", pointColor: "#00ffff", pointStrokeColor: "#00ffff", pointHighlightFill: "#00ffff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue4 },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ffa500", pointColor: "#ffa500", pointStrokeColor: "#ffa500", pointHighlightFill: "#ffa500", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue5 },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#008080", pointColor: "#008080", pointStrokeColor: "#008080", pointHighlightFill: "#008080", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue6 },
                            { label: dv_chart_groupData[7].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#a52a2a", pointColor: "#a52a2a", pointStrokeColor: "#a52a2a", pointHighlightFill: "#a52a2a", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue7 },
                            { label: dv_chart_groupData[8].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#e6e6fa", pointColor: "#e6e6fa", pointStrokeColor: "#e6e6fa", pointHighlightFill: "#e6e6fa", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue8 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }
                }

                else if (floorCount == 9) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li><span class='pink'></span> " + dv_chart_groupData[3].s_FloorMapImageName + "</li><li><span class='lightblue'></span> " + dv_chart_groupData[4].s_FloorMapImageName + "</li><li><span class='orange'></span> " + dv_chart_groupData[5].s_FloorMapImageName + "</li><li><span class='teal'></span> " + dv_chart_groupData[6].s_FloorMapImageName + "</li><li><span class='brown'></span> " + dv_chart_groupData[7].s_FloorMapImageName + "</li><li><span class='lavender'></span> " + dv_chart_groupData[8].s_FloorMapImageName + "</li><li><span class='gray'></span> " + dv_chart_groupData[9].s_FloorMapImageName + "</li></ul>";

                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff00ff", pointColor: "#ff00ff", pointStrokeColor: "#ff00ff", pointHighlightFill: "#ff00ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue3 },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ffff", pointColor: "#00ffff", pointStrokeColor: "#00ffff", pointHighlightFill: "#00ffff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue4 },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ffa500", pointColor: "#ffa500", pointStrokeColor: "#ffa500", pointHighlightFill: "#ffa500", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue5 },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#008080", pointColor: "#008080", pointStrokeColor: "#008080", pointHighlightFill: "#008080", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue6 },
                            { label: dv_chart_groupData[7].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#a52a2a", pointColor: "#a52a2a", pointStrokeColor: "#a52a2a", pointHighlightFill: "#a52a2a", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue7 },
                            { label: dv_chart_groupData[8].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#e6e6fa", pointColor: "#e6e6fa", pointStrokeColor: "#e6e6fa", pointHighlightFill: "#e6e6fa", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue8 },
                            { label: dv_chart_groupData[9].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#808080", pointColor: "#e6e6fa", pointStrokeColor: "#e6e6fa", pointHighlightFill: "#e6e6fa", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue8 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }
                }
                var ctx = document.getElementById("canvas_chart").getContext("2d");
                var myLineBarChart = new Chart(ctx).Overlay(barChartData);
                document.getElementById("spLegend").innerHTML = txtLegend;
                document.getElementById("spChartHead").innerHTML = "Daily Activity for Individual Level";

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

function fnGetRodentAnalysisData2(intSiteId, dtStartDate, dtEndDate, intFloorId)  //Weekly-All Level
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
            url: '/services/dashboard.aspx?id=27&siteid=' + intSiteId + '&startdate=' + dtStartDate + '&enddate=' + dtEndDate + '&intFloorId=' + intFloorId,
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
                    if (tmpWeek <= 9)
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
                document.getElementById("spChartHead").innerHTML = "Weekly Activity for All Levels";
                txtLegend = "<ul class='legend'><li><span class='red'></span>All Floors</li></ul>";
                document.getElementById("spLegend").innerHTML = txtLegend;

            }
        });
    });
}
function fnGetRodentAnalysisData3(intSiteId, dtStartDate, dtEndDate, intFloorId)  //Weekly- Individual
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
            url: '/services/dashboard.aspx?id=28&siteid=' + intSiteId + '&startdate=' + dtStartDate + '&enddate=' + dtEndDate + '&intFloorId=' + intFloorId,
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
                var weekDiff = parseInt((dtTo - dtFrom) / (1000 * 60 * 60 * 24) / 7);

                var floorCount = dv_chart_groupData.length;

                for (var a = getNumberOfWeek(dtFrom) - 1; a < getNumberOfWeek(dtFrom) + weekDiff; a++) {
                    var tmpYear = dtFrom.getFullYear();
                    var triggerWeek = a;
                    if (a > 52) {
                        tmpYear = tmpYear + 1;
                        triggerWeek = triggerWeek % 52;
                    }

                    if (triggerWeek <= 9) {
                        arrXAxis.push(tmpYear + "-(W" + "0" + triggerWeek + ")");
                    }
                    else {
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


                    for (var arr_pos = 0; arr_pos < arrXAxis.length; arr_pos++) {
                        var tmpTriggerWeek1 = "";
                        tmpTriggerWeek1 = "" + dv_chart_data[i].triggerWeek;
                        var tmpTriggerWeek2 = tmpTriggerWeek1;
                        tmpTriggerWeek2 = tmpTriggerWeek1.slice(0, 4);
                        tmpTriggerWeek2 = tmpTriggerWeek2 + "-(W";
                        tmpTriggerWeek2 = tmpTriggerWeek2 + tmpTriggerWeek1.slice(4, 6);
                        tmpTriggerWeek2 = tmpTriggerWeek2 + ")";
                        if (arrXAxis[arr_pos] == tmpTriggerWeek2) {
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

                if (floorCount == 1) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li></ul>";
                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }

                    var ctx = document.getElementById("canvas_chart").getContext("2d");
                    var myLineBarChart = new Chart(ctx).Overlay(barChartData);
                }
                else if (floorCount == 2) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li></ul>";
                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }

                    var ctx = document.getElementById("canvas_chart").getContext("2d");
                    var myLineBarChart = new Chart(ctx).Overlay(barChartData);
                }

                else if (floorCount == 3) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li></ul>";


                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 }],
                            options: { legend: { display: true, position: 'bottom' } }

                        }

                    var ctx = document.getElementById("canvas_chart").getContext("2d");
                    var myLineBarChart = new Chart(ctx).Overlay(barChartData);
                }
                else if (floorCount == 4) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li><span class='pink'></span> " + dv_chart_groupData[3].s_FloorMapImageName + "</li></ul>";

                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff00ff", pointColor: "#ff00ff", pointStrokeColor: "#ff00ff", pointHighlightFill: "#ff00ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue3 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }
                }

                else if (floorCount == 5) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li><span class='pink'></span> " + dv_chart_groupData[3].s_FloorMapImageName + "</li><li><span class='lightblue'></span> " + dv_chart_groupData[4].s_FloorMapImageName + "</li></ul>";

                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff00ff", pointColor: "#ff00ff", pointStrokeColor: "#ff00ff", pointHighlightFill: "#ff00ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue3 },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ffff", pointColor: "#00ffff", pointStrokeColor: "#00ffff", pointHighlightFill: "#00ffff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue4 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }
                }

                else if (floorCount == 6) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li><span class='pink'></span> " + dv_chart_groupData[3].s_FloorMapImageName + "</li><li><span class='lightblue'></span> " + dv_chart_groupData[4].s_FloorMapImageName + "</li><li><span class='orange'></span> " + dv_chart_groupData[5].s_FloorMapImageName + "</li></ul>";

                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff00ff", pointColor: "#ff00ff", pointStrokeColor: "#ff00ff", pointHighlightFill: "#ff00ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue3 },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ffff", pointColor: "#00ffff", pointStrokeColor: "#00ffff", pointHighlightFill: "#00ffff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue4 },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ffa500", pointColor: "#ffa500", pointStrokeColor: "#ffa500", pointHighlightFill: "#ffa500", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue5 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }
                }

                else if (floorCount == 7) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li><span class='pink'></span> " + dv_chart_groupData[3].s_FloorMapImageName + "</li><li><span class='lightblue'></span> " + dv_chart_groupData[4].s_FloorMapImageName + "</li><li><span class='orange'></span> " + dv_chart_groupData[5].s_FloorMapImageName + "</li><li><span class='teal'></span> " + dv_chart_groupData[6].s_FloorMapImageName + "</li></ul>";

                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff00ff", pointColor: "#ff00ff", pointStrokeColor: "#ff00ff", pointHighlightFill: "#ff00ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue3 },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ffff", pointColor: "#00ffff", pointStrokeColor: "#00ffff", pointHighlightFill: "#00ffff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue4 },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ffa500", pointColor: "#ffa500", pointStrokeColor: "#ffa500", pointHighlightFill: "#ffa500", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue5 },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#008080", pointColor: "#008080", pointStrokeColor: "#008080", pointHighlightFill: "#008080", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue6 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }
                }

                else if (floorCount == 8) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li><span class='pink'></span> " + dv_chart_groupData[3].s_FloorMapImageName + "</li><li><span class='lightblue'></span> " + dv_chart_groupData[4].s_FloorMapImageName + "</li><li><span class='orange'></span> " + dv_chart_groupData[5].s_FloorMapImageName + "</li><li><span class='teal'></span> " + dv_chart_groupData[6].s_FloorMapImageName + "</li><li><span class='brown'></span> " + dv_chart_groupData[7].s_FloorMapImageName + "</li></ul>";

                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff00ff", pointColor: "#ff00ff", pointStrokeColor: "#ff00ff", pointHighlightFill: "#ff00ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue3 },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ffff", pointColor: "#00ffff", pointStrokeColor: "#00ffff", pointHighlightFill: "#00ffff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue4 },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ffa500", pointColor: "#ffa500", pointStrokeColor: "#ffa500", pointHighlightFill: "#ffa500", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue5 },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#008080", pointColor: "#008080", pointStrokeColor: "#008080", pointHighlightFill: "#008080", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue6 },
                            { label: dv_chart_groupData[7].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#a52a2a", pointColor: "#a52a2a", pointStrokeColor: "#a52a2a", pointHighlightFill: "#a52a2a", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue7 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }
                }

                else if (floorCount == 9) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li><span class='pink'></span> " + dv_chart_groupData[3].s_FloorMapImageName + "</li><li><span class='lightblue'></span> " + dv_chart_groupData[4].s_FloorMapImageName + "</li><li><span class='orange'></span> " + dv_chart_groupData[5].s_FloorMapImageName + "</li><li><span class='teal'></span> " + dv_chart_groupData[6].s_FloorMapImageName + "</li><li><span class='brown'></span> " + dv_chart_groupData[7].s_FloorMapImageName + "</li><li><span class='lavender'></span> " + dv_chart_groupData[8].s_FloorMapImageName + "</li></ul>";

                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff00ff", pointColor: "#ff00ff", pointStrokeColor: "#ff00ff", pointHighlightFill: "#ff00ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue3 },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ffff", pointColor: "#00ffff", pointStrokeColor: "#00ffff", pointHighlightFill: "#00ffff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue4 },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ffa500", pointColor: "#ffa500", pointStrokeColor: "#ffa500", pointHighlightFill: "#ffa500", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue5 },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#008080", pointColor: "#008080", pointStrokeColor: "#008080", pointHighlightFill: "#008080", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue6 },
                            { label: dv_chart_groupData[7].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#a52a2a", pointColor: "#a52a2a", pointStrokeColor: "#a52a2a", pointHighlightFill: "#a52a2a", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue7 },
                            { label: dv_chart_groupData[8].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#e6e6fa", pointColor: "#e6e6fa", pointStrokeColor: "#e6e6fa", pointHighlightFill: "#e6e6fa", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue8 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }
                }

                else if (floorCount == 10) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li><span class='pink'></span> " + dv_chart_groupData[3].s_FloorMapImageName + "</li><li><span class='lightblue'></span> " + dv_chart_groupData[4].s_FloorMapImageName + "</li><li><span class='orange'></span> " + dv_chart_groupData[5].s_FloorMapImageName + "</li><li><span class='teal'></span> " + dv_chart_groupData[6].s_FloorMapImageName + "</li><li><span class='brown'></span> " + dv_chart_groupData[7].s_FloorMapImageName + "</li><li><span class='lavender'></span> " + dv_chart_groupData[8].s_FloorMapImageName + "</li><li><span class='gray'></span> " + dv_chart_groupData[9].s_FloorMapImageName + "</li></ul>";

                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff00ff", pointColor: "#ff00ff", pointStrokeColor: "#ff00ff", pointHighlightFill: "#ff00ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue3 },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ffff", pointColor: "#00ffff", pointStrokeColor: "#00ffff", pointHighlightFill: "#00ffff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue4 },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ffa500", pointColor: "#ffa500", pointStrokeColor: "#ffa500", pointHighlightFill: "#ffa500", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue5 },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#008080", pointColor: "#008080", pointStrokeColor: "#008080", pointHighlightFill: "#008080", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue6 },
                            { label: dv_chart_groupData[7].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#a52a2a", pointColor: "#a52a2a", pointStrokeColor: "#a52a2a", pointHighlightFill: "#a52a2a", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue7 },
                            { label: dv_chart_groupData[8].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#e6e6fa", pointColor: "#e6e6fa", pointStrokeColor: "#e6e6fa", pointHighlightFill: "#e6e6fa", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue8 },
                            { label: dv_chart_groupData[9].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#808080", pointColor: "#e6e6fa", pointStrokeColor: "#e6e6fa", pointHighlightFill: "#e6e6fa", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue8 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }
                }
                var ctx = document.getElementById("canvas_chart").getContext("2d");
                var myLineBarChart = new Chart(ctx).Overlay(barChartData);
                document.getElementById("spLegend").innerHTML = txtLegend;
                document.getElementById("spChartHead").innerHTML = "Weekly Activity for Individual level";

            }
        });
    });
}
function fnGetRodentAnalysisData4(intSiteId, dtStartDate, dtEndDate, intFloorId)  //Monthly-All Level
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
            url: '/services/dashboard.aspx?id=29&siteid=' + intSiteId + '&startdate=' + dtStartDate + '&enddate=' + dtEndDate + '&intFloorId=' + intFloorId,
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
                txtLegend = "<ul class='legend'><li><span class='red'></span>All Floors</li></ul>";
                document.getElementById("spLegend").innerHTML = txtLegend;
                document.getElementById("spChartHead").innerHTML = "Monthly Activity for All Levels";

            }
        });
    });
}
function fnGetRodentAnalysisData5(intSiteId, dtStartDate, dtEndDate, intFloorId)  //Monthly- Individual
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
            url: '/services/dashboard.aspx?id=30&siteid=' + intSiteId + '&startdate=' + dtStartDate + '&enddate=' + dtEndDate + '&intFloorId=' + intFloorId,
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
                    st1 = dtFrom.getFullYear() + "0" + (dtFrom.getMonth() + 1);
                }
                else {
                    st1 = dtFrom.getFullYear() + "" + (dtFrom.getMonth() + 1);
                }

                var i_st1 = parseInt(st1);

                var st2;
                if (dtTo.getMonth() + 1 <= 9) {
                    st2 = dtTo.getFullYear() + "0" + (dtTo.getMonth() + 1);
                }
                else {
                    st2 = dtTo.getFullYear() + "" + (dtTo.getMonth() + 1);
                }
                var i_st2 = parseInt(st2);


                for (var a = i_st1; a <= i_st2; a++) {
                    if (a % 100 == 13) {
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
                                arrValue[pos] = dv_chart_data[i].monthCount;
                            else if (x == 1)
                                arrValue1[pos] = dv_chart_data[i].monthCount;
                            else if (x == 2)
                                arrValue2[pos] = dv_chart_data[i].monthCount;
                            else if (x == 3)
                                arrValue3[pos] = dv_chart_data[i].monthCount;
                            else if (x == 4)
                                arrValue4[pos] = dv_chart_data[i].monthCount;
                            else if (x == 5)
                                arrValue5[pos] = dv_chart_data[i].monthCount;
                            else if (x == 6)
                                arrValue6[pos] = dv_chart_data[i].monthCount;
                            else if (x == 7)
                                arrValue7[pos] = dv_chart_data[i].monthCount;
                            else if (x == 8)
                                arrValue8[pos] = dv_chart_data[i].monthCount;
                            else if (x == 9)
                                arrValue9[pos] = dv_chart_data[i].monthCount;

                        }
                    }
                }
                var uniquearrXAxis = removeDups(arrXAxis);

                var canvas = document.createElement('canvas');
                canvas.id = "canvas_chart";
                canvas.width = 900;
                canvas.height = 300;
                document.getElementById("tab_chartAnalysis").appendChild(canvas);

                if (floorCount == 1) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li></ul>";
                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }

                    var ctx = document.getElementById("canvas_chart").getContext("2d");
                    var myLineBarChart = new Chart(ctx).Overlay(barChartData);
                }
                if (floorCount == 2) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li></ul>";
                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }

                    var ctx = document.getElementById("canvas_chart").getContext("2d");
                    var myLineBarChart = new Chart(ctx).Overlay(barChartData);
                }

                if (floorCount == 3) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li></ul>";


                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 }],
                            options: { legend: { display: true, position: 'bottom' } }

                        }

                    var ctx = document.getElementById("canvas_chart").getContext("2d");
                    var myLineBarChart = new Chart(ctx).Overlay(barChartData);
                }
                if (floorCount == 4) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li><span class='pink'></span> " + dv_chart_groupData[3].s_FloorMapImageName + "</li></ul>";

                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff00ff", pointColor: "#ff00ff", pointStrokeColor: "#ff00ff", pointHighlightFill: "#ff00ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue3 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }
                }

                if (floorCount == 5) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li><span class='pink'></span> " + dv_chart_groupData[3].s_FloorMapImageName + "</li><li><span class='lightblue'></span> " + dv_chart_groupData[4].s_FloorMapImageName + "</li></ul>";

                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff00ff", pointColor: "#ff00ff", pointStrokeColor: "#ff00ff", pointHighlightFill: "#ff00ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue3 },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ffff", pointColor: "#00ffff", pointStrokeColor: "#00ffff", pointHighlightFill: "#00ffff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue4 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }
                }

                if (floorCount == 6) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li><span class='pink'></span> " + dv_chart_groupData[3].s_FloorMapImageName + "</li><li><span class='lightblue'></span> " + dv_chart_groupData[4].s_FloorMapImageName + "</li><li><span class='orange'></span> " + dv_chart_groupData[5].s_FloorMapImageName + "</li></ul>";

                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff00ff", pointColor: "#ff00ff", pointStrokeColor: "#ff00ff", pointHighlightFill: "#ff00ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue3 },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ffff", pointColor: "#00ffff", pointStrokeColor: "#00ffff", pointHighlightFill: "#00ffff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue4 },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ffa500", pointColor: "#ffa500", pointStrokeColor: "#ffa500", pointHighlightFill: "#ffa500", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue5 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }
                }


                else if (floorCount == 7) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li><span class='pink'></span> " + dv_chart_groupData[3].s_FloorMapImageName + "</li><li><span class='lightblue'></span> " + dv_chart_groupData[4].s_FloorMapImageName + "</li><li><span class='orange'></span> " + dv_chart_groupData[5].s_FloorMapImageName + "</li><li><span class='teal'></span> " + dv_chart_groupData[6].s_FloorMapImageName + "</li></ul>";

                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff00ff", pointColor: "#ff00ff", pointStrokeColor: "#ff00ff", pointHighlightFill: "#ff00ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue3 },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ffff", pointColor: "#00ffff", pointStrokeColor: "#00ffff", pointHighlightFill: "#00ffff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue4 },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ffa500", pointColor: "#ffa500", pointStrokeColor: "#ffa500", pointHighlightFill: "#ffa500", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue5 },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#008080", pointColor: "#008080", pointStrokeColor: "#008080", pointHighlightFill: "#008080", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue6 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }
                }

                else if (floorCount == 8) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li><span class='pink'></span> " + dv_chart_groupData[3].s_FloorMapImageName + "</li><li><span class='lightblue'></span> " + dv_chart_groupData[4].s_FloorMapImageName + "</li><li><span class='orange'></span> " + dv_chart_groupData[5].s_FloorMapImageName + "</li><li><span class='teal'></span> " + dv_chart_groupData[6].s_FloorMapImageName + "</li><li><span class='brown'></span> " + dv_chart_groupData[7].s_FloorMapImageName + "</li></ul>";

                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff00ff", pointColor: "#ff00ff", pointStrokeColor: "#ff00ff", pointHighlightFill: "#ff00ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue3 },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ffff", pointColor: "#00ffff", pointStrokeColor: "#00ffff", pointHighlightFill: "#00ffff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue4 },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ffa500", pointColor: "#ffa500", pointStrokeColor: "#ffa500", pointHighlightFill: "#ffa500", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue5 },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#008080", pointColor: "#008080", pointStrokeColor: "#008080", pointHighlightFill: "#008080", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue6 },
                            { label: dv_chart_groupData[7].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#a52a2a", pointColor: "#a52a2a", pointStrokeColor: "#a52a2a", pointHighlightFill: "#a52a2a", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue7 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }
                }

                else if (floorCount == 9) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li><span class='pink'></span> " + dv_chart_groupData[3].s_FloorMapImageName + "</li><li><span class='lightblue'></span> " + dv_chart_groupData[4].s_FloorMapImageName + "</li><li><span class='orange'></span> " + dv_chart_groupData[5].s_FloorMapImageName + "</li><li><span class='teal'></span> " + dv_chart_groupData[6].s_FloorMapImageName + "</li><li><span class='brown'></span> " + dv_chart_groupData[7].s_FloorMapImageName + "</li><li><span class='lavender'></span> " + dv_chart_groupData[8].s_FloorMapImageName + "</li></ul>";

                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff00ff", pointColor: "#ff00ff", pointStrokeColor: "#ff00ff", pointHighlightFill: "#ff00ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue3 },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ffff", pointColor: "#00ffff", pointStrokeColor: "#00ffff", pointHighlightFill: "#00ffff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue4 },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ffa500", pointColor: "#ffa500", pointStrokeColor: "#ffa500", pointHighlightFill: "#ffa500", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue5 },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#008080", pointColor: "#008080", pointStrokeColor: "#008080", pointHighlightFill: "#008080", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue6 },
                            { label: dv_chart_groupData[7].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#a52a2a", pointColor: "#a52a2a", pointStrokeColor: "#a52a2a", pointHighlightFill: "#a52a2a", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue7 },
                            { label: dv_chart_groupData[8].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#e6e6fa", pointColor: "#e6e6fa", pointStrokeColor: "#e6e6fa", pointHighlightFill: "#e6e6fa", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue8 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }
                }

                else if (floorCount == 10) {
                    txtLegend = "<ul class='legend'><li><span class='red'></span> " + dv_chart_groupData[0].s_FloorMapImageName + "</li><li><span class='green'></span> " + dv_chart_groupData[1].s_FloorMapImageName + "</li><li><span class='blue'></span> " + dv_chart_groupData[2].s_FloorMapImageName + "</li><li><span class='pink'></span> " + dv_chart_groupData[3].s_FloorMapImageName + "</li><li><span class='lightblue'></span> " + dv_chart_groupData[4].s_FloorMapImageName + "</li><li><span class='orange'></span> " + dv_chart_groupData[5].s_FloorMapImageName + "</li><li><span class='teal'></span> " + dv_chart_groupData[6].s_FloorMapImageName + "</li><li><span class='brown'></span> " + dv_chart_groupData[7].s_FloorMapImageName + "</li><li><span class='lavender'></span> " + dv_chart_groupData[8].s_FloorMapImageName + "</li><li><span class='gray'></span> " + dv_chart_groupData[9].s_FloorMapImageName + "</li></ul>";

                    var barChartData =
                        {
                            labels: uniquearrXAxis,
                            datasets: [{ label: dv_chart_groupData[0].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff0000", pointColor: "#ff0000", pointStrokeColor: "#ff0000", pointHighlightFill: "#ff0000", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue },
                            { label: dv_chart_groupData[1].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ff00", pointColor: "#00ff00", pointStrokeColor: "#00ff00", pointHighlightFill: "#00ff00", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue1 },
                            { label: dv_chart_groupData[2].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#0000ff", pointColor: "#0000ff", pointStrokeColor: "#0000ff", pointHighlightFill: "#0000ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue2 },
                            { label: dv_chart_groupData[3].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ff00ff", pointColor: "#ff00ff", pointStrokeColor: "#ff00ff", pointHighlightFill: "#ff00ff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue3 },
                            { label: dv_chart_groupData[4].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#00ffff", pointColor: "#00ffff", pointStrokeColor: "#00ffff", pointHighlightFill: "#00ffff", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue4 },
                            { label: dv_chart_groupData[5].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#ffa500", pointColor: "#ffa500", pointStrokeColor: "#ffa500", pointHighlightFill: "#ffa500", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue5 },
                            { label: dv_chart_groupData[6].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#008080", pointColor: "#008080", pointStrokeColor: "#008080", pointHighlightFill: "#008080", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue6 },
                            { label: dv_chart_groupData[7].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#a52a2a", pointColor: "#a52a2a", pointStrokeColor: "#a52a2a", pointHighlightFill: "#a52a2a", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue7 },
                            { label: dv_chart_groupData[8].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#e6e6fa", pointColor: "#e6e6fa", pointStrokeColor: "#e6e6fa", pointHighlightFill: "#e6e6fa", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue8 },
                            { label: dv_chart_groupData[9].s_FloorMapImageName, type: "line", fillColor: "rgba(220,220,220,0)", strokeColor: "#808080", pointColor: "#e6e6fa", pointStrokeColor: "#e6e6fa", pointHighlightFill: "#e6e6fa", pointHighlightStroke: "rgba(220,220,220,1)", data: arrValue8 }],
                            options: { legend: { display: true, position: 'bottom' } }
                        }
                }
                var ctx = document.getElementById("canvas_chart").getContext("2d");
                var myLineBarChart = new Chart(ctx).Overlay(barChartData);
                document.getElementById("spLegend").innerHTML = txtLegend;
                document.getElementById("spChartHead").innerHTML = "Monthly Activity for Individual Levels";

            }
        });
    });
}