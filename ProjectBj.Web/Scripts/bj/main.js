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
    var name = gameData.Dealer.Name;
    var hand = gameData.Dealer.Hand.Cards;
    var score = gameData.Dealer.Hand.Score;
    var inGame = gameData.Dealer.inGame;
    $("#name-d").text(name);
    $("#score-d").text(score);
    $.each(hand, function (index, card) {
        $("#hand-d").add("<img src='" + card.ImageUrl + "' />");
    });

    name = gameData.Player.Name;
    inGame = gameData.Player.InGame;
    hand = gameData.Player.Hand;
    score = gameData.Player.Hand.Score;
    var isHuman = gameData.Player.IsHunan;
    var ballance = gameData.Player.Ballance;
    var gameResult = gameData.Player.GameResult;
    var bet = gameData.Player.Bet;
    $("#name-p").text(name);
    $("#score-p").text(score);
    $.each(hand, function (index, card) {
        $("#hand-p").add("<img src='" + card.ImageUrl + "' />");
    });

    var bots = gameData.Bots;
    $.each(bots, function (botIndex, bot) {
        var name = bot.Name + botIndex;
        var isHuman = bot.IsHuman;
        var ballance = bot.Ballance;
        var inGame = bot.InGame;
        var hand = bot.Hand;
        var score = bot.Hand.Score;
        var gameResult = bot.GameResult;
        var bet = bot.Bet;
        $("#name-" + botIndex).text(name);
        $("#score-" + botIndex).text(score);
        $.each(hand, function (cardIndex, card) {
            $("#hand-" + botIndex).add("<img src='" + card.ImageUrl + "' />");
        });
    });
}