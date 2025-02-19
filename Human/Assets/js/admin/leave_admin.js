
let currentPage = 1;
let pagesize = 10;
let sort = "";
let sortby = "descending";
document.addEventListener('DOMContentLoaded', () => {
    Fix_Table();
    const headerTb = document.querySelectorAll('table#tbl-leave th');

    headerTb.forEach((header, index) => {
        header.addEventListener('click', () => {
            sortTable(index);
        });
    });
})
/*document.addEventListener('DOMContentLoaded', () => {
    Fix_Table();
    const headerTb = document.querySelectorAll('table#tbl-leave th');

    headerTb.forEach((header, index) => {
        header.addEventListener('click', () => {
            // Chuyển đổi giữa ascending và descending khi click
            if (sortby === "ascending") {
                sortby = "descending";
            } else {
                sortby = "ascending";
            }

            // Lưu chỉ số cột đã click vào
            sortTable(index);
        });
    });
})*/
//paging

//entries
document.addEventListener("DOMContentLoaded", function () {
    // Mã JavaScript của bạn ở đây
    document.getElementById('dataTableSelect').addEventListener("change", function () {
        const selectedvalue = this.value;
        pagesize = selectedvalue;
        Paging(currentPage);
    });
});

//end entries
function Paging(page) {

    $.ajax({
        data: { sort: sort, page: page, size: pagesize },
        datatype: "html",
        url: "/LeaveAdmin/PagingSite",
        success: function (data) {
            $('#tbl-leave tbody').empty();
            $('#tbl-leave tbody').append(data);
            $('#page-' + currentPage).addClass('text-color-gray-light');
            $('#page-' + page).removeClass('text-color-gray-light');
            currentPage = page;

            updatePagination();
        }
    })
}
//end entries
function Paging(page) {

    $.ajax({
        data: { sort: sort, page: page, size: pagesize },
        datatype: "html",
        url: "/LeaveAdmin/PagingSite",
        success: function (data) {

            $('#tbl-leave tbody').empty();
            $('#tbl-leave tbody').append(data);

            //

            $('#page-' + currentPage).addClass('text-color-gray-light');
            $('#page-' + page).removeClass('text-color-gray-light');
            currentPage = page;

            updatePagination();
        }
    })
}

function FirstPage() {
    Paging(1);
}
function PreviousPage() {
    if (currentPage > 1) {
        Paging(currentPage - 1);
    }
}
function NextPage() {
    var totalpage = parseInt($('.btn-paging').data('total-page'));
    if (currentPage < totalpage) {
        Paging(currentPage + 1);
    }
    else {
        Paging(currentPage);
    }
}
function LastPage() {
    var totalpage = parseInt($('.btn-paging').data('total-page'));
    Paging(totalpage);
}

function updatePagination() {
    $.ajax({
        url: "/LeaveAdmin/GetTotalPage",
        type: "GET",
        contentType: "application/json",
        type: "json",
        success: function (data_page) {

            $('.btn-paging').attr('data-total-page', data_page);

            const pagination = document.querySelector('.btn-paging');
            pagination.innerHTML = ""; // Clear existing pagination links

            const firstPageLink = '<i class="icon icon-paging fa fa-angles-left cursor-pointer margin-l-r" onclick="FirstPage()"></i>';
            const prevPageLink = '<i class="icon icon-paging fa fa-angle-left cursor-pointer margin-l-r" onclick="PreviousPage()"></i>';
            const nextPageLink = '<i class="icon icon-paging fa fa-angle-right cursor-pointer margin-l-r" onclick="NextPage()"></i>';
            const lastPageLink = '<i class="icon icon-paging fa fa-angles-right cursor-pointer margin-l-r" onclick="LastPage()"></i>';

            if (currentPage != 1) {
                pagination.insertAdjacentHTML('beforeend', firstPageLink);
                pagination.insertAdjacentHTML('beforeend', prevPageLink);

            }

            if (currentPage > data_page) {
                currentPage = data_page;
            }
            for (let i = 1; i <= data_page; i++) {
                const pageLink = `<a id="page-${i}" href="#" onclick="Paging(${i})" class="${currentPage === i ? '' : 'text-color-gray-light'} margin-l-r">${i}</a>`;
                pagination.insertAdjacentHTML('beforeend', pageLink);
            }
            if (currentPage != data_page) {

                pagination.insertAdjacentHTML('beforeend', nextPageLink);
                pagination.insertAdjacentHTML('beforeend', lastPageLink);
            }

        },
        error: function (data) {

        }
    })


}
//end paging

