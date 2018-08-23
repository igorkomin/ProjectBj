import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from '../../../node_modules/rxjs';

import { GameService } from '../services/game.service';
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
    botsNumber: number;
    sliderValue: number = 50;
    sessionId: number;
    playerId: number;
    game: any;

    constructor(
        private gameService: GameService,
        private route: ActivatedRoute,
        private router: Router,
        private http: HttpClient
    ) { }

    ngOnInit() {
        this.route
            .queryParams
            .subscribe(params => {
                this.playerName = params['name'];
                this.botsNumber = params['bots'];
            });
    }

    updateSliderValue(value: number): void {
        this.sliderValue = value;
    }
    getGame(): void {
        let gameSettings = new Settings();
        gameSettings.playerName = this.playerName;
        gameSettings.botsNumber = this.botsNumber;
        this.gameService.getGameViewModel(gameSettings).subscribe(
            response => {
                this.game = response;
                this.sessionId = response.sessionId;
                this.playerId = response.player.id;
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
        this.gameService.hit(identifier).subscribe(
            response => {
                this.game = response;
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
        this.gameService.stand(identifier).subscribe(
            response => {
                this.game = response;
            },
            exception => {
                console.error(exception.error.exceptionMessage);
            }
        )
    }
}
