import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameService } from 'src/app/game/game.service';
import { GameRequest } from 'src/app/shared/models/game-request.model';
import { HistoryView } from 'src/app/shared/models/history-view.model';
import { NewGameRequest } from 'src/app/shared/models/new-game-request.model';
import { ResponseGameView } from 'src/app/shared/models/response-game-view.model';

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
    game: ResponseGameView;
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
        const request = new NewGameRequest();
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
        const request = new NewGameRequest();
        request.playerName = this.playerName;
        this.gameService.loadGame(request).subscribe(
            response => {
                this.game = response;
                this.getHistory();
            }
        );
    }

    hit(): void {
        const request = new GameRequest();
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
        const request = new GameRequest();
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
        const request = new GameRequest();
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
        const request = new GameRequest();
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
