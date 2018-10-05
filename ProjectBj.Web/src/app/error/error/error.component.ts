import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-error',
    templateUrl: 'error.component.html',
})
export class ErrorComponent implements OnInit {
    errorCode: string;

    constructor(
        private route: ActivatedRoute
    ) { }

    ngOnInit() {
        this.route
            .params
            .subscribe(params => {
                this.errorCode = params['code'];
            });
    }
}