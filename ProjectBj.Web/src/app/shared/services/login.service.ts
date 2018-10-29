import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RequestLoginAuthorizationView } from 'src/app/shared/models/request-login-authorization-view.model';
import { ResponseLoginAuthorizationView } from 'src/app/shared/models/response-login-authorization-view.model';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class LoginService {
    constructor(private readonly http: HttpClient) { }

    login(request: RequestLoginAuthorizationView): Observable<ResponseLoginAuthorizationView> {
        const requestUrl = `${environment.authorizationApiUrl}Login`;
        return this.http.post<ResponseLoginAuthorizationView>(requestUrl, request);
    }
}