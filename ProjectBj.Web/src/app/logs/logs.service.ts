import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LogView } from 'src/app/shared/models/log-view.model';
import { environment } from 'src/environments/environment'

@Injectable({
    providedIn: 'root'
})
export class LogsService {
    constructor(private readonly http: HttpClient) { }

    getSystemLogs(): Observable<LogView> {
        const requestUrl = `${environment.logApiUrl}/GetFullLog`;
        return this.http.get<LogView>(requestUrl);
    }
}