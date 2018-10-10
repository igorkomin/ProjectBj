import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { GridModule } from '@progress/kendo-angular-grid';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { AppComponent } from 'src/app/app.component';
import { HttpErrorInterceptor } from 'src/app/shared/http-error.interceptor';
import { SharedModule } from 'src/app/shared/shared.module';



@NgModule({
    declarations: [
      AppComponent,
    ],
    imports: [
        SharedModule,
        AppRoutingModule,
        RouterModule,
        HttpClientModule,
        GridModule,
        BrowserAnimationsModule
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
