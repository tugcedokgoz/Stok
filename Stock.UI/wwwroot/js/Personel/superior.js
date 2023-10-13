function GetRequest() {
    var req = $("#inputUserId").val();
    Get("Request/GetRequestsByUserSuperiorId/" + req, (data) => {
        var html = `<table class="table table-hover">` +
            `<tr>
                <th>Kullanıcı Adı</th>
                <th>Kategori Adı</th>
                <th>Ürün Adı</th>
                <th>Talep Durum</th>
                <th>Talep Tarih</th>
                <th>Adet</th>
                <th>RET/ONAY</th>
           
            </tr>`;

        var arr = data;
        console.log(data);
        for (var i = 0; i < arr.length; i++) {
            html += `<tr>`;
            html += `<td>${arr[i].userFullName}</td>`;
            html += `<td>${arr[i].categoryName}</td>`;
            html += `<td>${arr[i].productName}</td>`;
            html += `<td>${arr[i].statusRequest}</td>`;
            html += `<td>${arr[i].createDate}</td>`;
            html += `<td>${arr[i].amount}</td>`;
            html += `<td class="d-flex flex-row">
                           <button type="button" class="btn btn-danger btn-sm m-2" onclick='RetStatus(${arr[i].id})'>Ret</button>
                        <button type="button" class="btn btn-success btn-sm m-2"  onclick='OnayStatus(${arr[i].id})'>Onay</button>
                      </td>`;
            html += `</tr>`;
        }
        html += `</table>`;
        $("#divSuperiorRequests").html(html);
    });
}
function RetStatus(requestId) {
    RetRequestStatus(requestId, 4);
}
function RetRequestStatus(requestId, status) {
    $.ajax({
        type: "POST",
        url: `${BASE_API_URI}/Request/UpdateRequestStatus/${requestId}/${status}`,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            alert("Talep Başarıyla Reddedildi");
            location.reload();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest + "-" + textStatus + "-" + errorThrown);
        }
    });
}

function OnayStatus(requestId) {
    OnayRequestStatus(requestId, 5);
}
function OnayRequestStatus(requestId, status) {
    $.ajax({
        type: "POST",
        url: `${BASE_API_URI}/Request/UpdateRequestStatus/${requestId}/${status}`,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            alert("Talep Başarıyla Reddedildi");
            location.reload();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest + "-" + textStatus + "-" + errorThrown);
        }
    });
}
$(document).ready(function () {
    GetRequest();
});
