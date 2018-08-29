import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../services/api.service';

@Component({
    selector: 'app-logs',
    templateUrl: './../views/logs.view.html',
    styleUrls: ['./../styles/logs.style.css']
})
export class LogsComponent implements OnInit {

    logs: any;

    constructor(
        private router: Router,
        private apiService: ApiService
    ) { }

    ngOnInit() {
        
    }
}