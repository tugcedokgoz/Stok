let requests = [];
function GetRequest() {
    var req = $("#inputUserId").val();
    Get("Request/GetRequestsByUserId/" + req , (data) => {
        var html = `<table class="table table-hover">` +
            `<tr>
                <th>Kullanıcı Adı</th>
                <th>Kategori Adı</th>
                <th>Ürün Adı</th>
                <th>Talep Durum</th>
                <th>Talep Tarih</th>
                <th>Adet</th>
                <th>Düzenle</th>
           
            </tr>`;

        var arr = data;

        for (var i = 0; i < arr.length; i++) {
            html += `<tr>`;
            html += `<td>${arr[i].userFullName}</td>`;
            html += `<td>${arr[i].categoryName}</td>`;
            html += `<td>${arr[i].productName}</td>`;
            html += `<td>${arr[i].statusRequest}</td>`;
            html += `<td>${formatDate(arr[i].createDate)}</td>`;
            html += `<td>${arr[i].amount}</td>`;
            html += `<td class="d-flex flex-row">
                        <button type="button" class="btn btn-success btn-sm m-2" data-bs-toggle="modal" data-bs-target="#userEditModal" onclick='EditUser(${arr[i].id})'>Onay</button>
                      </td>`;
            html += `</tr>`;
        }
        html += `</table>`;
        $("#divRequests").html(html);
    });
}

function LoadSelectOptions() {
    LoadCategories();
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

function SaveRequest() {
    var request = {
        Id: 0,
        UserId: $("#inputUserfullName").val(),
        CategoryId: $("#inputCategoryName").val(),
        ProductId: $("#inputProductName").val(),
        Amount: $("#inputAmount").val(),
        RequestStatusId: null,  
        UserSuperiorId: null,
        UpdateDate: null
        
       
    };
    Post("Request/Save", request, (data) => {
        GetRequest();
        $("#requestModal").modal("hide");
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
    GetRequest();
    LoadSelectOptions();

});