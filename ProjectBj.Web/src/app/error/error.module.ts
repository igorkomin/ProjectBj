import { NgModule } from '@angular/core';
import { ErrorRoutingModule } from 'src/app/error/error-routing.module';
import { ErrorComponent } from 'src/app/error/error/error.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
    imports: [
        SharedModule,
        ErrorRoutingModule
    ],
    declarations: [ErrorComponent]
})
export class ErrorModule { }