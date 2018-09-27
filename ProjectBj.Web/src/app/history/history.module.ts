import { NgModule } from '@angular/core';
import { GridModule } from '@progress/kendo-angular-grid';
import { HistoryRoutingModule } from 'src/app/history/history-routing.module';
import { HistoryComponent } from 'src/app/history/history/history.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
    imports: [
        SharedModule,
        HistoryRoutingModule,
        GridModule
    ],
    declarations: [HistoryComponent]
})
export class HistoryModule { }
