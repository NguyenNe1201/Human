
document.addEventListener('DOMContentLoaded', function () {
    Fix_Table();
})
function Fix_Table() {
    const tb = document.getElementById('tbl-leave');
    const tbody = tb.querySelector('tbody');
    const rows = tbody.getElementsByTagName('tr');
    if (rows.length > 0) {
        tb.classList.add('table-fix-column');
    }
    else {
        tb.classList.remove('table-fix-column');
    }
}
$(document).ready(function () {
    //FORM insert LEAVE
    $('#CHECK_EMPCODE').click(function () {
        var EmpCode = $('#EMP_CODE').val();

        $.ajax({
            url: "/Admin/Employees/CheckEmpByCode",
            type: "GET",
            data: { emp_code: EmpCode },
            success: function (result) {

                $('#NAME_EMP').html(result[0].FULLNAME);
                $('#EMPLOYEE_ID').val(result[0].EMPLOYEE_ID);
                $('#PNCL').val(result[1]);
            },
            error: function (ErrorMessage) {
                alert("Không tồn tại");
                $('#NAME_EMP').html("");
            }
        })
    });
    //end FORM INSERT LEAVE
});
function Update_Status(ID, STATUS) {
    var LEAVE_ID = ID;
    $.ajax({
        url: "/Admin/Leave/UpdateStatusLeave",
        data: JSON.stringify({ LEAVE_ID: LEAVE_ID, STATUS: STATUS }),
        type: "POST",
        contentType: "application/json",
        datatype: "json"
    }).done(function (result) {
        LoadUpdate();
    });
}

