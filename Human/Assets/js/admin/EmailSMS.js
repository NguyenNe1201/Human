$(document).ready(function () {
    $("#emailSMSForm").submit(function (e) {
        e.preventDefault();

        var email_address = $("#email_address").val();
        var password_app = $("#password_app").val();
        var password_confirm = $("#password_confirm").val(); 
        if (password_app !== password_confirm) {
            // Hiển thị thông báo lỗi khi mật khẩu xác nhận không khớp
            Swal.fire({
                position: 'center-center',
                icon: 'warning',
                title: 'Mật Khẩu xác nhận không đúng!',
                showConfirmButton: false,
                timer: 3000
            });
            // Xóa giá trị trong trường nhập mật khẩu và mật khẩu xác nhận
            $("#password_app").val("");
            $("#password_confirm").val("");
            return; // Không gửi Ajax request nếu mật khẩu không khớp
        }
        $.ajax({
            url: "/Home/ProcessEmailSMS",
            data: JSON.stringify({ email_address: email_address, password_app: password_app }),
            type: "POST",
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
             /*   $("#add_emailsms").modal("hide");*/
                if (data.success) {
                    Swal.fire({
                        position: 'center-center',
                        icon: 'success',
                        title: 'Cập nhật Email SMS thành công!',
                        showConfirmButton: false,
                        timer: 3000
                    });
                   
                } else {
                    Swal.fire({
                        position: 'center-center',
                        icon: 'error',
                        title: 'Cập nhật Email SMS không thành công!',
                        showConfirmButton: false,
                        timer: 3000
                    });
                }
                $("#email_address").val("");
                $("#password_app").val("");
                $("#password_confirm").val("");
            },
            error: function () {
                Swal.fire({
                    position: 'center-center',
                    icon: 'error',
                    title: 'Có lỗi xảy ra trong quá trình cập nhật!',
                    showConfirmButton: false,
                    timer: 3000
                })
            }
        });
    });
});
