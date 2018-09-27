import { Component, OnInit } from '@angular/core';
import { DataStateChangeEvent, GridDataResult } from '@progress/kendo-angular-grid';
import { State, process } from '@progress/kendo-data-query';
import { HistoryService } from '../history.service';

@Component({
    selector: 'app-history',
    templateUrl: 'history.component.html',
})
export class HistoryComponent implements OnInit {
    history: any;
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
        private historyService: HistoryService
    ) { }

    ngOnInit() {
        this.getHistory();
    }

    getHistory() {
        this.historyService.getFullHistory().subscribe(
            response => {
                this.history = response;
                this.gridView = process(this.history, this.state);
            },
            exception => {
                console.error(exception);
            }
        );
    }

    dataStateChange(state: DataStateChangeEvent): void {
        this.state = state;
        this.gridView = process(this.history, this.state);
    }
}
