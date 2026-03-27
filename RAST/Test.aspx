<%@ Page Title="" Language="C#" MasterPageFile="~/masters/MasterPage.master" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link href="/plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
     <link href="/plugins/daterangepicker/daterangepicker-bs3.css" rel="stylesheet" type="text/css" />     
    <script src="/dist/js/Chart.js"></script>
   <%-- <script type="text/javascript" src="http://ajax.googleapis.com/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes.start/jquery-ui.css "rel="stylesheet" type="text/css" />
   <script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script> --%>

    <script src="/plugins/jQuery/jQuery-2.1.3.min.js"></script>
    <script src="/plugins/jQueryUI/jquery-ui-1.10.3.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#Button1").click(function () {
                $("#popup").dialog({
                    title: "Data View",
                    width: 600,
                    height: 600,
                    modal: true,
                    buttons: {
                        close: function () {
                            $(this).dialog('Close');
                        }
                        }
                    });
            });
        });
        //$("#Button1").live("click", function () {
        //    $("#popup").dialog({
        //        title: "Data Table",
        //        width: 600,
        //    })
        //    return false;
        //});
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


     <form id="form1" runat="server">
        <div>
            <div id="popup" style="display:none">
                <asp:GridView ID="GridView1" runat="server" autoCompleteColumns="false">
                    <Columns>
                        <asp:BoundField DataField ="i_SensorId" HeaderText="Sensor Id" />
                     <%--   <asp:BoundField  DataField="Datetime" HeaderText="TimeStamp" />--%>
                        <asp:BoundField DataField ="i_AlertType" HeaderText="Alert Type" />
                    </Columns>
                </asp:GridView>
                
                </div>
            <asp:Button ID="Buttton1" runat="server" Text="show grid" OnClick="Buttton1_Click" />

        </div>
         </form>

</asp:Content>

