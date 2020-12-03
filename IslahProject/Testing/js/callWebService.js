function callws(webServiceName, functionName, data, callback) {
   

    $.ajax({
        url: `127.0.0.1/islah/api/Vehicle/StoreVehicle`,
        type: 'POST',
        dataType: 'json',
        
        contentType: 'application/json',
        data: JSON.stringify(data)
    })
    .done(function (data) {
        console.log("success");
        callback(data);
    })
    .fail(function (error) {
        console.log('error');
    })
    .always(function () {
        console.log("complete");
    });
}


function get(webS,func, dd, callback) {
  
    var d = { DATA: JSON.stringify(dd) };
    console.log(d);
    callws(webS+'.asmx', func, d, function (data) {
        console.log("success get ");
        callback(data);
    });

}



