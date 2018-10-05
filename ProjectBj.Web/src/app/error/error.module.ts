import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { ErrorRoutingModule } from 'src/app/error/error-routing.module';
import { ErrorComponent } from 'src/app/error/error/error.component';

@NgModule({
    imports: [
        SharedModule,
        ErrorRoutingModule
    ],
    declarations: [ErrorComponent]
})
export class ErrorModule { }