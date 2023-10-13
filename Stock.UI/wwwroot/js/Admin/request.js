function GetRequests() {
    Get("Request/GetRequests", (data) => {
        var html = `<table class="table table-hover">` +
            `<tr>
                <th>Kullanıcı Adı</th>
                <th>Kategori Adı</th>
                <th>Ürün Adı</th>
                <th>Talep Durum</th>
                <th>Talep Tarih</th>
                <th>Adet</th>
           
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
            //html += `<td><i class="fa fa-trash text-danger" onclick='DeleteRole(${arr[i].id})'></i><i class="fa-pencil-square" onclick='EditRole(${arr[i]})'></i></td>`;

        }
        html += `</table>`;
        $("#divRequests").html(html);
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
    GetRequests();
});