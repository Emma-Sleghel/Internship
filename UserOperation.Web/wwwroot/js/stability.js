function initialiseStabilityCharts() {
    //Create DataTable
    var table = $('#table-stability').DataTable({
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
    stabilityPositionChart(table);
    levelOfStabilityChart(table);
    stCriticallyChart(table);
}

$(document).ready(initialiseStabilityCharts);


//STABILITY POSITION chart
function stabilityPositionChart(table) {

    table.searchPanes.container().hide();

    let stPositionChart = document.getElementById('st-position-chart');

    // Create the chart with initial data
    var container = $('<div/>').appendTo(stPositionChart);

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
                data: chartDataPositionStability(table),
            },
        ],
    });

    // On each draw, update the data in the chart
    table.on('draw', function () {
        chart.series[0].setData(chartDataPositionStability(table));
    });
};

function chartDataPositionStability(table) {
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



//LEVEL OF STABILITY chart
function levelOfStabilityChart(table) {

    table.searchPanes.container().hide();

    let levelOfStabilityChart = document.getElementById('st-level-of-stability');

    // Create the chart with initial data
    var container = $('<div/>').appendTo(levelOfStabilityChart);

    var chart = Highcharts.chart(container[0], {
        chart: {
            type: 'bar',
        },
        credits: {
            enabled: false
        },
        title: {
            text: 'Level of stability',
        },
        series: [
            {
                data: chartDataLevelOfStability(table),
                name: 'Level'
            },
        ],
    });

    // On each draw, update the data in the chart
    table.on('draw', function () {
        chart.series[0].setData(chartDataLevelOfStability(table));
    });
};

function chartDataLevelOfStability(table) {
    var counts = {};

    // Count the number of entries for each position
    table
        .column(8, { search: 'applied' })
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


//CRITICALLY chart
function stCriticallyChart(table) {

    table.searchPanes.container().hide();

    let criticallyChart = document.getElementById('st-critically');

    // Create the chart with initial data
    var container = $('<div/>').appendTo(criticallyChart);

    var chart = Highcharts.chart(container[0], {
        chart: {
            type: 'pie',
        },
        credits: {
            enabled: false
        },
        title: {
            text: 'Criticalities',
        },
        series: [
            {
                data: chartDataCritically(table)
            },
        ],
    });

    // On each draw, update the data in the chart
    table.on('draw', function () {
        chart.series[0].setData(chartDataCritically(table));
    });
};

function chartDataCritically(table) {
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

    // And map it to the format highcharts uses
    return $.map(counts, function (val, key) {
        return {
            name: key,
            y: val,
        };
    });
}