function handleSelectChange(selectElement) {
    var selectedValue = selectElement.value;
    var leaveId = selectElement.dataset.leaveid;
    var employee_id = selectElement.dataset.employeeid;
    $.ajax({
        url: "/Admin/Leave/UpdateKindLeave",
        data: JSON.stringify({ LEAVE_ID: leaveId, KINDLEAVE_ID: selectedValue, EMPLOYEE_ID: employee_id }),
        type: "POST",
        contentType: "application/json",
        datatype: "json"
    }).done(function (result) {

        if (result == "False") {
            alert("Nhân viên này đã hết phép");
        }

        LoadUpdate();

    });

}
function DeleteLeave(LEAVE_ID) {

    if (confirm("Đồng ý xóa ?")) {
        $.ajax({
            url: "/Admin/Leave/Delete",
            type: "POST",
            data: JSON.stringify({ LEAVE_ID: LEAVE_ID }),
            contentType: "application/json",
            datatype: "json"
        }).done(function (result) {
            LoadUpdate();
        });
    }
}
function LoadUpdate() {
    $.ajax({
        url: "/Admin/Leave/LoadLeaveUpdate",
        type: "GET",
        contentType: "application/json",
        dataType: "json"

    }).done(function (data) {
        var num = 1;

        //
        var futuredate = moment(new Date()).add(-1, 'M');
        var getyear = futuredate.format("yyyy");
        var getmonth = futuredate.format("MM");
        var FirstMonth = getyear + '-' + getmonth + '-25';
        var position = data.POSITION;
        //
        $('.table-wrapper tbody').empty();
        $.each(data.LST_LEAVEVIEW, function (key, item) {
            var nameKind = "";

            $.each(data.LST_KINDLEAVE, function (key, item_K) {
                if (item_K.KINDLEAVE_ID == item.KINDLEAVE_ID) {
                    nameKind = item_K.NAMELEAVE_VI;
                    return true;
                }
            });
            var formatdate = moment(item.LEAVE_STARTDATE).format("DD/MM/YYYY");
            var classStatus = "";
            var textStatus = "";
            var SupCheck = 1;
            var ManCheck = 1;
            var SelCheck = 1;
            if (item.SUP_STATUS > 0) {
                if (item.DIR_STATUS > 0 || item.MAN_STATUS > 0) {
                    SupCheck = 0;
                }
            }
            if (item.MAN_STATUS > 0) {
                if (item.DIR_STATUS > 0) {
                    ManCheck = 0;
                }
            }
            if (position == "SUP") {
                if (item.DIR_STATUS > 0 || item.MAN_STATUS > 0) {
                    SelCheck = 0;
                }
            }
            else if (position == "MAN") {
                if (item.DIR_STATUS > 0) {
                    SelCheck = 0;
                }
            }
            switch (item.STATUS_L) {
                case 1: classStatus = "active"; textStatus = "Accept"; break;
                case 2: classStatus = "deny"; textStatus = "Deny"; break;
                default: classStatus = "waiting"; textStatus = "Pending"; break;
            }
            var html = '';
            html += `<tr>
            <td class="hidden-item">${item.LEAVE_ID} </td>
            <td> ${num}</td>
            <td><input type="checkbox" /></td>
            <td>${formatdate}</td>
            <td>${item.EMP_CODE}</td>
            <td>${item.FULLNAME}</td>
            <td>${item.SECTION_NAME_EN}</td>
            
            <td>`
            if (SelCheck == 1) {
                html += `<select class="remove-dropdown" onchange="handleSelectChange(this)" data-leaveid="${item.LEAVE_ID}" data-employeeid="${item.EMPLOYEE_ID}">`
                $.each(data.LST_KINDLEAVE, function (key, i) {
                    if (i.KINDLEAVE_ID > 0) {
                        html += `<option value=${i.KINDLEAVE_ID} ${i.KINDLEAVE_ID == item.KINDLEAVE_ID ? "selected" : ""}>${i.NAMELEAVE_VI}</option>`
                    }
                });
            }
            else {
                html += nameKind;
            }
            html += ` </td><td>${item.HOURS}</td> <td> ${(item.REASON == null ? " " : item.REASON)} </td><td class="text-center">`;
            if (data.POSITION == "SUP" && SupCheck == 1) {
                html += `<button class="button-circle ${(item.SUP_STATUS == 1 ? "color-green btn-disabled" : "")}" onclick=Update_Status(${item.LEAVE_ID},1)><i class="fa fa-check"></i></button>`
                html += `<button class="button-circle ${(item.SUP_STATUS == 2 ? "color-red btn-disabled" : "")}" onclick=Update_Status(${item.LEAVE_ID},2)><i class="fa fa-xmark"></i></button>`

            }
            else {
                html += `<button disabled class="button-circle ${(item.SUP_STATUS == 1 ? "color-green btn-disabled" : "")}"><i class="fa fa-check"></i></button>`
                html += `<button disabled class="button-circle ${(item.SUP_STATUS == 2 ? "color-red btn-disabled" : "")}"><i class="fa fa-xmark"></i></button>`

            }
            html += '</td><td class="text-center">';

            if (data.POSITION == "MAN" && ManCheck == 1) {
                html += `<button class="button-circle ${(item.MAN_STATUS == 1 ? "color-green btn-disabled" : "")}" onclick=Update_Status(${item.LEAVE_ID},1)><i class="fa fa-check"></i></button>`
                html += `<button class="button-circle ${(item.MAN_STATUS == 2 ? "color-red btn-disabled" : "")}" onclick=Update_Status(${item.LEAVE_ID},2)><i class="fa fa-xmark"></i></button>`

            }
            else {
                html += `<button disabled class="button-circle ${(item.MAN_STATUS == 1 ? "color-green btn-disabled" : "")}"><i class="fa fa-check"></i></button>`
                html += `<button disabled class="button-circle ${(item.MAN_STATUS == 2 ? "color-red btn-disabled" : "")}"><i class="fa fa-xmark"></i></button>`

            }
            html += '</td><td class="text-center">';
            if (data.POSITION == "DIR") {
                html += `<button class="button-circle ${(item.DIR_STATUS == 1 ? "color-green text-white" : "")}" onclick=Update_Status(${item.LEAVE_ID},1)><i class="fa fa-check"></i></button>`
                html += `<button class="button-circle ${(item.DIR_STATUS == 2 ? "color-red text-white" : "")}" onclick=Update_Status(${item.LEAVE_ID},2)><i class="fa fa-xmark"></i></button>`

            }
            else {
                html += `<button disabled class="button-circle ${(item.DIR_STATUS == 1 ? "color-green text-white" : "")}"><i class="fa fa-check"></i></button>`
                html += `<button disabled class="button-circle ${(item.DIR_STATUS == 2 ? "color-red text-white" : "")}"><i class="fa fa-xmark"></i></button>`

            }
            html += '</td>';
            html += `<td class="status"> <span class="${classStatus}">${textStatus}</span> </td>`

            html += '</tr>';
            num++;

            $('.table-wrapper tbody').append(html);

        });
    });
}
function fail() {
    var kindleaveid = document.getElementById("kindleave_id");
    var selectKindLeave = document.getElementById("ListKindLeave");
    for (var i = 0; i < selectKindLeave.options.length; i++) {
        var option = selectKindLeave.options[i];
        if (option.value == kindleaveid.value) {
            option.selected = true;
            break;
        }
    }
}


