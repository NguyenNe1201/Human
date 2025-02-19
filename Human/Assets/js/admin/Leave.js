


 
//button detail
const btnDetail = document.querySelector("#btn-detail");
const formDetail = document.getElementById("FormLeaveOverlay");
const overlay = document.getElementById("Overlay");
const btn_close = document.querySelector('.close-btn');
//show form
if (btnDetail) {
    btnDetail.addEventListener("click", function (event) {
        event.preventDefault();
        toggleFormVisibility();
        GetListLeaveByMonth(0);
    })
}
//hidden form
if (overlay) {
    overlay.addEventListener("click", function () {
        toggleFormVisibility();
    });
}
if (btn_close) {
    btn_close.addEventListener("click", function () {
        toggleFormVisibility();
    })
}
function toggleFormVisibility() {
    formDetail.classList.toggle("hidden-item");
    overlay.classList.toggle("hidden-item");
}


//filter by month
var monthcells = document.getElementsByClassName("month-cell");
for (var i = 0; i < monthcells.length; i++) {
    monthcells[i].addEventListener('click', function () {
        formDetail.classList.toggle("hidden-item");
        overlay.classList.toggle("hidden-item");
        var month = this.cellIndex;
        GetListLeaveByMonth(month);
    })
}
// Retrieve the value from the data attribute
var totalYearText = document.getElementById('total_leavel_year_Text').getAttribute('data-total-year-text');
var totalMonthText = document.getElementById('total_leavel_month_Text').getAttribute('data-total-month-text');
var AcceptText = document.getElementById('accept_Text').getAttribute('data-accept-text');
var DenyText = document.getElementById('deny_Text').getAttribute('data-deny-text');
var CancelText = document.getElementById('cancel_Text').getAttribute('data-cancel-text');
var PendingText = document.getElementById('pending_Text').getAttribute('data-pending-text');
var DeleteText = document.getElementById('delete_Text').getAttribute('data-delete-text');
var RequestText = document.getElementById('request_Text').getAttribute('data-request-text');
function GetListLeaveByMonth(month) {
    const tb_leave = document.querySelector('.tb-leave-detail tbody');
    $.ajax({
        url: "/Leave/GetListLeaveByMonth",
        data: JSON.stringify({ month: month }),
        type: "POST",
        contentType: "application/json"
    }).done(function (result) {
        tb_leave.innerHTML = '';
        var caption_month = document.getElementById('caption-detail');
        var dt = new Date();
        var datenow = moment().format("YYYY-MM-DD");
        
        if (month == 0) {
            caption_month.innerText = totalYearText;
        }
        else {
            caption_month.innerText = totalMonthText+ " - " + month;

        }
        if (result.LST_LEAVE != 0) {
            $.each(result.LST_LEAVE, function (key, item) {
                var nameKind = "";
                var status = item.STATUS_L;
                var classCheck = "";
                var classIcon = "";
                var classStatus = "";
                var classCheck_td = "";
                switch (item.STATUS_L) {
                    /*   case 1:
                           classIcon = "fa-solid fa-check icon text-color-green";
                           break;
                       case 2:
                           classCheck = "text-color-red";
                           classIcon = "fa fa-times icon";
                           break;
                       case 3:
                           classCheck = "text-line-through";
                           classIcon = "fa-solid fa-minus text-color-copper";
                           break;
                       default:
                           classIcon = "fa-solid fa-circle-exclamation icon text-color-copper";
                           break;*/
                    case 1:
                        classStatus = "active";
                        textStatus = AcceptText;
                        break;
                    case 2:
                        classCheck = "text-color-red";
                        classStatus = "deny";
                        textStatus = DenyText;
                        break;
                    case 3:
                        classCheck_td = "cancel-cell";
                        classStatus = "cancel";
                        textStatus = CancelText;
                        break;
                    case 4:
                        /*  styleCheck = "text-decoration: line-through;";*/
                        classStatus = "request";
                        textStatus = RequestText;
                        break;
                    default:
                        classStatus = "waiting"; textStatus = PendingText;
                        break;
                }
                $.each(result.LST_KINDLEAVE, function (key, item_K) {
                    if (item_K.KINDLEAVE_ID == item.KINDLEAVE_ID) {
                        nameKind = item_K.NAMELEAVE_VI;
                        return true;
                    }
                });
                var html = '';
                var formatdate = moment(item.LEAVE_STARTDATE).format("DD/MM/YYYY");
                var formatdate_check = moment(item.LEAVE_STARTDATE).format("YYYY-MM-DD");
                let check_date = moment(formatdate_check).isAfter(datenow);
                /*<td> <i class="${classIcon}"></i> </td>*/
            html += `<tr class="${classCheck}">
            <td class="status span-home"><span class="${classStatus}">${textStatus}</span></td>
            <td class="${classCheck_td}">${formatdate}</td>
            <td class="nowrap ${classCheck_td}">${nameKind}</td>
            <td  class="${classCheck_td}">${item.HOURS / 8}</td>
            <td> ${(item.REASON == null ? " " : item.REASON)} </td>
            `;
                html += "<td class='exclude-underline flex-center'>";

                if (item.STATUS_L == 0) {
                    /* html += `  <a  class="leave-btn color-copper" href="/Leave/Edit?LEAVE_ID=${item.LEAVE_ID}">Edit</a>`;
     */
                    if (check_date) {
                        html += `<a style="border-radius: 25px;" class="leave-btn color-red" href="/Leave/Delete?LEAVE_ID=${item.LEAVE_ID}">` + DeleteText + `</a>`;
                    } else {
                        html += `<a style="border-radius: 25px;"  class="leave-btn color-red" href="/Leave/Request?LEAVE_ID=${item.LEAVE_ID}">` + RequestText + `</a>`;

                    }

                }
                else if (item.STATUS_L==1) {
                    if (check_date) {
                        html += `<a style="border-radius: 25px;"  class="leave-btn color-red" href="/Leave/Request?LEAVE_ID=${item.LEAVE_ID}">` + RequestText + `</a>`;
                    }else{
                        html += `<a style="border-radius: 25px;"  class="leave-btn color-red" href="/Leave/Request?LEAVE_ID=${item.LEAVE_ID}">` + RequestText + `</a>`;
                    }
                }

                html += `</td></tr>`;
                tb_leave.insertAdjacentHTML('beforeend', html);
            });
        } else {
            var html = '';
            html += `<tr class="${classCheck}">`;
            html +=`<td colspan="6" style="text-align: center; font-size:13px;"> Không có dữ liệu<br>(No matching records found)</td>`;
            html += `</tr>`;
            tb_leave.insertAdjacentHTML('beforeend', html);
        }
    });
}
//
function DeleteLeave(LEAVE_ID) {
    if (confirm("Đồng ý xóa ?")) {
        $.ajax({
            url: "/Leave/DeleteById",
            data: JSON.stringify({ LEAVE_ID: LEAVE_ID }),
            type: "POST",
            contentType: "application/json",
        }).done(function (result) {
            
            LoadLeave();

        });
    } 
}

