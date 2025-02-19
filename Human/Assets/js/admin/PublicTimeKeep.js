
$(document).ready(function () {
    $('#search_loglist').on('submit', function (e) {
        e.preventDefault();
        var seach_code = $('#seach_code').val();
        var select_type = $('input[name="select_type"]:checked').val(); // Lấy giá trị từ radio button được chọn
        // Gửi yêu cầu Ajax
        $.ajax({
            url: '/Public/SearchLogList', // Điểm cuối của controller và action
            type: 'POST',
            data: {
                seach_code: seach_code,
                select_type: select_type
            },
            type: 'json', // Loại dữ liệu dự kiến trả về từ controller
            success: function (data) {
                if (data.success) {
                    var employeeInfo = data.employeeInfo;
                    var model_controller = data.data;
                    if (select_type === "1") {
                        var resultHtml = '<div class="table-wrapper-time-keep" style="overflow-y:auto;"><div class="table-caption"> <div>' + employeeInfo.EMP_CODE + ' - ' + employeeInfo.FULLNAME + ' - ' + employeeInfo.TitleName_en + ' - ' + employeeInfo.DEPARTMENT_NAME_VI + '</div></div >';

                        resultHtml += '<table id ="table_public_loglist" style="width:100%!important;">';
                        resultHtml += '<thead>';
                        resultHtml += '<tr>';

                        resultHtml += '<th>';
                        resultHtml += 'Mã thẻ <br />(ID card)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'Họ tên <br />(Full name)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'Ngày quét <br />(Scan date)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'Giờ quét <br />(Scan time)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'Nút F <br />(Tnakey)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'Hình thức <br />(Event type)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'Tên máy <br />(Machine name)';
                        resultHtml += '</th>';

                        resultHtml += '</tr>';
                        resultHtml += '</thead>';
                        resultHtml += '<tbody>';


                        // Sử dụng dữ liệu trong model_controller thay vì @Model.LST_TIMESHEETVIEW
                        model_controller.forEach(function (item, index) {
                            var formatdate = moment(item.DATECHECK).format("DD/MM/YYYY");
                            resultHtml += '<tr';

                            // Kiểm tra xem hàng có phải là hàng số chẵn không
                            if (index % 2 != 0) {
                                resultHtml += ' style="background-color: #fbf4e4;"';
                            }

                            resultHtml += '>';
                            resultHtml += '<td>' + (item.EMP_CODE ? item.EMP_CODE : "-") + '</td>';
                            resultHtml += '<td>' + (item.FULLNAME ? item.FULLNAME : "-") + '</td>';

                            resultHtml += '<td>' + formatdate + '</td>';
                            if (item.TIME_TEMP === "/Date(-62135596800000)/") {
                                resultHtml += '<td>-</td>';

                            } else {
                                resultHtml += '<td>' + moment(item.TIME_TEMP).format("HH:mm:ss") + '</td>';
                            }
                            resultHtml += '<td>' + (item.TNAKEY ? item.TNAKEY : "-") + '</td>';

                            resultHtml += '<td>' + (item.EVENT_TYPE ? item.EVENT_TYPE : "-") + '</td>';
                            resultHtml += '<td>' + (item.NM ? item.NM : "-") + '</td>';
                            resultHtml += '</tr>';

                        });

                        resultHtml += '</tbody>';
                        resultHtml += '</table></div>';

                    }
                    else if (select_type === "2") {
                        var data_month = data.month;
                        var data_year = data.year;
                        var resultHtml = '<div class="table-wrapper-time-keep" style="overflow-y:auto;"><div class="table-caption"> <div>' + employeeInfo.EMP_CODE + ' - ' + employeeInfo.FULLNAME + ' - ' + employeeInfo.TitleName_en + ' - ' + employeeInfo.DEPARTMENT_NAME_VI + ' (' + data_month+'/'+data_year +')'+'</div></div >';
                        resultHtml += '<table id ="table_public_timekeep" style="width:100%!important;">';
                        resultHtml += '<thead>';
                        resultHtml += '<tr>';

                        resultHtml += '<th>';
                        resultHtml += 'NGÀY <br /> (DATE)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'THỨ <br />(DAY)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'GIỜ VÀO <br />(CHECKIN)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'GIỜ RA <br />(CHECKOUT)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'GIỜ CÔNG <br />(HOUR)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'TC 150 <br />(OT 150)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'TC 210 <br />OT 210';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'CA ĐÊM <br />(NIGHT)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'TC 200 <br />OT 200';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'PNĂM <br />(ANL)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'CƯỚI,TANG <br />(WED,FUL)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'K.THAI <br />(PREG)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'KHÁC <br />(OTHER)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'LỄ <br />(HOL)';
                        resultHtml += '</th>';

                        resultHtml += '<th>';
                        resultHtml += 'GHI CHÚ <br />(REMARK)';
                        resultHtml += '</th>';

                        resultHtml += '</tr>';
                        resultHtml += '</thead>';
                        resultHtml += '<tbody>';

                        // Khai báo biến tổng ở đầu vòng lặp
                        var total_hourwork = 0;
                        var total_otwork = 0;
                        var total_ot215 = 0;
                        var total_nighthour = 0;
                        var total_anuallave = 0;
                        var total_wed_ful = 0;
                        var total_other = 0;
                        var total_holiday = 0;
                        // Sử dụng dữ liệu trong model_controller thay vì @Model.LST_TIMEKEEPTVIEW
                        if (model_controller != 0) {
                            model_controller.forEach(function (item1) {
                                var rowStyle = (item1.DATENAME == "SUN") ? "background-color:#F3ecec;" : "";
                                total_hourwork += item1.HOUR_WORK || 0;
                                total_otwork += item1.OT_WORK || 0;
                                total_ot215 += item1.OT200_WORK || 0;
                                total_nighthour += item1.NIGHT_TIME || 0;
                                total_anuallave += item1.ANUAL_LEAVE || 0;
                                total_wed_ful += item1.WED_FUL_LEAVE || 0;
                                total_other += item1.OTHER_LEAVE || 0;
                                total_holiday += item1.DAYOFF || 0;

                                resultHtml += `<tr style="${rowStyle}">`;
                                resultHtml += '<td>' + moment(item1.DATEOFMONTH).format("DD/MM") + '</td>';
                                resultHtml += '<td>' + item1.DATENAME + '</td>';

                                if (item1.TIME_CHECKIN === "/Date(-62135596800000)/") {
                                    resultHtml += '<td>-</td>';
                                } else {
                                    resultHtml += '<td>' + moment(item1.TIME_CHECKIN).format("HH:mm:ss") + '</td>';
                                }

                                if (item1.TIME_CHECKOUT === "/Date(-62135596800000)/") {
                                    resultHtml += '<td>-</td>';

                                } else {
                                    resultHtml += '<td>' + moment(item1.TIME_CHECKOUT).format("HH:mm:ss") + '</td>';
                                }

                                resultHtml += '<td>' + (item1.HOUR_WORK ? item1.HOUR_WORK : "-") + '</td>';
                                resultHtml += '<td>' + (item1.OT_WORK ? item1.OT_WORK : "-") + '</td>';
                                resultHtml += '<td>' + (item1.OT200_WORK ? item1.OT200_WORK : "-") + '</td>';
                                resultHtml += '<td>' + (item1.NIGHT_TIME ? item1.NIGHT_TIME : "-") + '</td>';
                                resultHtml += '<td> - </td>';
                                resultHtml += '<td>' + (item1.ANUAL_LEAVE ? item1.ANUAL_LEAVE : "-") + '</td>';
                                resultHtml += '<td>' + (item1.WED_FUL_LEAVE ? item1.WED_FUL_LEAVE : "-") + '</td>';
                                resultHtml += '<td> - </td>';
                                resultHtml += '<td>' + (item1.OTHER_LEAVE ? item1.OTHER_LEAVE : "-") + '</td>';
                                resultHtml += '<td>' + (item1.DAYOFF ? item1.DAYOFF : "-") + '</td>';

                                resultHtml += '<td>' + (item1.REMARK ? item1.REMARK : "-") + '</td>';

                                resultHtml += '</tr>';
                            });
                            // Hiển thị tổng ở ngoài vòng lặp
                            resultHtml += '<tr style=" border-top: 2px solid #ccc2c2; font-size: 15px; font-weight: 600;">';
                            resultHtml += '<td colspan="3" class="text-center">Tổng cộng (Total):</td>';
                            resultHtml += '<td>(Hour)<br />(Day)</td>';
                            resultHtml += '<td>' + (total_hourwork == 0 ? "-" : Math.round(total_hourwork, 1).toString()) + '<br />' + (total_hourwork == 0 ? "-" : (Math.round(total_hourwork / 8, 2)).toString()) + '</td>';
                            resultHtml += '<td>' + (total_otwork == 0 ? "-" : total_otwork.toString()) + '</td>';
                            resultHtml += '<td>' + (total_ot215 == 0 ? "-" : total_ot215.toString()) + '</td>';
                            resultHtml += '<td>' + (total_nighthour == 0 ? "-" : total_nighthour.toString()) + '</td>';
                            resultHtml += '<td>-</td>';
                            resultHtml += '<td>' + (total_anuallave == 0 ? "-" : total_anuallave.toString()) + '</td>';
                            resultHtml += '<td>' + (total_wed_ful == 0 ? "-" : total_wed_ful.toString()) + '</td>';
                            resultHtml += '<td>-</td>';
                            resultHtml += '<td>' + (total_other == 0 ? "-" : total_other.toString()) + '</td>';
                            resultHtml += '<td>' + (total_holiday == 0 ? "-" : total_holiday.toString()) + '</td>';
                            resultHtml += '<td></td>';
                            resultHtml += '</tr>';
                            resultHtml += '</tbody>';
                            resultHtml += '</table></div>';
                        }else {
                            resultHtml += '<tr>';
                            resultHtml += '<td colspan="15" style="text-align: center; font-size:14px;"> Không có dữ liệu<br>(No matching records found)</td>'
                            resultHtml += '</tr>';
                            resultHtml += '</tbody>';
                            resultHtml += '</table></div>';

                        }
                        resultHtml += ' <div class="footer-table" style="border-top: 2px solid #ccc2c2;"><span class="text-color-gray btn-paging"></span></div >';
                    }
                   $('#result_table').html(resultHtml);
                    // show DataTable js
                    $('#table_public_loglist').DataTable({
                        "pageLength": 50,
                        "lengthMenu": [
                            [50, 100, 200, -1],
                            [50, 100, 200, "All"]
                        ],
                        "ordering": false,
                        "searching": false
                        
                    });
                } else {
                    // Hiển thị thông báo lỗi nếu có
                    Swal.fire({
                        position: 'center-center',
                        icon: 'warning',
                        title: data.message,
                        showConfirmButton: false,
                        timer: 2000
                    });
                }
            },
            error: function (error) {
                // Xử lý lỗi nếu có
                console.error('Lỗi:', error);
            }
        });
    });
});
