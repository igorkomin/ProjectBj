import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameService } from 'src/app/game/game.service';
import { RequestGameView } from 'src/app/shared/models/request-game-view.model';
import { HistoryView } from 'src/app/shared/models/history-view.model';
import { RequestNewGameView } from 'src/app/shared/models/request-new-game-view.model';
import { ResponseDoubleGameView } from 'src/app/shared/models/response-double-game-view.model';
import { ResponseHitGameView } from 'src/app/shared/models/response-hit-game-view.model';
import { ResponseLoadGameView } from 'src/app/shared/models/response-load-game-view.model';
import { ResponseStandGameView } from 'src/app/shared/models/response-stand-game-view.model';
import { ResponseStartGameView } from 'src/app/shared/models/response-start-game-view.model';
import { ResponseSurrenderGameView } from 'src/app/shared/models/response-surrender-game-view.model';

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
    history: HistoryView;

    constructor(
        private readonly gameService: GameService,
        private readonly route: ActivatedRoute,
    ) { }

    ngOnInit() {
        this.route
            .params
            .subscribe(params => {
                this.playerName = params['name'];
            });
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
        this.gameService.getHistory(this.game.sessionId).subscribe(
            response => {
                this.history = response;
            }
        );
    }
}
