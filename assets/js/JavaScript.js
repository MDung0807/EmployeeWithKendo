
//Load form Create Employeee
function showFormCreateEmployee() {
    var wnd = $("#Details").data("kendoWindow");
    wnd.title("Thêm Nhân Viên Mới")
    wnd.param = 'createEmployee'
    wnd.center().open();
}


// Show Form Update
function showFormUpdateEmployee(e) {
    e.preventDefault();
    const userId = this.dataItem($(e.currentTarget).closest("tr")).userId;
    updateForm(userId);
    var wnd = $("#Details").data("kendoWindow");
    wnd.title("Chỉnh Sửa Thông Tin Nhân Viên")
    wnd.param= "updateEmployee"
    wnd.center().open();
}

//Check UserId exist
async function checkUserIdExist() {
    const userId = document.getElementById('userId').value;
    const param = "userId="+userId
    const status = await sendRequestTypeGet("/home/checkUserIdExist?", param)
    if (status === "TRUE") {
        document.getElementById("userIdText").innerHTML = "UserId exist";
        return true;
    }
    else {
        document.getElementById("userIdText").innerHTML = "";
        return false;
    }
}

//CheckPassword Cofirm
function checkPasswordConfirmIsTrue() {
    const password = document.getElementById('password').value;
    const passwordConfirm = document.getElementById('passwordConfirm').value;
    if (password.localeCompare(passwordConfirm) != 0) {
        document.getElementById('passwordConfirmText').innerHTML = "Password not match";
        return false;
    }
    document.getElementById('passwordConfirmText').innerHTML = "";
    return true;
}

//Update Employee
 async function updateForm(userId) {
        document.getElementById('form_password').style.display = 'none';
        document.getElementById("userId").disabled = true
        param = "userId=" + userId
     datas = await sendRequestTypeGet("/home/findbyId?", param)
        console.log(datas)
        document.getElementById("userId").value = datas[0].userId
        document.getElementById("username").value = datas[0].username
        document.getElementById("email").value = datas[0].email
        document.getElementById("tel").value = datas[0].tel
}


//Submit button click
async function submit() {
    var wnd = $("#Details").data("kendoWindow");
    var status
    if (validateInput()) {
        var employee = {
            userId: document.getElementById("userId").value,
            username: document.getElementById("username").value,
            email: document.getElementById("email").value,
            tel: document.getElementById("tel").value,
        };
        if (wnd.param == 'createEmployee') {
            employee.password = document.getElementById("password").value;
            status = await submitForm("/home/create", employee)
            if (status) {
                clearText();
            }
        }
        else if (wnd.param == 'updateEmployee') {
            status = await submitForm('/home/update', employee)
            if (status) {
                closeWindow()
            }
        }
        if (status) {
            document.getElementById('message-validate').innerHTML = ""
            reloadData()
        }
        else {
            document.getElementById('message-validate').innerHTML = "Error!"
        }
    }
    
}
//Clear button click
function clearText() {
    var wnd = $("#Details").data("kendoWindow");
    if (wnd.param == 'createEmployee') {
        document.getElementById('userId').value = null
        document.getElementById("userIdText").innerHTML = "";
        document.getElementById('message-validate').innerHTML = ""
        document.getElementById('passwordConfirmText').innerHTML = "";



    }
    document.getElementById("userId").valu = null
    document.getElementById("username").value = null
    document.getElementById("password").value = null
    document.getElementById("passwordConfirm").value = null
    document.getElementById("email").value = null
    document.getElementById("tel").value = null
}


