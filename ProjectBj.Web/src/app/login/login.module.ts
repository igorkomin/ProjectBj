import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { LoginRoutingModule } from 'src/app/login/login-routing.module';
import { LoginComponent } from 'src/app/login/login/login.component';

@NgModule({
    imports: [
        LoginRoutingModule,
        SharedModule
    ],
    declarations: [LoginComponent]
})
export class LoginModule { }
