import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GetGameHistoryHistoryView } from 'src/app/shared/models/get-game-history-history-view.model';
import { RequestGameView } from 'src/app/shared/models/request-game-view.model';
import { RequestNewGameView } from 'src/app/shared/models/request-new-game-view.model';
import { ResponseDoubleGameView } from 'src/app/shared/models/response-double-game-view.model';
import { ResponseHitGameView } from 'src/app/shared/models/response-hit-game-view.model';
import { ResponseLoadGameView } from 'src/app/shared/models/response-load-game-view.model';
import { ResponseStandGameView } from 'src/app/shared/models/response-stand-game-view.model';
import { ResponseStartGameView } from 'src/app/shared/models/response-start-game-view.model';
import { ResponseSurrenderGameView } from 'src/app/shared/models/response-surrender-game-view.model';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class GameService {
    constructor(private readonly http: HttpClient) { }

    newGame(request: RequestNewGameView): Observable<ResponseStartGameView> {
        const requestUrl = `${environment.gameApiUrl}/Start`;
        return this.http.post<ResponseStartGameView>(requestUrl, request);
    }

    loadGame(request: RequestNewGameView): Observable<ResponseLoadGameView> {
        const requestUrl = `${environment.gameApiUrl}/Load`;
        return this.http.post<ResponseLoadGameView>(requestUrl, request);
    }

    hit(request: RequestGameView): Observable<ResponseHitGameView> {
        const requestUrl = `${environment.gameApiUrl}/Hit`;
        return this.http.post<ResponseHitGameView>(requestUrl, request);
    }

    stand(request: RequestGameView): Observable<ResponseStandGameView> {
        const requestUrl = `${environment.gameApiUrl}/Stand`;
        return this.http.post<ResponseStandGameView>(requestUrl, request);
    }

    double(request: RequestGameView): Observable<ResponseDoubleGameView> {
        const requestUrl = `${environment.gameApiUrl}/Double`;
        return this.http.post<ResponseDoubleGameView>(requestUrl, request);
    }

    surrender(request: RequestGameView): Observable<ResponseSurrenderGameView> {
        const requestUrl = `${environment.gameApiUrl}/Surrender`;
        return this.http.post<ResponseSurrenderGameView>(requestUrl, request);
    }

    getHistory(sessionId: number): Observable<GetGameHistoryHistoryView> {
        const requestUrl = `${environment.gameApiUrl}/../History/GetGameHistory`;
        return this.http.post<GetGameHistoryHistoryView>(requestUrl, sessionId);
    }
}