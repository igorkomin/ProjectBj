$(document).ready(function (event) {
    $("#play-button").click(function (event) {
        event.preventDefault();
        if ($("#player-name").val().trim().length > 0) {
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
    $.cookie("playerName", playerName);
    $("#game-form").submit();
}