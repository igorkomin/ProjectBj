import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { History } from 'src/app/shared/models/history.model';
import { environment } from 'src/environments/environment'

@Injectable({
    providedIn: 'root'
})
export class HistoryService {
    constructor(private http: HttpClient) { }

    getFullHistory(): Observable<History> {
        let requestUrl = `${environment.historyApiUrl}/GetFullHistory`;
        return this.http.get<History>(requestUrl);
    }
}