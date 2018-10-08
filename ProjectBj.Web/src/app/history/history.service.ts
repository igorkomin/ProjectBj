import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HistoryView } from 'src/app/shared/models/history-view.model';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class HistoryService {
    constructor(private http: HttpClient) { }

    getFullHistory(): Observable<HistoryView> {
        const requestUrl = `${environment.historyApiUrl}/GetFullHistory`;
        return this.http.get<HistoryView>(requestUrl);
    }
}