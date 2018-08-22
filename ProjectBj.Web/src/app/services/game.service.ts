import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Settings } from '../models/settings.model';
import { Game } from '../models/game.model';

const apiUrl = '../api/main/';
const requestOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json'
    })
};

@Injectable({
    providedIn: 'root'
})

export class GameService {
    constructor(private http: HttpClient) { }

    getGameViewModel(settings: Settings): Observable<Game> {
        let requestUrl = apiUrl + 'Game';
        return this.http.post<Game>(requestUrl, settings, requestOptions);
    }

    hit(playerId: number, sessionId: number) {
        let requestUrl = apiUrl + 'Hit';
        return this.http.post<Game>(requestUrl, { playerId: playerId, sessionId: sessionId }, requestOptions);
    }

    stand(playerId: number, sessionId: number) {
        let requestUrl = apiUrl + 'Stand';
        return this.http.post<Game>(requestUrl, { playerId: playerId, sessionId: sessionId }, requestOptions);
    }
}