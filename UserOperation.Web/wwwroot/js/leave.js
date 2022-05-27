
function initialiseLeaveCharts() {
    //Create DataTable
    var table = $('#table-leave').DataTable({
        dom: 'PBfrtip',
        retrieve: true,
        paging: true,
        buttons: [
            {
                extend: 'csv',
                text: 'Export',
                exportOptions: {
                    modifier: {

                    }
                }
            }
        ]
    });
    leavePrimaryReasonChart(table);
    leaveSecondaryReasonChart(table);
    leavePositionNameChart(table);
}

$(document).ready(initialiseLeaveCharts);


//PRIMARY LEAVING REASON chart
function leavePrimaryReasonChart(table) {

    table.searchPanes.container().hide();

    let lvPrimaryReasonChart = document.getElementById('lv-primary-reason-chart');

    // Create the chart with initial data
    var container = $('<div/>').appendTo(lvPrimaryReasonChart);

    var chart = Highcharts.chart(container[0], {
        chart: {
            type: 'spline',
        },
        credits: {
            enabled: false
        },
        title: {
            text: 'Primary leaving reason',
        },
        series: [
            {
                data: chartDataPrimaryReason(table),
                name: 'Reason'
            },
        ],
    });

    // On each draw, update the data in the chart
    table.on('draw', function () {
        chart.series[0].setData(chartDataPrimaryReason(table));
    });
};

function chartDataPrimaryReason(table) {
    var counts = {};

    // Count the number of entries for each position
    table
        .column(9, { search: 'applied' })
        .data()
        .each(function (val) {
            if (counts[val]) {
                counts[val] += 1;
            } else {
                counts[val] = 1;
            }
        });

    //    // And map it to the format highcharts uses
    return $.map(counts, function (val, key) {
        return {
            name: key,
            y: val,
        };
    });
}




//SECONDARY LEAVING REASON chart
function leaveSecondaryReasonChart(table) {

    table.searchPanes.container().hide();

    let lvSecondaryReasonChart = document.getElementById('lv-secondary-reason-chart');

    // Create the chart with initial data
    var container = $('<div/>').appendTo(lvSecondaryReasonChart);

    var chart = Highcharts.chart(container[0], {
        chart: {
            type: 'spline',
        },
        credits: {
            enabled: false
        },
        title: {
            text: 'Secondary leaving reason',
        },
        series: [
            {
                data: chartDataSecondaryReason(table),
                name: 'Reason'
            },
        ],
    });

    // On each draw, update the data in the chart
    table.on('draw', function () {
        chart.series[0].setData(chartDataSecondaryReason(table));
    });
};

function chartDataSecondaryReason(table) {
    var counts = {};

    // Count the number of entries for each position
    table
        .column(10, { search: 'applied' })
        .data()
        .each(function (val) {
            if (counts[val]) {
                counts[val] += 1;
            } else {
                counts[val] = 1;
            }
        });

    //    // And map it to the format highcharts uses
    return $.map(counts, function (val, key) {
        return {
            name: key,
            y: val,
        };
    });
}




//POSITION NAME LEAVE chart
function leavePositionNameChart(table) {

    table.searchPanes.container().hide();

    let lvPositionChart = document.getElementById('lv-position-chart');

    // Create the chart with initial data
    var container = $('<div/>').appendTo(lvPositionChart);

    var chart = Highcharts.chart(container[0], {
        chart: {
            type: 'pie',
        },
        credits: {
            enabled: false
        },
        title: {
            text: 'Staff count per position',
        },
        series: [
            {
                data: chartDataPositionLeave(table),
            },
        ],
    });

    // On each draw, update the data in the chart
    table.on('draw', function () {
        chart.series[0].setData(chartDataPositionLeave(table));
    });
};

function chartDataPositionLeave(table) {
    var counts = {};

    // Count the number of entries for each position
    table
        .column(4, { search: 'applied' })
        .data()
        .each(function (val) {
            if (counts[val]) {
                counts[val] += 1;
            } else {
                counts[val] = 1;
            }
        });

    //    // And map it to the format highcharts uses
    return $.map(counts, function (val, key) {
        return {
            name: key,
            y: val,
        };
    });
}