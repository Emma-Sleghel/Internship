$(document).ready(function () {
    $("table").DataTable();

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

});
