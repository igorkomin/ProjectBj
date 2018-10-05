import { Component, OnInit } from '@angular/core';
import { DataStateChangeEvent, GridDataResult } from '@progress/kendo-angular-grid';
import { State, process } from '@progress/kendo-data-query';
import { LogsService } from 'src/app/logs/logs.service';

@Component({
    selector: 'app-logs',
    templateUrl: 'logs.component.html',
})
export class LogsComponent implements OnInit {
    logs: any;
    data: Object[];

    public state: State = {
        skip: 0,
        take: 15,
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
            }
        );
    }

    dataStateChange(state: DataStateChangeEvent): void {
        this.state = state;
        this.gridView = process(this.logs, this.state);
    }
}