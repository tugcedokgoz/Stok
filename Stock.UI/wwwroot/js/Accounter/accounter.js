function GetApprovedOffer() {
    Get("Offer/GetApprovedOffer", (data) => {
        var html = `<table class="table table-hover">` +
            `<tr>
                <th>Tedarikçi Şirket</th>
                <th>Ürün Adı</th>
                <th>Adet Fiyat</th>
                <th>Adet</th>
                <th>Toplam Fiyat</th>
                <th>Teklif Tarihi</th>
                <th>Fatura</th>
            </tr>`;

        var arr = data;

        for (var i = 0; i < arr.length; i++) {
            html += `<tr>`;
            html += `<td>${arr[i].supplierCompanyName}</td>`;
            html += `<td>${arr[i].productName}</td>`;
            html += `<td>${arr[i].unitPrice}</td>`;
            html += `<td>${arr[i].amount}</td>`;
            html += `<td>${arr[i].offerPrice}</td>`;
            html += `<td>${formatDate(arr[i].createDate)}</td>`;
            html += `<td class="d-flex flex-row">
                           <button type="button" class="btn btn-danger btn-sm m-2" data-bs-toggle="modal" data-bs-target="#billModal" onclick='SetBillModal(${arr[i].id})' >Fatura Oluştur</button>      
                           </td>`;
            html += `</tr>`;
        }
        html += `</table>`;
        $("#divOfferApproved").html(html);
    });
}

function SetBillModal(id) {
    $("#BillId").val(parseInt(id))
}
function SaveRole() {
    var role = {
        Id: $("#BillId").val(),
        Name: $("#inputSupplierName").val(),
        Name: $("#inputCategoryName").val(),
        Name: $("#inputProductName").val(),
        Name: $("#inputAmount").val(),
        Name: $("#inputPrice").val(),
    };

    Post("Bill/Save", role, (data) => {
        GetRoles();
        $("#billModal").modal("hide");
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
    GetApprovedOffer();

    $("#billForm").submit(function (event) {
        event.preventDefault();
        SaveRole();
    });
});

