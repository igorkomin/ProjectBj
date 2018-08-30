import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../services/api.service';
import { State, process } from '@progress/kendo-data-query';
import { PageChangeEvent, GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';

@Component({
    selector: 'app-logs',
    templateUrl: './../views/logs.view.html',
    styleUrls: ['./../styles/logs.style.css']
})
export class LogsComponent implements OnInit {

    logs: any[];

    state: State = {
        skip: 0,
        take: 10
    };
    gridView: GridDataResult;

    constructor(
        private router: Router,
        private apiService: ApiService
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