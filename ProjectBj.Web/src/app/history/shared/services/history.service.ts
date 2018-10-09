import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GetFullHistoryHistoryView } from 'src/app/history/shared/models/get-full-history-history-view.model';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class HistoryService {
    constructor(private readonly http: HttpClient) { }

    getFullHistory(): Observable<GetFullHistoryHistoryView> {
        const requestUrl = `${environment.historyApiUrl}/GetFullHistory`;
        return this.http.get<GetFullHistoryHistoryView>(requestUrl);
    }
}