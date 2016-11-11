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
                $('div#block_view_files_folders').html(data);
                updateTreeview($folderID);
               
            },
            error: function (xhr) {
                 alert('addFolder1 - Error getting data.');
            }
        });
    }
}