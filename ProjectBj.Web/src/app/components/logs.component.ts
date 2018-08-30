import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { GridDataResult, PageChangeEvent } from '@progress/kendo-angular-grid';
import { ActivatedRoute, Router } from '@angular/router';
import { State } from '@progress/kendo-data-query';

import { ApiService } from '../services/api.service';
import { SystemLog } from '../models/system-log.model';
import { customers } from './customers';

@Component({
    selector: 'app-logs',
    templateUrl: './../views/logs.view.html',
    styleUrls: ['./../styles/logs.style.css']
})
export class LogsComponent implements OnInit {
    logs: any;
    gridView: GridDataResult;
    pageSize = 10;
    skip = 0;
    data: Object[];

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
                this.loadItems();
            },
            exception => {
                console.error(exception);
            }
        );
    }

    pageChange(event: PageChangeEvent): void {
        this.skip = event.skip;
        this.loadItems();
    }

    loadItems(): void {
        this.gridView = {
            data: this.logs.slice(this.skip, this.skip + this.pageSize),
            total: this.logs.length
        };
    }
}