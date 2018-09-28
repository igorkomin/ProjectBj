import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Game } from 'src/app/shared/models/game.model';
import { History } from 'src/app/shared/models/history.model';
import { GameRequest } from 'src/app/shared/models/game-request.model';
import { NewGameRequest } from 'src/app/shared/models/new-game-request.model';
import { GameService } from 'src/app/game/game.service';
import { HistoryService } from 'src/app/history/history.service';

@Component({
    selector: 'app-game',
    templateUrl: 'game.component.html',
    styleUrls: [
        'game.component.css',
    ]
})
export class GameComponent implements OnInit {
    playerName: string;
    botsNumber: number = 0;
    error: string;
    game: Game;
    log: History;

    constructor(
        private gameService: GameService,
        private historyService: HistoryService,
        private route: ActivatedRoute,
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
        this.error = undefined;
        let request = new NewGameRequest();
        request.playerName = this.playerName;
        request.botsNumber = this.botsNumber;
        this.gameService.newGame(request).subscribe(
            response => {
                this.game = response;
                this.getHistory();
            },
            exception => {
                console.error(exception);
                this.error = exception.error.exceptionMessage;
            }
        );
    }

    loadGame(): void {
        this.error = undefined;
        let request = new NewGameRequest();
        request.playerName = this.playerName;
        this.gameService.loadGame(request).subscribe(
            response => {
                this.game = response;
                this.getHistory();
            },
            exception => {
                console.error(exception);
                this.error = exception.error.exceptionMessage;
            }
        );
    }

    hit(): void {
        this.error = undefined;
        let request = new GameRequest();
        request.playerId = this.game.player.id;
        request.sessionId = this.game.sessionId;
        this.gameService.hit(request).subscribe(
            response => {
                this.game = response;
                this.getHistory();
            },
            exception => {
                console.error(exception);
                this.error = exception.error.exceptionMessage;
            }
        );
    }

    stand(): void {
        this.error = undefined;
        let request = new GameRequest();
        request.playerId = this.game.player.id;
        request.sessionId = this.game.sessionId;
        this.gameService.stand(request).subscribe(
            response => {
                this.game = response;
                this.getHistory();
            },
            exception => {
                console.error(exception);
                this.error = exception.error.exceptionMessage;
            }
        );
    }

    doubleDown(): void {
        this.error = undefined;
        let request = new GameRequest();
        request.playerId = this.game.player.id;
        request.sessionId = this.game.sessionId;
        this.gameService.double(request).subscribe(
            response => {
                this.game = response;
                this.getHistory();
            },
            exception => {
                console.error(exception);
                this.error = exception.error.exceptionMessage;
            }
        );
    }

    surrender(): void {
        this.error = undefined;
        let request = new GameRequest();
        request.playerId = this.game.player.id;
        request.sessionId = this.game.sessionId;
        this.gameService.surrender(request).subscribe(
            response => {
                this.game = response;
                this.getHistory();
            },
            exception => {
                console.log(exception);
                this.error = exception.error.exceptionMessage;
            }
        );
    }

    getHistory(): void {
        this.error = undefined;
        this.historyService.getHistory(this.game.sessionId).subscribe(
            response => {
                this.log = response;
            },
            exception => {
                console.error(exception);
                this.error = exception.error.exceptionMessage;
            }
        );
    }
}
