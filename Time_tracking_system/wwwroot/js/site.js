// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// Advanced interaction for HTML-tables
$(document).ready(function () {
    $('#tblData').DataTable({
        order: [[3, 'desc']],
    });
});

$(document).ready(function () {
    $('#tblDataOrder0').DataTable({
        order: [[0, 'asc']],
    });
});