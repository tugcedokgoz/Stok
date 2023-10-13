function GetCompany() {
    Get("Company/GetCompany", (data) => {
        var html = `<table class="table table-hover">` +
            `<tr>
            <th style="width:50px"Id></th>
            <th>Şirket Adı</th>
            <th></th>
            </tr>`;
        var arr = data;
        for (var i = 0; i < arr.length; i++) {
            html += `<tr>`;
            html += `<td>${arr[i].id}</td><td>${arr[i].companyName}</td>`;
            //html += `<td><i class="fa fa-trash text-danger" onclick='DeleteRole(${arr[i].id})'></i><i class="fa-pencil-square" onclick='EditRole(${arr[i]})'></i></td>`;
            html += `<td>
                                     <button type="button" class="btn btn-danger"  onclick='DeleteRole(${arr[i].id})'>Delete</button>
                                     &nbsp;
                                     <button type="button" class="btn btn-warning"  data-bs-toggle="modal" data-bs-target="#roleEditModal" onclick='SetRoleIdforEditModal(${arr[i].id})'>Edit</button>
                             </td>`;
            html += `</tr>`
        }
        html += `</table>`;
        $("#divCompanies").html(html);
    });
}
$(document).ready(function () {
    GetCompany();
});