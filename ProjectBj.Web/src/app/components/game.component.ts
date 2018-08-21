import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from '../../../node_modules/rxjs';

import { GameService } from '../services/game.service';
import { Settings } from '../models/settings.model';

@Component({
    selector: 'app-game',
    templateUrl: './../views/game.view.html',
    styleUrls: [
        './../styles/common/bootstrap.css',
        './../styles/common/slider.css',
        './../styles/common/cards.css'
    ]
})
export class GameComponent implements OnInit {

    gameSettings: Settings;
    sliderValue: number = 50;
    sessionId: number;
    playerId: number;
    game: any;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private http: HttpClient,
        private gameService: GameService
    ) { }

    ngOnInit() {
        this.route
            .queryParams
            .subscribe(params => {
                this.gameSettings.playerName = params['name'];
                this.gameSettings.botsNumber = params['bots'];
            });
    }

    updateSliderValue(value: number): void {
        this.sliderValue = value;
    }
    getGame(): void {
        this.gameService.getGameViewModel(this.gameSettings).subscribe({
            next(game) {
                this.game = game;
            },
            error(exception) {
                console.error(exception.error.exceptionMessage);
            }
        });
    }
}
