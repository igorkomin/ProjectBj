import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Observable } from 'rxjs';

import { Log } from '../models/log.model';
import { Identifier } from '../models/identifier.model';

const apiUrl = '../api/game/';
const requestOptions = {
    headers: new HttpHeaders({
        'Content-Type' : 'application/json'
    })
};

@Injectable({
    providedIn: 'root'
})

export class LogService {
    constructor(private http: HttpClient) { }

    getLogs(identifier: Identifier): Observable<Log> {
        let requestUrl = apiUrl + 'Logs';
        return this.http.post<Log>(requestUrl, identifier, requestOptions);
    }
}
