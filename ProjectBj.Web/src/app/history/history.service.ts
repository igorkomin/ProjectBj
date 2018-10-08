import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HistoryList } from 'src/app/shared/models/history-list.model';
import { environment } from 'src/environments/environment'

@Injectable({
    providedIn: 'root'
})
export class HistoryService {
    constructor(private http: HttpClient) { }

    getFullHistory(): Observable<HistoryList> {
        const requestUrl = `${environment.historyApiUrl}/GetFullHistory`;
        return this.http.get<HistoryList>(requestUrl);
    }
}