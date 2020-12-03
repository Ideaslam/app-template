

//android
//var DATA = { employeeToken: 'GKKYIE684YIHKFGf1002', lang: "AR", printStatus: "2", employeeStatus: "1", txtsearch: "" }; /// employees

//var DATA = { employeeToken: '1000', lang: "_AR_V", txtsearch: "", pageId :"27"};

//var DATA = {employeeToken: 'GKKYIE684YIHKFGf1002',lang: "AR",employeeId: "46197",status: "M",dateType: "0",DaysNumber: "0",startDate: "2023-05-03",endDate: "2023-05-03",notes: "test programming" };




//var DATA = {
//    employeeToken: 'GKKYIE684YIHKFGf1002',
//    employeeId: '',
//    lang: "AR",
//    dateType: "0",
//    fromDate: "2018-04-01",
//    toDate: "2018-05-01",
//    administration: "",
//    status: "",
//    orderby: "",
//    sex: "",
//    level: ""
//};

//var DATA = {
//    employeeToken: '111',
//    lang:"AR",
//    Id: "396830",
//    In1:"08:02",
//    Out1:"14:02",
//    Note:"test101",
//};

///*(
//var DATA = { employeeToken: 'GKKYIE684YIHKFGf1002', lang: "AR", employeeId: "46197", status: "M", dateType: "0", DaysNumber: "0", startDate: "2018-06-03", endDate: "2018-06-03", notes: "test programming" };


//var DATA = {
//    employeeToken: '2000',
//    lang: "AR",
//    orderId: "26",
//    status: "1"
//};
//*/
////token = "IZ8GQQABR8VWAEQFFHNZKGA77R66M1ISVNCAY4B8";

//console.log(DATA);
//var DATA = { employeeToken: '5000', lang: "AR" };
 
////var DATA = { employeeToken: '111', lang: "AR", employeeId: "46161", status: "M", dateType: "0", DaysNumber: "0", startDate: "2085-08-10", endDate: "2085-08-10", notes: "test programmfdfdfing 455454" };
//  var DATA = { employeeToken: '5000', lang: "AR", employeeId: "46197", status: "M", dateType: "0", DaysNumber: "0", startDate: "2024-05-08", endDate: "2024-05-08", notes: "test programming" };

var DATA = {
    
Number: "122asx",
    manufacturer: "Toyota",
    model: "Colora",
    color: "Black",
    registrationType: 2,
    foundDate: "2018-03-25"
};

//var DATA =  "cbUyAzdT5kI:APA91bHN2-t-jYgbzmv3sLYpzCEXa4Sa6YTPDnwLjuJWozi_lMa17DAongfHUzHLXn9frql-4Xie3xWZB84MvZpLZ3v77aSiyjyA1EfwA7FJ7mlqcGmnLggMyHvNGhaWYu39F9Sp9qWXpXGZ_5lTX0YSBcGUyppDfA"
/*
delete_device_id('getdata', 'delete_deviceId', DATA, function (data) {
    console.log(data);
    console.log(data.d.InnerData);
});
*/

get_db('getdata', 'get', DATA, function (data) {

    console.log(data);
    console.log(data.d.InnerData);

});