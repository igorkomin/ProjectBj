import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GetGameHistoryHistoryView } from 'src/app/shared/models/get-game-history-history-view.model';
import { RequestGameView } from 'src/app/shared/models/request-game-view.model';
import { RequestNewGameView } from 'src/app/shared/models/request-new-game-view.model';
import { ResponseDoubleGameView } from 'src/app/shared/models/response-double-game-view.model';
import { ResponseHitGameView } from 'src/app/shared/models/response-hit-game-view.model';
import { ResponseLoadGameView } from 'src/app/shared/models/response-load-game-view.model';
import { ResponseStandGameView } from 'src/app/shared/models/response-stand-game-view.model';
import { ResponseStartGameView } from 'src/app/shared/models/response-start-game-view.model';
import { ResponseSurrenderGameView } from 'src/app/shared/models/response-surrender-game-view.model';
import { GameService } from 'src/app/shared/services/game.service';
import { HistoryService } from 'src/app/shared/services/history.service';
import { CardRank } from 'src/app/shared/enums/card-rank.enum';
import { CardSuit } from 'src/app/shared/enums/card-suit.enum'

@Component({
    selector: 'app-game',
    templateUrl: 'game.component.html',
    styleUrls: [
        'game.component.css'
    ]
})
export class GameComponent implements OnInit {
    playerName: string;
    botsNumber: number = 0;
    game: ResponseDoubleGameView | ResponseHitGameView |
        ResponseLoadGameView | ResponseStandGameView |
        ResponseStartGameView | ResponseSurrenderGameView;
    history: GetGameHistoryHistoryView;
    cardRank = CardRank;
    cardSuit = CardSuit;

    constructor(
        private readonly gameService: GameService,
        private readonly historyService: HistoryService,
        private readonly route: ActivatedRoute,
    ) { }

    ngOnInit() {
        this.route
            .params
            .subscribe(params => {
                this.playerName = params['name'];
            });
    }

    isFaceCard(card: CardRank): boolean {
        if (card > 1 && card < 11) {
            return false;
        }
        return true;
    }

    incBotsNumber(): void {
        this.botsNumber++;
    }

    decBotsNumber(): void {
        this.botsNumber--;
    }

    newGame(): void {
        const request = new RequestNewGameView();
        request.playerName = this.playerName;
        request.botsNumber = this.botsNumber;
        this.gameService.newGame(request).subscribe(
            response => {
                this.game = response;
                this.getHistory();
            }
        );
    }

    loadGame(): void {
        const request = new RequestNewGameView();
        request.playerName = this.playerName;
        this.gameService.loadGame(request).subscribe(
            response => {
                this.game = response;
                this.getHistory();
            }
        );
    }

    hit(): void {
        const request = new RequestGameView();
        request.playerId = this.game.player.id;
        request.sessionId = this.game.sessionId;
        this.gameService.hit(request).subscribe(
            response => {
                this.game = response;
                this.getHistory();
            }
        );
    }

    stand(): void {
        const request = new RequestGameView();
        request.playerId = this.game.player.id;
        request.sessionId = this.game.sessionId;
        this.gameService.stand(request).subscribe(
            response => {
                this.game = response;
                this.getHistory();
            }
        );
    }

    doubleDown(): void {
        const request = new RequestGameView();
        request.playerId = this.game.player.id;
        request.sessionId = this.game.sessionId;
        this.gameService.double(request).subscribe(
            response => {
                this.game = response;
                this.getHistory();
            }
        );
    }

    surrender(): void {
        const request = new RequestGameView();
        request.playerId = this.game.player.id;
        request.sessionId = this.game.sessionId;
        this.gameService.surrender(request).subscribe(
            response => {
                this.game = response;
                this.getHistory();
            }
        );
    }

    getHistory(): void {
        this.historyService.getHistory(this.game.sessionId).subscribe(
            response => {
                this.history = response;
            }
        );
    }
}
