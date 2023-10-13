function GetProduct() {
    Get("Product/GetProduct", (data) => {
        var html = `<table class="table table-hover">` +
            `<tr>
                <th>Kategori Adı</th>
                <th>Ürün Adı</th>
                <th>Price</th>

           
            </tr>`;

        var arr = data;

        for (var i = 0; i < arr.length; i++) {
            html += `<tr>`;
            html += `<td>${arr[i].categoryName}</td>`;
            html += `<td>${arr[i].productName}</td>`;
            html += `<td>${arr[i].price}</td>`;
            html += `<td class="d-flex flex-row">
                      
                      </td>`;
            html += `</tr>`;
        }
        html += `</table>`;
        $("#divProductDetails").html(html);
    });
}

$(document).ready(function () {
    GetProduct();

});

