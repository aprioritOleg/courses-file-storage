      var itemId;
function test(obj) {
    itemId = obj;
            
    console.log($('input[name=' + itemId + ']').val());
    $("#" + obj).popr();
    //var ext = $('input[name=' + itemId + ']').val();
    //var textFormat = ".txt";
    //var docxFormat = ".txt";
    //var textFormat = ".txt";
        
    //if (ext.search(textFormat) >= 0) {
    //    $("#edit").enabled = false;
    //    $("#" + obj).popr();
    //};
         
};
function del(obj) {
    var a = "123";

    $.ajax({
        url: "/Files/Delete",
        type: "GET",
        dataType: "json",
        data: { id: itemId }              
    }

    ).complete(function (partialViewResult) {
               
        console.log("Done delete");
        location.reload();
        //$("#brows").html(partialViewResult);
    });
          
};
function redact(obj) {
    console.log("aaa");
    $.ajax({
        url: "/Files/Redact",
        type: "GET",
        dataType: "json",
        data: { id: itemId },

        success: function (data) {
            CKEDITOR.instances['editor1'].setData(data.responseText);
            $("textarea").text(itemId);
            $(".modal-title").text(data.fileName);
            $("#myModal").modal();
        }
    });
};