//sort


function sortTable(columnIndex) {
    const table = document.getElementById('tbl-leave');
    const dataType = table.rows[0].cells[columnIndex].getAttribute('data-sort');
    const column = table.rows[0].cells[columnIndex];
    if (dataType != null) {

        for (var i = 1; i < table.rows[0].cells.length; i++) {

            if (i != columnIndex) {
                table.rows[0].cells[i].removeAttribute('aria-sort');
            }
        }
        sort = dataType;
        var attr = column.getAttribute('aria-sort');
        if (attr == null) {
            console.log(attr);
            column.setAttribute('aria-sort', 'descending');
        } else {
            if (attr == 'ascending') {
                column.setAttribute('aria-sort', 'descending');
                sortby = 'descending';
            }
            else {
                column.setAttribute('aria-sort', 'ascending');
                sortby = 'ascending';
            }

        }
        Paging(1);
    }
}
/*function sortTable(columnIndex) {
    const table = document.getElementById('tbl-leave');
    const dataType = table.rows[0].cells[columnIndex].getAttribute('data-sort');
    const column = table.rows[0].cells[columnIndex];

    if (dataType != null) {
        // Xóa trạng thái sắp xếp ở các cột khác
        for (let i = 0; i < table.rows[0].cells.length; i++) {
            if (i !== columnIndex) {
                table.rows[0].cells[i].removeAttribute('aria-sort');
            }
        }

        // Kiểm tra trạng thái sắp xếp hiện tại của cột và cập nhật lại
        let currentSort = column.getAttribute('aria-sort');
        if (currentSort === 'ascending' || currentSort === null) {
            column.setAttribute('aria-sort', 'descending');
            sortby = 'descending';
        } else {
            column.setAttribute('aria-sort', 'ascending');
            sortby = 'ascending';
        }

        // Thực hiện logic sắp xếp dữ liệu dựa trên dataType và trạng thái sắp xếp mới
        // ...

        // Sau khi sắp xếp dữ liệu, cập nhật lại giao diện bảng
        Paging(1);
    }
}*/



//end sort

function SendListCheck(status) {
    var table = document.getElementById('tbl-leave');
    var rows = table.getElementsByTagName('tr');
    var lst_leaveId = [];

    for (var i = 1; i < rows.length; i++) {
        var row = rows[i];
        var check_item = row.querySelector('.checkall-item');
        var id_leave = row.cells[0].textContent;

        if (check_item.checked) {
            lst_leaveId.push(id_leave);
        }
    }

    if (status == 3) {
        CancelByList(lst_leaveId);
    }
    else {
        UpdateStatusByList(lst_leaveId, status);
    }

    var table_headerBtn = document.getElementById('btn_header');
    table_headerBtn.classList.add('hidden-item');
}
function UpdateStatusByList(lst_leaveId, status) {
    $.ajax({
        url: "/leaveAdmin/UpdateStatusByList",
        type: "POST",
        data: JSON.stringify({ LST_LeaveID: lst_leaveId, STATUS: status }),
        contentType: "application/json",
        type: "json",
        success: function () {
            Paging(currentPage); 
            Swal.fire({
                position: 'center-center',
                icon: 'success',
                title: 'Duyệt phép thành công!',
                showConfirmButton: false,
                timer: 3000
            });
        },
        error: function () {
            /*alert("Error");*/
            Swal.fire({
                position: 'center-center',
                icon: 'error',
                title: 'duyệt phép thất bại!',
                showConfirmButton: false,
                timer: 3000
            });
        }
    })
}
function CancelByList(Lst_LeaveID) {
    $.ajax({
        url: "/LeaveAdmin/CancelByList",
        type: "POST",
        data: JSON.stringify({ LST_LEAVEID: Lst_LeaveID }),
        contentType: "application/json",
        type: "json",
        success: function () {

        },
        error: function () {
            alert("error");
        }
    }).done(function () {
        //LoadUpdate();
        Paging(currentPage);
    });
}

