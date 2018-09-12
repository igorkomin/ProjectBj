import { Component, OnInit } from '@angular/core';
import { DataStateChangeEvent, GridDataResult } from '@progress/kendo-angular-grid';
import { State, process } from '@progress/kendo-data-query';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/core/api.service';

@Component({
    selector: 'app-gamelogs',
    templateUrl: 'gamelogs.component.html',
    styleUrls: ['gamelogs.component.css']
})
export class GamelogsComponent implements OnInit {
    logs: any;
    data: Object[];

    public allowUnsort = true;

    public state: State = {
        skip: 0,
        take: 15,

        filter: {
            logic: 'and',
            filters: [{ field: 'message', operator: 'contains', value: '' }]
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
        this.apiService.getAllLogs().subscribe(
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
