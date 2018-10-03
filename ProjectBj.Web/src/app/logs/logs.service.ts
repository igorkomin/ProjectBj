import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SystemLog } from 'src/app/shared/models/system-log.model';
import { environment } from 'src/environments/environment'

@Injectable({
    providedIn: 'root'
})
export class LogsService {
    constructor(private http: HttpClient) { }

    getSystemLogs(): Observable<SystemLog> {
        let requestUrl = `${environment.logApiUrl}/GetFullLog`;
        return this.http.get<SystemLog>(requestUrl);
    }
}