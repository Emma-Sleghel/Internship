$(document).ready(function () {
    $("table").DataTable();
    //$('table').DataTable({
    //    dom: 'Bfrtip',
    //    buttons: [
    //        {
    //            extend: 'csv',
    //            text: 'Export',
    //            exportOptions: {
    //                modifier: {
    //                   /* search: 'none'*/
    //                }
    //            }
    //        }
    //    ]
    //});

    $('.multiple-select').select2({
        placeholder: "Select projects"
    });

    $("#datepicker").datepicker({
        format: "yyyy",
        startView: "years",
        minViewMode: "years",
        autoclose: true
    });
    $(".place-position").select2({
        placeholder: "Select a position",
        allowClear: true
    });
    $(".place-level").select2({
        placeholder: "Select a level",
        allowClear: true
    });
    $(".place-month").select2({
        placeholder: "Select a stability month",
        allowClear: true
    });

    $(".place-stalevel").select2({
        placeholder: "Select a level of stability",
        allowClear: true
    });
    $(".place-crit").select2({
        placeholder: "Select a critically",
        allowClear: true
    });
    $(".place-reason").select2({
        placeholder: "Select a reason",
        allowClear: true
    });
    $(".place-leave-month").select2({
        placeholder: "Select a leave month",
        allowClear: true
    });

    $("#tabletwo").on("click", "#viewbtn", function () {
        var id = $(this).closest("tr").find("td").eq(0).html();
        $.ajax({
            url: "stability/delete",
            data: { id: id },
            success: function (data) {
                $("#showmodal .modal-dialog").html(data);
                $("#showmodal").modal("show");
            }
        });
    });

    $("#table-one").on("click", "#viewbtn", function () {
        var id = $(this).closest("tr").find("td").eq(0).html();
        $.ajax({
            url: "leave/delete",
            data: { id: id },
            success: function (data) {
                $("#showmodal .modal-dialog").html(data);
                $("#showmodal").modal("show");
            }
        });
    });


    $("#showmodal").on('click', '[data-save="modal"]', function () {
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        $.post(actionUrl, sendData).done(function (data) {
            $("#showmodal").modal("hide");
            location.reload();
        })
    })


    $('#send-data').click(function () {
        var rows = [];
        $('#tabletwo tbody tr').each(function () {
            var id = $(this).find('td').eq(1).text().trim();
            var name = $(this).find('td').eq(2).text().trim();
            var projects = $(this).find('td').eq(3).text().trim();
            var position = $(this).find('td').eq(4).text().trim();
            var level = $(this).find('td').eq(5).text().trim();
            var month = $(this).find('td').eq(6).text().trim();
            var year = $(this).find('td').eq(7).text().trim();
            var slevel = $(this).find('td').eq(8).text().trim();
            var crit = $(this).find('td').eq(9).text().trim();

            var row = id + "-" + name + "-" + projects + "-" + position + "-" + level + "-" + month + "-" + year + "-" + slevel + "-" + crit;
            rows.push(row);
        });


        $.post('stability/export', { rows: rows }, function (data) {
            console.log(data);
            //window.location = "/Stability/Download?file="+ data;
            var blobObj = new Blob([data], { type: "application/vnd.ms-excel" });
            var a = document.createElement('a');
            var url = window.URL.createObjectURL(blobObj);
            a.href = url;
            a.download = 'myfile.xlsx';
            blobObj.download = "myfile.xlsx";
            document.body.append(a);
            a.click();
            a.remove();
            window.URL.revokeObjectURL(url); 


            
        });

    });


});
