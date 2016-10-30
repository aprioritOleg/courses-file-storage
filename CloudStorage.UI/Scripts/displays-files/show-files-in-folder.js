function showFilesInFolder(itemID)
{
    $.ajax({
        url: '/Files/ShowUserFiles',
        type: "POST",
        data: { fileSystemStructureID: itemID },
        dataType: "html",

        success: function (data) {
            $('div#block_view_files_folders').html(data);
        },
        error: function (xhr) {
            alert(xhr);
        }
    });
}