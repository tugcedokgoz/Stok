function GetSupplier() {
    Get("SupplierProduct/GetSupplierProduct", (data) => {
        var html = `<table class="table table-hover">` +
            `<tr>
                <th>Tedarikçi Şirket Adı</th>
                <th>Kategori Adı</th>
                <th>Ürün Adı</th>
                <th>Ürün Fiyat</th>


           
            </tr>`;

        var arr = data;

        for (var i = 0; i < arr.length; i++) {
            html += `<tr>`;
            html += `<td>${arr[i].supplierCompany.supplierCompanyName}</td>`;
            html += `<td>${arr[i].product.category.categoryName}</td>`;
            html += `<td>${arr[i].product.productName}</td>`;
            html += `<td>${arr[i].price}</td>`;
            html += `<td class="d-flex flex-row">
                      
                      </td>`;
            html += `</tr>`;
        }
        html += `</table>`;
        $("#divSupplierProduct").html(html);
    });
}

$(document).ready(function () {
    GetSupplier();

});

