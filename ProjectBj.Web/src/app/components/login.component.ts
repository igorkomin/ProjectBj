import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Login } from './../models/login.model';

@Component({
  selector: "app-login",
  templateUrl: "./../views/login.view.html",
    styleUrls: [
        "./../styles/login.style.css",
        "./../styles/common/bootstrap.css",
        "./../styles/common/slider.css"
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