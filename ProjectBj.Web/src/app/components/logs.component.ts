import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { GridDataResult, PageChangeEvent, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { ActivatedRoute, Router } from '@angular/router';
import { State, SortDescriptor, orderBy, process } from '@progress/kendo-data-query';

import { ApiService } from '../services/api.service';
import { SystemLog } from '../models/system-log.model';

@Component({
    selector: 'app-logs',
    templateUrl: './../views/logs.view.html',
    styleUrls: ['./../styles/logs.style.css']
})
export class LogsComponent implements OnInit {
    logs: any;
    data: Object[];

    public allowUnsort = true;
    public sort: SortDescriptor[] = [{
        field: 'level',
        dir: 'asc'
    }];

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