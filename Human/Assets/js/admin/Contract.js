
var msform = document.getElementById('msform');
var inputs = msform.querySelectorAll('input, select, textarea');
document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('btnSave_Contract').style.display = 'none';
    document.getElementById('btnAdd_Contract').style.display = 'none';
    document.getElementById('btn-fieldset').style.display = 'none';
  
    // Hàm để set tất cả các input/select/textarea về trạng thái disabled
   /* inputs.forEach(function (input) {
        if (input.className === 'check-contract' && input.disabled) {
            // Loại bỏ thuộc tính disabled cho các phần tử có class 'check-contract'
            input.removeAttribute('disabled');
        } else {
            input.setAttribute('disabled', 'true');
        }
    });*/
    document.getElementById('select_contractType').setAttribute('disabled', 'true');
    document.getElementById('date_start').setAttribute('disabled', 'true');
    document.getElementById('date_end').setAttribute('disabled', 'true');
    document.getElementById('ip_specialize').setAttribute('disabled', 'true');
    document.getElementById('ip_jobToDo').setAttribute('disabled', 'true');
    document.getElementById('ip_note_contract').setAttribute('disabled', 'true');
    document.getElementById('btn-edit').addEventListener('click', function () {
        inputs.forEach(function (input) {
            if (input.id !== 'contract_code') {
                input.removeAttribute('disabled');
            }

        });
        var checkContract = document.getElementsByClassName('check-contract');
        for (var i = 0; i < checkContract.length; i++) {
            checkContract[i].removeAttribute('disabled');
        }
        document.getElementById('btnSave_Contract').style.display = 'block';
        document.getElementById('btnAdd_Contract').style.display = 'none';
    });
    document.getElementById('btn-add').addEventListener('click', function () {
        inputs.forEach(function (input) {
            if (input.id !== 'contract_code') {
                input.removeAttribute('disabled');
            }   
        });
        var checkContracts = document.getElementsByClassName('check-contract');
        for (var i = 0; i < checkContracts.length; i++) {
            checkContracts[i].setAttribute('disabled', 'true');
        }
        document.getElementById('btnSave_Contract').style.display = 'none';
        document.getElementById('btnAdd_Contract').style.display = 'block';
    });
    document.getElementById('btn-cancel').addEventListener('click', function () {
        document.getElementById('btnSave_Contract').style.display = 'none';
        document.getElementById('btnAdd_Contract').style.display = 'none';
        inputs.forEach(function (input) {
            if (input.className !== 'check-contract') {
                input.setAttribute('disabled', 'true');
            }
        });
    });
    
});

