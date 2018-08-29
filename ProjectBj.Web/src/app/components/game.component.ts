import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from '../../../node_modules/rxjs';

import { ApiService } from '../services/api.service';
import { Settings } from '../models/settings.model';
import { Identifier } from '../models/identifier.model';

@Component({
    selector: 'app-game',
    templateUrl: './../views/game.view.html',
    styleUrls: [
        './../styles/game.style.css',
        './../styles/common/bootstrap.css',
        './../styles/common/slider.css',
        './../styles/common/cards.css'
    ]
})
export class GameComponent implements OnInit {
    playerName: string;
    botsNumber: number = 0;
    sliderValue: number = 50;
    sessionId: number;
    playerId: number;
    game: any;
    log: any;

    constructor(
        private apiService: ApiService,
        private route: ActivatedRoute,
        private router: Router,
        private http: HttpClient
    ) { }

    ngOnInit() {
        this.route
            .queryParams
            .subscribe(params => {
                this.playerName = params['name'];
            });
    }

    updateSliderValue(value: number): void {
        this.sliderValue = value;
    }

    IncBotsNumber(): void {
        this.botsNumber++;
    }

    DecBotsNumber(): void {
        this.botsNumber--;
    }

    getGame(): void {
        let gameSettings = new Settings();
        gameSettings.playerName = this.playerName;
        gameSettings.botsNumber = this.botsNumber;
        this.apiService.getGame(gameSettings).subscribe(
            response => {
                this.game = response;
                this.sessionId = response.sessionId;
                this.playerId = response.player.id;
                this.getLogs();
            },
            exception => {
                console.error(exception.error.exceptionMessage);
            }
        );
    }

    getLoadedGame(): void {
        let gameSettings = new Settings();
        gameSettings.playerName = this.playerName;
        this.apiService.getLoadedGame(gameSettings).subscribe(
            response => {
                this.game = response;
                this.sessionId = response.sessionId;
                this.playerId = response.player.id;
                this.getLogs();
            },
            exception => {
                console.error(exception.error.exceptionMessage);
            }
        );
    }

    hit(): void {
        let identifier = new Identifier();
        identifier.playerId = this.playerId;
        identifier.sessionId = this.sessionId;
        this.apiService.hit(identifier).subscribe(
            response => {
                this.game = response;
                this.getLogs();
            },
            exception => {
                console.error(exception.error.exceptionMessage);
            }
        );
    }

    stand(): void {
        let identifier = new Identifier();
        identifier.playerId = this.playerId;
        identifier.sessionId = this.sessionId;
        this.apiService.stand(identifier).subscribe(
            response => {
                this.game = response;
                this.getLogs();
            },
            exception => {
                console.error(exception.error.exceptionMessage);
            }
        );
    }

    getLogs(): void {
        let identifier = new Identifier();
        identifier.sessionId = this.sessionId;
        this.apiService.getLogs(identifier).subscribe(
            response => {
                this.log = response;
            },
            exception => {
                console.error(exception.error.exceptionMessage);
            }
        );
    }
}
