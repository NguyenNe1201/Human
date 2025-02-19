
/* $(function (e) {
            var table = $('#working_table').DataTable({
     "pageLength": 10,
 "lengthMenu": [
 [10, 50, 100, -1],
 [10, 50, 100, "All"]
 ],
 "ordering": false, // Tắt sắp xếp
 *//*"bLengthChange": false,*//* // Tắt "Show entries"
"searching": false // Tắt phần tìm kiếm
   });
var table1 = $('#salary_history_table').DataTable({
"pageLength": 10,
"lengthMenu": [
[10, 50, 100, -1],
[10, 50, 100, "All"]
],
"ordering": false, // Tắt sắp xếp
*//*"bLengthChange": false,*//* // Tắt "Show entries"
"searching": false // Tắt phần tìm kiếm
            });
    });*/

document.getElementById('fieldset1').style.display = 'block'; // Hiển thị fieldset 1 ban đầu
document.getElementById('btn-fieldset1').style.display = 'block';
document.getElementById('fieldset2').style.display = 'none';
document.getElementById('btn-fieldset2').style.display = 'none';
document.getElementById('fieldset3').style.display = 'none';
document.getElementById('btn-fieldset3').style.display = 'none';
function nextFieldset() {
    document.getElementById('fieldset1').style.display = 'none';
    document.getElementById('btn-fieldset1').style.display = 'none';
    document.getElementById('fieldset2').style.display = 'block';
    document.getElementById('btn-fieldset2').style.display = 'block';
    document.getElementById('fieldset3').style.display = 'none';
    document.getElementById('btn-fieldset3').style.display = 'none';
    document.querySelector('.step1').classList.remove('active');
    document.querySelector('.step3').classList.remove('active');
    document.querySelector('.step2').classList.add('active');
}
function prevFieldset() {
    document.getElementById('fieldset2').style.display = 'none';
    document.getElementById('btn-fieldset2').style.display = 'none';
    document.getElementById('fieldset1').style.display = 'block';
    document.getElementById('btn-fieldset1').style.display = 'block';
    document.getElementById('fieldset3').style.display = 'none';
    document.getElementById('btn-fieldset3').style.display = 'none';
    document.querySelector('.step3').classList.remove('active');
    document.querySelector('.step2').classList.remove('active');
    document.querySelector('.step1').classList.add('active');
}
function nextFieldset_Salary() {
    document.getElementById('fieldset1').style.display = 'none';
    document.getElementById('btn-fieldset1').style.display = 'none';
    document.getElementById('fieldset2').style.display = 'none';
    document.getElementById('btn-fieldset2').style.display = 'none';
    document.getElementById('fieldset3').style.display = 'block';
    document.getElementById('btn-fieldset3').style.display = 'block';
    document.querySelector('.step1').classList.remove('active');
    document.querySelector('.step2').classList.remove('active');
    document.querySelector('.step3').classList.add('active');
}
document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('btnSave_empHistory').style.display = 'none';
    document.getElementById('btnAdd_empHistory').style.display = 'none';
    document.getElementById('btnAdd_empSalary').style.display = 'none';
    document.getElementById('btnSave_empSalary').style.display = 'none';
    document.getElementById('btnSave_EmpTab').style.display = 'none';
    var msform = document.getElementById('msform');
    var inputs = msform.querySelectorAll('input, select, textarea');
    var msform_1 = document.getElementById('msform1');
    var input_1 = msform_1.querySelectorAll('input, select, textarea');
    var msform_2 = document.getElementById('msform2');
    var input_2 = msform_2.querySelectorAll('input, select, textarea');
    // Hàm để set tất cả các input/select/textarea về trạng thái disabled
    inputs.forEach(function (input) {
        input.setAttribute('disabled', 'true');
    });
    input_1.forEach(function (input) {
        if (!input.classList.contains('check_working')) {
            input.setAttribute('disabled', 'true');
        }
    });
    input_2.forEach(function (input) {
        if (!input.classList.contains('check_allowance')) {
            input.setAttribute('disabled', 'true');
        }
    });
    // Hàm để xử lý khi click vào nút 'Cancel' và "edit" của fieldset1
    document.getElementById('btn-cancel-fieldset1').addEventListener('click', function () {
        inputs.forEach(function (input) {
            input.setAttribute('disabled', 'true');
        });
        document.getElementById('btnSave_EmpTab').style.display = 'none';
    });
    document.getElementById('btn-edit-fieldset1').addEventListener('click', function () {
        inputs.forEach(function (input) {
            if (input.id !== 'emp_code' && input.id !== 'emp_id') {
                input.removeAttribute('disabled');
            }
        });
        document.getElementById('btnSave_EmpTab').style.display = 'block';
    });
    // Hàm để xử lý khi click vào nút 'Edit' và 'cancel' và "add" của fieldset2
    document.getElementById('btn-add-fieldset2').addEventListener('click', function () {
        input_1.forEach(function (input) {

            if (input.id === 'quit_date' || input.classList.contains('check_working_item')) {
                input.setAttribute('disabled', 'true');
            }
            else if (input.classList.contains('check_working')) {
                input.setAttribute('disabled', 'true');
            } else {
                input.removeAttribute('disabled');
            }
        });
       
        document.getElementById('note_working').value = '';
        document.getElementById('date_end').disabled = true;
        // Set giá trị ngày hiện tại
        var today = new Date();
        var dd = String(today.getDate()).padStart(2, '0');
        var mm = String(today.getMonth() + 1).padStart(2, '0');
        var yyyy = today.getFullYear();
        var todayString = yyyy + '-' + mm + '-' + dd;
        document.getElementById('date_start').value = todayString;
        document.getElementById('date_end').value = '';
        document.getElementById('quit_date').value = '';
        document.getElementById('probation_edit').checked = false;
        document.getElementById('select_quit').checked = false;

        document.getElementById('quit_date').disabled = true;
        document.getElementById('btnSave_empHistory').style.display = 'none';
        document.getElementById('btnAdd_empHistory').style.display = 'block';
    });
    document.getElementById('btn-cancel-fieldset2').addEventListener('click', function () {
        input_1.forEach(function (input) {
            if (!input.classList.contains('check_working')) {
                input.setAttribute('disabled', 'true');
            } else {
                input.removeAttribute('disabled');
            }
        });
      
        document.getElementById('btnSave_empHistory').style.display = 'none';
        document.getElementById('btnAdd_empHistory').style.display = 'none';
    });
    document.getElementById('btn-edit-fieldset2').addEventListener('click', function () {
        input_1.forEach(function (input) {
            if (input.id == 'quit_date' || input.classList.contains('check_working_item')) {
                input.setAttribute('disabled', 'true');
            } else{
                input.removeAttribute('disabled');
            }
        });
    
        //  var probationCheckbox = document.getElementById('probation_edit');
        var quitCheckbox = document.getElementById('select_quit');
        if (quitCheckbox && quitCheckbox.checked === true) {
            document.getElementById('quit_date').removeAttribute('disabled');

        }
        document.getElementById('btnSave_empHistory').style.display = 'block';
        document.getElementById('btnAdd_empHistory').style.display = 'none';
    });

    //------------------ Hàm để xử lý khi click vào nút 'Edit' và 'cancel' của fieldset3 -------------------
    document.getElementById('btn-add-fieldset3').addEventListener('click', function () {
        input_2.forEach(function (input) {
            if (input.classList.contains('check_allowance') || input.id == 'basicSalaryInput') {
                input.setAttribute('disabled', 'true');
                
            } else {
                input.removeAttribute('disabled');
            }
           
        });
      
        // Set giá trị ngày hiện tại
        var today = new Date();
        var dd = String(today.getDate()).padStart(2, '0');
        var mm = String(today.getMonth() + 1).padStart(2, '0');
        var yyyy = today.getFullYear();
        var todayString = yyyy + '-' + mm + '-' + dd;
        document.getElementById('begin_date').value = todayString;
        document.getElementById('end_date').value = '';
        document.getElementById('job_allowance').value = '';
        document.getElementById('phone_allowance').value = '';
        document.getElementById('petrol_allowance').value = '';
        document.getElementById('meal_allowance').value = '';
        document.getElementById('cashier_allowance').value = '';
        document.getElementById('house_allowance').value = '';
        document.getElementById('sup_allowance').value = '';
        document.getElementById('salaryDropdown').value = '';
        document.getElementById('basicSalaryInput').value = '';
        document.getElementById('btnAdd_empSalary').style.display = 'block';
        document.getElementById('btnSave_empSalary').style.display = 'none';
    });
    document.getElementById('btn-cancel-fieldset3').addEventListener('click', function () {
        input_2.forEach(function (input) {
            if (!input.classList.contains('check_allowance')) {
                input.setAttribute('disabled', 'true');
            } else {
                input.removeAttribute('disabled');
            }
        });
        document.getElementById('btnAdd_empSalary').style.display = 'none';
        document.getElementById('btnSave_empSalary').style.display = 'none';
    });
    document.getElementById('btn-edit-fieldset3').addEventListener('click', function () {
        input_2.forEach(function (input) {
            if (input.id === 'basicSalaryInput') {
                input.setAttribute('disabled','true');
            } else {
                input.removeAttribute('disabled');
            }
        });
        var checkContracts = document.getElementsByClassName('check_allowance');
        for (var i = 0; i < checkContracts.length; i++) {
            checkContracts[i].removeAttribute('disabled');
        }
        document.getElementById('btnAdd_empSalary').style.display = 'none';
        document.getElementById('btnSave_empSalary').style.display = 'block';
    });

});
// --------------------------------------- Update Employee Tab = AJAX ----------------------------------------
var msform_s = document.getElementById('msform');
var input_s = msform_s.querySelectorAll('input, select, textarea');
$('#msform').submit(function (event) {
    event.preventDefault(); // Ngăn chặn việc gửi form một cách truyền thống
    // Lấy ra dữ liệu từ form
    var formData = $(this).serialize();
    $.ajax({
        type: 'POST',
        url: '/Employees/UpdateEmp_Tab', // Thay đổi đường dẫn thành endpoint thực tế của bạn
        data: formData,
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
                // Vô hiệu hóa tất cả các trường input/select/textarea sau khi gửi thành công
                input_s.forEach(function (input) {
                    if (!input.classList.contains('check_working')) {
                        input.setAttribute('disabled', 'true');
                    } else {
                        input.removeAttribute('disabled');
                    }
                });

            } else {
                Swal.fire({
                    position: 'center-center',
                    icon: 'error',
                    title: response.message,
                    showConfirmButton: false,
                    timer: 3000
                });
                input_s.forEach(function (input) {
                    if (!input.classList.contains('check_working')) {
                        input.setAttribute('disabled', 'true');
                    } else {
                        input.removeAttribute('disabled');
                    }
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

/*-------------------------------------- Add, edit employee_history -----------------------------------------*/
$('#btn_add_his').click(function (event) {
    event.preventDefault();
    var formData_add = $('#msform1').serialize();
    $.ajax({
        type: 'POST',
        url: '/Employees/AddEmp_History',
        data: formData_add,
        dataType: 'json',
        success: function (response) {
            if (response.success) {
                Swal.fire({
                    position: 'center-center',
                    icon: 'success',
                    title: response.message,
                    showConfirmButton: false,
                    timer: 3000
                });
                var model_data = response.data
                var resultHtml = '';
                if (model_data.length > 0) {
                    var i = 1;
                    model_data.forEach(function (item) {
                        var begin_date = moment(item.CONGTACTU).format("DD/MM/YYYY");
                        var end_date = moment(item.CONGTACDEN).format("DD/MM/YYYY");
                        var quit_date = moment(item.DATE_QUIT).format("DD/MM/YYYY");
                        if (end_date === '01/01/0001') {
                            end_date = '';
                        }
                        if (quit_date === '01/01/0001') {
                            quit_date = '';
                        }
                        resultHtml += '<tr id="tr-work-' + item.HISTORY_ID + '">';
                        resultHtml += '<td><div class="form-check-inline mr-0"><input ' + (i === 1 ? "checked" : "") + ' name="radio_history_id" class="check_working form-check-input mr-0" type="radio" value="' + item.HISTORY_ID + '" /></div></td>';
                        resultHtml += '<td>' + i + '<input type = "hidden" class="department_id_work" value = "' + item.DEPARTMENT_ID + '" /> <input type = "hidden" class="section_id_work" value = "' + item.SECTION_ID + '" /> <input type="hidden" class="job_id_work" value="' + item.JOBTITLE_ID + '" /></td>';
                        resultHtml += '<td><div class="form-check-inline mr-0"><input disabled class="check_working_item form-check-input mr-0" ' + (item.PROBATION === true ? "checked" : "") + ' style="margin:auto;" type="checkbox" /></div></td>';
                        resultHtml += '<td>' + begin_date + '</td>';
                        resultHtml += '<td>' + end_date + '</td>';
                        resultHtml += '<td><div class="form-check-inline mr-0"><input disabled class="check_working_item form-check-input mr-0" ' + (item.QUIT === true ? "checked" : "") + ' style="margin:auto;" type="checkbox" /></div></td>';
                        resultHtml += '<td>' + quit_date + '</td>';
                        resultHtml += '<td>' + (item.QUIT_REASON ? item.QUIT_REASON : "") + '</td>';
                        resultHtml += '</tr>';
                        i++;
                    });
                }
                $('#tbody_work').html(resultHtml);
               /* input_s.forEach(function (input) {
                    if (!input.classList.contains('check_working')) {
                        input.setAttribute('disabled', 'true');
                        input.value == null;
                    } else {
                        input.removeAttribute('disabled');
                    }
                });*/
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
$('#btn_save_his').click(function (event1) {
    event1.preventDefault();
    var selectedRadio = $('input[name="radio_history_id"]:checked');
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
    var formData_save = $('#msform1').serialize();
    // Lưu trạng thái trước khi reload trang
    $.ajax({
        type: 'POST',
        url: '/Employees/UpdateEmp_History', // Thay đổi đường dẫn thành endpoint thực tế của bạn
        data: formData_save,
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
                var data_controller = response.data;
                var begin_date = moment(data_controller.CONGTACTU).format("DD/MM/YYYY");
                var end_date = moment(data_controller.CONGTACDEN).format("DD/MM/YYYY");
                var quit_date = moment(data_controller.DATE_QUIT).format("DD/MM/YYYY");
                if (end_date === '01/01/0001') {
                    end_date = '';
                }
                if (quit_date === '01/01/0001') {
                    quit_date = '';
                }
                // Get the selected radio button
                var selectedRadioValue = selectedRadio.val();
                var selectedRow = $('input[value="' + selectedRadioValue + '"]').closest('tr');
                selectedRow.find('.department_id_work').val(data_controller.DEPARTMENT_ID);
                selectedRow.find('.section_id_work').val(data_controller.SECTION_ID);
                selectedRow.find('.job_id_work').val(data_controller.JOBTITLE_ID);
                if (data_controller.PROBATION === 1) {
                    selectedRow.find('td:eq(2) input').prop('checked', true);
                } else {
                    selectedRow.find('td:eq(2) input').prop('checked', false);
                }
                selectedRow.find('td:eq(3)').text(begin_date);
                selectedRow.find('td:eq(4)').text(end_date);
                if (data_controller.QUIT === 1) {
                    selectedRow.find('td:eq(5) input').prop('checked', true);
                } else {
                    selectedRow.find('td:eq(5) input').prop('checked', false);
                }
                selectedRow.find('td:eq(6)').text(quit_date);
                selectedRow.find('td:eq(7)').text(data_controller.QUIT_REASON || "");

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
                icon: 'warning',
                title: 'Đã xảy ra lỗi khi gửi request: ' + error,
                showConfirmButton: false,
                timer: 3000
            });
        }
    });
});
/*---------------------------------------- Add, edit salary_history_tab ------------------------------------*/
$('#btn_save_salary').click(function (event2) {
    event2.preventDefault();
    var selectedRadio = $('input[name="radio_SalaryId"]:checked');
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
    var formData_save_sa = $('#msform2').serialize();
    // Lưu trạng thái trước khi reload trang
    $.ajax({
        type: 'POST',
        url: '/Employees/UpdateSalary_History',
        data: formData_save_sa,
        dataType: 'json',
        success: function (response) {
            if (response.success) {
                Swal.fire({
                    position: 'center-center',
                    icon: 'success',
                    title: response.message,
                    showConfirmButton: false,
                    timer: 3000

                });
                var model_data = response.data
                var begin_date = moment(model_data.BEGIN_DATE).format("DD/MM/YYYY");
                var end_date = moment(model_data.END_DATE).format("DD/MM/YYYY");
                if (end_date === '01/01/0001') {
                    end_date = '';
                }
                // Get the selected radio button
                var selectedRadioValue = selectedRadio.val();
                var selectedRow = $('input[value="' + selectedRadioValue + '"]').closest('tr');
                selectedRow.find('td:eq(1)').text(begin_date);
                selectedRow.find('.salary_cat_id').val(model_data.SALARY_CAT_ID);
                selectedRow.find('td:eq(2)').text(end_date);
                selectedRow.find('td:eq(3)').text(model_data.BASIC_SALARY === 0 ? "" : formatCurrency(model_data.BASIC_SALARY));
                selectedRow.find('td:eq(4)').text(model_data.ALLOWANCE_JOB === 0 ? "" : formatCurrency(model_data.ALLOWANCE_JOB));
                selectedRow.find('td:eq(5)').text(model_data.ALLOWANCE_PHONE === 0 ? "" : formatCurrency(model_data.ALLOWANCE_PHONE));
                selectedRow.find('td:eq(6)').text(model_data.ALLOWANCE_PETROL === 0 ? "" : formatCurrency(model_data.ALLOWANCE_PETROL));
                selectedRow.find('td:eq(7)').text(model_data.ALLOWANCE_MEAL === 0 ? "" : formatCurrency(model_data.ALLOWANCE_MEAL));
                selectedRow.find('td:eq(8)').text(model_data.ALLOWANCE_CASHIER === 0 ? "" : formatCurrency(model_data.ALLOWANCE_CASHIER));
                selectedRow.find('td:eq(9)').text(model_data.ALLOWANCE_HOUSE === 0 ? "" : formatCurrency(model_data.ALLOWANCE_HOUSE));
                selectedRow.find('td:eq(10)').text(model_data.ALLOWANCE_SUPPORT === 0 ? "" : formatCurrency(model_data.ALLOWANCE_SUPPORT));
                selectedRow.find('td:eq(11)').text(model_data.NOTE || "");
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
                icon: 'warning',
                title: 'Đã xảy ra lỗi khi gửi request: ' + error,
                showConfirmButton: false,
                timer: 3000
            });
        }
    });
});
$('#btn_add_salary').click(function (event3) {
    event3.preventDefault();
    var formData_add_sa = $('#msform2').serialize();
    $.ajax({
        type: 'POST',
        url: '/Employees/AddSalary_History',
        data: formData_add_sa,
        dataType: 'json',
        success: function (response) {
            if (response.success) {
                Swal.fire({
                    position: 'center-center',
                    icon: 'success',
                    title: response.message,
                    showConfirmButton: false,
                    timer: 3000

                });
                var model_data = response.data
                var resultHtml = '';
                if (model_data.length > 0) {
                    model_data.forEach(function (item) {
                        var begin_date = moment(item.BEGIN_DATE).format("DD/MM/YYYY");
                        var end_date = moment(item.END_DATE).format("DD/MM/YYYY");
                        if (end_date === '01/01/0001') {
                            end_date = '';
                        }
                        resultHtml += '<tr id="tr-salary-' + item.SALARY_HISTORY_ID + '">';
                        resultHtml += '<td><div class="form-check-inline mr-0"><input class="check_allowance form-check-input mr-0" name="radio_SalaryId" type="radio" value="' + item.SALARY_HISTORY_ID + '" /> <input type="hidden" class="salary_cat_id" value="' + item.SALARY_CAT_ID + '" /></div></td>';
                        resultHtml += '<td>' + begin_date + '</td>';
                        resultHtml += '<td>' + end_date + '</td>';
                        resultHtml += '<td>' + (item.BASIC_SALARY === 0 ? "" : formatCurrency(item.BASIC_SALARY)) + '</td>';
                        resultHtml += '<td>' + (item.ALLOWANCE_JOB === 0 ? "" : formatCurrency(item.ALLOWANCE_JOB)) + '</td>';
                        resultHtml += '<td>' + (item.ALLOWANCE_PHONE === 0 ? "" : formatCurrency(item.ALLOWANCE_PHONE)) + '</td>';
                        resultHtml += '<td>' + (item.ALLOWANCE_PETROL === 0 ? "" : formatCurrency(item.ALLOWANCE_PETROL)) + '</td>';
                        resultHtml += '<td>' + (item.ALLOWANCE_MEAL === 0 ? "" : formatCurrency(item.ALLOWANCE_MEAL)) + '</td>';
                        resultHtml += '<td>' + (item.ALLOWANCE_CASHIER === 0 ? "" : formatCurrency(item.ALLOWANCE_CASHIER)) + '</td>';
                        resultHtml += '<td>' + (item.ALLOWANCE_HOUSE === 0 ? "" : formatCurrency(item.ALLOWANCE_HOUSE)) + '</td>';
                        resultHtml += '<td>' + (item.ALLOWANCE_SUPPORT === 0 ? "" : formatCurrency(item.ALLOWANCE_SUPPORT)) + '</td>';
                        resultHtml += '<td>' + (item.NOTE ? item.NOTE : "") + '</td>';
                        resultHtml += '</tr>';
                    });
                }
                $('#tbody_salary').html(resultHtml);
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

/*------------------------------ Delete salary_history_tab employee_history ------------------------------------*/
$('#btn_del_salary').click(function (event4) {
    event4.preventDefault();
    // Kiểm tra xem có radio nào được chọn hay không
    var selectedRadio = $('input[name="radio_SalaryId"]:checked');
    if (selectedRadio.length === 0) {
        Swal.fire({
            position: 'center-center',
            icon: 'warning',
            title: 'Vui lòng chọn 1 hàng để xóa!',
            showConfirmButton: false,
            timer: 3000
        });
        return;
    }
    // Hiển thị cửa sổ xác nhận trước khi xóa
    Swal.fire({
        title: 'Bạn có chắc chắn muốn xóa?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Xóa',
        cancelButtonText: 'Hủy'
    }).then((result) => {
        if (result.isConfirmed) {
            var selectedId = selectedRadio.val();
            $.ajax({
                url: '/Employees/DeleteSalary',
                method: 'POST',
                data: {
                    id: selectedId,
                },
                success: function (response) {
                    if (response.success) {
                        Swal.fire({
                            position: 'center-center',
                            icon: 'success',
                            title: response.message,
                            showConfirmButton: false,
                            timer: 3000

                        });

                        $('#tr-salary-' + selectedId).hide();
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
        }
    });
});
$('#btnDel_EmpHistory').click(function (event5) {
    event5.preventDefault();
    // Kiểm tra xem có radio nào được chọn hay không
    var selectedRadio = $('input[name="radio_history_id"]:checked');
    if (selectedRadio.length === 0) {
        Swal.fire({
            position: 'center-center',
            icon: 'warning',
            title: 'Vui lòng chọn 1 hàng để xóa!',
            showConfirmButton: false,
            timer: 3000
        });
        return; // Dừng việc thực hiện khi không có radio nào được chọn
    }
    // Hiển thị cửa sổ xác nhận trước khi xóa
    Swal.fire({
        title: 'Bạn có chắc chắn muốn xóa?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Xóa',
        cancelButtonText: 'Hủy'
    }).then((result) => {
        if (result.isConfirmed) {
            var selectedId = selectedRadio.val();
            $.ajax({
                url: '/Employees/DeleteEmp_His',
                method: 'POST',
                data: {
                    id: selectedId
                },
                success: function (response) {
                    if (response.success) {
                        Swal.fire({
                            position: 'center-center',
                            icon: 'success',
                            title: response.message,
                            showConfirmButton: false,
                            timer: 3000
                        });
                        $('#tr-work-' + selectedId).hide();

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
                    // Xử lý lỗi
                }
            });
        }
    });
});

/*---------------------------------  Hàm định dạng số thành chuỗi tiền tệ -----------------------------------------*/
function formatCurrency(amount) {
    return amount.toLocaleString('vi-VN') + ' VND';
}

//------------------------ Đảm bảo khi trang được load, input date sẽ ở trạng thái disabled ----------------------------------
function toggleDateField(checkbox) {
    var quitDateField = document.getElementById('quit_date');
    if (checkbox.checked) {
        quitDateField.disabled = false;
        // Set giá trị ngày hiện tại
        var today = new Date();
        var dd = String(today.getDate()).padStart(2, '0');
        var mm = String(today.getMonth() + 1).padStart(2, '0');
        var yyyy = today.getFullYear();
        var todayString = yyyy + '-' + mm + '-' + dd;
        quitDateField.value = todayString;
    } else {
        quitDateField.disabled = true;
        quitDateField.value = ""; // Reset giá trị khi checkbox không được chọn
    }
}
// ------------------------- Hàm chuyển đổi ngày từ định dạng text "dd/MM/yyyy" sang "yyyy-MM-dd" ---------------------
function formatDateToISO(dateString) {
    var parts = dateString.split('/');
    if (parts.length === 3) {
        var day = parts[0];
        var month = parts[1];
        var year = parts[2];
        return year + '-' + month + '-' + day;
    }
    return dateString;
}
// ---------------------------- chuyển đổi định dạng vnd => number ----------------------------------
function convertVNDToNumber(vndString) {
    // Loại bỏ ký tự không phải số từ chuỗi
    var numberString = vndString.replace(/[^\d]/g, '');
    // Chuyển chuỗi số sang dạng số nguyên
    var number = parseInt(numberString);
    return number;
}
// ----------------------------------------- Get all elements with the class name 'check_working' -------------------------------
$('#working_table').on('change', 'input.check_working', function () {
    // Get the values from the selected row
    var selectedRow = $(this).closest('tr');
    var work_Id = $(this).val();
    document.getElementById('history_id_edit').value = work_Id;
    var probationChecked = selectedRow.find('td:eq(2) input').is(':checked');
    var startDateValue = selectedRow.find('td:eq(3)').text().trim();
    var endDateValue = selectedRow.find('td:eq(4)').text().trim();
    var quitChecked = selectedRow.find('td:eq(5) input').is(':checked');
    var quitDateValue = selectedRow.find('td:eq(6)').text().trim();
    var noteValue = selectedRow.find('td:eq(7)').text().trim();
    var departmentIdHidden = selectedRow.find('.department_id_work').val();
    var sectionIdHidden = selectedRow.find('.section_id_work').val();
    var jobIdHidden = selectedRow.find('.job_id_work').val();
    // Set the values to the corresponding input fields
    document.getElementById('date_start').value = formatDateToISO(startDateValue);
    document.getElementById('date_end').value = formatDateToISO(endDateValue);
    document.getElementById('quit_date').value = formatDateToISO(quitDateValue);
    document.getElementById('note_working').value = noteValue;
    var probationCheckbox = document.getElementById('probation_edit');
    probationCheckbox.checked = probationChecked;

    var quitCheckbox = document.getElementById('select_quit');
    quitCheckbox.checked = quitChecked;
    if (quitCheckbox && quitCheckbox.checked === true) {

    } else {
        document.getElementById('quit_date').setAttribute('disabled', 'true');
    }
    // Bắt sự kiện chọn cho các select tương ứng với giá trị từ input ẩn
    var selects = [
        { id: 'departmentSelect', hiddenValue: departmentIdHidden },
        { id: 'sectionSelect', hiddenValue: sectionIdHidden },
        { id: 'jobSelect', hiddenValue: jobIdHidden }
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
// ------------------------- Xử lý sự kiện ở Salary history ----------------------------------------------
document.getElementById('salaryDropdown').addEventListener('change', function () {
    var selectedValue = parseFloat(this.value); // Chuyển đổi giá trị sang kiểu số (nếu cần thiết)
    var selectedOption = this.options[this.selectedIndex];
    var selectedID = selectedOption.getAttribute('data-id');
    var formattedValue = selectedValue.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
    document.getElementById('basicSalaryInput').value = formattedValue;
    document.getElementById('salary_level_id').value = selectedID;
});
//var radioBtn_salary = document.querySelectorAll('.check_allowance');
//radioBtn_salary.forEach(function (radioButton1) {
//    radioButton1.addEventListener('click', function () {
//        var selectedValue = this.value;
//        document.getElementById('SalaryId_edit').value = selectedValue;
//        var row = this.closest('tr');
//        var cells = row.getElementsByTagName('td');

//        var startDateValue = cells[1].innerText;
//        var endDateValue = cells[2].innerText;
//        var basicSalaryValue = cells[3].innerText;
//        var jobValue = cells[4].innerText;
//        var phoneValue = cells[5].innerText;
//        var petrolValue = cells[6].innerText;
//        var mealValue = cells[7].innerText;
//        var cashierValue = cells[8].innerText;
//        var houseValue = cells[9].innerText;
//        var supValue = cells[10].innerText;
//        var note_salaryValue = cells[11].innerText;
//        salaryCatIdHidden = row.querySelector('.salary_cat_id').value;
//        // gán giá trị
//        document.getElementById('end_date').value = formatDateToISO(endDateValue);
//        document.getElementById('begin_date').value = formatDateToISO(startDateValue);
//        document.getElementById('basicSalaryInput').value = basicSalaryValue;
//        document.getElementById('salary_note').value = note_salaryValue;
//        document.getElementById('job_allowance').value = convertVNDToNumber(jobValue);
//        document.getElementById('phone_allowance').value = convertVNDToNumber(phoneValue);
//        document.getElementById('petrol_allowance').value = convertVNDToNumber(petrolValue);
//        document.getElementById('meal_allowance').value = convertVNDToNumber(mealValue);
//        document.getElementById('cashier_allowance').value = convertVNDToNumber(cashierValue);
//        document.getElementById('house_allowance').value = convertVNDToNumber(houseValue);
//        document.getElementById('sup_allowance').value = convertVNDToNumber(supValue);

//        var salarySelect = document.getElementById('salaryDropdown');
//        var option_sa = salarySelect.querySelectorAll('option');
//        for (var i = 0; i < option_sa.length; i++) {
//            var optionDataId = option_sa[i].getAttribute('data-id');
//            if (optionDataId && optionDataId === salaryCatIdHidden) {
//                option_sa[i].selected = true;
//                document.getElementById('salary_level_id').value = optionDataId;
//            } else {
//                option_sa[i].selected = false;
//            }
//        }

//    });
//});
$('#salary_history_table').on('change', 'input.check_allowance', function () {
    // Get the values from the selected row
    var selectedRow = $(this).closest('tr');
    var salary_Id = $(this).val();
    document.getElementById('SalaryId_edit').value = salary_Id;

    // Set the values to the corresponding input fields
    var startDateValue = selectedRow.find('td:eq(1)').text().trim();
    var endDateValue = selectedRow.find('td:eq(2)').text().trim();
    var basicSalaryValue = selectedRow.find('td:eq(3)').text().trim();
    var jobValue = selectedRow.find('td:eq(4)').text().trim();
    var phoneValue = selectedRow.find('td:eq(5)').text().trim();
    var petrolValue = selectedRow.find('td:eq(6)').text().trim();
    var mealValue = selectedRow.find('td:eq(7)').text().trim();
    var cashierValue = selectedRow.find('td:eq(8)').text().trim();
    var houseValue = selectedRow.find('td:eq(9)').text().trim();
    var supValue = selectedRow.find('td:eq(10)').text().trim();
    var note_salaryValue = selectedRow.find('td:eq(11)').text().trim();
    var salaryCatIdHidden = selectedRow.find('.salary_cat_id').val();
    // gán giá trị
    document.getElementById('end_date').value = formatDateToISO(endDateValue);
    document.getElementById('begin_date').value = formatDateToISO(startDateValue);
    document.getElementById('basicSalaryInput').value = basicSalaryValue;
    document.getElementById('salary_note').value = note_salaryValue;
    document.getElementById('job_allowance').value = convertVNDToNumber(jobValue);
    document.getElementById('phone_allowance').value = convertVNDToNumber(phoneValue);
    document.getElementById('petrol_allowance').value = convertVNDToNumber(petrolValue);
    document.getElementById('meal_allowance').value = convertVNDToNumber(mealValue);
    document.getElementById('cashier_allowance').value = convertVNDToNumber(cashierValue);
    document.getElementById('house_allowance').value = convertVNDToNumber(houseValue);
    document.getElementById('sup_allowance').value = convertVNDToNumber(supValue);
    // Bắt sự kiện chọn cho các select tương ứng với giá trị từ input ẩn
    var salarySelect = document.getElementById('salaryDropdown');
    var option_sa = salarySelect.querySelectorAll('option');
    for (var i = 0; i < option_sa.length; i++) {
        var optionDataId = option_sa[i].getAttribute('data-id');
        if (optionDataId && optionDataId === salaryCatIdHidden) {
            option_sa[i].selected = true;
            document.getElementById('salary_level_id').value = optionDataId;
        } else {
            option_sa[i].selected = false;
        }
    }

});