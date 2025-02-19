
//chỉ hiện ra 1
//function dropdown(element) {

//    const dropdownMenus = document.querySelectorAll('.dropdown-menu-heading');
//    const iconDropdowns = document.querySelectorAll('.icon-dropdown svg');

//    dropdownMenus.forEach(function (dropdownMenu) {
//        dropdownMenu.classList.add("hidden-item");
//    });

//    iconDropdowns.forEach(function (iconDropdown) {
//        iconDropdown.classList.remove("fa-chevron-down");
//        iconDropdown.classList.add("fa-chevron-left");
//    });

//    const dropdownMenu = element.nextElementSibling;
//    const iconDropdown = element.querySelector('.icon-dropdown svg');

//    dropdownMenu.classList.toggle("hidden-item");
//    iconDropdown.classList.toggle("fa-chevron-left");
//    iconDropdown.classList.toggle("fa-chevron-down");
//}

//hiện ra nhiều
function dropdown(element) {
    const dropdownMenu = element.nextElementSibling;

    const iconElements = element.getElementsByClassName("hidden-open");

    if (iconElements.length > 0) {
        Array.from(iconElements).forEach(function (iElement) {
            iElement.classList.toggle("fa-chevron-left");
            iElement.classList.toggle("fa-chevron-down");
        });
    }

    dropdownMenu.classList.toggle("hidden-item");

}
function saveDropdownState(id) {
    //   sessionStorage.setItem(`dropdown-${id}`, "hidden-item");
    localStorage.setItem("dropdownStage", id);
}

window.addEventListener("load", function () {
    const dropdownMenus = document.querySelectorAll('[data-dropdown]');
    dropdownMenus.forEach(function (dropdownMenu) {
        const dropdownId = dropdownMenu.getAttribute("data-dropdown");
        const currentState = localStorage.getItem(`dropdownStage`);

        const elementMenu = document.querySelector(`[data-menu="${dropdownId}"]`);

        if (elementMenu) { // Kiểm tra nếu elementMenu không null
            const iconElements = elementMenu.getElementsByClassName("hidden-open");

            if (currentState === dropdownId) {
                dropdownMenu.classList.remove("hidden-item");

                if (iconElements.length > 0) { // Kiểm tra nếu IconMenu không null
                    Array.from(iconElements).forEach(function (iElement) {
                        iElement.classList.toggle("fa-chevron-left");
                        iElement.classList.toggle("fa-chevron-down");
                    });
                }
            } else {
                dropdownMenu.classList.add("hidden-item");
            }
        }
    });
    dropdownMenus.forEach(function (dropdownMenu) {
        dropdownMenu.addEventListener("click", function () {

            const dropdownId = dropdownMenu.getAttribute("data-dropdown");
            saveDropdownState(dropdownId);
        });
    });
});
var currentDate = new Date();
var currentMonth = currentDate.getMonth() + 1;
var currentYear = currentDate.getFullYear();

var selectElement = document.getElementById("MONTH");
var ElementYear = document.getElementById("YEAR");
// Lặp qua từng tùy chọn và chọn tùy chọn mặc định
if (selectElement != null) {
    for (var i = 0; i < selectElement.options.length; i++) {
        var option = selectElement.options[i];
        if (option.value == currentMonth) {
            option.selected = true;
            break;
        }
    }
}
if (ElementYear != null) {
    for (var i = 0; i < ElementYear.options.length; i++) {
        var option = ElementYear.options[i];
        if (option.value == currentYear) {
            option.selected = true;
            break;
        }
    }
}


//clear localStorage
function Logout() {
  //  ClearLocalStorage();
    window.location.href = "/Login/Index";
}
function ClearLocalStorage() {
    localStorage.clear();
}
