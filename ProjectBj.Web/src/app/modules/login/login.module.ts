import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/modules/shared.module';
import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from 'src/app/components/login/login.component';

@NgModule({
    imports: [
        LoginRoutingModule,
        SharedModule
    ],
    declarations: [LoginComponent]
})
export class LoginModule { }
