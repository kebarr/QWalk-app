$(document).ready(function () {
    var AllResults = $();
    var result = [];
    // assume not plotting more than 5 walks on one graph... need to work out how to stop these changing every time a new series is plotted.
    var seriesColors = ["#0022FF", "#FF7070", "#000000", "#800080", "#FFFF00"];
    $("#thing").mouseover(function () { $("#instructions").html("Choose initial state. This must make probability sum to one, If unsure, take two decimals which sum to one, and square root them, put one in each coin state. Enter square roots as sqrt(number). Enter complex numbers as a + bi where a/b can be sqrt(c). Inputs can be negative.")});
    $("#thing").mouseout(function () { $("#instructions").html("")});
    $("#runWalk").click(function() {
        var left = $("#left").val();
        var right = $("#right").val();
        var data = { left: left, right: right };
        $.post('/input/Walk', data, function(returnedData) {
            if (returnedData.ErrorMessage == "error") {
                alert("invalid input");
            }
                // this returns object Object- ok needed to specify to show the results
                //results = { "data": returnedData.Results };
                // this overwrites previous result, so need a big 'results array to append this to
            else {
                var results = [];
                results.push(returnedData.Results);
                AllResults = AllResults.add(results);
                //var results = results.data;
                // error is- d[i].data = null, can't work out what d is though....
                //alert("works" + AllResults);
                $.each(AllResults, function(index, value) {
                    result.push(value);
                    // this gives correct value... but only plots one at a time again...
                    //alert(result);
                    // now plots but reassigns colours each time. adding [index] to seriesColor makes them all same color again.
                });
                // taking out of loop... probably more efficient, but doesn't solve colours problem- still a problem if their default colours are used.
                // plagiarised: hard-code color indices to prevent them from shifting
                //var i = 0;
                //$.each(result, function (key, val) {
                //    val.color = seriesColors[i];
                //    ++i;
                //});
                // problem with above and all exmples is that the series exist when the colors are set. if i could reset the index that it uses to get the colour each time, i think that would work, as its scrolling through in order.
                $.plot($("#graph"), result, { xaxis: { min: -50, max: 50 }, colors: ["#0022FF", "#0022FF", "#FF7070", "#0022FF", "#FF7070", "#000000", "#0022FF", "#FF7070", "#000000", "#800080", "#0022FF", "#FF7070", "#000000", "#800080", "#FFFF00"] });
                $("#graphlabel").removeClass("hidden");
                $("#runWalk2").removeClass("hidden");
                $("#xaxis").removeClass("hidden");
            };
        }, "json")});
        $("#runWalk2").click(function () {
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
        });
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

