$(document).ready(function () {
    //--------------------------- quy đổi ra tiền tệ VND -----------------------------------
    function formatCurrency(amount) {
        return amount.toLocaleString('vi-VN') + ' VND';
    }
    function convertVNDToNumber(vndString) {
        // Loại bỏ ký tự không phải số từ chuỗi
        var numberString = vndString.replace(/[^\d]/g, '');
        // Chuyển chuỗi số sang dạng số nguyên
        var number = parseInt(numberString);
        return number;
    }
    //---------------------- bắt sự kiện display ='none', disabled = 'true', remove cho các thẻ button --------------------
    function hideOtherButtons(buttons) {
        const btn_tabs_none = document.querySelectorAll(buttons.join(', '));
        btn_tabs_none.forEach(button => {
            button.style.display = 'none';
        });
    }
    function disableOtherButtons(buttons) {
        const btns = document.querySelectorAll(buttons.join(', '));
        btns.forEach(button => {
            button.setAttribute('disabled', 'true');
        });
    }
    function enableAllButtons(buttons) {
        const btns = document.querySelectorAll(buttons.join(', '));
        btns.forEach(button => {
            button.removeAttribute('disabled');
        });
    }
    //--------------------- edit datatalbe -----------------------------
   var table_section = function () {
        return $('#table-list-section').DataTable({
            "pageLength": 10,
            "lengthMenu": [
                [10, 20, 50, -1],
                [10, 20, 50, "All"]
            ],
            "columnDefs": [
                { "orderable": false, "targets": [0, 3] }
            ]
        });
    };
    var table_job = function () {
        return $('#table-list-job').DataTable({
            "pageLength": 10,
            "lengthMenu": [
                [10, 20, 50, -1],
                [10, 20, 50, "All"]
            ],
            "columnDefs": [
                { "orderable": false, "targets": [0, 2] }
            ]
        });
    };
    var table_salaryLevel = function () {
        return $('#table-list-salaryLevel').DataTable({
            "pageLength": 10,
            "lengthMenu": [
                [10, 20, 50, -1],
                [10, 20, 50, "All"]
            ],
            "columnDefs": [
                { "orderable": false, "targets": [0] }
            ]
        });
    };
    // Gọi hàm để khởi tạo DataTable lần đầu
    var tableSection = table_section();
    var tableJobTitle = table_job();
    var tableSalaryLv = table_salaryLevel();
    
    // ------------------------------- đặt giá trị ban đầu cho form --------------------------
    var msform = document.getElementById('msform');
    var input_ = msform.querySelectorAll('input, select');
    var msform_1 = document.getElementById('msform1');
    var input_1 = msform_1.querySelectorAll('input, select');
    var msform_2 = document.getElementById('msform2');
    var input_2 = msform_2.querySelectorAll('input, select');

    var form_status_tab;
    //------------------------ bắt xự kiện khi load trang ban đầu vào -------------------------
    $('fieldset').hide();
    // gán targerr = tab1 khi vào load vào trang
    var target = "#tab1";
    if (target == "#tab1") {
        document.getElementById('tab1').style.display = "block";
        document.getElementById('btn-fieldset-tab1').style.display = "block";
        document.getElementById('btn_save_tab1').setAttribute('disabled', 'true');
        hideOtherButtons(['#btn-fieldset-tab2', '#btn-fieldset-tab3']);
    }
    // bắt sự kiện khi click vào button trong danh sách danh mục
    $('a[data-toggle="tab"]').on('click', function () {
        // Lấy id của fieldset được chọn
        target = $(this).data('target');
        $('fieldset').hide();
        $('a[data-toggle="tab"]').removeClass('active');
        $(this).addClass('active');
        // Hiển thị fieldset tương ứng
        $(target).show();
        if (target == "#tab1") {
            document.getElementById('btn-fieldset-tab1').style.display = "block";
            document.getElementById('btn_save_tab1').setAttribute('disabled', 'true');
            hideOtherButtons(['#btn-fieldset-tab2', '#btn-fieldset-tab3',]);
        } else if (target == "#tab2") {
            document.getElementById('btn-fieldset-tab2').style.display = "block";
            document.getElementById('btn_save_tab2').setAttribute('disabled', 'true');
            hideOtherButtons(['#btn-fieldset-tab1', '#btn-fieldset-tab3']);
        }
        else if (target == "#tab3") {
            document.getElementById('btn-fieldset-tab3').style.display = "block";
            document.getElementById('btn_save_tab3').setAttribute('disabled', 'true');
            hideOtherButtons(['#btn-fieldset-tab1', '#btn-fieldset-tab2']);
        }
    });
    //---------------------------------- tab1 ------------------------------
    input_.forEach(function (input) {
        if (!input.classList.contains('section_select')) {
            input.setAttribute('disabled', 'true');
        }
    });
    // Hàm để xử lý khi click vào nút 'Cancel' và "edit" ,'add' của tab1
    document.getElementById('btn_cancel_tab1').addEventListener('click', function () {
        input_.forEach(function (input) {
            if (!input.classList.contains('section_select')) {
                input.setAttribute('disabled', 'true');
            } else {
                input.removeAttribute('disabled');
            }
        });
        document.getElementById('btn_save_tab1').setAttribute('disabled', 'true');
        enableAllButtons(['#btn_add_tab1', '#btn_edit_tab1', '#btn_del_tab1']);
        form_status_tab = "";
    });
    document.getElementById('btn_edit_tab1').addEventListener('click', function () {
        input_.forEach(function (input) {

            input.removeAttribute('disabled');

        });
        document.getElementById('section_VN').value = "";
        document.getElementById('section_EN').value = "";
        document.getElementById('btn_save_tab1').removeAttribute('disabled');
        disableOtherButtons(['#btn_add_tab1', '#btn_edit_tab1', '#btn_del_tab1']);
        form_status_tab = "EditTab1";
    });
    document.getElementById('btn_add_tab1').addEventListener('click', function () {
        input_.forEach(function (input) {
            if (input.classList.contains('section_select')) {
                input.setAttribute('disabled', 'true');
            } else {
                input.removeAttribute('disabled');
                document.getElementById('section_VN').value = "";
                document.getElementById('section_EN').value = "";
            }
        });
        form_status_tab = "AddNewTab1";
        document.getElementById('btn_save_tab1').removeAttribute('disabled');
        disableOtherButtons(['#btn_add_tab1', '#btn_edit_tab1', '#btn_del_tab1']);
    });
    // bắt sự kiện khi tích chọn radio button của tab1
    $('#table-list-section').on('change', 'input.section_select', function () {
        // Get the values from the selected row
        var selectedRow = $(this).closest('tr');
        var section_Id = $(this).val();
        //document.getElementById('history_id_edit').value = work_Id;
        var sectionEnValue = selectedRow.find('td:eq(2)').text().trim();
        var sectionVnValue = selectedRow.find('td:eq(3)').text().trim();
        var departmentIdHidden = selectedRow.find('.department_id_select').val();
        // Set the values to the corresponding input fields
        document.getElementById('section_EN').value = sectionEnValue;
        document.getElementById('section_VN').value = sectionVnValue;

        // Bắt sự kiện chọn cho các select tương ứng với giá trị từ input ẩn
        var selects = [
            { id: 'departmentSelect', hiddenValue: departmentIdHidden }
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
    // xử lý sự kiện khi click vào nút save tab1
    $('#btn_save_tab1').click(function (event) {
        event.preventDefault();
        var formData_save = $('#msform').serialize();
        if (form_status_tab == "AddNewTab1") {
            $.ajax({
                type: 'POST',
                url: '/Setup/AddNewSection',
                data: formData_save,
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
                        var item = response.data
                        var resultHtml = '';
                        resultHtml += '<tr id="tr_section_' + item.SECTION_ID + '">';
                        resultHtml += '<td><div class="form-check-inline mr-0"><input class="section_select form-check-input mr-0" name="section_id_radio" type="radio" value="' + item.SECTION_ID + '" /><input type="hidden" class="department_id_select" value="' + item.DEPARTMENT_ID + '" /></div></td>';
                        resultHtml += '<td>' + item.DEPARTMENT_NAME_VI + '</td>';
                        resultHtml += '<td>' + item.SECTION_NAME_EN + '</td>';
                        resultHtml += '<td>' + item.SECTION_NAME_VI + '</td>';
                        resultHtml += '</tr>';           
                        // Phá hủy DataTable hiện tại
                        var table = $('#table-list-section').DataTable();
                        table.destroy();
                        // $('#tbody_section').html(resultHtml);
                        $('#tbody_section').prepend(resultHtml);
                        // Khởi tạo lại DataTable
                        tableSection = table_section();

                    } else {
                        Swal.fire({
                            position: 'center-center',
                            icon: 'error',
                            title: response.message,
                            showConfirmButton: false,
                            timer: 3000
                        });
                    }
                    document.getElementById('btn_cancel_tab1').click();
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
        }
        else if (form_status_tab == "EditTab1") {
            var selectedRadio = $('input[name="section_id_radio"]:checked');
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
            $.ajax({
                type: 'POST',
                url: '/Setup/UpdateSection',
                data: formData_save,
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
                        var controller_data = response.data

                        // Get the selected radio button
                        var selectedRow = selectedRadio.closest('tr');
                        selectedRow.find('.department_id_select').val(controller_data.DEPARTMENT_ID);
                        selectedRow.find('td:eq(1)').text(controller_data.DEPARTMENT_NAME_VI);
                        selectedRow.find('td:eq(2)').text(controller_data.SECTION_NAME_EN);
                        selectedRow.find('td:eq(3)').text(controller_data.SECTION_NAME_VI);
                    } else {
                        Swal.fire({
                            position: 'center-center',
                            icon: 'error',
                            title: response.message,
                            showConfirmButton: false,
                            timer: 3000
                        });
                    }
                    document.getElementById('btn_cancel_tab1').click();
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
        }
    });
    // xử ký sư kiện khi click vào delete tab1
    $('#btn_del_tab1').click(function (e) {
        e.preventDefault();
        var selectedRadio_del = $('input[name="section_id_radio"]:checked');
        if (selectedRadio_del.length === 0) {
            Swal.fire({
                position: 'center-center',
                icon: 'warning',
                title: 'Vui lòng chọn 1 hàng để chỉnh sửa!',
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
                var selectedId = selectedRadio_del.val();
                $.ajax({
                    type: 'POST',
                    url: '/Setup/DeleteSection',
                    data: { id: selectedId },
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
                            $('#tr_section_' + selectedId).hide();

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
            }
        });
    });
    // ---------------------------------- tab2 -------------------------------------
    input_1.forEach(function (input) {
        if (!input.classList.contains('job_select')) {
            input.setAttribute('disabled', 'true');
        }
    });
    document.getElementById('btn_add_tab2').addEventListener('click', function () {
        input_1.forEach(function (input) {
            if (input.classList.contains('job_select')) {
                input.setAttribute('disabled', 'true');
            } else {
                input.removeAttribute('disabled');
            }
        });
        document.getElementById('job_name_VN').value = "";
        document.getElementById('job_name_EN').value = "";
        form_status_tab = "AddNewTab2";
        document.getElementById('btn_save_tab2').removeAttribute('disabled');
        disableOtherButtons(['#btn_add_tab2', '#btn_edit_tab2', '#btn_del_tab2']);
    });
    document.getElementById('btn_cancel_tab2').addEventListener('click', function () {
        input_1.forEach(function (input) {
            if (!input.classList.contains('job_select')) {
                input.setAttribute('disabled', 'true');
            } else {
                input.removeAttribute('disabled');
            }
        });
        document.getElementById('btn_save_tab2').setAttribute('disabled', 'true');
        enableAllButtons(['#btn_add_tab2', '#btn_edit_tab2', '#btn_del_tab2']);
        form_status_tab = "";
    });
    document.getElementById('btn_edit_tab2').addEventListener('click', function () {
        input_1.forEach(function (input) {
            input.removeAttribute('disabled');
        });
        document.getElementById('btn_save_tab2').removeAttribute('disabled');
        disableOtherButtons(['#btn_add_tab2', '#btn_edit_tab2', '#btn_del_tab2']);
        form_status_tab = "EditTab2";
    });
    // xử lý sự kiện click vào table thì truyền dữ liệu vào textbox
    $('#table-list-job').on('change', 'input.job_select', function () {
        // Get the values from the selected row
        var selectedRow = $(this).closest('tr');
        var job_Id = $(this).val();
        //document.getElementById('history_id_edit').value = work_Id;
        var jobVnValue = selectedRow.find('td:eq(1)').text().trim();
        var jobEnValue = selectedRow.find('td:eq(2)').text().trim();
        document.getElementById('job_name_VN').value = jobVnValue;
        document.getElementById('job_name_EN').value = jobEnValue;
    });

    $('#btn_save_tab2').click(function (event) {
        event.preventDefault();
        var formData_save = $('#msform1').serialize();
        if (form_status_tab == "AddNewTab2") {
            $.ajax({
                type: 'POST',
                url: '/Setup/AddNewJob_Tab',
                data: formData_save,
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

                        resultHtml += '<tr id="tr_job_' + model_data.JobTitle_ID + '">';
                        resultHtml += '<td><div class="form-check-inline mr-0"><input class="job_select form-check-input mr-0" name="job_id_select" type="radio" value="' + model_data.JobTitle_ID + '" /></div></td>';
                        resultHtml += '<td>' + model_data.TitleName_vi + '</td>';
                        resultHtml += '<td>' + model_data.TitleName_en + '</td>';
                        resultHtml += '</tr>';

                        // Phá hủy DataTable hiện tại
                        var table = $('#table-list-job').DataTable();
                        table.destroy();
                        // $('#tbody_section').html(resultHtml);
                        $('#tbody_job').prepend(resultHtml);
                        // Khởi tạo lại DataTable
                        tableJobTitle = table_job();
                        
                    } else {
                        Swal.fire({
                            position: 'center-center',
                            icon: 'error',
                            title: response.message,
                            showConfirmButton: false,
                            timer: 3000
                        });
                    }
                    document.getElementById('btn_cancel_tab2').click();
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
        }
        else if (form_status_tab == "EditTab2") {
            var selectedRadio = $('input[name="job_id_select"]:checked');
            if (selectedRadio.length === 0) {
                Swal.fire({
                    position: 'center-center',
                    icon: 'warning',
                    title: 'Vui lòng chọn 1 hàng để chỉnh sửa!',
                    showConfirmButton: false,
                    timer: 3000
                });
                return;
            }
            $.ajax({
                type: 'POST',
                url: '/Setup/UpdateJob_Tab',
                data: formData_save,
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
                        var controller_data = response.data

                        // Get the selected radio button
                        var selectedRow = selectedRadio.closest('tr');
                        selectedRow.find('td:eq(1)').text(controller_data.TitleName_vi);
                        selectedRow.find('td:eq(2)').text(controller_data.TitleName_en);
                    } else {
                        Swal.fire({
                            position: 'center-center',
                            icon: 'error',
                            title: response.message,
                            showConfirmButton: false,
                            timer: 3000
                        });
                    }
                    document.getElementById('btn_cancel_tab2').click();
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
        }
    });

    $('#btn_del_tab2').click(function (e) {
        e.preventDefault();
        var selectedRadio_del = $('input[name="job_id_select"]:checked');
        if (selectedRadio_del.length === 0) {
            Swal.fire({
                position: 'center-center',
                icon: 'warning',
                title: 'Vui lòng chọn 1 hàng để chỉnh sửa!',
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
                var selectedId = selectedRadio_del.val();
                $.ajax({
                    type: 'POST',
                    url: '/Setup/DeleteJob_Title',
                    data: { id: selectedId },
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
                            $('#tr_job_' + selectedId).hide();

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
            }
        });
    });
    //----------------------------------- tab3 --------------------------------------
    input_2.forEach(function (input) {
        if (!input.classList.contains('salarylv_select')) {
            input.setAttribute('disabled', 'true');
        }
    });
    $('#table-list-salaryLevel').on('change', 'input.salarylv_select', function () {
        // Get the values from the selected row
        var selectedRow = $(this).closest('tr');
        var salarylv_Id = $(this).val();
        //document.getElementById('history_id_edit').value = work_Id;
        var salaryValue = selectedRow.find('td:eq(2)').text().trim();
        var salaryCode = selectedRow.find('td:eq(1)').text().trim();
        document.getElementById('salary_code').value = salaryCode;
        document.getElementById('salary_value').value = convertVNDToNumber(salaryValue);
    });

    // Hàm để xử lý khi click vào nút 'Cancel' và "edit" ,'add' của tab3
    document.getElementById('btn_cancel_tab3').addEventListener('click', function () {
        input_2.forEach(function (input) {
            if (!input.classList.contains('salarylv_select')) {
                input.setAttribute('disabled', 'true');
            } else {
                input.removeAttribute('disabled');
            }
        });
        document.getElementById('btn_save_tab3').setAttribute('disabled', 'true');
        enableAllButtons(['#btn_add_tab3', '#btn_edit_tab3', '#btn_del_tab3']);
        form_status_tab = "";
    });
    document.getElementById('btn_edit_tab3').addEventListener('click', function () {
        input_2.forEach(function (input) {

            input.removeAttribute('disabled');

        });
        document.getElementById('btn_save_tab3').removeAttribute('disabled');
        disableOtherButtons(['#btn_add_tab3', '#btn_edit_tab3', '#btn_del_tab3']);
        form_status_tab = "EditTab3";
    });
    document.getElementById('btn_add_tab3').addEventListener('click', function () {
        input_2.forEach(function (input) {
            if (input.classList.contains('salarylv_select')) {
                input.setAttribute('disabled', 'true');
            } else {
                input.removeAttribute('disabled');
       
            }
        });
        document.getElementById('salary_code').value = "";
        document.getElementById('salary_value').value = "";
        form_status_tab = "AddNewTab3";
        document.getElementById('btn_save_tab3').removeAttribute('disabled');
        disableOtherButtons(['#btn_add_tab3', '#btn_edit_tab3', '#btn_del_tab3']);
    });
    $('#btn_save_tab3').click(function (e) {
        e.preventDefault();
        var formData_save = $('#msform2').serialize();
        if (form_status_tab == "AddNewTab3") {
            $.ajax({
                type: 'POST',
                url: '/Setup/AddSalary_Level',
                data: formData_save,
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

                        resultHtml += '<tr id="tr_salarylv_' + model_data.ID + '">';
                        resultHtml += '<td><div class="form-check-inline mr-0"><input class="salarylv_select form-check-input mr-0" name="salarylv_id_select" type="radio" value="' + model_data.ID + '" /></div></td>';
                        resultHtml += '<td>' + model_data.SALARY_CODE + '</td>';
                        resultHtml += '<td>' + (model_data.SALARY_VALUE === 0 ? "" : formatCurrency(model_data.SALARY_VALUE)) + '</td>';
                        resultHtml += '</tr>';

                        // Phá hủy DataTable hiện tại
                        var table = $('#table-list-salaryLevel').DataTable();
                        table.destroy();
                        // $('#tbody_section').html(resultHtml);
                        $('#tbody_salary_level').prepend(resultHtml);
                        // Khởi tạo lại DataTable
                        tableSalaryLv = table_salaryLevel();

                    } else {
                        Swal.fire({
                            position: 'center-center',
                            icon: 'error',
                            title: response.message,
                            showConfirmButton: false,
                            timer: 3000
                        });
                    }
                    document.getElementById('btn_cancel_tab3').click();
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
        }
        else if (form_status_tab == "EditTab3") {
            var selectedRadio = $('input[name="salarylv_id_select"]:checked');
            if (selectedRadio.length === 0) {
                Swal.fire({
                    position: 'center-center',
                    icon: 'warning',
                    title: 'Vui lòng chọn 1 hàng để chỉnh sửa!',
                    showConfirmButton: false,
                    timer: 3000
                });
                return;
            }
            $.ajax({
                type: 'POST',
                url: '/Setup/UpdateSalary_Level',
                data: formData_save,
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
                        var controller_data = response.data

                        // Get the selected radio button
                        var selectedRow = selectedRadio.closest('tr');
                        selectedRow.find('td:eq(1)').text(controller_data.SALARY_CODE);
                        selectedRow.find('td:eq(2)').text(controller_data.SALARY_VALUE=== 0 ? "" : formatCurrency(controller_data.SALARY_VALUE));
                    } else {
                        Swal.fire({
                            position: 'center-center',
                            icon: 'error',
                            title: response.message,
                            showConfirmButton: false,
                            timer: 3000
                        });
                    }
                    document.getElementById('btn_cancel_tab3').click();
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
        }
    });
    $('#btn_del_tab3').click(function (e) {
        e.preventDefault();
        var selectedRadio_del = $('input[name="salarylv_id_select"]:checked');
        if (selectedRadio_del.length === 0) {
            Swal.fire({
                position: 'center-center',
                icon: 'warning',
                title: 'Vui lòng chọn 1 hàng để chỉnh sửa!',
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
                var selectedId = selectedRadio_del.val();
                $.ajax({
                    type: 'POST',
                    url: '/Setup/DeleteSalary_Level',
                    data: { id: selectedId },
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
                            $('#tr_salarylv_' + selectedId).hide();

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
            }
        });
    });
});