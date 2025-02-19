// jquery validate
function validateDate(startDateInput, endDateInput) {
    var startDate = $(startDateInput).val();
    var endDate = $(endDateInput).val();

    if (startDate && endDate) {
        if (startDate > endDate) {
            $(endDateInput).addClass('is-invalid');
            $(endDateInput).closest('.form-group').find('.invalid-feedback').remove();
            $(endDateInput).after('<div class="invalid-feedback">Ngày kết thúc phải lớn hơn ngày hiệu lực.</div>');
        } else {
            $(endDateInput).removeClass('is-invalid');
            $(endDateInput).closest('.form-group').find('.invalid-feedback').remove();
        }
    } else if (!endDate) {
        $(endDateInput).removeClass('is-invalid');
        $(endDateInput).closest('.form-group').find('.invalid-feedback').remove();
    }
}
function validateNumberInput(inputElement) {
    var inputText = $(inputElement).val().trim(); // Sử dụng trim để loại bỏ khoảng trắng ở đầu và cuối chuỗi

    if (inputText === '') {
        $(inputElement).removeClass('is-invalid');
        $(inputElement).closest('.form-group').find('.invalid-feedback').remove();
    } else if (!/^\d+$/.test(inputText)) {
        $(inputElement).addClass('is-invalid');
        $(inputElement).closest('.form-group').find('.invalid-feedback').remove();
        $(inputElement).after('<div class="invalid-feedback">Chỉ được nhập số, không được nhập chữ.</div>');
    } else {
        $(inputElement).removeClass('is-invalid');
        $(inputElement).closest('.form-group').find('.invalid-feedback').remove();
    }
}
function validatePhoneNumber(inputElement) {
    var phoneNumber = $(inputElement).val().trim();

    if (phoneNumber === '') {
        $(inputElement).removeClass('is-invalid');
        $(inputElement).closest('.form-group').find('.invalid-feedback').remove();
    } else {
        phoneNumber = phoneNumber.replace(/\D/g, ''); // Loại bỏ các ký tự không phải số

        if (phoneNumber.length !== 10 || $(inputElement).val().length !== phoneNumber.length) {
            $(inputElement).addClass('is-invalid');
            $(inputElement).closest('.form-group').find('.invalid-feedback').remove();
            $(inputElement).after('<div class="invalid-feedback">Số điện thoại cần phải có đúng 10 số.</div>');
        } else {
            $(inputElement).removeClass('is-invalid');
            $(inputElement).closest('.form-group').find('.invalid-feedback').remove();
        }
    }
}
$(document).ready(function () {
    //validate phone number
    $('input[name="PHONE_NUMBER"], input[name="MOBILE_NUMBER"]').on('input', function () {
        validatePhoneNumber(this);
    });
    // validate chỉ được nhập số
    $('input[name="ID_CARDNUMBER"], input[name="BANKACOUNT_NUMBER"], input[name="TAX_NUMBER"], input[name="SOBHXH"], input[name="SOBHYT"]').on('input', function () {
        validateNumberInput(this);
    });
    // validate email
    $('input[name="EMAIL"]').on('input', function () {
        var email = $(this).val().trim();

        if (email === '') {
            $(this).removeClass('is-invalid');
            $(this).closest('.form-group').find('.invalid-feedback').remove();
        } else {
            var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

            if (!emailPattern.test(email)) {
                $(this).addClass('is-invalid');
                $(this).closest('.form-group').find('.invalid-feedback').remove();
                $(this).after('<div class="invalid-feedback">Vui lòng nhập địa chỉ email hợp lệ.</div>');
            } else {
                $(this).removeClass('is-invalid');
                $(this).closest('.form-group').find('.invalid-feedback').remove();
            }
        }
    });
    // validate date
    $('input[name="CONGTACDEN"]').on('input', function () {
        validateDate('input[name="CONGTACTU"]', this);
    });
    $('input[name="END_DATE"]').on('input', function () {
        validateDate('input[name="BEGIN_DATE"]', this);
    });
    // validate check emp_code
    $('input[name="EMP_CODE"]').on('blur', function () {
        var inputVal = $(this).val().trim();
        // Kiểm tra nếu giá trị nhập vào không rỗng
        if (inputVal !== '') {
            // Gửi request kiểm tra mã nhân viên trùng lên server
            $.ajax({
                url: '/Employees/CheckEmployeeCode', // Điều chỉnh đường dẫn đến action kiểm tra mã nhân viên trùng
                method: 'POST',
                data: { empCode: inputVal },
                success: function (data) {
                    if (data.isExists >= 1) {
                        // Nếu mã nhân viên đã tồn tại, hiển thị thông báo lỗi
                        $('input[name="EMP_CODE"]').addClass('is-invalid');
                        $('input[name="EMP_CODE"]').closest('.form-group').find('.invalid-feedback').remove();
                        $('input[name="EMP_CODE"]').after('<div class="invalid-feedback">Mã nhân viên đã tồn tại.</div>');
                    } else {
                        // Nếu mã nhân viên không trùng, xóa thông báo lỗi nếu có
                        $('input[name="EMP_CODE"]').removeClass('is-invalid');
                        $('input[name="EMP_CODE"]').closest('.form-group').find('.invalid-feedback').remove();
                    }
                },
                error: function () {
                }
            });
        }
    });
});