import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

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
      AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
