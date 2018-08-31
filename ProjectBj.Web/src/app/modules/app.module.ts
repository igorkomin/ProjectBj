import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { GridModule } from '@progress/kendo-angular-grid';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './shared.module';
import { AppComponent } from './../components/app.component';
import { LoginComponent } from '../components/login.component';
import { GameComponent } from '../components/game.component';
import { AppRoutingModule } from './app-routing.module';



@NgModule({
    declarations: [
      AppComponent,
  ],
  imports: [
      SharedModule,
      AppRoutingModule,
      RouterModule,
      NgbModule,
      HttpClientModule,
      GridModule,
      BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
