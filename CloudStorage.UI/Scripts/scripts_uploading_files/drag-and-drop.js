
$(document).ready(function () {
    var obj = $("#dropArea");
    obj.on('dragenter', function (e) {
        e.stopPropagation();
        e.preventDefault();
    });
    obj.on('dragover', function (e) {
        e.stopPropagation();
        e.preventDefault();
    });
    obj.on('drop', function (e) {
        e.preventDefault();
        var files = e.originalEvent.dataTransfer.files;
        uploadFiles(files);
    });
    $(document).on('dragenter', function (e) {
        e.stopPropagation();
        e.preventDefault();
    });
    $(document).on('dragover', function (e) {
        e.stopPropagation();
        e.preventDefault();
        obj.css('border', '2px dotted #0B85A1');
    });
    $(document).on('drop', function (e) {
        e.stopPropagation();
        e.preventDefault();
    });
});