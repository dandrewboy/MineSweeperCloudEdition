// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    console.log("Page is ready.");

    $(document).on("click", ".game-button", function (event) {
        document.on("click", ".game-button").disabled = true;
        event.preventDefault();

        var index = $(this).val();
        console.log("Button number " + index + " was clicked");
        doButtonUpdate(index, "/Board/HandleButtonClick");
    });
});

$(document).ready(function () {

    $("table").contextmenu = function () { return false; };
    $(document).on("mousedown", ".game-button", function (e) {
        if (e.button == 2) {
            var index = $(this).val();
            console.log("Right Mouse Click: " + index);
            doOneButtonUpdate(index, "/Board/OneButton");
        }
    });
});

function doButtonUpdate(index, urlString)
{
    $.ajax(
        {
            datatype: 'json',
            method: 'POST',
            url: urlString,
            data:
            {
                "index": index
            },
            success: function (data)
            {
                console.log("success");
                console.log(data);
                $("body").html(data);
            },
            error: function (jqXHR, textStatus)
            {
                alert("Failed");
            }

        });

};

function doOneButtonUpdate(index, urlString)
{
    $.ajax(
        {
            datatype: 'json',
            method: 'POST',
            url: urlString,
            data:
            {
                "index": index
            },
            success: function (data)
            {
                console.log("success");
                //console.log(data);
                $("#" + index).html(data);
            },
            error: function (jqXHR, textStatus)
            {
                alert("Failed");
            }

        });

};
