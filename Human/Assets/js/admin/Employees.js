var EmpJs;
$(document).ready(function () {
    EmpJs = new Employees();
})
class Employees {
    constructor() {
        this.inintEvents();
        
    }
    inintEvents() {
        $('#btnSearchEMP').click(this.LoadData);
        $('#btnCreateOTP').click(this.GetOTP);
    }
    LoadData() {
        var CommonModel = {};
        CommonModel.SEARCH_DIVID = $('#branch').val();
        CommonModel.SEARCH_DEPID = $('#department').val();

        $('.table-emp tbody').empty();

        var btn_search = document.getElementById("btn-search");
        var gif_spin = document.getElementById("gif-spin");

        btn_search.classList.add("hidden-item");
        gif_spin.classList.remove("hidden-item");
        $.ajax({
            url: "/EmployeeAPI",
            method: "POST",
            data: JSON.stringify(CommonModel),
            contentType: "application/json",
            datatype: "json"
        }).done(function (res) {

            $.each(res, function (index, item) {
               // var FormatDate = new Date(item.HIRE_DAY).toISOString().slice(0, 10); //yyyy/MM/dd

                var FormatDate = new Date(item.HIRE_DAY).toLocaleDateString('en-GB'); // dd/MM/yyyy
               // var FormatDate = new Date(item.HIRE_DAY).toLocaleDateString(); // MM/dd/yyyy

                var trhtml = $(`<tr>
                 
                <td>`+ item.EMP_CODE + `</td>
                <td>`+ item.FULLNAME + `</td>
                <td>`+ FormatDate + `</td>
                <td>`+ item.DIVISION_NAME_EN + `</td>
                <td>`+ item.DEPARTMENT_NAME_EN + `</td>
                 <td>`+ item.SECTION_NAME_EN + `</td>
                  <td>`+ item.TitleName_en + `</td>
                </tr>`);
                $('.table-emp tbody').append(trhtml);
                if (index == 500) {
                    btn_search.classList.remove("hidden-item");
                    gif_spin.classList.add("hidden-item");
                    return false;

                }
            })

        });
    }
    GetOTP() {
        $.ajax({
            url: "/EmployeeAPI/GetOTP",
            method: "GET",
            data: "",
            datatype: "appication/json",
            datatype:""
        }).done(function (res) {
            $('#txtOTP').val(res);
        })
    }
}
