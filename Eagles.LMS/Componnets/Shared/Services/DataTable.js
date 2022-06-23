angular.module("LmsApp_Services").factory("DataTableServices", () => {

    pushDataTable = (tableId) => {
        angular.element(document).ready(function () {
            $("" + tableId + "").DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ]
            });
        });
    }
    return {
        pushDataTable: pushDataTable
    };
});