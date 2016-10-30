function addFolder() {
    $("#dialog").dialog("open");

    $('#dialog').unbind('submit').bind('submit', function () {
        var name = $("#name").val();
        requestAddFolder(name);
        $("#dialog").dialog("close");
    });
    function requestAddFolder($name) {
        $.ajax({
            url: '/Files/AddFolder',
            type: "POST",
            data: { folderName: $name },
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