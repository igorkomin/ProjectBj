import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Game } from 'src/app/shared/models/game.model';
import { History } from 'src/app/shared/models/history.model';
import { GameRequest } from 'src/app/shared/models/game-request.model';
import { NewGameRequest } from 'src/app/shared/models/new-game-request.model';
import { ApiService } from 'src/app/shared/api.service';

@Component({
    selector: 'app-game',
    templateUrl: 'game.component.html',
    styleUrls: [
        'game.component.css',
        '../shared/style/bootstrap.css',
        '../shared/style/slider.css',
        '../shared/style/cards.css'
    ]
})
export class GameComponent implements OnInit {
    playerName: string;
    botsNumber: number = 0;
    sessionId: number;
    playerId: number;
    error: string;
    game: Game;
    log: History;

    constructor(
        private apiService: ApiService,
        private route: ActivatedRoute,
    ) { }

    ngOnInit() {
        this.route
            .queryParams
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
        let gameSettings = new NewGameRequest();
        gameSettings.playerName = this.playerName;
        gameSettings.botsNumber = this.botsNumber;
        this.apiService.newGame(gameSettings).subscribe(
            response => {
                this.game = response;
                this.sessionId = response.sessionId;
                this.playerId = response.player.id;
                this.getLogs();
            },
            exception => {
                console.error(exception);
                this.error = exception.error.exceptionMessage;
            }
        );
    }

    loadGame(): void {
        this.error = undefined;
        let gameSettings = new NewGameRequest();
        gameSettings.playerName = this.playerName;
        this.apiService.loadGame(gameSettings).subscribe(
            response => {
                this.game = response;
                this.sessionId = response.sessionId;
                this.playerId = response.player.id;
                this.getLogs();
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
        request.playerId = this.playerId;
        request.sessionId = this.sessionId;
        this.apiService.hit(request).subscribe(
            response => {
                this.game = response;
                this.getLogs();
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
        request.playerId = this.playerId;
        request.sessionId = this.sessionId;
        this.apiService.stand(request).subscribe(
            response => {
                this.game = response;
                this.getLogs();
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
        request.playerId = this.playerId;
        request.sessionId = this.sessionId;
        this.apiService.double(request).subscribe(
            response => {
                this.game = response;
                this.getLogs();
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
        request.playerId = this.playerId;
        request.sessionId = this.sessionId;
        this.apiService.surrender(request).subscribe(
            response => {
                this.game = response;
                this.getLogs();
            },
            exception => {
                console.log(exception);
                this.error = exception.error.exceptionMessage;
            }
        );
    }

    getLogs(): void {
        this.error = undefined;
        this.apiService.getHistory(this.sessionId).subscribe(
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
