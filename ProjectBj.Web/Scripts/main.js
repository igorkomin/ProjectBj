$(document).ready(function (event) {
    $("#play-button").click(function (event) {
        if ($("#player-name").val.length > 0) {
            Start();
        }
        ShowError();
    });
});

function ShowError() {
    $("#player-name").sha
}

function Start() {
    var playerName = $("#player-name").val();
    var botsNumber = $("#bots-number").text();

    $.ajax({
        url: "/api/main/start",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ PlayerName: playerName, BotsNumber: botsNumber }),
        success: function () {
            console.log("OK");
        },
        error: function (response) {
            console.log(response.responseText);
        }
    });
}