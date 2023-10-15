function GetOffer() {
    Get("Offer/ListOffersWithRequestStatus8", (data) => {
        var html = `<table class="table table-hover">` +
            `<tr>
                <th>Talep Id</th>
                <th>Tedarikçi Şirket</th>
                <th>Ürün Adı</th>
                <th>Adet Fiyat</th>
                <th>Adet</th>
                <th>Toplam Fiyat</th>
                <th>Teklif Tarihi</th>
                <th>Teklif Cevabı</th>
            </tr>`;

        var arr = data;

        for (var i = 0; i < arr.length; i++) {
            html += `<tr>`;
            html += `<td>${arr[i].requestId}</td>`;
            html += `<td>${arr[i].supplierCompanyName}</td>`;
            html += `<td>${arr[i].productName}</td>`;
            html += `<td>${arr[i].unitPrice}</td>`;
            html += `<td>${arr[i].amount}</td>`;
            html += `<td>${arr[i].offerPrice}</td>`;
            html += `<td>${formatDate(arr[i].createDate)}</td>`;
            html += `<td class="d-flex flex-row">
                           <button type="button" class="btn btn-danger btn-sm m-2" onclick='RetRequestStatus(${arr[i].requestId},10)'>Ret</button>
                        <button type="button" class="btn btn-success btn-sm m-2"  onclick='OnayRequestStatus(${arr[i].requestId},14)'>Onay</button>
                      </td>`;
            html += `</tr>`;
        }
        html += `</table>`;
        $("#divOfferManager").html(html);
    });
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
function formatDate(inputDate) {
    const dateObj = new Date(inputDate);
    const year = dateObj.getFullYear();
    const month = String(dateObj.getMonth() + 1).padStart(2, '0');  // Ay değeri 0'dan başlar, bu nedenle 1 ekledik.
    const day = String(dateObj.getDate()).padStart(2, '0');
    const hours = String(dateObj.getHours()).padStart(2, '0');
    const minutes = String(dateObj.getMinutes()).padStart(2, '0');
    const seconds = String(dateObj.getSeconds()).padStart(2, '0');
    return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
}
$(document).ready(function () {
    GetOffer();

});

