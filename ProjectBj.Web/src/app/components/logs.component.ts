import { Component, OnInit } from '@angular/core';
import { DataStateChangeEvent, GridDataResult } from '@progress/kendo-angular-grid';
import { State, process } from '@progress/kendo-data-query';
import { Router } from '@angular/router';

import { ApiService } from '../services/api.service';

@Component({
    selector: 'app-logs',
    templateUrl: './../views/logs.view.html',
    styleUrls: ['./../styles/logs.style.css']
})
export class LogsComponent implements OnInit {
    logs: any;
    data: Object[];

    public allowUnsort = true;

    public state: State = {
        skip: 0,
        take: 10,

        filter: {
            logic: 'and',
            filters: [{ field: 'level', operator: 'contains', value: 'Info' }]
        },
    };
    gridView: GridDataResult;

    constructor(
        private router: Router,
        private apiService: ApiService,
    ) { }

    ngOnInit() {
        this.getLogs();
    }

    getLogs() {
        this.apiService.getSystemLogs().subscribe(
            response => {
                this.logs = response;
                this.gridView = process(this.logs, this.state);
            },
            exception => {
                console.error(exception);
            }
        );
    }

    public dataStateChange(state: DataStateChangeEvent): void {
        this.state = state;
        this.gridView = process(this.logs, this.state);
    }
}