//-------------------------- Xử lý sự kiện chọn nhân viên để xem contract trong table modal ----------------------------------
    $('input[name="radio-emp"]').change(function () {
        var select_type = $('input[name="radio-emp"]:checked').val();
        if (select_type !== undefined) {
            $.ajax({
                url: '/Contract/SearchContractByEmpID',
                type: 'POST',
                dataType: 'json',
                data: {
                    select_id: select_type
                },
                success: function (data) {
                    if (data.success) {
                        var employeeInfo = data.emp;
                        var model_controller = data.data;
                        /*đổ dữ liêu vào thẻ input*/
                        var hire_date = moment(employeeInfo.HIRE_DAY).format("YYYY-MM-DD");
                        document.getElementById('ip_emp_code').value = employeeInfo.EMP_CODE;
                        document.querySelector('input[name="EMP_CODE_HIDDEN"]').value = employeeInfo.EMP_CODE;
                        document.getElementById('ip_emp_id').value = employeeInfo.EMPLOYEE_ID;
                        document.getElementById('emp_id_hidden').value = employeeInfo.EMPLOYEE_ID;
                        document.getElementById('ip_fullname').value = employeeInfo.FULLNAME;
                        document.getElementById('ip_date_join').value = hire_date;;
                        document.getElementById('ip_deparment').value = employeeInfo.DEPARTMENT_NAME_VI;
                        document.getElementById('ip_section').value = employeeInfo.SECTION_NAME_VI;
                        document.getElementById('ip_job').value = employeeInfo.TitleName_vi;
                        /*đổ dữ liệu vào table*/
                        var resultHtml = '<table style="width:100%!important;">';
                        resultHtml += '<thead>';
                        resultHtml += '<tr>';

                        resultHtml += '<th>';
                        resultHtml += '###';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'Mã số hợp đồng <br />(cotract code)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'Loại hợp đồng <br />(contract type)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'NGÀY Ký<br>(START DATE)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'NGÀY HẾT HẠN<br>(END DATE)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'Chức danh chuyên môn <br />(specialize)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'Công việc phải làm <br />(work)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'Ghi Chú <br />(Note)';
                        resultHtml += '</th>';

                        resultHtml += '</tr>';
                        resultHtml += '</thead>';
                        resultHtml += '<tbody id="result_tbody">';
                        if (model_controller.length >0) {
                            model_controller.forEach(function (item, index) {
                                var issue_date = moment(item.ISSUEDATE).format("DD/MM/YYYY");
                                var deadline_date = moment(item.DEADLINE).format("DD/MM/YYYY");
                                resultHtml += '<tr>';
                                resultHtml += '<td><div class="form-check-inline mr-0"><input type="radio" name="radio-contract" class="check-contract form-check-input mr-0" value="' + item.CONTRACT_ID + '" /> <input type="hidden" class="contractType_id_hidden" value="' + item.CONTRACTTYPE_ID +'" /></div></td>';
                                resultHtml += '<td>' + (item.CONTRACT_NO ? item.CONTRACT_NO : "") + '</td>';
                                resultHtml += '<td>' + item.CONTRACTNAME_VI + '</td>';
                                resultHtml += '<td>' + issue_date + '</td>';
                                resultHtml += '<td>' + deadline_date + '</td>';
                                resultHtml += '<td>' + (item.BASEON ? item.BASEON : "") + '</td>';
                                resultHtml += '<td>' + (item.BASEON_KYNGAY ? item.BASEON_KYNGAY : "") + '</td>';
                                resultHtml += '<td>' + (item.REMARK ? item.REMARK : "") + '</td>';
                                resultHtml += '</tr>';

                            });
                        }
                        resultHtml += '</tbody>';
                        resultHtml += '</table>';
                        $('#contract_table').html(resultHtml);
                        document.getElementById('btn-fieldset').style.display = 'block';
                       
                    } else {
                        // Hiển thị thông báo lỗi nếu có
                        Swal.fire({
                            position: 'center-center',
                            icon: 'error',
                            title: data.message,
                            showConfirmButton: false,
                            timer: 2000
                        });
                    }
                },
                error: function (xhr, status, error) {
                    // Xử lý khi có lỗi trong quá trình yêu cầu AJAX
                    Swal.fire({
                        position: 'center-center',
                        icon: 'error',
                        title: xhr.responseText,
                        showConfirmButton: false,
                        timer: 2000
                    });
                }
            });
        } else {

        }
    });
