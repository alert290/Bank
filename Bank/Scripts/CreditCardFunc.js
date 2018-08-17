function AddCreditCard() {
    $.post('../api/creditcard/AddCreditCard', $('#add_credit_card').serialize())
        .done(function (response) {
            $("#add_credit_card").trigger('reset');
            $("#add_credit_card_result").text(response)
        })
        .fail(function (response) { $("#add_credit_card_result").text(response.responseJSON) })
}

function EditCreditCard() {
    $.post('../api/creditcard/EditCreditCard', $('#edit_credit_card').serialize() )
        .done(function (response) {
            $("#current_pin").val('').change();
            $("#new_pin").val('').change();
            $("#edit_credit_card_result").text(response);
            GetAllCustomersEdit();
        })
        .fail(function (response) { $("#edit_credit_card_result").text(response.responseJSON) })
}

function GetAllCustomersAdd() {
    $.get('../api/customer/GetAllCustomers')
        .done(function (response) {
            var output = [];
            $.each(response, function (index) {
                output.push('<option value="' + response[index].CustomerID + '">' + response[index].Login + '</option>');
            });
            $('#customers_list').empty();
            $('#customers_list').html(output.join(''));
            if (output.length !== 0) {
                $("#customer_id").attr('value', $("#customers_list").val());
            }
        })
}

function GetAllCustomersEdit() {
    $.get('../api/customer/GetAllCustomers')
        .done(function (response) {
            var output = [];
            $.each(response, function (index) {
                output.push('<option value="' + response[index].CustomerID + '">' + response[index].Login + '</option>');
            });
            $('#customers_list').empty();
            $('#customers_list').html(output.join(''));
            if (output.length !== 0) {
                var customerID = $("#customers_list").val();
                GetCustomerCreditCards(customerID);
            }
        })
}

function GetCustomerCreditCards(customerID) {
    $.get('../api/creditcard/GetCustomerCreditCards?customerID=' + customerID)
        .done(function (response) {
            var output = [];
            $.each(response, function (index) {
                output.push('<option value="' + response[index].CreditCardID + '">' + response[index].CardNumber + '</option>');
            });
            $('#credit_cards_list').empty();
            $('#credit_cards_list').html(output.join(''));
            if (output.length !== 0) {
                GetSelectedCreditCardInfo();
            }
        })
        .fail(function (response) { $("#edit_customer_result").text(response.responseJSON) })
}

function GetSelectedCreditCardInfo() {
    $.get('../api/creditcard/GetCreditCard?creditCardID=' + $("#credit_cards_list").val())
        .done(function (data) {
            $("#credit_card_id").val(data.CreditCardID).change();
            $("#amount").val(data.Amount).change();
            $("#card_number").val(data.CardNumber).change();
            $("#current_pin").val('').change();
            $("#new_pin").val('').change();
        })
        .fail(function (response) { $("#edit_customer_result").text(response.responseJSON) })
}

function SetCardsAddEvents() {
    $("#add_credit_card").submit(function (e) {
        e.preventDefault();
        AddCreditCard();
    });

    $("#customers_list").change(function (e) {
        $("#customer_id").attr('value', $("#customers_list").val());
    })

    GetAllCustomersAdd();
}


function SetCardsEditEvents() {
    $("#edit_credit_card").submit(function (e) {
        e.preventDefault();
        EditCreditCard();
    });

    $("#customers_list").change(function (e) {
        var customerID = $("#customers_list").val();
        GetCustomerCreditCards(customerID);
    })

    $("#credit_cards_list").change(function (e) {
        GetSelectedCreditCardInfo();
    })

    GetAllCustomersEdit();
}