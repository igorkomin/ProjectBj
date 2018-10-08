import { Component, OnInit } from '@angular/core';
import { DataStateChangeEvent, GridDataResult } from '@progress/kendo-angular-grid';
import { State, process } from '@progress/kendo-data-query';
import { HistoryService } from 'src/app/history/history.service';
import { HistoryView } from 'src/app/shared/models/history-view.model';

@Component({
    selector: 'app-history',
    templateUrl: 'history.component.html'
})
export class HistoryComponent implements OnInit {
    history: HistoryView;

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