function Check_all() {
    const chk_all = document.getElementById('chk_all');
    var chkall_item = document.querySelectorAll('.checkall-item');
    if (chk_all.checked) {
        chkall_item.forEach(element => {
            element.checked = true;
        })
    }
    else {
        chkall_item.forEach(element => {
            element.checked = false;
        })
    }
    handleCheckboxClick();
}
function anyCheckboxChecked() {
    const checkboxes = document.querySelectorAll('.checkall-item');
    for (let i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].checked) {
            return true;
        }
    }
    return false;
}

//function show/hide button on checkbox
function toggleButtonVisibily() {
    const btnHeader = document.getElementById('btn_header');
    if (anyCheckboxChecked()) {
        btnHeader.classList.remove('hidden-item');
    }
    else {
        btnHeader.classList.add('hidden-item');
    }
}
// Function to handle checkbox click event
function handleCheckboxClick() {
    toggleButtonVisibily();
}

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
        url: "/LeaveAdmin/UpdateStatusLeave",
        type: "POST",
        data: JSON.stringify({ LEAVE_ID: LEAVE_ID, STATUS: STATUS }),
        contentType: "application/json",
        type: "json",
        success: function () {
            Paging(currentPage);    
            Swal.fire({
                position: 'center-center',
                icon: 'success',
                title: 'Duyệt phép thành công!',
                showConfirmButton: false,
                timer: 3000
            });
        },
        error: function () {
            /*alert("Error");*/
            Swal.fire({
                position: 'center-center',
                icon: 'error',
                title: 'duyệt phép thất bại!',
                showConfirmButton: false,
                timer: 3000
            });
        }
        /*}).done(function (result) {
            Swal.fire({
                position: 'center-center',
                icon: 'success',
                title: 'Duyệt phép thành công!',
                showConfirmButton: false,
                timer: 3000
            }).then(function () {
                location.reload(); // Reload sau khi người dùng đóng thông báo
            });*/
        /*  LoadUpdate();
          Paging(currentPage);*/

    });

}


function handleSelectChange(selectElement) {
    var selectedValue = selectElement.value;
    var leaveId = selectElement.dataset.leaveid;
    var employee_id = selectElement.dataset.employeeid;
    $.ajax({
        url: "/LeaveAdmin/UpdateKindLeave",
        data: JSON.stringify({ LEAVE_ID: leaveId, KINDLEAVE_ID: selectedValue, EMPLOYEE_ID: employee_id }),
        type: "POST",
        contentType: "application/json",
        datatype: "json"
    }).done(function (result) {

        if (result == "False") {
            alert("Nhân viên này đã hết phép");
        }

        //LoadUpdate();
        Paging(currentPage);
    });

}
function DeleteLeave(LEAVE_ID) {

    if (confirm("Đồng ý xóa ?")) {
        $.ajax({
            url: "/LeaveAdmin/Delete",
            type: "POST",
            data: JSON.stringify({ LEAVE_ID: LEAVE_ID }),
            contentType: "application/json",
            datatype: "json"
        }).done(function (result) {
            Paging(currentPage);
        });
    }
}
function refundCancel(LEAVE_ID) {
    $.ajax({
        url: "/LeaveAdmin/RefundStatus",
        type: "POST",
        data: JSON.stringify({ LEAVE_ID: LEAVE_ID }),
        contentType: "application/json",
        datatype: "json"
    }).done(function (result) {
        Paging(currentPage);
    });
}
//function LoadUpdate() {
//    $.ajax({
//        url: "/LeaveAdmin/LoadLeaveUpdate",
//        type: "GET",
//        contentType: "application/json",
//        dataType: "json"

