$(document).ready(function () {
    function updateSelectedCount() {
        // Đếm số lượng các checkbox phòng ban đã được chọn
        var selectedDepartments = $('.department-checkbox:checked').length;
        $('#count_select_department').text(selectedDepartments);

        // Đếm số lượng các checkbox phần đã được chọn
        var selectedSections = $('.section-checkbox:checked').length;
        $('#count_select_section').text(selectedSections);
    }
    // Khi checkbox của phòng ban được thay đổi
    $('.department-checkbox').change(function () {
        var departmentId = $(this).val();
        var isChecked = $(this).prop('checked');

        // Cập nhật trạng thái checkbox phần dựa trên checkbox phòng ban tương ứng
        $('.section-checkbox[data-department-id="' + departmentId + '"]').prop('checked', isChecked);

        // Kiểm tra và cập nhật trạng thái checkbox "Chọn tất cả" của phòng ban
        var allDepartmentsChecked = $('.department-checkbox:checked').length === $('.department-checkbox').length;
        $('#chk_all_department').prop('checked', allDepartmentsChecked);

        // Cập nhật số lượng đã chọn
        updateSelectedCount();
        // Gán checked cho input có id "staffBySection_cb"
        $('#staffBySection_cb').prop('checked', isChecked);
    });
    // Khi checkbox "Chọn tất cả" của phòng ban được thay đổi
    $('#chk_all_department').change(function () {
        var allDepartmentsChecked = $(this).prop('checked');
        $('.department-checkbox').prop('checked', allDepartmentsChecked);

        // Cập nhật trạng thái của tất cả checkbox phần tương ứng dựa trên trạng thái của checkbox "Chọn tất cả" phòng ban
        $('.section-checkbox').prop('checked', allDepartmentsChecked);

        // Cập nhật số lượng đã chọn
        updateSelectedCount();
        // Gán checked cho input có id "staffBySection_cb"
        $('#staffBySection_cb').prop('checked', allDepartmentsChecked);
    });
    // Khi checkbox "Chọn tất cả" của section được thay đổi
    $('#check_all_section').change(function () {
        var allSectionChecked = $(this).prop('checked')
        $('.section-checkbox').prop('checked', allSectionChecked);
        
        var uniqueDepartments = [];
        $('.section-checkbox').each(function () {
            var departmentId = $(this).data('department-id');
            if (uniqueDepartments.indexOf(departmentId) === -1) {
                uniqueDepartments.push(departmentId);
            }
        });
        uniqueDepartments.forEach(function (departmentId) {
            var allChecked = $('.section-checkbox[data-department-id="' + departmentId + '"]:checked').length === $('.section-checkbox[data-department-id="' + departmentId + '"]').length;
            $('.department-checkbox[value="' + departmentId + '"]').prop('checked', allChecked);
        });

        updateSelectedCount();
        // Gán checked cho input có id "staffBySection_cb"
        $('#staffBySection_cb').prop('checked', allSectionChecked);
    });
    // Khi bất kỳ checkbox nào khác được bỏ chọn, kiểm tra xem có phải tất cả đều được chọn không
    $('.section-checkbox').change(function () {
        var departmentId = $(this).data('department-id');
       
        // Kiểm tra và cập nhật trạng thái checkbox phòng ban tương ứng
        if ($('.section-checkbox[data-department-id="' + departmentId + '"]:checked').length != 0) {
            $('.department-checkbox[value="' + departmentId + '"]').prop('checked', true);
        } else {
            $('.department-checkbox[value="' + departmentId + '"]').prop('checked', false);
        }
        // Cập nhật số lượng đã chọn
        updateSelectedCount();
        // Gán checked cho input có id "staffBySection_cb"
        $('#staffBySection_cb').prop('checked', $(this).prop('checked'));
    });
    // Cập nhật số lượng đã chọn ban đầu
    updateSelectedCount();

    // ----------------------------------- bắt lỗi sự kiện -------------------------------
    var hasError = false; // Biến để kiểm tra xem có lỗi không
    // Sự kiện khi blur khỏi input mã nhân viên
    $('input[name="code_emp"]').on('blur', function () {
        var inputVal = $(this).val().trim();
        // Kiểm tra nếu giá trị nhập vào không rỗng
        if (inputVal !== '') {
            $.ajax({
                url: '/Employees/CheckEmployeeCode',
                method: 'POST',
                data: { empCode: inputVal },
                success: function (data) {
                    if (data.isExists == 0) {
                        // Nếu mã nhân viên không tồn tại, hiển thị thông báo lỗi
                        $('input[name="code_emp"]').addClass('is-invalid');
                        $('input[name="code_emp"]').closest('.form-group').find('.invalid-feedback').remove();
                        $('input[name="code_emp"]').after('<div class="invalid-feedback">Mã nhân viên không tồn tại.</div>');
                        hasError = true; // Đặt biến lỗi là true
                    } else {
                        // Nếu mã nhân viên không trùng, xóa thông báo lỗi nếu có
                        $('input[name="code_emp"]').removeClass('is-invalid');
                        $('input[name="code_emp"]').closest('.form-group').find('.invalid-feedback').remove();
                        hasError = false; // Đặt biến lỗi là false
                    }
                },
                error: function () {
                    hasError = true; // Nếu có lỗi xảy ra, đặt biến lỗi là true
                }
            });
        }
    });

    // Sự kiện khi submit form
    $('#power_form').submit(function (event) {
       if (hasError) {
            event.preventDefault();
        }
    });
});
