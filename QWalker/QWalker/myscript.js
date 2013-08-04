$(document).ready(function () {
    $("#thing").mouseover(function () { $("#instructions").html("hovering your mouse?")});
    $("#thing").mouseout(function () { $("#instructions").html("")});
    $("#runWalk").click(function () {
        var left = $("#left").val();
        var right = $("#right").val();
        var data = { left: left, right: right };
        $.post('/input/Walk', data, function (returnedData) {
            // this returns object Object- ok needed to specify to show the results
            //results = { "data": returnedData.Results };
            var results = []
            results.push(returnedData.Results);
            //var results = results.data;
            // error is- d[i].data = null, can't work out what d is though....
            //alert("works" + results);
            $.plot($("#graph"), results, { xaxis: { min: -50, max: 50 }, colors: ["#0022FF"] });
        }, "json");
        // gah= its still a 1d array, now just with the positions, so still need to format it before it can be plotted.
        //the data is just the y values, need to convert it into an array of form [x, y], x going from -50 to +50- ok think its better to try to do this in the csharp code and have what arrives here in correct format already
        //function plotting(returnedData){
            //var array = Array();
            //for (var i = 0; returnedData.length; i++)
            //{
              //  array[i] = [0, 0];
              // array[i][0] = i - 51;
              //  array[i][1] = returnedData[i];}
            //return array

        //}
        //alert("You selected left coin state amplitude: " + left + "and right coin state amplitude" + right);
    });

});

