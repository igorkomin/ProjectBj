$(document).ready(function (event) {
    var playerName = $.cookie("playerName");
    var botsNumber = $.cookie("botsNumber");

    if (playerName === undefined || botsNumber === undefined) {
        $('#error-modal').modal({
            backdrop: "static",
            keyboard: false
        });
    }
    else {
        getGameData(playerName, botsNumber);
    }
});

function getGameData(playerName, botsNumber) {
    $.ajax({
        url: "/api/main/game",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ PlayerName: playerName, BotsNumber: botsNumber }),
        success: function (response) {
            showData(response);
        },
        error: function (response) {
            console.error(response.responseText);
        }
    });
}

function showData(gameData) {
    // dealer
    var id = gameData.Dealer.Id;
    var name = gameData.Dealer.Name;
    var inGame = gameData.Dealer.InGame;
    var hand = gameData.Dealer.Hand;

    
}