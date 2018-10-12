import { Component, OnInit } from '@angular/core';
import { DataStateChangeEvent, GridDataResult } from '@progress/kendo-angular-grid';
import { process, State } from '@progress/kendo-data-query';
import { GetFullHistoryHistoryView } from 'src/app/shared/models/get-full-history-history-view.model';
import { HistoryService } from 'src/app/shared/services/history.service';

@Component({
    selector: 'app-history',
    templateUrl: 'history.component.html'
})
export class HistoryComponent implements OnInit {
    history: GetFullHistoryHistoryView;

    state: State = {
        skip: 0,
        take: 15,
    };
    gridView: GridDataResult;

    constructor(
        private readonly historyService: HistoryService
    ) { }

    ngOnInit() {
        this.getHistory();
    }

    getHistory() {
        this.historyService.getFullHistory().subscribe(
            response => {
                this.history = response;
                this.gridView = process(this.history.entries, this.state);
            }
        );
    }

    dataStateChange(state: DataStateChangeEvent): void {
        this.state = state;
        this.gridView = process(this.history.entries, this.state);
    }
}
