import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Login } from 'src/app/models/login.model';

@Component({
  selector: "app-login",
  templateUrl: "login.view.html",
    styleUrls: [
        "login.style.css",
        '../app/common/bootstrap.css',
        '../app/common/slider.css'
    ]
})
export class LoginComponent implements OnInit {
    model = new Login("", 0);

    sliderValue = 0;
    loginLinkEnabled = false;
    constructor(private router: Router) { }

    ngOnInit() {
    }

    updateSliderValue(value: number): void {
        this.sliderValue = value;
    }
}