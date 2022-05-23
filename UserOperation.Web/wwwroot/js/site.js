$(document).ready(function () {
    $("table").DataTable();
    var dp = $("#datepicker").datepicker({
        format: "yyyy",
        viewMode: "years",
        minViewMode: "years",
        autoclose: true
    });
   
    $('.project-select').select2();
   
    dp.on('changeYear', function (e) {
        alert("Year changed ");
    });
});
