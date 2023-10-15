function GetCompany() {
    Get("Company/GetCompany", (data) => {
        var html = `<table class="table table-hover">` +
            `<tr>
            <th>Şirket Adı</th>
            <th>Üst Şirket Adı</th>
            <th></th>
            </tr>`;
        var arr = data;
        for (var i = 0; i < arr.length; i++) {
            html += `<tr>`;
            html += `<td>${arr[i].companyName}</td>`;
            html += `<td>${arr[i].superiorName}</td>`;
            //html += `<td><i class="fa fa-trash text-danger" onclick='DeleteRole(${arr[i].id})'></i><i class="fa-pencil-square" onclick='EditRole(${arr[i]})'></i></td>`;
          
            html += `</tr>`
        }
        html += `</table>`;
        $("#divCompanies").html(html);
    });
}
$(document).ready(function () {
    GetCompany();
});