//    }).done(function (data) {
//        var num = 1;
//        var hidden_status = "hidden-item";
//        var check_button = document.getElementById('chk_statusCancel');
//        if (check_button.checked) {
//            hidden_status = "";
//        }
//        //
//        var futuredate = moment(new Date()).add(-1, 'M');
//        var getyear = futuredate.format("yyyy");
//        var getmonth = futuredate.format("MM");
//        var FirstMonth = getyear + '-' + getmonth + '-25';
//        var position = data.POSITION;
//        //
//        $('.table-wrapper tbody').empty();
//        $.each(data.LST_LEAVEVIEW, function (key, item) {
//            var nameKind = "";
//            var checkCancel = "";
//            var classCancel = "";
//            if (item.STATUS_L == 3) {
//                checkCancel = "disabled";
//                classCancel = "cancel-cell";
//            }
//            $.each(data.LST_KINDLEAVE, function (key, item_K) {
//                if (item_K.KINDLEAVE_ID == item.KINDLEAVE_ID) {
//                    nameKind = item_K.NAMELEAVE_EN;
//                    return true;
//                }
//            });
//            var formatdate = moment(item.LEAVE_STARTDATE).format("DD/MM/YYYY");
//            var classStatus = "";
//            var textStatus = "";
//            var SupCheck = 1;
//            var ManCheck = 1;
//            var SelCheck = 1;
//            if (item.SUP_STATUS > 0) {
//                if (item.DIR_STATUS > 0 || item.MAN_STATUS > 0) {
//                    SupCheck = 0;
//                }
//            }
//            if (item.MAN_STATUS > 0) {
//                if (item.DIR_STATUS > 0) {
//                    ManCheck = 0;
//                }
//            }
//            if (position == "SUP") {
//                if (item.DIR_STATUS > 0 || item.MAN_STATUS > 0) {
//                    SelCheck = 0;
//                }
//            }
//            else if (position == "MAN") {
//                if (item.DIR_STATUS > 0) {
//                    SelCheck = 0;
//                }
//            }
//            switch (item.STATUS_L) {
//                case 1: classStatus = "active"; textStatus = "Accept"; break;
//                case 2: classStatus = "deny"; textStatus = "Deny"; break;
//                case 3: classStatus = "cancel"; textStatus = "Cancel"; break;
//                default: classStatus = "waiting"; textStatus = "Pending"; break;
//            }
//            var html = '';
//            html += `<tr>
//            <td class="hidden-item leave_id">${item.LEAVE_ID} </td>

//            <td><input type="checkbox" class="checkall-item"/></td>
//            <td class="${classCancel}">${formatdate}</td>
//            <td class="${classCancel}">${item.EMP_CODE}</td>
//            <td class="${classCancel}">${item.FULLNAME}</td>
//            <td class="${classCancel}">${item.SECTION_NAME_EN}</td>

//            <td class="${classCancel}">`
//            if (SelCheck == 1 && item.STATUS_L != 3) {
//                html += `<select class="remove-dropdown" onchange="handleSelectChange(this)" data-leaveid="${item.LEAVE_ID}" data-employeeid="${item.EMPLOYEE_ID}">`
//                $.each(data.LST_KINDLEAVE, function (key, i) {
//                    if (i.KINDLEAVE_ID > 0) {
//                        html += `<option value=${i.KINDLEAVE_ID} ${i.KINDLEAVE_ID == item.KINDLEAVE_ID ? "selected" : ""}>${i.NAMELEAVE_EN}</option>`
//                    }
//                });
//            }
//            else {
//                html += nameKind;
//            }
//            html += ` </td><td class="${classCancel}">${item.HOURS/8}</td>
//            <td> ${(item.REASON == null ? " " : item.REASON)} </td>
//             <td class="${hidden_status} cancel-column">
//              <button class="button-circle ${(item.STATUS_L == 3 ? "color-green" : "")}" onclick="refundCancel(${item.LEAVE_ID})"><i class="fa fa-check"></i></button>

