import { NgModule } from '@angular/core';
import { LoginRoutingModule } from 'src/app/login/login-routing.module';
import { LoginComponent } from 'src/app/login/login/login.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
    imports: [
        LoginRoutingModule,
        SharedModule
    ],
    declarations: [LoginComponent]
})
export class LoginModule { }
