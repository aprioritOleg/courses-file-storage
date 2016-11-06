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
            dataType: 'json',
            success: function (result) {
                if (result != null) {
                    $('div#partial_view_treeview').html(result.dataTreeview);
                    $('div#block_view_files_folders').html(result.dataArea);
                } else {
                    alert('Error getting data.');
                }
                showTreeview();
            },
            error: function (xhr) {
                 alert('Error getting data.');
            }
        });
    }
}