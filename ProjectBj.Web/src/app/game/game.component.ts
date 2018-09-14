import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Game } from 'src/app/shared/models/game.model';
import { History } from 'src/app/shared/models/history.model';
import { Identifier } from 'src/app/shared/models/identifier.model';
import { Settings } from 'src/app/shared/models/settings.model';
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
    //playerBet: number = 50;
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

    updateSliderValue(value: number): void {
        //this.playerBet = value;
    }

    incBotsNumber(): void {
        this.botsNumber++;
    }

    decBotsNumber(): void {
        this.botsNumber--;
    }

    newGame(): void {
        this.error = undefined;
        let gameSettings = new Settings();
        gameSettings.playerName = this.playerName;
        gameSettings.botsNumber = this.botsNumber;
        //gameSettings.bet = this.playerBet;
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
        let gameSettings = new Settings();
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
        let identifier = new Identifier();
        identifier.playerId = this.playerId;
        identifier.sessionId = this.sessionId;
        this.apiService.hit(identifier).subscribe(
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
        let identifier = new Identifier();
        identifier.playerId = this.playerId;
        identifier.sessionId = this.sessionId;
        this.apiService.stand(identifier).subscribe(
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
        let identifier = new Identifier();
        identifier.playerId = this.playerId;
        identifier.sessionId = this.sessionId;
        this.apiService.double(identifier).subscribe(
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
        let identifier = new Identifier();
        identifier.playerId = this.playerId;
        identifier.sessionId = this.sessionId;
        this.apiService.surrender(identifier).subscribe(
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
