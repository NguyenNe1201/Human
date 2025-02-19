/*document.addEventListener('DOMContentLoaded', function () {
    localStorage.clear();
})
*/
//checkbox password
const chk_pwd = document.getElementById('chk_pwd');
const input_pwd = document.getElementById('pwd_login');
const input_otp = document.getElementById('otp_login');
const btn_otp = document.getElementById('btn-OTP');
const btn_login = document.getElementById('btn-login');
chk_pwd.addEventListener('click', function () {

    if (chk_pwd.checked) {
        StatusItem(true);
    }
    else {
        StatusItem(false);
    }
})
function StatusItem(status) {
    if (status == true) {
        input_otp.classList.add('hidden-item');
        $('#inputotp').val("");
        $('#inputotp').removeAttr('required');

        input_pwd.classList.remove('hidden-item');
        $('#inputPassword').val("");
        $('#inputPassword').attr('required', true);

        btn_otp.classList.add('hidden-item');
        btn_login.classList.remove('hidden-item');
        btn_login.removeAttribute("disabled");
    }
    else {
        $('#otp_login').attr('required', true);
        $('#inputPassword').removeAttr('required');

        input_pwd.classList.add('hidden-item');
        $('#inputPassword').val("");
        btn_otp.classList.remove('hidden-item');
        btn_login.classList.add('hidden-item');

    }
}
//end checkbox

function GetOTP() {

    var gmail = $('#inputEmail').val();
    var btn_login = document.getElementById("btn-login");
    var gmailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    var phoneRegex = /^\d{10,}$/;
    var usernameRegex = /^[a-zA-Z0-9._%+-]+$/;
    if (gmail === "") {
        alert('Vui lòng không để trống!');
        return;
    } else {
        if (gmailRegex.test(gmail)) {
            // Nếu giá trị là email
            
        } else if (phoneRegex.test(gmail)) {
            // Nếu giá trị là số điện thoại
            if (gmail.length < 10) {
              /*  alert('Số điện thoại cần ít nhất 10 chữ số!');*/
                Swal.fire({
                    position: 'center-center',
                    icon: 'warning',
                    title: 'Số điện thoại cần ít nhất 10 chữ số!',
                    showConfirmButton: false,
                    timer: 3000
                });
                return;
            }
            
        } else if (usernameRegex.test(gmail)) {
            // Nếu giá trị là username
            /*alert('Vui lòng nhập email hoặc số điện thoại để sử dụng OTP!');*/
            Swal.fire({
                position: 'center-center',
                icon: 'warning',
                title: 'Vui lòng nhập email hoặc số điện thoại để sử dụng OTP!',
                showConfirmButton: false,
                timer: 3000
            });
            return;
        } else {
           /* alert('Vui lòng nhập địa chỉ Gmail hoặc số điện thoại hợp lệ!');*/
            Swal.fire({
                position: 'center-center',
                icon: 'warning',
                title: 'Vui lòng nhập địa chỉ Gmail hoặc số điện thoại hợp lệ!',
                showConfirmButton: false,
                timer: 3000
            });
            return;
        }
    }

    $.ajax({
        url: "/Login/CheckEmail",
        data: JSON.stringify({ USER_GMAIL: gmail }),
        type: "POST",
        contentType: "application/json",
        datatype: "json"

    }).done(async function (res) {

        if (res == 0) {
            /*alert('Gmail hoặc sô điện thoại không tồn tại!');*/
            Swal.fire({
                position: 'center-center',
                icon: 'warning',
                title: 'Gmail hoặc sô điện thoại không tồn tại!',
                showConfirmButton: false,
                timer: 3000
            });
        }
        else {
            var pwd_login = document.getElementById('pwd_login');
            $('#btn-OTP').addClass('btn-disable');
            $('#btn-OTP').prop('disabled', true);

            btn_login.classList.remove('hidden-item');
            btn_login.removeAttribute("disabled");

            $("#otp_login").removeClass('hidden-item');
            $('#inputotp').attr('required', true);
            pwd_login.classList.add('hidden-item');
            //clear input password
          //  $('#check_password').addClass('hidden-item');
            $("#inputPass").val("");
            $('#inputPass').removeAttr('required');
            try {
                await Promise.all([TimeBackPromise(), SendOtp(gmail)]);
            }
            catch (error) {

            }
        }
    })
}
async function SendOtp(gmail) {

    $.ajax({
        url: "/Login/SendEmail",
        data: JSON.stringify({ GMAIL: gmail }),
        type: "POST",
        contentType: "application/json",
        datatype: "json"
    }).done(function (result) {

    });
}
async function TimeBackPromise() {
    return new Promise(function (resolve, reject) {
        var second = 60;
        document.getElementById("btn-OTP").innerText = second;
        var IdInterval = setInterval(function () {
            second--;
            document.getElementById("btn-OTP").innerText = second;
            if (second < 0) {
                clearInterval(IdInterval);
                reject(); // đánh dấu promise thất bại
            }

        }, 1000);

        setTimeout(function () {
            clearInterval(IdInterval);
            document.getElementById("btn-OTP").innerText = "Get OTP";
            $('#btn-OTP').prop('disabled', false);
            $('#btn-OTP').removeClass('btn-disable');

            resolve();// đánh dấu promise thành công
        }, 60000);
    })

}
function Timestop() {
    clearInterval(IdInterval);
    document.getElementById("btn-OTP").innerText = "Get OTP";
    $('#btn-OTP').prop('disabled', false);
    $('#btn-OTP').removeClass('btn-disable');
}
