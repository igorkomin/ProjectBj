import { NgModule } from '@angular/core';
import { GridModule } from '@progress/kendo-angular-grid';
import { LogsRoutingModule } from 'src/app/logs/logs-routing.module';
import { LogsComponent } from 'src/app/logs/logs/logs.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
    imports: [
        SharedModule,
        LogsRoutingModule,
        GridModule
    ],
    declarations: [LogsComponent]
})
export class LogsModule { }
