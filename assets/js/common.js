

// Send Request to Server
async function sendRequestTypeGet(api, param) {
     // Gửi dữ liệu bằng AJAX bất đồng bộ qua phương thức GET
    try {
        const result = await $.ajax({
            url: api + param, // Địa chỉ action và dữ liệu biểu mẫu
            type: "GET",
        });
        console.log(result)
        return result.Data
    }
    catch (error) {
        console.log("Eror", error)
        return null
    }
}

//Requet Post method
async function submitForm(api, data) {
    try {
        const result = await $.ajax({
            url: api, // Địa chỉ action để lưu dữ liệu
            type: "POST",
            data: data, // Dữ liệu biểu mẫu
        });
        console.log("Result: ", result)
        return result;
    }
    catch (error) {
        console.log("Error: ", error)
    }
    return false;
}

function reloadData() {
    $("#grid").data("kendoGrid").dataSource.read();
}


function validateInput() {
    if (checkUserIdExist() && !checkPasswordConfirmIsTrue()) {
        document.getElementById('message-validate').innerHTML = "Error!"
        return false
    }
    document.getElementById('message-validate').innerHTML = ""
    return true
}

//Close Window
function closeWindow() {
    var wnd = $("#Details").data("kendoWindow");
    wnd.param = "createEmployee"
    clearText()
    document.getElementById('form_password').style.display = 'block';
    document.getElementById("userId").disabled = false
    document.getElementById("userIdText").innerHTML = "";
    wnd.close()
}
