﻿let users = [];
function GetUsers() {
    Get("User/GetUsers", (data) => {
        var html = `<table class="table table-hover">` +
            `<tr>
                <th>Kullanıcı Adı</th>
                <th>Kullanıcı Email</th>
                <th>Şifre</th>
                <th>Şirket</th>
                <th>Şirket Departman Adı</th>
                <th>Rol Adı</th>
                <th>Amir Adı</th>
                <th>Düzenle</th>
            </tr>`;

        var arr = data;

        for (var i = 0; i < arr.length; i++) {
            html += `<tr>`;
            html += `<td>${arr[i].userFullName}</td>`;
            html += `<td>${arr[i].userEmail}</td>`;
            html += `<td>${arr[i].password}</td>`;
            html += `<td>${arr[i].companyName}</td>`;
            html += `<td>${arr[i].unitName}</td>`;
            html += `<td>${arr[i].roleName}</td>`;
            html += `<td>${arr[i].superiorName}</td>`;
            html += `<td class="d-flex flex-row">
                        <button type="button" class="btn btn-danger btn-sm m-2" onclick='DeleteUser(${arr[i].id})'>Delete</button>
                      /*  <button type="button" class="btn btn-warning btn-sm m-2" data-bs-toggle="modal" data-bs-target="#userEditModal" onclick='SetUserIdforEditModal(${arr[i].id})'>Edit</button>*/
                      </td>`;
            html += `</tr>`;
        }
        html += `</table>`;
        $("#divUsers").html(html);
    });
}
function SaveUser() {

    var user = {
        Id: 0,
        UserFullName: $("#inputuserfullName").val(),
        UserEmail: $("#inputuserEmail").val(),
        Password: $("#inputuserPassword").val(),
        CompanyDepartmentId: $("#inputCompanyDepartmentName").val(),
        RoleId: $("#inputRole").val(),
        SuperiorId: $("#inputSuperiorName").val(),
      
    };

    Post("User/Save", user, (data) => {

        GetUsers();
        $("#userModal").modal("hide");
    });
}
//function SetUserIdforEditModal(id) {
//    GetUsers();
//    $("#EditUserId").val(parseInt(id))



//}
//function UpdateUser() {

//    var user = {
//        Id: $("#EditUserId").val(),
//        UserFullName: $("#inputEdituserfullName").val(),
//        UserEmail: $("#inputEdituserEmail").val(),
//        Password: $("#inputEdituserPassword").val(),
//        CompanyDepartmentId: $("#inputEditCompanyDepartmentName").val(),
//        RoleId: $("#inputEditRole").val(),
//        SuperiorId: $("#inputEditSuperiorName").val(),
//    };

//    Post("User/Save", user, (data) => {

//        GetUsers();
//        $("#userEditModal").modal("hide");
//    });
//}

$(document).ready(function () {
    GetUsers();
    LoadSelectOptions();
    $("#userForm").submit(function (event) {
        event.preventDefault();
        SaveUser();
    });
    //$("#userEditForm").submit(function (event) {
    //    event.preventDefault();
    //    UpdateProduct();
    //});
});

function LoadSelectOptions() {
    LoadCompanies();
    LoadRoles();
    LoadSuperiors();
}

function LoadCompanies() {
    var select = $("#inputCompanyName");
    select.empty();
    select.append($('<option>', {
        value: "",
        text: "Şirket Seçiniz"
    }));

    Get("Company/GetCompany", (data) => {
        data.forEach(function (company) {
            select.append($('<option>', {
                value: company.id,
                text: company.companyName
            }));
        });


        select.change(function () {
            var selectedCompanyId = $(this).val();
            if (selectedCompanyId) {
                LoadCompanyDepartments(selectedCompanyId);
            } else {
                var departmentSelect = $("#inputCompanyDepartmentName");
                departmentSelect.empty();
                departmentSelect.append($('<option>', {
                    value: "",
                    text: "Departman Seçiniz"
                }));
            }
        });
    });
}


function LoadCompanyDepartments(companyId) {
    Get(`CompanyDepartment/GetDepartmentsByCompanyId/${companyId}`, (data) => {
        var select = $("#inputCompanyDepartmentName");
        select.empty();
        select.append($('<option>', {
            value: "",
            text: "Departman Seçiniz"
        }));
        data.forEach(function (department) {
            select.append($('<option>', {
                value: department.id,
                text: department.departmentName
            }));
        });
    });
}

function LoadRoles() {
    Get("Role/GetRoles", (data) => {
        var select = $("#inputRole");
        select.empty();
        select.append($('<option>', {
            value: "",
            text: "Rol Seçiniz"
        }));
        data.forEach(function (role) {
            select.append($('<option>', {
                value: role.id,
                text: role.roleName
            }));
        });
    });
}


function LoadSuperiors() {
    Get("User/GetUsers", (data) => {
        var select = $("#inputSuperiorName");
        select.empty();
        select.append($('<option>', {
            value: "",
            text: "Amir Seçiniz"
        }));
        data.forEach(function (superior) {
            select.append($('<option>', {
                value: superior.id,
                text: superior.userFullName
            }));
        });
    });
}






//$(document).ready(function () {
//    GetUsers();
//});
