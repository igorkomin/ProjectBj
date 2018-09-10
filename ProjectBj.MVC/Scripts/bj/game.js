var playerId = 0;
var sessionId = 0;
$(document).ready(function () {
    $("#player-seat").hide();
    var playerName = $.cookie("playerName");

    $("#inc-bots-btn").click(function () {
        var number = $("#bots-number").val();
        if (number == 5) {
            enableBotDecrease();
            return;
        }
        number++;
        $("#bots-number").val(number);
    });

    $("#dec-bots-btn").click(function () {
        var number = $("#bots-number").val();
        if (number == 0) {
            enableBotIncrease();
            return;
        }
        number--;
        $("#bots-number").val(number);
    });

    $("#bet-slider").on("input change", function () {
        var number = $(this).val();
        $("#bet-output").text(number);
    });

    $("#new-game-btn").click(function () {
        var botsNumber = $("#bots-number").val();
        var playerBet = $("#bet-slider").val();
        newGame(playerName, botsNumber, playerBet);
    });

    $("#hit-btn").click(function () {
        hit();
    });

    $("#stand-btn").click(function () {
        stand();
    });

    $("#double-btn").click(function () {
        double();
    });

    $("#surrender-btn").click(function () {
        surrender();
    });
});

function newGame(playerName, botsNumber, playerBet) {
    $.ajax({
        url: "/api/gameapi/start",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ PlayerName: playerName, BotsNumber: botsNumber, Bet: playerBet }),
        success: function (response) {
            showData(response);
            playerId = response.player.id;
            sessionId = response.sessionId;
        },
        error: function (exception) {
            console.error(exception);
        }
    });
}

function loadGame(playerName) {
    $.ajax({
        url: "/api/gameapi/load",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ PlayerName: playerName }),
        success: function (response) {
            showData(response);
            playerId = response.player.id;
            sessionId = response.sessionId;
        },
        error: function (exception) {
            console.error(exception);
        }
    });
}

function hit() {
    $.ajax({
        url: "/api/gameapi/hit",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ PlayerId: playerId, SessionId: sessionId }),
        success: function (response) {
            showData(response);
        },
        error: function (exception) {
            console.error(exception);
        }
    });
}

function stand() {
    $.ajax({
        url: "/api/gameapi/stand",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ PlayerId: playerId, SessionId: sessionId }),
        success: function (response) {
            showData(response);
        },
        error: function (exception) {
            console.error(exception);
        }
    });
}

function double() {
    $.ajax({
        url: "/api/gameapi/double",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ PlayerId: playerId, SessionId: sessionId }),
        success: function (response) {
            showData(response);
        },
        error: function (exception) {
            console.error(exception);
        }
    });
}

function surrender() {
    $.ajax({
        url: "/api/gameapi/surrender",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ PlayerId: playerId, SessionId: sessionId }),
        success: function (response) {
            showData(response);
        },
        error: function (exception) {
            console.error(exception);
        }
    });
}

function showData(game) {
    checkGameStatus(game);
    showDealerData(game);
    showPlayerData(game);
    showBotsData(game);
}

function showDealerData(game) {
    $("#dealer-name").html(game.dealer.name + " <span class='badge badge-secondary'>"
        + game.dealer.hand.score + "</span>");
    var cards = "";
    $.each(game.dealer.hand.cards, function (index, card) {
        cards += "<li><span class='card rank-" + card.rank + " " + card.suit + "'><span class='rank'>"
            + card.rank + "</span><span class='suit'></span></span></li>";
    });
    $("#dealer-hand").html("<ul class='hand'>" + cards + "</ul>");
}

function showPlayerData(game) {
    $("#player-seat").show();
    $("#player-name").html(game.player.name + " <span class='badge badge-secondary'>"
        + game.player.hand.score + "</span>");

    var resultMessage = "";
    if (game.player.gameResult > 0) {
        resultMessage = game.player.gameResultMessage;
    }
    $("#player-result-message").text(resultMessage);

    var cards = "";
    $.each(game.player.hand.cards, function (index, card) {
        cards += "<li><span class='card rank-" + card.rank + " " + card.suit + "'><span class='rank'>"
            + card.rank + "</span><span class='suit'></span></span></li>";
    });
    $("#player-hand").html("<ul class='hand'>" + cards + "</ul>");
}

function showBotsData(game) {
    var innerHtml="";
    $.each(game.bots, function (index, bot) {
        innerHtml += "<div class='bot-seat col-md-1'>";
        if (bot.gameResult > 0) {
            innerHtml += "<span>" + bot.gameResultMessage + "</span>";
        }
        innerHtml += "<p class='caption player-name'>" + bot.name + " <span class='badge badge-secondary'>"
            + bot.hand.score + "</span></p>";
        innerHtml += "<div class='playingCards simpleCards rotateHand'>";
        innerHtml += "<ul class='hand'>";
        $.each(bot.hand.cards, function (index, card) {
            innerHtml += "<li><span class='card rank-" + card.rank + " " + card.suit + "'><span class='rank'>"
                + card.rank + "</span><span class='suit'></span></span></li>";
        });
        innerHtml += "</ul></div></div>";
    });
    $("#bot-seats").html(innerHtml);
}

function checkGameStatus(game) {
    switchToMainMenu();
    if (game && game.player.gameResult == 0) {
        switchToGameControls();
    }
}

function enableBotIncrease() {
    $("#inc-bots-btn").attr("disabled", false);
    $("#dec-bots-btn").attr("disabled", true);
}

function enableBotDecrease() {
    $("#dec-bots-btn").attr("disabled", false);
    $("#inc-bots-btn").attr("disabled", true);
}

function switchToGameControls() {
    $("#bet-slider").attr("disabled", true);
    $("#new-game-btn").attr("disabled", true);
    $("#load-game-btn").attr("disabled", true);

    $("#actions-block").attr("disabled", false);
    $("#hit-btn").attr("disabled", false);
    $("#double-btn").attr("disabled", false);
    $("#stand-btn").attr("disabled", false);
    $("#surrender-btn").attr("disabled", false);
}

function switchToMainMenu() {
    $("#bet-slider").attr("disabled", false);
    $("#new-game-btn").attr("disabled", false);
    $("#load-game-btn").attr("disabled", false);

    $("#hit-btn").attr("disabled", true);
    $("#double-btn").attr("disabled", true);
    $("#stand-btn").attr("disabled", true);
    $("#surrender-btn").attr("disabled", true);
}