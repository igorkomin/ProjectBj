import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Settings } from 'src/app/models/settings.model';
import { Game } from 'src/app/models/game.model';
import { SystemLog } from 'src/app/models/system-log.model';
import { GameLog } from 'src/app/models/gamelog.model';
import { Identifier } from 'src/app/models/identifier.model';

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

    newGame(settings: Settings): Observable<Game> {
        let requestUrl = `${apiUrl}/Start`;
        return this.http.post<Game>(requestUrl, settings, requestOptions);
    }

    loadGame(settings: Settings): Observable<Game> {
        let requestUrl = `${apiUrl}/Load`;
        return this.http.post<Game>(requestUrl, settings, requestOptions);
    }

    hit(identifier: Identifier): Observable<Game> {
        let requestUrl = `${apiUrl}/Hit`;
        return this.http.post<Game>(requestUrl, identifier, requestOptions);
    }

    stand(identifier: Identifier): Observable<Game> {
        let requestUrl = `${apiUrl}/Stand`;
        return this.http.post<Game>(requestUrl, identifier, requestOptions);
    }

    double(identifier: Identifier): Observable<Game> {
        let requestUrl = `${apiUrl}/Double`;
        return this.http.post<Game>(requestUrl, identifier, requestOptions);
    }

    surrender(identifier: Identifier): Observable<Game> {
        let requestUrl = `${apiUrl}/Surrender`;
        return this.http.post<Game>(requestUrl, identifier, requestOptions);
    }

    getLogs(identifier: Identifier): Observable<GameLog> {
        let requestUrl = `${apiUrl}/History`;
        return this.http.post<GameLog>(requestUrl, identifier, requestOptions);
    }

    getAllLogs(): Observable<GameLog> {
        let requestUrl = `${apiUrl}/FullHistory`;
        return this.http.get<GameLog>(requestUrl, requestOptions);
    }

    getSystemLogs(): Observable<SystemLog> {
        let requestUrl = `${apiUrl}/../Log/SystemLogs`;
        return this.http.get<SystemLog>(requestUrl, requestOptions);
    }
}
