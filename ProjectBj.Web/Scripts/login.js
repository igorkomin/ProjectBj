$(document).ready(function (event) {
    $("#play-button").click(function (event) {
        event.preventDefault();
        if ($("#player-name").val().length > 0) {
            login();
        }
        else {
            showError();
        }
    });
});

function showError() {
    $("#player-name-block").effect("pulsate", { times: 2 }, 500);
    $("#player-name").focus();
}

function login() {
    var playerName = $("#player-name").val();
    var botsNumber = $("#bots-number").text();
    $.cookie("playerName", playerName);
    $.cookie("botsNumber", botsNumber);
    $("#game-form").submit();
}