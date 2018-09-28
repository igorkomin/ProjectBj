import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GameRequest } from '../shared/models/game-request.model';
import { Game } from '../shared/models/game.model';
import { History } from '../shared/models/history.model';
import { NewGameRequest } from '../shared/models/new-game-request.model';

const apiUrl = '../api/game';

@Injectable({
    providedIn: 'root'
})
export class GameService {
    constructor(private http: HttpClient) { }

    newGame(request: NewGameRequest): Observable<Game> {
        let requestUrl = `${apiUrl}/Start`;
        return this.http.post<Game>(requestUrl, request);
    }

    loadGame(request: NewGameRequest): Observable<Game> {
        let requestUrl = `${apiUrl}/Load`;
        return this.http.post<Game>(requestUrl, request);
    }

    hit(request: GameRequest): Observable<Game> {
        let requestUrl = `${apiUrl}/Hit`;
        return this.http.post<Game>(requestUrl, request);
    }

    stand(request: GameRequest): Observable<Game> {
        let requestUrl = `${apiUrl}/Stand`;
        return this.http.post<Game>(requestUrl, request);
    }

    double(request: GameRequest): Observable<Game> {
        let requestUrl = `${apiUrl}/Double`;
        return this.http.post<Game>(requestUrl, request);
    }

    surrender(request: GameRequest): Observable<Game> {
        let requestUrl = `${apiUrl}/Surrender`;
        return this.http.post<Game>(requestUrl, request);
    }

    getHistory(sessionId: number): Observable<History> {
        let requestUrl = `${apiUrl}/../History/GetGameHistory`;
        return this.http.post<History>(requestUrl, sessionId);
    }
}