document.addEventListener("DOMContentLoaded", function () {
    // Lấy danh sách các mục chọn màu
    var colorItems = document.querySelectorAll('.choose-skin li');
    // Kiểm tra nếu có màu đã lưu trong localStorage
    var selectedColor = localStorage.getItem('selectedColor');
    if (!selectedColor) {
        // nếu chưa có giá trị ở localstorage thì set màu mặc định cho website
        /*var activeNavItem = document.querySelector('.sb-sidenav-dark .sb-sidenav-menu .nav-link.active');
        if (activeNavItem) {
            activeNavItem.style.color = getComputedStyle(document.documentElement).getPropertyValue('--web-color-orange');
        }*/
        // Đặt giá trị biến màu cho .nav-link:hover
        var hoverColor = getComputedStyle(document.documentElement).getPropertyValue('--web-color-orange');
        document.documentElement.style.setProperty('--nav-link-hover-color', hoverColor);
    } else {
        // Áp dụng màu đã chọn vào giao diện
        applyColor(selectedColor);
        // Di chuyển class 'active' tới mục đã chọn
        var activeItem = document.querySelector('.choose-skin li[data-theme="' + selectedColor + '"]');
        if (activeItem) {
            document.querySelector('.choose-skin li.active').classList.remove('active');
            activeItem.classList.add('active');
        }
    }
    // Thêm sự kiện click cho mỗi mục chọn màu
    colorItems.forEach(function (item) {
        item.addEventListener('click', function () {
            // Lấy màu từ thuộc tính data-theme
            var color = item.getAttribute('data-theme');
            // Lưu màu đã chọn vào localStorage
            localStorage.setItem('selectedColor', color);
            // Áp dụng màu đã chọn vào giao diện
            applyColor(color);
            // Di chuyển class 'active' tới mục đã chọn
            document.querySelector('.choose-skin li.active').classList.remove('active');
            item.classList.add('active');
        });
    });

    // Hàm áp dụng màu vào giao diện
    function applyColor(color) {
        /* var mainColorWeb = document.querySelector('.main-color-web');
        if (mainColorWeb) {
            // Áp dụng màu đã chọn vào các yếu tố có class 'main-color-web'
            mainColorWeb.style.color = getComputedStyle(document.documentElement).getPropertyValue('--web-color-' + color);
        }*/
        // Áp dụng màu vào .sb-sidenav-dark .sb-sidenav-menu .nav-link.active
     //   var activeNavItem = document.querySelector('.sb-sidenav-dark .sb-sidenav-menu .nav-link.active');
   //     if (activeNavItem) {
    //        activeNavItem.style.color = getComputedStyle(document.documentElement).getPropertyValue('--web-color-' + color);
     //   }
        // Đặt giá trị biến màu cho .nav-link:hover
        var hoverColor = getComputedStyle(document.documentElement).getPropertyValue('--web-color-' + color);
        document.documentElement.style.setProperty('--nav-link-hover-color', hoverColor);
    }
});
