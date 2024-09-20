﻿$(document).ready(function () {

    $("#btnSave").click(function (event) {
        event.preventDefault();

        var addUrl = app.Urls.categoryAddUrl;
        var redirectUrl = app.Urls.articleAddUrl;

        var salerAddDto = {
            Name: $("input[id=salerName]").val(),
            Address: $("input[id=salerAddress]").val(),
            PhoneNumber: $("input[id=salerPhone]").val(),
            TaxNo: $("input[id=salerTaxNo]").val(),
        }

        var jsonData = JSON.stringify(salerAddDto);
        console.log(jsonData);

        $.ajax({
            url: addUrl,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: jsonData,
            success: function (data) {
                setTimeout(function () {
                    window.location.href = redirectUrl;
                }, 1500);
            },
            error: function () {
                toast.error("Bir hata oluştu.", "Hata");
            }
        });
    });
});