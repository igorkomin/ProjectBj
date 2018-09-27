import { Component, OnInit } from '@angular/core';
import { DataStateChangeEvent, GridDataResult } from '@progress/kendo-angular-grid';
import { State, process } from '@progress/kendo-data-query';
import { LogsService } from 'src/app/logs/logs.service';

@Component({
    selector: 'app-logs',
    templateUrl: 'logs.component.html',
    styleUrls: ['logs.component.css']
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
        private logsService: LogsService
    ) { }

    ngOnInit() {
        this.getLogs();
    }

    getLogs() {
        this.logsService.getSystemLogs().subscribe(
            response => {
                this.logs = response;
                this.gridView = process(this.logs, this.state);
            },
            exception => {
                console.error(exception);
            }
        );
    }

    dataStateChange(state: DataStateChangeEvent): void {
        this.state = state;
        this.gridView = process(this.logs, this.state);
    }
}