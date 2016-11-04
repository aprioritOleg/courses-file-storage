function addFolder() {
    //get current folder id from hidden field
    var folderID = $('#currentFolderID').val();
    $("#dialog").dialog("open");

    $('#dialog').unbind('submit').bind('submit', function () {
        var name = $("#name").val();
        requestAddFolder(name, folderID);
        $("#dialog").dialog("close");
    });
    function requestAddFolder($name, $folderID) {
        $.ajax({
            url: '/Files/AddFolder',
            type: "POST",
            data: { folderName: $name, currentFolderID: $folderID},
            dataType: "html",
     
            success: function (data) {
                $('div#partial_view_treeview').html(data);
            },
            error: function (xhr) {
                alert(xhr);
            }
        });
    }
}