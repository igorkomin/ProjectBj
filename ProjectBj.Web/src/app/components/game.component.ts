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
        './../styles/common/slider.css'
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
        let data = this.postData().subscribe({
            next(response) {
                console.log(response.playerName);
                console.log(response.botsNumber);
            },
            error(exception) {
                console.error(exception.error.exceptionMessage);
            }
        });
        /*let array = [0, 1, 2];
        array.forEach((item, index) => {
            document.getElementById("name-" + index).innerText = index.toString();
        });*/
    }

    updateSliderValue(value: number): void {
        this.sliderValue = value;
    }

    postData(): Observable<any> {
        let url = "/api/main/debug";
        let options = {
            headers: new HttpHeaders({ 'Content-Type' : 'application/json' })
        };
        return this.http.post(url, {
            PlayerName: this.playerName,
            BotsNumber: this.botsNumber
        }, options);
    }
}
