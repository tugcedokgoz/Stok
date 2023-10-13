function GetStock() {
    Get("DepartmentStock/GetDepartmentStock", (data) => {
        var html = `<table class="table table-hover">` +
            `<tr>
                <th>Şirket Adı</th>
                <th>Departman Adı</th>
                <th>Kategori Adı</th>
                <th>Ürün Adı</th>
                <th>Adet</th>
           
            </tr>`;

        var arr = data;

        for (var i = 0; i < arr.length; i++) {
            html += `<tr>`;
            html += `<td>${arr[i].companyName}</td>`;
            html += `<td>${arr[i].departmentName}</td>`;
            html += `<td>${arr[i].categoryName}</td>`;
            html += `<td>${arr[i].productName}</td>`;
            html += `<td>${arr[i].amount}</td>`;
            html += `<td class="d-flex flex-row">
                      
                      </td>`;
            html += `</tr>`;
        }
        html += `</table>`;
        $("#divDepartmentStock").html(html);
    });
}

$(document).ready(function () {
    GetStock();

});