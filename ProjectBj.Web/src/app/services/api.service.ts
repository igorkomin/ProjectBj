import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Settings } from '../models/settings.model';
import { Game } from '../models/game.model';
import { Log } from '../models/log.model';
import { Identifier } from '../models/identifier.model';

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

    getGame(settings: Settings): Observable<Game> {
        let requestUrl = apiUrl + 'Start';
        return this.http.post<Game>(requestUrl, settings, requestOptions);
    }

    hit(identifier: Identifier): Observable<Game> {
        let requestUrl = apiUrl + 'Hit';
        return this.http.post<Game>(requestUrl, identifier, requestOptions);
    }

    stand(identifier: Identifier): Observable<Game> {
        let requestUrl = apiUrl + 'Stand';
        return this.http.post<Game>(requestUrl, identifier, requestOptions);
    }

    getLogs(identifier: Identifier): Observable<Log> {
        let requestUrl = apiUrl + 'Logs';
        return this.http.post<Log>(requestUrl, identifier, requestOptions);
    }
}
