import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { finalize, tap } from 'rxjs/operators';

@Injectable()
export class ApiInterceptor implements HttpInterceptor {
    intercept(
        request: HttpRequest<any>,
        next: HttpHandler
    ): Observable<HttpEvent<any>> {
        const started = Date.now();
        let ok: string;
        let res: any;

        const requestClone = request.clone({ setHeaders: { 'Content-Type' : 'application/json' } });
        return next.handle(requestClone).pipe(
            tap(
                event => {
                    ok = event instanceof HttpResponse ? 'succeeded' : ''
                    if (ok) {
                        res = event;
                    }
                },
                error => ok = 'failed'
            ),
            finalize(() => {
                const elapsed = Date.now() - started;
                const msg = `${request.method} "${request.urlWithParams}" ${ok} in ${elapsed} ms.`;
                console.log(msg);
            })
        );
    }
}