function LoadLeave() {
    $.ajax({
        url: "/Leave/LoadLeave",
        type: "GET",
        contentType: "application/json",
        datatype: "json"
    }).done(function (result) {
        $('.tbl-content tbody').empty();
        var num = 1;
        $.each(result.LST_LEAVE, function (key, item) {
            var nameKind = "";
            $.each(result.LST_KINDLEAVE, function (key, item_K) {
                if (item_K.KINDLEAVE_ID == item.KINDLEAVE_ID) {
                    nameKind = item_K.NAMELEAVE_VI;
                    return true;
                }
            });
            var html = '';
            var formatdate = moment(item.LEAVE_STARTDATE).format("DD/MM/YYYY");

            html += `<tr>
            <td class="hidden-item">${item.LEAVE_ID} </td>
            <td class="w-5"> ${num}</td>
            <td>${formatdate}</td>
            <td class="w-20">${nameKind}</td>
            <td class="w-8">${item.HOURS}</td>
            <td> ${(item.REASON == null ? " " : item.REASON)} </td>
            <td class="w-10">${item.STATUS_L == true ? "YES" : "NO"}</td>`;
            html += "<td>";
            if (item.STATUS_L != true) {
                html += `  <a  class="leave-btn color-copper" href="/Leave/Edit?LEAVE_ID=${item.LEAVE_ID}">Sửa</a>
                <button type="button" class="leave-btn color-red" onclick="DeleteLeave(${item.LEAVE_ID})">Xóa</button>
                `;
            }
           
            html += `</td></tr>`;
            num++;
            $('.tbl-content tbody').append(html);
        });
    });
}
function test(LEAVE_ID) {
    var a = 3;
    debugger
    $.ajax({
        url: "/Admin/Leave/UpdateStatus/" + LEAVE_ID,
        type: "POST",
        contentType: "application/json;",
        datatype: "json",
        success: function (result) {
            alert("OK");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//page add
var fromdateInput = document.getElementById('LEAVE_STARTDATE');
var todateInput = document.getElementById('LEAVE_ENDDATE');
if (fromdateInput) {
    fromdateInput.addEventListener('change', Check_todate);
}

if (todateInput) {
    todateInput.addEventListener('change', Check_todate);
}
function Check_todate() {
  
        // If To date is less than From date, set it to From date
        if (todateInput.value < fromdateInput.value) {
            todateInput.value = fromdateInput.value;
        }
   
}

// end page add