// ------------------------------ Xử lý sự kiện khi click vào radio button ở table ----------------------------------
$('#contract_table').on('change', 'input.check-contract', function () {
    // Get the values from the selected row
    var selectedRow = $(this).closest('tr');
    var contractId = $(this).val();
    document.getElementById('contract_id_hidden').value = contractId;
    var contractCode = selectedRow.find('td:eq(1)').text().trim();
    var dateStart = selectedRow.find('td:eq(3)').text();
    var dateEnd = selectedRow.find('td:eq(4)').text();
    var specialize = selectedRow.find('td:eq(5)').text().trim();
    var jobToDo = selectedRow.find('td:eq(6)').text().trim();
    var noteContract = selectedRow.find('td:eq(7)').text().trim();
    var contractTypeId = selectedRow.find('.contractType_id_hidden').val();  // Value of contractType_id_hidden
    // Set the values to the corresponding input fields
    document.getElementById('contract_code').value = contractCode;
    document.getElementById('date_start').value = formatDateToISO(dateStart);
    document.getElementById('date_end').value = formatDateToISO(dateEnd);
    document.getElementById('ip_specialize').value = specialize;
    document.getElementById('ip_jobToDo').value = jobToDo;
    document.getElementById('ip_note_contract').value = noteContract;
    document.getElementById('contract_code_hidden').value = contractCode;
    // Bắt sự kiện chọn cho các select tương ứng với giá trị từ input ẩn
    var selects = [
        { id: 'select_contractType', hiddenValue: contractTypeId }
    ];
    // Duyệt qua các option trong dropdown để so sánh với department_id từ input ẩn
    selects.forEach(function (selectData) {
        var select = document.getElementById(selectData.id);
        var options = select.querySelectorAll('option');

        options.forEach(function (option) {
            option.selected = option.value === selectData.hiddenValue;
        });
    });

});
// ------------------------------ chuyển đổi ngày dạng text sang type="date" -----------------------------------
function formatDateToISO(dateString) {
    var parts = dateString.split('/');
    if (parts.length === 3) {
        var day = parts[0].trim();
        var month = parts[1].trim();
        var year = parts[2].trim();
        return year + '-' + month + '-' + day;
    }
    return dateString;
}
// ------------------------------------- bắt sự kiện khi click chọn ContractType thì tự động insert mã hợp đồng ---------------
var selectContract = document.querySelector('select[name="CONTRACTTYPE_ID"]');
//var issuedateInput = document.querySelector('input[name="ISSUEDATE"]');
var issuedateInput = document.getElementById('date_start');
var deadlinedateInput = document.getElementById('date_end');
var contractCodeInput = document.getElementById('contract_code');
var emp_code_contract = document.querySelector('input[name="EMP_CODE_HIDDEN"]');
// Function to add months to a date
function addMonths(date, months) {
    var d = new Date(date);
    d.setMonth(d.getMonth() + months);
    return d.toISOString().split('T')[0];
}
// Update function for CONTRACTCODE
function updateContractCode() {
    var selectedOption = selectContract.options[selectContract.selectedIndex];
    var issuedateYear = new Date(issuedateInput.value).getFullYear();
    var contractTypedata = selectedOption.dataset.period;
    
    var name_contract = "";

    if (contractTypedata === "1") {
        name_contract = "HĐTV";
    } else if (contractTypedata === "3") {
        name_contract = "HĐTN";
    } else if (contractTypedata === "12" || contractTypedata === "0") {
        name_contract = "HĐLĐ";
    }

    contractCodeInput.value = emp_code_contract.value + '/' + issuedateYear + '/' + name_contract;
    document.getElementById('contract_code_hidden').value = emp_code_contract.value + '/' + issuedateYear + '/' + name_contract;
    // Update DEADLINE
    var issuedate = new Date(issuedateInput.value);
    var deadlineDate = addMonths(issuedate, parseInt(contractTypedata, 10));
    deadlinedateInput.value = deadlineDate;
}
// Add event listener to the select element
selectContract.addEventListener('change', function () {
    var currentDate = new Date().toISOString().split('T')[0];
    issuedateInput.value = currentDate;
    // Update contract code on initial selection
    updateContractCode();
});
 issuedateInput.addEventListener('change', function () {
    updateContractCode();
});

