import { Component, OnInit } from '@angular/core';
import { DataStateChangeEvent, GridDataResult } from '@progress/kendo-angular-grid';
import { process, State } from '@progress/kendo-data-query';
import { GetFullLogView } from 'src/app/logs/shared/models/get-full-log-view.model';
import { LogsService } from 'src/app/logs/shared/services/logs.service';

@Component({
    selector: 'app-logs',
    templateUrl: 'logs.component.html'
})
export class LogsComponent implements OnInit {
    logs: GetFullLogView;

    state: State = {
        skip: 0,
        take: 15,
    };
    gridView: GridDataResult;

    constructor(
        private readonly logsService: LogsService
    ) { }

    ngOnInit() {
        this.getLogs();
    }

    getLogs() {
        this.logsService.getSystemLogs().subscribe(
            response => {
                this.logs = response;
                this.gridView = process(this.logs.entries, this.state);
            }
        );
    }

    dataStateChange(state: DataStateChangeEvent): void {
        this.state = state;
        this.gridView = process(this.logs.entries, this.state);
    }
}