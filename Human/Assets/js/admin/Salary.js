var SalaryJs;
document.addEventListener("DOMContentLoaded", function () {
   //Format số
    // Lấy bảng
    var table = document.getElementById("table_salary");

    // Lấy tất cả các hàng (tr) trong bảng
    var rows = table.getElementsByTagName("tr");

    // Duyệt qua từng hàng (tr) và định dạng lại số decimal trong các cột (td)
    for (var i = 0; i < rows.length; i++) {
        var row = rows[i];
        var cells = row.getElementsByTagName("td");

        for (var j = 1; j < cells.length; j++) {
            var cell = cells[j];
            if (cell.innerHTML == "") {

            }
            else {
                var number = parseFloat(cell.innerHTML);
                var formatNumber = addCommas(number);
                cell.innerHTML = formatNumber;
            }
        }
    }
});

function addCommas(number) {
    return number.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}


$(document).ready(function () {
    SalaryJs = new Salary();

})
class Salary {
    constructor() {
        this.initEvents();
    }
    initEvents() {
        this.PrintPDF();
    }
    PrintPDF() {
        $('#PrintPDF').on('click', function () {
            $.get("/Salary/GeneratePdf", function (reps) {
                location.href = "/Resource/Pdf/" + reps;  
            });
        })
    }
};
   