/*------------------------------ ADD, EDIT Contract_tab bằng AJAX ---------------------------------------*/
$('#btn_add_Contract').click(function (event) {
    event.preventDefault();
    var formData_add = $('#msform').serialize();
    var selectedOption = $('#select_contractType option:selected');
    var contractTypeName = selectedOption.data('contractypename');
    $.ajax({
        type: 'POST',
        url: '/Contract/AddContract_Tab',
        data: formData_add,

        dataType: 'json', // Đặt kiểu dữ liệu trả về là JSON
        success: function (response) {
            if (response.success) {
                Swal.fire({
                    position: 'center-center',
                    icon: 'success',
                    title: response.message,
                    showConfirmButton: false,
                    timer: 3000
                });
                var data_insert = response.data;
                var issue_date = moment(data_insert.ISSUEDATE).format("DD/MM/YYYY");
                var deadline_date = moment(data_insert.DEADLINE).format("DD/MM/YYYY");
                
                var resultHtml = '<tr>';
                resultHtml += '<td><div class="form-check-inline mr-0"><input type="radio" name="radio-contract" class="check-contract form-check-input mr-0" value="' + data_insert.CONTRACT_ID + '" /> <input type="hidden" class="contractType_id_hidden" value="' + data_insert.CONTRACTTYPE_ID + '" /></div></td>';
                resultHtml += '<td>' + (data_insert.CONTRACT_NO ? data_insert.CONTRACT_NO : "") + '</td>';
                resultHtml += '<td>' + contractTypeName + '</td>';
                resultHtml += '<td>' + issue_date + '</td>';
                resultHtml += '<td>' + deadline_date + '</td>';
                resultHtml += '<td>' + (data_insert.BASEON ? data_insert.BASEON : "") + '</td>';
                resultHtml += '<td>' + (data_insert.BASEON_KYNGAY ? data_insert.BASEON_KYNGAY : "") + '</td>';
                resultHtml += '<td>' + (data_insert.REMARK ? data_insert.REMARK : "") + '</td>';
                resultHtml += '</tr>';
              
                $('#result_tbody').prepend(resultHtml);
                var checkContract = document.getElementsByClassName('check-contract');
                for (var i = 0; i < checkContract.length; i++) {
                    checkContract[i].removeAttribute('disabled');
                }
                inputs.forEach(function (input) {
                    if (input.className !== 'check-contract') {
                        input.setAttribute('disabled', 'true');
                    }
                });
                document.getElementById('btnAdd_Contract').style.display = 'none';
            } else {
                Swal.fire({
                    position: 'center-center',
                    icon: 'error',
                    title: response.message,
                    showConfirmButton: false,
                    timer: 3000
                });

            }
        },
        error: function (error) {
            Swal.fire({
                position: 'center-center',
                icon: 'error',
                title: 'Đã xảy ra lỗi khi gửi request: ' + error,
                showConfirmButton: false,
                timer: 3000
            });
        }
    });
});
$('#btn_save_Contract').click(function (event1) {
    event1.preventDefault();
    var selectedRadio = $('input[name="radio-contract"]:checked');
    var selectedOption = $('#select_contractType option:selected');
    var contractTypeName = selectedOption.data('contractypename');
    if (selectedRadio.length === 0) {
        Swal.fire({
            position: 'center-center',
            icon: 'warning',
            title: 'Vui lòng chọn 1 hàng để chỉnh sửa!',
            showConfirmButton: false,
            timer: 3000
        });
        return; // Dừng việc thực hiện khi không có radio nào được chọn
    }
    var formData_add = $('#msform').serialize();
    $.ajax({
        type: 'POST',
        url: '/Contract/UPD_ContractTab', 
        data: formData_add,
        dataType: 'json', // Đặt kiểu dữ liệu trả về là JSON
        success: function (response) {
            if (response.success) {
                Swal.fire({
                    position: 'center-center',
                    icon: 'success',
                    title: response.message,
                    showConfirmButton: false,
                    timer: 3000
                });
                var data_update = response.data;
                var issue_date = moment(data_update.ISSUEDATE).format("DD/MM/YYYY");
                var deadline_date = moment(data_update.DEADLINE).format("DD/MM/YYYY");
                // Get the selected radio button
                var selectedRadioValue = selectedRadio.val();
                // Find the corresponding row using the unique identifier (CONTRACT_ID)
                var selectedRow = $('input[value="' + selectedRadioValue + '"]').closest('tr');
                // Update the row with the new data
                selectedRow.find('td:eq(1)').text(data_update.CONTRACT_NO || "");
                selectedRow.find('.contractType_id_hidden').val(data_update.CONTRACTTYPE_ID);
                selectedRow.find('td:eq(2)').text(contractTypeName);
                selectedRow.find('td:eq(3)').text(issue_date);
                selectedRow.find('td:eq(4)').text(deadline_date);
                selectedRow.find('td:eq(5)').text(data_update.BASEON || "");
                selectedRow.find('td:eq(6)').text(data_update.BASEON_KYNGAY || "");
                selectedRow.find('td:eq(7)').text(data_update.REMARK || "");
                inputs.forEach(function (input) {
                    if (input.className !== 'check-contract') {
                        input.setAttribute('disabled', 'true');
                    }
                });
                document.getElementById('btnSave_Contract').style.display = 'none';
            } else {
                Swal.fire({
                    position: 'center-center',
                    icon: 'error',
                    title: response.message,
                    showConfirmButton: false,
                    timer: 3000
                });
            }
        },
        error: function (error) {
            Swal.fire({
                position: 'center-center',
                icon: 'error',
                title: 'Đã xảy ra lỗi khi gửi request: ' + error,
                showConfirmButton: false,
                timer: 3000
            });
        }
    });
});