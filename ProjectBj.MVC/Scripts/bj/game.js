var playerId = 0;
var sessionId = 0;
$(document).ready(function () {
    var playerName = $.cookie("playerName");

    $("#inc-bots-btn").click(function () {
        var number = $("#bots-number").val();
        if (number == 5) {
            return;
        }
        number++;
        $("#bots-number").val(number);
    });

    $("#dec-bots-btn").click(function () {
        var number = $("#bots-number").val();
        if (number == 0) {
            return;
        }
        number--;
        $("#bots-number").val(number);
    });

    $("#bet-slider").change(function () {
        var number = $("#bet-slider").val();
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
    $("#player-name").html(game.player.name + " <span class='badge badge-secondary'>"
        + game.player.hand.score + "</span>");

    if (game.player.gameResult > 0) {
        $("#player-result-message").text(game.player.gameResultMessage);
    }

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