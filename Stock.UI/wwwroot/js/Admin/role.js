function GetRoles() {
    Get("Role/GetRoles", (data) => {
        var html = `<table class="table table-hover">` +
            `<tr>
                <th>Rol Adı</th>

            </tr>`;

        var arr = data;

        for (var i = 0; i < arr.length; i++) {
            html += `<tr>`;
            html += `<td>${arr[i].roleName}</td>`;

            html += `<td class="d-flex flex-row">
                        <button type="button" class="btn btn-danger btn-sm m-2" onclick='DeleteUser(${arr[i].id})'>Delete</button>
                       <button type="button" class="btn btn-warning btn-sm m-2" data-bs-toggle="modal" data-bs-target="#userEditModal" onclick='SetUserIdforEditModal(${arr[i].id})'>Edit</button>
                      </td>`;
            html += `</tr>`;
        }
        html += `</table>`;
        $("#divRoles").html(html);
    });
}

function SaveRole() {
    var role = {
        Id: 0,
        roleName: $("#inputRoleName").val()
    };

    Post("Role/SaveRole", role, (data) => {
        GetRoles();
        $("#roleModal").modal("hide");
    });
}
$(document).ready(function () {
    GetRoles();
    $("#roleForm").submit(function (event) {
        event.preventDefault();
        SaveRole();
    });

});