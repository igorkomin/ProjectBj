import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { History } from 'src/app/shared/models/history.model';

const apiUrl = '../api/history';
const requestOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json'
    })
};

@Injectable({
    providedIn: 'root'
})

export class HistoryService {
    constructor(private http: HttpClient) { }

    getHistory(sessionId: number): Observable<History> {
        let requestUrl = `${apiUrl}/GetGameHistory`;
        return this.http.post<History>(requestUrl, sessionId, requestOptions);
    }

    getFullHistory(): Observable<History> {
        let requestUrl = `${apiUrl}/GetFullHistory`;
        return this.http.get<History>(requestUrl, requestOptions);
    }
}