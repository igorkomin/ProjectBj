import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RequestGameView } from 'src/app/shared/models/request-game-view.model';
import { HistoryView } from 'src/app/shared/models/history-view.model';
import { RequestNewGameView } from 'src/app/shared/models/request-new-game-view.model';
import { ResponseGameView } from 'src/app/shared/models/response-game-view.model';
import { environment } from 'src/environments/environment';


@Injectable({
    providedIn: 'root'
})
export class GameService {
    constructor(private readonly http: HttpClient) { }

    newGame(request: RequestNewGameView): Observable<ResponseGameView> {
        const requestUrl = `${environment.gameApiUrl}/Start`;
        return this.http.post<ResponseGameView>(requestUrl, request);
    }

    loadGame(request: RequestNewGameView): Observable<ResponseGameView> {
        const requestUrl = `${environment.gameApiUrl}/Load`;
        return this.http.post<ResponseGameView>(requestUrl, request);
    }

    hit(request: RequestGameView): Observable<ResponseGameView> {
        const requestUrl = `${environment.gameApiUrl}/Hit`;
        return this.http.post<ResponseGameView>(requestUrl, request);
    }

    stand(request: RequestGameView): Observable<ResponseGameView> {
        const requestUrl = `${environment.gameApiUrl}/Stand`;
        return this.http.post<ResponseGameView>(requestUrl, request);
    }

    double(request: RequestGameView): Observable<ResponseGameView> {
        const requestUrl = `${environment.gameApiUrl}/Double`;
        return this.http.post<ResponseGameView>(requestUrl, request);
    }

    surrender(request: RequestGameView): Observable<ResponseGameView> {
        const requestUrl = `${environment.gameApiUrl}/Surrender`;
        return this.http.post<ResponseGameView>(requestUrl, request);
    }

    getHistory(sessionId: number): Observable<HistoryView> {
        const requestUrl = `${environment.gameApiUrl}/../History/GetGameHistory`;
        return this.http.post<HistoryView>(requestUrl, sessionId);
    }
}