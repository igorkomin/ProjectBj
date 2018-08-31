import { NgModule } from '@angular/core';
import { SharedModule } from './../shared.module';
import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from '../../components/login.component';

@NgModule({
  imports: [
      LoginRoutingModule,
      SharedModule
   ],
    declarations: [LoginComponent]
})
export class LoginModule { }
