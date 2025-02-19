$(document).ready(function () {
    $("#imageInput").on("change", function () {
        var fileInput = document.getElementById("imageInput");
        var file = fileInput.files[0];
        if (file) {
            var formData = new FormData();
            formData.append("file_avt", file);
            $.ajax({
                url: "/ProfileEmployee/UploadImage",
                type: "POST",
                data: formData,
                processData: false,
                contentType: false,
                success: function (data) {
                    if (data.success) {
                        $("#value_upload_img").val(data.value_fileName);
                        // Thêm phiên làm việc vào URL ảnh
                        var imageUrl =  data.imagePath + "?random=" + Math.random();
                        $("#profile_img").attr("src", imageUrl);
                        // thông báo thêm avt thành công 
                        alert("upload ảnh thành công!");
                    } else {

                        Swal.fire({
                            position: 'top-right',
                            icon: 'error',
                            title: 'Lỗi khi tải hình ảnh lên!',
                            showConfirmButton: false,
                            timer: 3000
                        });

                    }
                },
                error: function () {
                    Swal.fire({
                        position: 'center-center',
                        icon: 'error',
                        title: 'Lỗi khi tải lên hình ảnh!',
                        showConfirmButton: false,
                        timer: 3000
                    })
                }
            });
        }
    });
});
