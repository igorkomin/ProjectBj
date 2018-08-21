import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from '../../../node_modules/rxjs';

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

    constructor(
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
}
