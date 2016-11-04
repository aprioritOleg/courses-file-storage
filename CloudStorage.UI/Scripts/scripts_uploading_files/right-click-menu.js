function ShowMenu(control, e) {
    var posx = e.clientX + 'px';
    var posy = e.clientY + 'px';
    document.getElementById(control).style.position = 'absolute';
    document.getElementById(control).style.left = posx;
    document.getElementById(control).style.top = posy;
    $(document).ready(function () {
        $('#' + control).slideDown(200);

    });
}
function HideMenu(control) {
    $(document).ready(function () {
        $('#' + control).slideUp(200);
    });
}
function addFiles() {
    $('#inputUpload').trigger('click');
    document.getElementById("inputUpload").onchange = function () {
        var files = document.getElementById('inputUpload').files;
        uploadFiles(files);
    };
}