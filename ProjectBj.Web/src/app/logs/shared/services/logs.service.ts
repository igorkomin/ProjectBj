import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GetFullLogView } from 'src/app/logs/shared/models/get-full-log-view.model';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class LogsService {
    constructor(private readonly http: HttpClient) { }

    getSystemLogs(): Observable<GetFullLogView> {
        const requestUrl = `${environment.logApiUrl}/GetFullLog`;
        return this.http.get<GetFullLogView>(requestUrl);
    }
}