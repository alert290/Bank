function GetAllCustomersTran() {
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
                GetCustomerCreditCardsTran(customerID);
            }
        })
}

function GetCustomerCreditCardsTran(customerID) {
    $.get('../api/creditcard/GetCustomerCreditCards?customerID=' + customerID)
        .done(function (response) {
            var output = [];
            $.each(response, function (index) {
                output.push('<option value="' + response[index].CreditCardID + '">' + response[index].CardNumber + '</option>');
            });
            $('#credit_cards_list').empty();
            $('#credit_cards_list').html(output.join(''));
            SetSearchTransCardID();
        })
        .fail(function (response) { $("#transactions_result").text(response.responseJSON) })
}

function GetTransactionsListByCard() {
    $.post('../api/transaction/GetTransactionsByCard', $("#search_cards_transactions").serialize())
        .done(function (response) {
            $('#cards_transactions').find("tr:gt(0)").remove();
            $.each(response, function (index) {
                var $tr = $('<tr/>');
                $tr.append($('<td/>').html(response[index].Date));
                $tr.append($('<td/>').html(response[index].FromCreditCardNumber));
                $tr.append($('<td/>').html(response[index].ToCreditCardNumber));
                $tr.append($('<td/>').html(response[index].Amount));
                $tr.append($('<td/>').html(response[index].Comment));
                $('.cards-transactions tr:last').after($tr);
            });
        })
        .fail(function (response) { $("#transactions_result").text(response.responseJSON) })
}

function GenerateCardRandomTransactions(toCardID) {
    $.post('../api/transaction/GenerateCardRandomTransactions?toCardID=' + toCardID)
        .done(function (response) { $("#transactions_result").text(response) })
        .fail(function (response) { $("#transactions_result").text(response.responseJSON) })
}


//Can be called via browser console
function PerformTransaction(comment, amount, fromCreditCardID, toCreditCardID) {
    $.post('../api/Transaction/PerformTransaction', { Comment: comment, Amount: amount, FromCreditCardID: fromCreditCardID, ToCreditCardID: toCreditCardID })
        .done(function (response) { alert(response) })
        .fail(function (response) { alert(response.responseJSON) })
}

function SetGenTransEvents() {
    $("#generate_transactions").submit(function (e) {
        e.preventDefault();
        var toCardID = $("#credit_cards_list").val();
        GenerateCardRandomTransactions(toCardID);
    });

    $("#customers_list").change(function (e) {
        var customerID = $("#customers_list").val();
        GetCustomerCreditCardsTran(customerID);
    })

    GetAllCustomersTran();
}

function SetSearchTransCardID() {
    if ($("#credit_card_id").length > 0) {
        var cardID = $("#credit_cards_list").val();
        $("#credit_card_id").val(cardID).change();
    }
}

function SetSearchTransEvents() {
    $("#search_cards_transactions").submit(function (e) {
        e.preventDefault();
        GetTransactionsListByCard();
    });

    $("#customers_list").change(function (e) {
        var customerID = $("#customers_list").val();
        GetCustomerCreditCardsTran(customerID);
    })

    $("#credit_cards_list").change(function (e) {
        SetSearchTransCardID();
    })

    GetAllCustomersTran();
}

