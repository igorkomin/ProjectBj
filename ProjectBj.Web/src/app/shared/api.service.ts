import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NewGameRequest } from 'src/app/shared/models/new-game-request.model';
import { GameRequest } from 'src/app/shared/models/game-request.model';
import { Game } from 'src/app/shared/models/game.model';
import { SystemLog } from 'src/app/shared/models/system-log.model';
import { History } from 'src/app/shared/models/history.model';

const apiUrl = '../api/game';
const requestOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json'
    })
};

@Injectable({
    providedIn: 'root'
})

export class ApiService {
    constructor(private http: HttpClient) { }

    newGame(settings: NewGameRequest): Observable<Game> {
        let requestUrl = `${apiUrl}/Start`;
        return this.http.post<Game>(requestUrl, settings, requestOptions);
    }

    loadGame(settings: NewGameRequest): Observable<Game> {
        let requestUrl = `${apiUrl}/Load`;
        return this.http.post<Game>(requestUrl, settings, requestOptions);
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

    getHistory(sessionId: number): Observable<History> {
        let requestUrl = `${apiUrl}/../History/GetGameHistory`;
        return this.http.post<History>(requestUrl, sessionId, requestOptions);
    }

    getFullHistory(): Observable<History> {
        let requestUrl = `${apiUrl}/../History/GetFullHistory`;
        return this.http.get<History>(requestUrl, requestOptions);
    }

    getSystemLogs(): Observable<SystemLog> {
        let requestUrl = `${apiUrl}/../Log/GetFull`;
        return this.http.get<SystemLog>(requestUrl, requestOptions);
    }
}
