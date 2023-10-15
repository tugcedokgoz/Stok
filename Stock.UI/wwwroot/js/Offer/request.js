function GetRequest() {
    Get("Request/GetRequestsByStatus/7", (data) => {
        var html = `<table class="table table-hover">` +
            `<tr>
                <th>Talep Id</th>
                <th>Kullanıcı Adı</th>
                <th>Kategori Adı</th>
                <th>Ürün Adı</th>
                <th>Talep Durum</th>
                <th>Talep Tarih</th>
                <th>Adet</th>
                <th>Teklif</th>

            </tr>`;

        var arr = data;
        console.log(data);
        for (var i = 0; i < arr.length; i++) {
            html += `<tr>`;
            html += `<td>${arr[i].id}</td>`;
            html += `<td>${arr[i].userFullName}</td>`;
            html += `<td>${arr[i].categoryName}</td>`;
            html += `<td>${arr[i].productName}</td>`;
            html += `<td>${arr[i].statusRequest}</td>`;
            html += `<td>${formatDate(arr[i].createDate)}</td>`;
            html += `<td>${arr[i].amount}</td>`;
            html += `<td class="d-flex flex-row">
            <button type="button" class="btn btn-success btn-sm m-2" data-bs-toggle="modal" data-bs-target="#userModal"
            onclick='setOfferId(${arr[i].id})'>Teklif Ver</button>
              <button type="button" class="btn btn-danger btn-sm m-2" data-bs-toggle="modal" 
            onclick='finishOffer()'>Teklifi Bitir</button>
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
function LoadSelectOptions() {
    LoadSupplier();
}

function LoadSupplier() {
    var select = $("#inputSupplierName");
    select.empty();
    select.append($('<option>', {
        value: "",
        text: "Tedarikçi Şirket Seçiniz"
    }));

    Get("SupplierProduct/GetSupplierProduct", (data) => {
        data.forEach(function (supplier) {
            select.append($('<option>', {
                value: supplier.supplierCompany.id,
                text: supplier.supplierCompany.supplierCompanyName
            }));
        });
    });
}

function setOfferId(offerId) {
    document.getElementById("offerId").value = offerId;
}

function ManagerRequestStatus(requestId, status) {
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
function finishOffer() {
    var offerData = {
        RequestId: $("#offerId").val(),
        OfferPrice: $("#inputOffer").val(),
    }
    if (parseInt(offerData.OfferPrice) <= 5000)
        ManagerRequestStatus(offerData.RequestId, 8);
    else
        ManagerRequestStatus(offerData.RequestId, 11);
}
$(document).ready(function () {
    GetRequest();
    LoadSelectOptions();

    document.querySelectorAll(".btn-success").forEach(function (button) {
        button.addEventListener("click", function () {
            const offerId = button.getAttribute("data-offer-id");
            setOfferId(offerId);
        });
    })
    // Teklif formunu gönderme işlevi
    $("#userForm").submit(function (e) {
        e.preventDefault();

        var offerData = {
            RequestId: $("#offerId").val(),
            OfferPrice: $("#inputOffer").val(),
            Amount: $("#inputAmount").val(),
            UnitPrice: $("#inputUnitPrice").val(),
            SupplierCompanyId: $("#inputSupplierName").val()
        };

        Post("Offer/Save", offerData, (data) => {
            GetRequest();
            $("#userModal").modal("hide");
            alert("Teklif başarıyla kaydedildi");
        });

    });

});