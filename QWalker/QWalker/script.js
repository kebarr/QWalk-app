$(document).ready(function () {
    $("#runWalk").click(function () {
        var left = $("#left").val();
        var right = $("#right").val();
        var data = { left: left, right: right };
        $.post('/input/Walk', data, function (returnedData) {
            alert("works");
        }, "json");
        //alert("You selected left coin state amplitude: " + left + "and right coin state amplitude" + right);
    });

});

