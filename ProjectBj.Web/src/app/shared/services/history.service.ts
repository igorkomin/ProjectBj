import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GetFullHistoryHistoryView } from 'src/app/shared/models/get-full-history-history-view.model';
import { GetGameHistoryHistoryView } from 'src/app/shared/models/get-game-history-history-view.model';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class HistoryService {
    constructor(private readonly http: HttpClient) { }

    getHistory(sessionId: number): Observable<GetGameHistoryHistoryView> {
        const requestUrl = `${environment.historyApiUrl}GetGameHistory`;
        return this.http.post<GetGameHistoryHistoryView>(requestUrl, sessionId);
    }

    getFullHistory(): Observable<GetFullHistoryHistoryView> {
        const requestUrl = `${environment.historyApiUrl}GetFullHistory`;
        return this.http.get<GetFullHistoryHistoryView>(requestUrl);
    }
}