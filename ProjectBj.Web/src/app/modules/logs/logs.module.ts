import { NgModule } from '@angular/core';
import { GridModule } from '@progress/kendo-angular-grid';
import { LogsRoutingModule } from './logs-routing.module';
import { LogsComponent } from '../../components/logs.component';
import { SharedModule } from './../shared.module';

@NgModule({
  imports: [
      SharedModule,
      LogsRoutingModule,
      GridModule 
  ],
  declarations: [LogsComponent]
})
export class LogsModule { }
