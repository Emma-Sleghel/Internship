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
});
