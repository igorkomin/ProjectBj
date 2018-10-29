import { Component, OnInit } from '@angular/core';
import { RequestLoginAuthorizationView } from 'src/app/shared/models/request-login-authorization-view.model';
import { LoginService } from 'src/app/shared/services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: 'login.component.html',
  styleUrls: [
      'login.component.css'
  ]
})
export class LoginComponent implements OnInit {
    constructor(
        private readonly loginService: LoginService,
        private readonly router: Router
    ) { }

    ngOnInit() {
    }

    login(playerName: string): void {
        const request = new RequestLoginAuthorizationView();
        request.playerName = playerName;
        this.loginService.login(request).subscribe(
            response => {
                this.router.navigate(['/game', { 'id': response.playerId }]);
            }
        );
    }
}