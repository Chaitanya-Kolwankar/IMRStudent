$(document).ready(function () {
  
    var ddlf = $("[id*=ddlayid]");
    $.ajax({
        type: "POST",
        url: "fee_recpt.aspx/fillayid",
        data: '{stud_id:"'+stud_id+'"}',
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (r) {
            ddlf.empty().append('<option selected="selected" value="0">--Select--</option>');
            $.each(r.d, function () {
                ddlf.append($("<option></option>").val(this['Value']).html(this['Text']));
            });
        }
    });  
});

$("[id*=ddlayid]").change(function () {
    fillreceipt();
});


function fillreceipt() {
    $.ajax({
        type: "POST",
        url: "fee_recpt.aspx/StrudentFeeDetails",
        data: '{ayid:"' + $("[id*=ddlayid]").val() + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            if (response.d.length > 0 && response.d[0].message == "") {

                var tbltranscat = document.getElementById("tbltransaction");
                tbltranscat.innerHTML = "";
                $("[id*=tbltransaction]").empty();
                $("[id*=transaction]").show();
                Course = response.d[0].Courseid;
                $("[id*=tbltransaction]").append("<thead><tr class='alert alert-danger'><th colspan=9><center>PAYMENT DETAILS</th></tr><tr class='alert-success'><th><center>RECEIPT NO</center></th><th><center>AMOUNT</center></th><th ><center>PAY MODE</center></th><th ><center>PAY DATE</center></th><th style='display:none'><center>STRUCTNAME</center></th><th><center>RECEIPT</center></th></tr></thead>");
                var totalamt = 0;
                for (var i = 0; i < response.d.length; i++) {
                    if (i == 0) {
                        $("[id*=tbltransaction]").append("<tbody>");
                    }
                    if (response.d[i].RECIPTNO.startsWith("R")) {
                        $("[id*=tbltransaction]").append("<tr ><td>" + response.d[i].RECIPTNO + "</td><td>" + response.d[i].AMOUNT + "</td><td>" + response.d[i].RECIPTMODE + "</td><td>" + response.d[i].PAYDATE + "</td><td style='display:none'>" + response.d[i].structname + "</td><td><a  href='#' id='gdvfees" + i + "' class ='btn btn-success' >RECEIPT</a></td></tr>");
                    }
                    else {
                        $("[id*=tbltransaction]").append("<tr ><td>" + response.d[i].RECIPTNO + "</td><td>" + response.d[i].AMOUNT + "</td><td>" + response.d[i].RECIPTMODE + "</td><td>" + response.d[i].PAYDATE + "</td><td style='display:none'>" + response.d[i].structname + "</td><td><a  href='#' id='gdvfees" + i + "' class ='btn btn-success' >RECEIPT</a></td></tr>");
                    }
                    if (i == response.d.length - 1) {
                        $("[id*=tbltransaction]").append("</tbody>");
                    }
                }

             
                for (var i = 0; i <= $("[id^='gdvfees']").length - 1; i++) {
                    var str1 = String(response.d[i].RECIPTNO);
                    var str2 = String(response.d[i].structname);
                    response.d[i].structname
                    $("[id^='gdvfees']")[i].setAttribute("onclick", 'getreceipt("' + str1 + '","' + str2 + '")');
                }
            }
            else {
                $.notify("No Data Found", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                $("[id*=transaction]").hide();
                if (response.d.length > 0 && response.d[0].message == "course Fees") {
                    $("[id*='lblcoursefees']")[0].innerText = response.d[0].CRSAMOUNT;
                }
                else {
                }
            }
        },
        error: function () {
            //alert('Connection error, please retry');
            $.notify("Error ! Connection error, please retry.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    });
};

function getreceipt(receptno, structname) {

    PageMethods.Setsession(receptno + '|' + stud_id + '|' + $("[id*=ddlayid]").val());


    if (structname == "OTHER FEES") {
        window.open("FeeReceiptOtherFees.aspx", "_blank");

    }
    else {
        window.open("FeeReceiptTutionFees.aspx", "_blank");
    }

}