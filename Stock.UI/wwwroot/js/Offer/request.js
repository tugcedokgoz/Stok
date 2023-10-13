function GetRequest() {
    Get("Request/GetRequestsByStatus/7", (data) => {
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
            html += `<td>${formatDate(arr[i].createDate)}</td>`;
            html += `<td>${arr[i].amount}</td>`;
            html += `<td class="d-flex flex-row">
            <button type="button" class="btn btn-success btn-sm m-2" data-bs-toggle="modal" data-bs-target="#userModal"
            onclick='setOfferId(${arr[i].id})'>Teklif Ver</button>
            </td>`;
            html += `</tr>`;
        }
        html += `</table>`;
        $("#divOfferRequests").html(html);
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

//function setOfferId(offerId) {
//    // Gizli input alanına tıklanan satırın ID değerini ayarlama
//    document.getElementById('offerId').value = offerId;
//}
//function submitOffer(offerId, offerPrice, successCallback) {
//    var data = {
//        offerId: offerId,
//        offerPrice: offerPrice
//    };

//    Post("api/Offer/SubmitOffer", data, function (response) {
//        if (response.Status === "Success") {
//            // İşlem başarılı oldu, gerekirse başka bir işlem yapabilirsiniz.
//            console.log(response.Message);

//            // Örnek olarak, teklif formunu kapatma işlemi
//            $("#userModal").modal("hide");
//        } else {
//            // Hata durumunda işlem yapabilirsiniz.
//            console.error(response.Message);
//        }

//        if (typeof successCallback === 'function') {
//            successCallback(response);
//        }
//    });
//}

//// Ekstra işlevler burada

$(document).ready(function () {
    GetRequest();

    //$("#userForm").submit(function (e) {
    //    e.preventDefault();

    //    // offerId ve offerPrice değerlerini alarak submitOffer işlemini çağırın
    //    var offerId = $("#offerId").val();
    //    var offerPrice = $("#inputOffer").val();

    //    submitOffer(offerId, offerPrice);
    //});
});