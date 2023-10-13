var BASE_API_URI = "https://localhost:7177/api";

function Get(action, success) {
    $.ajax({
        type: "GET",
        url: `${BASE_API_URI}/${action}`,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            success(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest + "-" + textStatus + "-" + errorThrown);
        }
    });
}


function Post(action, data, success) {
    $.ajax({
        type: "POST",
        url: `${BASE_API_URI}/${action}`,
        data: JSON.stringify(data),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            success(response);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest + "-" + textStatus + "-" + errorThrown);
        }
    });
}


function Put(action, data, success) {
    $.ajax({
        type: "PUT",
        url: `${BASE_API_URI}/${action}`,
        data: JSON.stringify(data),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            success(response);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest + "-" + textStatus + "-" + errorThrown);
        }
    });
}



//function Delete(action, success) {
//    $.ajax({
//        type: "DELETE",
//        url: `${BASE_API_URI}/${action}`,
//        dataType: "json",
//        contentType: "application/json; charset=utf-8",
//        success: function (response) {
//            success(response);
//        },
//        error: function (XMLHttpRequest, textStatus, errorThrown) {
//            alert(XMLHttpRequest + "-" + textStatus + "-" + errorThrown);
//        }
//    });
//}
