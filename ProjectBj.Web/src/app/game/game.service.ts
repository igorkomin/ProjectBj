import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GameRequest } from '../shared/models/game-request.model';
import { Game } from '../shared/models/game.model';
import { NewGameRequest } from '../shared/models/new-game-request.model';

const apiUrl = '../api/game';
const requestOptions = {
    headers: new HttpHeaders({
        'Content-Type' : 'application/json'
    })
}

@Injectable({
    providedIn: 'root'
})

export class GameService {
    constructor(private http: HttpClient) { }

    newGame(request: NewGameRequest): Observable<Game> {
        let requestUrl = `${apiUrl}/Start`;
        return this.http.post<Game>(requestUrl, request, requestOptions);
    }

    loadGame(request: NewGameRequest): Observable<Game> {
        let requestUrl = `${apiUrl}/Load`;
        return this.http.post<Game>(requestUrl, request, requestOptions);
    }

    hit(request: GameRequest): Observable<Game> {
        let requestUrl = `${apiUrl}/Hit`;
        return this.http.post<Game>(requestUrl, request, requestOptions);
    }

    stand(request: GameRequest): Observable<Game> {
        let requestUrl = `${apiUrl}/Stand`;
        return this.http.post<Game>(requestUrl, request, requestOptions);
    }

    double(request: GameRequest): Observable<Game> {
        let requestUrl = `${apiUrl}/Double`;
        return this.http.post<Game>(requestUrl, request, requestOptions);
    }

    surrender(request: GameRequest): Observable<Game> {
        let requestUrl = `${apiUrl}/Surrender`;
        return this.http.post<Game>(requestUrl, request, requestOptions);
    }
}