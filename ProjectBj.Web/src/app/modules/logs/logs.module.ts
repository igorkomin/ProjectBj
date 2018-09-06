import { NgModule } from '@angular/core';
import { GridModule } from '@progress/kendo-angular-grid';
import { LogsRoutingModule } from './logs-routing.module';
import { LogsComponent } from 'src/app/components/logs/logs.component';
import { SharedModule } from 'src/app/modules/shared/shared.module';

@NgModule({
    imports: [
        SharedModule,
        LogsRoutingModule,
        GridModule 
    ],
  declarations: [LogsComponent]
})
export class LogsModule { }
