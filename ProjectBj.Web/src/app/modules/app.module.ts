import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './../components/app.component';
import { LoginComponent } from '../components/login.component';
import { GameComponent } from '../components/game.component';
import { AppRoutingModule } from './app-routing.module';

@NgModule({
    declarations: [
      AppComponent,
      LoginComponent,
      GameComponent
  ],
  imports: [
      BrowserModule,
      AppRoutingModule,
      RouterModule,
      NgbModule,
      HttpClientModule,
      FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
