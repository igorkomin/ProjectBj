import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { History } from 'src/app/shared/models/history.model';

const apiUrl = '../api/history';

@Injectable({
    providedIn: 'root'
})
export class HistoryService {
    constructor(private http: HttpClient) { }

    getFullHistory(): Observable<History> {
        let requestUrl = `${apiUrl}/GetFullHistory`;
        return this.http.get<History>(requestUrl);
    }
}