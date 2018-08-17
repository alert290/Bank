function AddCustomer() {
    $.post('../api/customer/AddCustomer', $('#add_customer').serialize())
        .done(function (response) {
            $("#add_customer").trigger('reset');
            $("#add_customer_result").text(response)
        })
        .fail(function (response) { $("#add_customer_result").text(response.responseJSON) })
}

function EditCustomer() {
    $.post('../api/customer/EditCustomer', $('#edit_customer').serialize())
        .done(function (response) {
            $("#current_password").val('').change();
            $("#new_password").val('').change();
            $("#edit_customer_result").text(response);
            GetCustomersList();
        })
        .fail(function (response) { $("#edit_customer_result").text(response.responseJSON) })
}

function GetCustomersList() {
    $.get('../api/customer/GetAllCustomers')
        .done(function (response) {
            var output = [];
            $.each(response, function (index) {
                output.push('<option value="' + response[index].CustomerID + '">' + response[index].Login + '</option>');
            });
            $('#customers_list').empty();
            $('#customers_list').html(output.join(''));
            if (output.length !== 0) {
                GetSelectedCustomerInfo();
            }
        })
}

function GetSelectedCustomerInfo() {
    $.get('../api/customer/GetCustomer?id=' + $("#customers_list").val())
        .done(function (data) {
            $("#customer_id").val(data.CustomerID).change();
            $("#first_name").val(data.FirstName).change();
            $("#last_name").val(data.LastName).change();
            $("#login").val(data.Login).change();
            $("#current_password").val('').change();
            $("#new_password").val('').change();
        })
        .fail(function (response) { $("#edit_customer_result").text(response.responseJSON) })
}

function SetAddEvents() {
    $("#add_customer").submit(function (e) {
        e.preventDefault();
        AddCustomer();
    });
}

function SetEditEvents() {
    $("#edit_customer").submit(function (e) {
        e.preventDefault();
        EditCustomer();
    })

    GetCustomersList();

    $("#customers_list").change(function (e) {
        GetSelectedCustomerInfo();
    })
}
