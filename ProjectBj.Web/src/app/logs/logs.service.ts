import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SystemLog } from 'src/app/shared/models/system-log.model';

const apiUrl = '../api/log';
const requestOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json'
    })
};

@Injectable({
    providedIn: 'root'
})

export class LogsService {
    constructor(private http: HttpClient) { }

    getSystemLogs(): Observable<SystemLog> {
        let requestUrl = `${apiUrl}/GetFull`;
        return this.http.get<SystemLog>(requestUrl, requestOptions);
    }
}