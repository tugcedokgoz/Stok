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
function SaveBill() {
    var bill = {
        Id: 0,
        SupplierCompanyId: $("#inputSupplierName").val(),
        CategoryId: $("#inputCategoryName").val(),
        ProductId: $("#inputProductName").val(),
        Amount: $("#inputAmount").val(),
        Price: $("#inputPrice").val(),
    };

    Post("Bill/Save", bill, (data) => {
        GetApprovedOffer();
        $("#billModal").modal("hide");
    });
}

function LoadSelectOptions() {
    LoadCategories();
}

function LoadSupplierCompany() {
    var select = $("#inputSupplierName");
    select.empty();
    select.append($('<option>', {
        value: "",
        text: "Şirket Seçiniz"
    }));

    Get("SupplierCompany/GetSupplierCompany", (data) => {
        data.forEach(function (supplier) {
            select.append($('<option>', {
                value: supplier.id,
                text: supplier.supplierCompanyName
            }));
        });
    });  
}
function LoadCategories() {
    var select = $("#inputCategoryName");
    select.empty();
    select.append($('<option>', {
        value: "",
        text: "Kategori Seçiniz"
    }));

    Get("Category/GetCategories", (data) => {
        data.forEach(function (category) {
            select.append($('<option>', {
                value: category.id,
                text: category.categoryName
            }));
        });


        select.change(function () {
            var selectedCategoryId = $(this).val();
            if (selectedCategoryId) {
                LoadCategoryProducts(selectedCategoryId);
            } else {
                var productSelect = $("#inputProductName");
                productSelect.empty();
                productSelect.append($('<option>', {
                    value: "",
                    text: "Ürün Seçiniz"
                }));
            }
        });
    });
}


function LoadCategoryProducts(categoryId) {
    Get(`Product/GetProductByCatgeoryId/${categoryId}`, (data) => {
        var select = $("#inputProductName");
        select.empty();
        select.append($('<option>', {
            value: "",
            text: "Ürün Seçiniz"
        }));
        data.forEach(function (product) {
            select.append($('<option>', {
                value: product.id,
                text: product.productName
            }));
        });
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
    LoadSelectOptions();
    LoadSupplierCompany();
    $("#billForm").submit(function (event) {
        event.preventDefault();
        SaveBill();
    });
});

