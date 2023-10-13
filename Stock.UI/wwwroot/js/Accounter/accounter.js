function GetRequest() {
    Get("Request/GetRequestsByStatus/5", (data) => {
        var html = `<table class="table table-hover">` +
            `<tr>
                <th>Kullanıcı Adı</th>
                <th>Kategori Adı</th>
                <th>Ürün Adı</th>
                <th>Talep Durum</th>
                <th>Talep Tarih</th>
                <th>Adet</th>
                <th>Teklifte Bulun</th>
           
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

                        <button type="button" class="btn btn-success btn-sm m-2"  onclick='OfferStatus(${arr[i].id})'>Teklif Al</button>
                      </td>`;
            html += `</tr>`;
        }
        html += `</table>`;
        $("#divAccounters").html(html);
    });
}

function OfferStatus(requestId) {
    OfferRequestStatus(requestId, 7);
}
function OfferRequestStatus(requestId, status) {
    $.ajax({
        type: "POST",
        url: `${BASE_API_URI}/Request/UpdateRequestStatus/${requestId}/${status}`,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            alert("Talep Satın Alma Birimine İletildi");
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