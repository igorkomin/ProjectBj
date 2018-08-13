import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  sliderValue = 0;
  constructor() { }

  ngOnInit() {
  }

  updateSliderValue(value: number): void {
    this.sliderValue = value;
  }

  login(playerName: string, botsNumber: number): void {
    
  }
}
