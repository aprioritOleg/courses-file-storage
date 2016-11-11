function uploadFiles(files) {
    var folderID = $('#currentFolderID').val();
    if (files.length > 0) {
        if (window.FormData !== undefined) {
            var data = new FormData();
            for (var x = 0; x < files.length; x++) {
                data.append("file" + x, files[x]);
            }
            var myUrl = '@Url.Action("Upload", "Files")?folderID=0';
            $.ajax({
                xhr: function () {
                    var xhr = new window.XMLHttpRequest();

                    xhr.upload.addEventListener("progress", function (evt) {
                        if (evt.lengthComputable) {
                            var percentComplete = evt.loaded / evt.total;
                            percentComplete = parseInt(percentComplete * 100);
                            document.getElementById('progresBar').value = percentComplete;
                            if (percentComplete === 100) {
                            }
                        }
                    }, false);
                    return xhr;
                },
                type: "POST",
                url: '/Files/UploadFile?currentFolderID=' + folderID,
                contentType: false,
                processData: false,
                dataType: 'html',
                data: data,
                success: function (data) {
                    $('div#block_view_files_folders').html(data);
                    updateTreeview(folderID);
                   
                },
                error: function (xhr, status, p3) {
                    alert(xhr.responseText);
                }
            });
        } else {
            alert("Браузер не поддерживает загрузку файлов HTML5!");
        }
    }
}