//             </td>
//            <td class="text-center">     
//            `;
//            if (data.POSITION == "SUP" && SupCheck == 1) {
//                html += `<button ${checkCancel} class="button-circle ${(item.SUP_STATUS == 1 ? "color-green btn-disabled" : "")}" onclick=Update_Status(${item.LEAVE_ID},1)><i class="fa fa-check"></i></button>`
//                html += `<button ${checkCancel} class="button-circle ${(item.SUP_STATUS == 2 ? "color-red btn-disabled" : "")}" onclick=Update_Status(${item.LEAVE_ID},2)><i class="fa fa-xmark"></i></button>`

//            }
//            else {
//                html += `<button disabled class="button-circle ${(item.SUP_STATUS == 1 ? "color-green btn-disabled" : "")}"><i class="fa fa-check"></i></button>`
//                html += `<button disabled class="button-circle ${(item.SUP_STATUS == 2 ? "color-red btn-disabled" : "")}"><i class="fa fa-xmark"></i></button>`

//            }
//            html += '</td><td class="text-center">';

//            if (data.POSITION == "MAN" && ManCheck == 1) {
//                html += `<button ${checkCancel} class="button-circle ${(item.MAN_STATUS == 1 ? "color-green btn-disabled" : "")}" onclick=Update_Status(${item.LEAVE_ID},1)><i class="fa fa-check"></i></button>`
//                html += `<button ${checkCancel} class="button-circle ${(item.MAN_STATUS == 2 ? "color-red btn-disabled" : "")}" onclick=Update_Status(${item.LEAVE_ID},2)><i class="fa fa-xmark"></i></button>`

//            }
//            else {
//                html += `<button disabled class="button-circle ${(item.MAN_STATUS == 1 ? "color-green btn-disabled" : "")}"><i class="fa fa-check"></i></button>`
//                html += `<button disabled class="button-circle ${(item.MAN_STATUS == 2 ? "color-red btn-disabled" : "")}"><i class="fa fa-xmark"></i></button>`

//            }
//            html += '</td><td class="text-center">';
//            if (data.POSITION == "DIR") {
//                html += `<button ${checkCancel} class="button-circle ${(item.DIR_STATUS == 1 ? "color-green text-white" : "")}" onclick=Update_Status(${item.LEAVE_ID},1)><i class="fa fa-check"></i></button>`
//                html += `<button ${checkCancel} class="button-circle ${(item.DIR_STATUS == 2 ? "color-red text-white" : "")}" onclick=Update_Status(${item.LEAVE_ID},2)><i class="fa fa-xmark"></i></button>`

//            }
//            else {
//                html += `<button disabled class="button-circle ${(item.DIR_STATUS == 1 ? "color-green text-white" : "")}"><i class="fa fa-check"></i></button>`
//                html += `<button disabled class="button-circle ${(item.DIR_STATUS == 2 ? "color-red text-white" : "")}"><i class="fa fa-xmark"></i></button>`

//            }
//            html += '</td>';
//            html += `<td class="status"> <span class="${classStatus}">${textStatus}</span> </td>`

//            html += '</tr>';
//            num++;

//            $('.table-wrapper tbody').append(html);

//        });


//    });
//}
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

//form cancel
//button detail
const btnDetail = document.querySelector("#btn-cancel-leave");
const formDetail = document.getElementById("FormLeaveOverlay");
const overlay = document.getElementById("Overlay");
const btn_close = document.querySelector('.close-btn');
//show form
if (btnDetail) {
    btnDetail.addEventListener("click", function (event) {
        var test = document.querySelectorAll(".cancel-column");
        test.forEach(element => {
            element.classList.toggle('hidden-item');
        });
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

//end form cancel


